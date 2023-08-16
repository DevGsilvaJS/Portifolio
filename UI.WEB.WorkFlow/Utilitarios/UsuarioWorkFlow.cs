using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WEB.Model.Utilitarios;
using UI.WEB.WorkFlow.Outros;

namespace WorkFlow.Utilitarios
{
    public class UsuarioWorkFlow : BaseWeb
    {
        DBComando db = new DBComando();

        public bool ValidaUsuario(string email, string senha)
        {
            EntityUsuario _ObjUsuario = new EntityUsuario();
            //Classe que retorna uma conexao com banco de dados

            EntityUsuario _Usuario = new EntityUsuario();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("   SELECT EML.EMLEMAIL, USU.USUSENHA                                                   ");
            sb.AppendLine("       FROM TB_PES_PESSOA PES                                                                       ");
            sb.AppendLine("        INNER JOIN TB_USU_USUARIO USU ON USU.PESID = PES.PESID      ");
            sb.AppendLine("        INNER JOIN TB_EML_EMAIL EML ON EML.PESID = PES.PESID           ");
            sb.AppendLine("        WHERE EML.EMLEMAIL ='" + email + "'AND USU.USUSENHA = " + senha +"");

            SqlDataReader _DataReader = ListarDadosEntity(sb.ToString());

            if (_DataReader.HasRows)
            {
                //Verifica se tem dados na linha
                while (_DataReader.Read())
                {
                    _Usuario.TbEmail.EMLEMAIL = _DataReader["EMLEMAIL"].ToString();
                    _Usuario.USUSENHA = _DataReader["USUSENHA"].ToString();
                }
            }

            db.FechaConexao(db.MinhaConexao());

            return true;
        }
        public EntityUsuario RetornaObjInclusao()
        {
            EntityUsuario _obj = new EntityUsuario();

            return _obj;
        }
        public string InsertUsuario(EntityUsuario _ObjUsuario)
        {

            string retorno = "OK";

            if (_ObjUsuario.TbPessoa.PESID > 0)
            {
                AddListaSalvar(RetornaQueryUpdate(_ObjUsuario, "TB_PES_PESSOA"));
                _ObjUsuario.PESID = _ObjUsuario.TbPessoa.PESID;
                AddListaSalvar(RetornaQueryUpdate(_ObjUsuario, "TB_USU_USUARIO"));


                string telefone = RetornaObjeto("TB_TEL_TELEFONE", "PESID", _ObjUsuario.TbTelefone.PESID);

                if (!string.IsNullOrEmpty(telefone))
                {
                    _ObjUsuario.TbTelefone.PESID = _ObjUsuario.PESID;
                    AddListaSalvar(RetornaQueryUpdate(_ObjUsuario.TbTelefone, "TB_TEL_TELEFONE"));
                }

                else
                {
                    _ObjUsuario.TbTelefone.TELID = RetornaSequencial("SEQ_TEL");
                    _ObjUsuario.TbTelefone.PESID = _ObjUsuario.PESID;
                    AddListaSalvar(RetornaQueryInclusao(_ObjUsuario.TbTelefone, "TB_TEL_TELEFONE"));
                }

                string sEmail = RetornaObjeto("TB_EML_EMAIL", "PESID", _ObjUsuario.PESID);

                if (!string.IsNullOrEmpty(sEmail))
                {
                    _ObjUsuario.TbEmail.PESID = _ObjUsuario.PESID;
                    AddListaSalvar(RetornaQueryUpdate(_ObjUsuario.TbEmail, "TB_EML_EMAIL"));
                }

                else
                {
                    _ObjUsuario.TbEmail.EMLID = RetornaSequencial("SEQ_EML");
                    _ObjUsuario.TbEmail.PESID = _ObjUsuario.PESID;
                    AddListaSalvar(RetornaQueryInclusao(_ObjUsuario.TbEmail, "TB_EML_EMAIL"));
                }
            }
            else
            {
                _ObjUsuario.PESID = RetornaSequencial("SEQ_PES");
                AddListaSalvar(RetornaQueryInclusao(_ObjUsuario, "TB_PES_PESSOA"));

                _ObjUsuario.USUID = RetornaSequencial("SEQ_USU");
                _ObjUsuario.PESID = _ObjUsuario.PESID;
                AddListaSalvar(RetornaQueryInclusao(_ObjUsuario, "TB_USU_USUARIO"));


                if (_ObjUsuario.TbTelefone.TELNUMERO != null || _ObjUsuario.TbTelefone.TELCELULAR != null)
                {
                    _ObjUsuario.TbTelefone.TELID = RetornaSequencial("SEQ_TEL");
                    _ObjUsuario.TbTelefone.PESID = _ObjUsuario.PESID;
                    AddListaSalvar(RetornaQueryInclusao(_ObjUsuario.TbTelefone, "TB_TEL_TELEFONE"));
                }

                if (_ObjUsuario.TbEmail.EMLEMAIL != null)
                {
                    _ObjUsuario.TbEmail.EMLID = RetornaSequencial("SEQ_EML");
                    _ObjUsuario.TbEmail.PESID = _ObjUsuario.PESID;
                    AddListaSalvar(RetornaQueryInclusao(_ObjUsuario.TbEmail, "TB_EML_EMAIL"));
                }

                AddListaSalvar("UPDATE TB_PRV_PARAMETROSVALOR SET PRVVALOR = PRVVALOR + 1 WHERE PRVCAMPO = 'USUARIO'");
            }

            ExecuteTransacao();

            return retorno;

        }
        public List<EntityUsuario> ListaUsuarios()
        {
            List<EntityUsuario> lsUsuarios = new List<EntityUsuario>();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(" SELECT    ");
                sb.AppendLine("       PES.PESID   ");
                sb.AppendLine("     , PES.PESNOME      ");
                sb.AppendLine("     , USU.USUSTATUS    ");
                sb.AppendLine("     , EML.EMLEMAIL   ");
                sb.AppendLine("  FROM TB_PES_PESSOA PES   ");
                sb.AppendLine("    INNER JOIN TB_USU_USUARIO USU ON USU.PESID = PES.PESID   ");
                sb.AppendLine("    LEFT JOIN TB_EML_EMAIL EML ON EML.PESID = PES.PESID  ");


                SqlDataReader dr = ListarDadosEntity(sb.ToString());

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        EntityUsuario _Usuario = new EntityUsuario();

                        _Usuario.TbPessoa.PESID = int.Parse(dr["PESID"].ToString());
                        _Usuario.TbPessoa.PESNOME = dr["PESNOME"].ToString();
                        _Usuario.USUSTATUS = dr["USUSTATUS"].ToString();
                        _Usuario.TbEmail.EMLEMAIL = dr["EMLEMAIL"].ToString();
                        lsUsuarios.Add(_Usuario);
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            return lsUsuarios;

        }
        public EntityUsuario GetUsuarioID(int pesid)
        {

            EntityUsuario _ObjUsuario = new EntityUsuario();
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("    SELECT                                                                                               ");
            sb.AppendLine("       PES.PESID                                                                                                 ");
            sb.AppendLine("     , (SELECT PRVVALOR from TB_PRV_PARAMETROSVALOR WHERE PRVCAMPO = 'USUARIO') USUSEQUENCIAL");
            sb.AppendLine("     , PES.PESNOME                                                                                         ");
            sb.AppendLine("     , PES.PESSOBRENOME                                                                             ");
            sb.AppendLine("     , USU.USUSTATUS                                                                                       ");
            sb.AppendLine("     , USU.USUSENHA                                                                                      ");
            sb.AppendLine("     , EML.EMLEMAIL                                                                                      ");
            sb.AppendLine("  FROM TB_PES_PESSOA PES                                                                     ");
            sb.AppendLine("      INNER JOIN TB_USU_USUARIO USU ON USU.PESID = PES.PESID   ");
            sb.AppendLine("      LEFT JOIN TB_EML_EMAIL EML ON EML.PESID = PES.PESID        ");
            sb.AppendLine("      LEFT JOIN TB_TEL_TELEFONE TEL ON TEL.PESID = PES.PESID       ");
            sb.AppendLine("   WHERE PES.PESID = " + pesid);


            SqlDataReader dr = ListarDadosEntity(sb.ToString());

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    _ObjUsuario.TbPessoa.PESID = int.Parse(dr["PESID"].ToString());
                    _ObjUsuario.TbPessoa.PESNOME = dr["PESNOME"].ToString();
                    _ObjUsuario.TbPessoa.PESSOBRENOME = dr["PESSOBRENOME"].ToString();
                    _ObjUsuario.USUSTATUS = dr["USUSTATUS"].ToString();
                    _ObjUsuario.USUSENHA = dr["USUSENHA"].ToString();
                    _ObjUsuario.TbEmail.EMLEMAIL = dr["EMLEMAIL"].ToString();
                }
            }

            return _ObjUsuario;
        }
        public string DeletarUsuario(int pesid)
        {
            string sRetornoEmail = RetornaObjeto("TB_EML_EMAIL", "PESID", pesid);

            if (!string.IsNullOrEmpty(sRetornoEmail))
            {
                string email = RetornaQueryDelete("TB_EML_EMAIL", "PESID", pesid);
                AddListaSalvar(email);
            }

            string sRetornaTelefone = RetornaObjeto("TB_TEL_TELEFONE", "PESID", pesid);

            if (!string.IsNullOrEmpty(sRetornaTelefone))
            {
                string telefone = RetornaQueryDelete("TB_TEL_TELEFONE", "PESID", pesid);
                AddListaSalvar(telefone);
            }

            string vendedor = RetornaQueryDelete("TB_USU_USUARIO", "PESID", pesid);
            AddListaSalvar(vendedor);

            string pessoa = RetornaQueryDelete("TB_PES_PESSOA", "PESID", pesid);
            AddListaSalvar(pessoa);

            ExecuteTransacao();

            return "";

        }
        public string RetornaSequencial()
        {
            string retorno = "";
            string query = "SELECT PRV.PRVVALOR FROM TB_PRV_PARAMETROSVALOR PRV WHERE PRV.PRVCAMPO = 'USUARIO'";

            SqlCommand Comando = new SqlCommand(query, db.MinhaConexao());
            Comando.CommandType = CommandType.Text;
            SqlDataReader dr = Comando.ExecuteReader();

            while (dr.Read())
            {
                retorno = dr["PRVVALOR"].ToString();
            }

            db.FechaConexao(db.MinhaConexao());
            return retorno;
        }
    }
}
