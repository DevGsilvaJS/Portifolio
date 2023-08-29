using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WEB.Model.Utilitarios;
using UI.WEB.Query.Utilitarios;
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
            sb.AppendLine("        WHERE EML.EMLEMAIL ='" + email + "'AND USU.USUSENHA = " + senha + "");

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
        public string GravarUsuario(EntityUsuario _Usuario)
        {

            string retorno = "OK";

            if (_Usuario.TbPessoa.PESID > 0)
            {
                AtualizarUsuario(_Usuario);
            }
            else
            {
                AddListaSalvar(_Usuario.TbPessoa);

                int PESID = _Usuario.TbPessoa.PESID;

                _Usuario.PESID = PESID;
                _Usuario.USUSTATUS = "1";

                AddListaSalvar(_Usuario);


                if (_Usuario.TbTelefone.TELNUMERO != null || _Usuario.TbTelefone.TELCELULAR != null)
                {
                    _Usuario.TbTelefone.PESID = PESID;
                    AddListaSalvar(_Usuario.TbTelefone);
                }

                if (_Usuario.TbEmail.EMLEMAIL != null)
                {
                    _Usuario.TbEmail.PESID = PESID;
                    AddListaSalvar(_Usuario.TbEmail);
                }


                if (_Usuario.TbEndereco.EDNLOGRADOURO != null)
                {
                    _Usuario.TbEndereco.PESID = PESID;
                    AddListaSalvar(_Usuario.TbEndereco);
                }

                AddListaParametros("UPDATE TB_PRV_PARAMETROVALOR SET PRVVALOR = PRVVALOR + 1 WHERE PRVCAMPO = 'USUARIO'");
            }

            ExecuteTransacao();

            return retorno;

        }
        public string AtualizarUsuario(EntityUsuario _Usuario)
        {
            string sRetorno = "NOTOK";

            AddListaAtualizar(_Usuario.TbPessoa);
            AddListaAtualizar(_Usuario);

            string sEmail = RetornaObjeto("TB_EML_EMAIL", "PESID", _Usuario.TbPessoa.PESID);

            if (!string.IsNullOrEmpty(sEmail))
            {
                AddListaAtualizar(_Usuario.TbEmail);
            }
            else
            {
                AddListaSalvar(_Usuario.TbEmail);
            }


            string sTelefone = RetornaObjeto("TB_TEL_TELEFONE", "PESID", _Usuario.TbPessoa.PESID);

            if (!string.IsNullOrEmpty(sTelefone))
            {
                AddListaAtualizar(_Usuario.TbTelefone);
            }

            string sEndereco = RetornaObjeto("TB_EDN_ENDERECO", "PESID", _Usuario.TbPessoa.PESID);

            if (!string.IsNullOrEmpty(sEndereco))
            {
                AddListaAtualizar(_Usuario.TbEndereco);
            }
            else
            {
                AddListaSalvar(_Usuario.TbEndereco);
            }



            return sRetorno;
        }
        public List<EntityUsuario> ListaDados()
        {
            List<EntityUsuario> lsUsuarios = new List<EntityUsuario>();
            try
            {

                UsuarioQuery Query = new UsuarioQuery();

                SqlDataReader dr = ListarDadosEntity(Query.ListaDadosQuery());


                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        EntityUsuario _Usuario = new EntityUsuario();

                        _Usuario.PESID = int.Parse(dr["PESID"].ToString());
                        _Usuario.TbPessoa.PESNOME = dr["PESNOME"].ToString();
                        _Usuario.TbEmail.EMLEMAIL = dr["EMLEMAIL"].ToString();
                        _Usuario.USUSTATUS = dr["USUSTATUS"].ToString();
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
                AddListaDeletar(email);
            }

            string sRetornaTelefone = RetornaObjeto("TB_TEL_TELEFONE", "PESID", pesid);

            if (!string.IsNullOrEmpty(sRetornaTelefone))
            {
                string telefone = RetornaQueryDelete("TB_TEL_TELEFONE", "PESID", pesid);
                AddListaDeletar(telefone);
            }

            string vendedor = RetornaQueryDelete("TB_USU_USUARIO", "PESID", pesid);
            AddListaDeletar(vendedor);

            string pessoa = RetornaQueryDelete("TB_PES_PESSOA", "PESID", pesid);
            AddListaDeletar(pessoa);

            ExecuteTransacao();

            return "";

        }
        public string RetornaSequencial()
        {
            string retorno = "";
            string query = "SELECT PRV.PRVVALOR FROM TB_PRV_PARAMETROVALOR PRV WHERE PRV.PRVCAMPO = 'USUARIO'";

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
