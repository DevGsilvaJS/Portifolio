using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WEB.WorkFlow.Outros;

namespace UI.WEB.WorkFlow.Vendas.TabelasAuxiliares
{
    public class VendedorWorkFlow : BaseWeb
    {
        DBComando db = new DBComando();
        public EntityVendedor RetornaObjInclusao()
        {
            EntityVendedor _ObjInclusao = new EntityVendedor();

            return _ObjInclusao;
        }
        public string GravarVendedor(EntityVendedor _ObjVendedor)
        {
            string retorno = "OK";

            if (_ObjVendedor.TbPessoa.PESID > 0)
            {
                AddListaAtualizar(_ObjVendedor.TbPessoa);
                _ObjVendedor.PESID = _ObjVendedor.TbPessoa.PESID;
                AddListaAtualizar(_ObjVendedor);

                string sEndereco = RetornaObjeto("TB_EDN_ENDERECO", "PESID", _ObjVendedor.PESID);

                if (!string.IsNullOrEmpty(sEndereco))
                {
                    _ObjVendedor.TbEndereco.PESID = _ObjVendedor.TbPessoa.PESID;
                    AddListaAtualizar(_ObjVendedor.TbEndereco);
                }

                else
                {
                    _ObjVendedor.TbEndereco.PESID = _ObjVendedor.PESID;
                    AddListaSalvar(_ObjVendedor.TbEndereco);
                }

                string sTelefone = RetornaObjeto("TB_TEL_TELEFONE", "PESID", _ObjVendedor.PESID);

                if (!string.IsNullOrEmpty(sTelefone))
                {
                    _ObjVendedor.TbTelefone.PESID = _ObjVendedor.TbPessoa.PESID;
                    AddListaAtualizar(_ObjVendedor.TbTelefone);
                }

                else
                {
                    _ObjVendedor.TbTelefone.PESID = _ObjVendedor.PESID;
                    AddListaSalvar(_ObjVendedor.TbTelefone);
                }

                string sEmail = RetornaObjeto("TB_EML_EMAIL", "PESID", _ObjVendedor.PESID);

                if (!string.IsNullOrEmpty(sEmail))
                {
                    _ObjVendedor.TbEmail.PESID = _ObjVendedor.TbPessoa.PESID;
                    AddListaAtualizar(_ObjVendedor.TbEmail);
                }

                else
                {
                    _ObjVendedor.TbEmail.PESID = _ObjVendedor.TbPessoa.PESID;
                    AddListaSalvar(_ObjVendedor.TbEmail);
                }
            }
            else
            {

                AddListaSalvar(_ObjVendedor.TbPessoa);
                _ObjVendedor.PESID = _ObjVendedor.TbPessoa.PESID;
                AddListaSalvar(_ObjVendedor);

                if (_ObjVendedor.TbEndereco.EDNCEP != null)
                {
                    _ObjVendedor.TbEndereco.PESID = _ObjVendedor.TbPessoa.PESID;
                    AddListaSalvar(_ObjVendedor.TbEndereco);
                }


                if (_ObjVendedor.TbTelefone.TELNUMERO != null || _ObjVendedor.TbTelefone.TELCELULAR != null)
                {
                    _ObjVendedor.TbTelefone.PESID = _ObjVendedor.TbPessoa.PESID;
                    AddListaSalvar(_ObjVendedor.TbTelefone);
                }

                if (_ObjVendedor.TbEmail.EMLEMAIL != null)
                {
                    _ObjVendedor.TbEmail.PESID = _ObjVendedor.TbPessoa.PESID;
                    AddListaSalvar(_ObjVendedor.TbEmail);
                }

                AddListaParametros("UPDATE TB_PRV_PARAMETROVALOR SET PRVVALOR = PRVVALOR + 1 WHERE PRVCAMPO = 'VENDEDOR'");
            }

            ExecuteTransacao();

            return retorno;

        }
        public List<EntityVendedor> ListaVendedores()
        {

            List<EntityVendedor> lsVendedor = new List<EntityVendedor>();
            EntityVendedor Vendedor = new EntityVendedor();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" SELECT     ");
            sb.AppendLine("       PES.PESID    ");
            sb.AppendLine("	    ,VND.VNDSEQUENCIAL    ");
            sb.AppendLine("	    ,PES.PESNOME    ");
            sb.AppendLine("	    ,EML.EMLEMAIL    ");
            sb.AppendLine("	    ,VND.VNDSTATUS     ");
            sb.AppendLine("	 FROM TB_PES_PESSOA PES    ");
            sb.AppendLine("		    INNER JOIN TB_VND_VENDEDOR VND ON VND.PESID = PES.PESID    ");
            sb.AppendLine("		    LEFT JOIN TB_EML_EMAIL EML ON EML.PESID = PES.PESID     ");



            SqlDataReader dr = ListarDadosEntity(sb.ToString());


            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    EntityVendedor _ObjVendedor = new EntityVendedor();
                    _ObjVendedor.PESID = int.Parse(dr["PESID"].ToString());
                    _ObjVendedor.VNDSEQUENCIAL = dr["VNDSEQUENCIAL"].ToString();
                    _ObjVendedor.TbPessoa.PESNOME = dr["PESNOME"].ToString();
                    _ObjVendedor.TbEmail.EMLEMAIL = dr["EMLEMAIL"].ToString();
                    _ObjVendedor.VNDSTATUS = dr["VNDSTATUS"].ToString();
                    lsVendedor.Add(_ObjVendedor);
                }
            }

            return lsVendedor;


        }
        public EntityVendedor GetVendedorID(int idVendedor)
        {

            EntityVendedor _ObjVendedor = new EntityVendedor();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT VND.VNDID");
            sb.AppendLine(" , PES.PESID                                                                                                    ");
            sb.AppendLine(" , VND.VNDSEQUENCIAL                                                                              ");
            sb.AppendLine(" , VND.VNDSTATUS                                                                                        ");
            sb.AppendLine(" , VNDNASCIMENTO                                                                                     ");
            sb.AppendLine(" , PES.PESNOME                                                                                            ");
            sb.AppendLine(" , PES.PESSOBRENOME                                                                                ");
            sb.AppendLine(" , PES.PESTIPO                                                                                               ");
            sb.AppendLine(" , PES.PESDOCESTADUAL                                                                             ");
            sb.AppendLine(" , PES.PESDOCFEDERAL                                                                             ");
            sb.AppendLine(" , PES.PESDTCADASTRO                                                                               ");
            sb.AppendLine(" , TEL.TELDDD                                                                                                ");
            sb.AppendLine(" , TEL.TELNUMERO                                                                                        ");
            sb.AppendLine(" , TEL.TELDDDC                                                                                              ");
            sb.AppendLine(" , TEL.TELCELULAR                                                                                         ");
            sb.AppendLine(" , EDN.EDNCEP                                                                                              ");
            sb.AppendLine(" , EDN.EDNUF                                                                                                ");
            sb.AppendLine(" , EDN.EDNCIDADE                                                                                       ");
            sb.AppendLine(" , EDN.EDNLOGRADOURO                                                                           ");
            sb.AppendLine(" , EDN.EDNBAIRRO                                                                                       ");
            sb.AppendLine(" , EDN.EDNNUMERO                                                                                    ");
            sb.AppendLine(" , EDN.EDNCOMPLEMENTO                                                                        ");
            sb.AppendLine("    FROM TB_VND_VENDEDOR VND                                                          ");
            sb.AppendLine("    INNER JOIN TB_PES_PESSOA PES ON PES.PESID = VND.PESID         ");
            sb.AppendLine("    INNER JOIN TB_TEL_TELEFONE TEL ON TEL.PESID = PES.PESID       ");
            sb.AppendLine("    INNER JOIN TB_EDN_ENDERECO EDN ON EDN.PESID = PES.PESID ");
            sb.AppendLine("    WHERE PES.PESID = " + idVendedor);




            SqlDataReader dr = ListarDadosEntity(sb.ToString());

            try
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        _ObjVendedor.PESID = int.Parse(dr["PESID"].ToString());
                        _ObjVendedor.VNDID = int.Parse(dr["VNDID"].ToString());
                        _ObjVendedor.VNDSEQUENCIAL = dr["VNDSEQUENCIAL"].ToString();
                        _ObjVendedor.VNDSTATUS = dr["VNDSTATUS"].ToString().Trim();
                        _ObjVendedor.VNDNASCIMENTO = dr["VNDNASCIMENTO"].ToString();
                        _ObjVendedor.TbPessoa.PESNOME = dr["PESNOME"].ToString();
                        _ObjVendedor.TbPessoa.PESSOBRENOME = dr["PESSOBRENOME"].ToString();
                        _ObjVendedor.TbPessoa.PESTIPO = dr["PESTIPO"].ToString();
                        _ObjVendedor.TbPessoa.PESDOCESTADUAL = dr["PESDOCESTADUAL"].ToString();
                        _ObjVendedor.TbPessoa.PESDOCFEDERAL = dr["PESDOCFEDERAL"].ToString();
                        _ObjVendedor.TbPessoa.PESDTCADASTRO = dr["PESDTCADASTRO"].ToString();
                        _ObjVendedor.TbTelefone.TELDDD = dr["TELDDD"].ToString();
                        _ObjVendedor.TbTelefone.TELNUMERO = dr["TELNUMERO"].ToString();
                        _ObjVendedor.TbTelefone.TELDDDC = dr["TELDDDC"].ToString();
                        _ObjVendedor.TbTelefone.TELCELULAR = dr["TELCELULAR"].ToString();
                        _ObjVendedor.TbEndereco.EDNCEP = dr["EDNCEP"].ToString();
                        _ObjVendedor.TbEndereco.EDNCIDADE = dr["EDNCIDADE"].ToString();
                        _ObjVendedor.TbEndereco.EDNUF = dr["EDNUF"].ToString();
                        _ObjVendedor.TbEndereco.EDNLOGRADOURO = dr["EDNLOGRADOURO"].ToString();
                        _ObjVendedor.TbEndereco.EDNBAIRRO = dr["EDNBAIRRO"].ToString();
                        _ObjVendedor.TbEndereco.EDNNUMERO = dr["EDNNUMERO"].ToString();
                        _ObjVendedor.TbEndereco.EDNCOMPLEMENTO = dr["EDNCOMPLEMENTO"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return _ObjVendedor;
        }
        public string ExcluirVendedor(int pesid)

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

            string sRetornaEndereco = RetornaObjeto("TB_EDN_ENDERECO", "PESID", pesid);

            if (!string.IsNullOrEmpty(sRetornaEndereco))
            {
                string endereco = RetornaQueryDelete("TB_EDN_ENDERECO", "PESID", pesid);
                AddListaDeletar(endereco);
            }

            string sRetornaVendedor = RetornaObjeto("TB_VND_VENDEDOR", "PESID", pesid);

            if (!string.IsNullOrEmpty(sRetornaVendedor))
            {
                string vendedor = RetornaQueryDelete("TB_VND_VENDEDOR", "PESID", pesid);
                AddListaDeletar(vendedor);

                string pessoa = RetornaQueryDelete("TB_PES_PESSOA", "PESID", pesid);
                AddListaDeletar(pessoa);

            }

            ExecuteTransacao();

            return "";

        }
        public string DadosVendedor()
        {
            string UltimoSequencial = "";

            string QueryDadosVendedor = "SELECT MAX(IDVENDEDOR) + 1 AS IDVENDEDOR FROM TB_VENDEDOR";

            SqlDataReader dr = ListarDadosEntity(QueryDadosVendedor);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    UltimoSequencial = dr["IDVENDEDOR"].ToString();
                }
            }
            return UltimoSequencial;
        }
        public string RetornaSequencial()
        {
            string retorno = "";
            string query = "SELECT PRV.PRVVALOR FROM TB_PRV_PARAMETROVALOR PRV WHERE PRV.PRVCAMPO = 'VENDEDOR'";

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

