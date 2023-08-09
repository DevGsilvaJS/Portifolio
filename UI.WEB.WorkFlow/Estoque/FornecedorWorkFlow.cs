using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WEB.Model.Estoque;
using UI.WEB.WorkFlow.Outros;

namespace UI.WEB.WorkFlow.Estoque
{
    public class FornecedorWorkFlow : BaseWeb
    {
        DBComando db = new DBComando();

        public EntityFornecedor RetornaObjInclusao()
        {
            EntityFornecedor obj = new EntityFornecedor();

            return obj;
        }
        public string RetornaSequencial()
        {
            string retorno = "";
            string query = "SELECT PRV.PRVVALOR FROM TB_PRV_PARAMETROVALOR PRV WHERE PRV.PRVCAMPO = 'FORNECEDOR'";

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
        public string GravarFornecedor(EntityFornecedor ObjFornecedor)
        {
            string sRetorno = "";

            try
            {
                if (ObjFornecedor.TbPessoa.PESID > 0)
                {
                    AddListaSalvar(RetornaQueryUpdate(ObjFornecedor.TbPessoa, "TB_PES_PESSOA"));
                    AddListaSalvar(RetornaQueryUpdate(ObjFornecedor, "TB_FOR_FORNECEDOR"));

                    bool bEndereco = RetornaObjeto("TB_EDN_ENDERECO", "PESID", ObjFornecedor.TbPessoa.PESID);

                    if (bEndereco)
                    {
                        AddListaSalvar(RetornaQueryUpdate(ObjFornecedor.TbEndereco, "TB_EDN_ENDERECO"));
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(ObjFornecedor.TbEndereco.EDNLOGRADOURO))
                        {
                            AddListaSalvar(RetornaQueryInclusao(ObjFornecedor.TbEndereco, "TB_EDN_ENDERECO"));
                        }
                    }

                    bool bTelefone = RetornaObjeto("TB_TEL_TELEFONE", "PESID", ObjFornecedor.TbPessoa.PESID);

                    if (bTelefone)
                    {
                        AddListaSalvar(RetornaQueryUpdate(ObjFornecedor.TbTelefone, "TB_TEL_TELEFONE"));
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(ObjFornecedor.TbTelefone.TELNUMERO) || !string.IsNullOrEmpty(ObjFornecedor.TbTelefone.TELCELULAR))
                        {
                            AddListaSalvar(RetornaQueryInclusao(ObjFornecedor.TbTelefone, "TB_TEL_TELEFONE"));
                        }
                    }

                    bool bEmail = RetornaObjeto("TB_EML_EMAIL", "PESID", ObjFornecedor.TbPessoa.PESID);

                    if (bEmail)
                    {
                        AddListaSalvar(RetornaQueryUpdate(ObjFornecedor.TbEmail, "TB_EML_EMAIL"));
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(ObjFornecedor.TbEmail.EMLEMAIL))
                        {
                            AddListaSalvar(RetornaQueryInclusao(ObjFornecedor.TbEmail, "TB_EML_EMAIL"));
                        }
                    }
                }

                else
                {
                    ObjFornecedor.TbPessoa.PESID = RetornaSequencial("SEQ_PES");
                    AddListaSalvar(RetornaQueryInclusao(ObjFornecedor.TbPessoa, "TB_PES_PESSOA"));
                    ObjFornecedor.PESID = ObjFornecedor.TbPessoa.PESID;


                    ObjFornecedor.FORID = RetornaSequencial("SEQ_FOR");
                    ObjFornecedor.PESID = ObjFornecedor.TbPessoa.PESID;
                    AddListaSalvar(RetornaQueryInclusao(ObjFornecedor, "TB_FOR_FORNECEDOR"));

                    if (!string.IsNullOrEmpty(ObjFornecedor.TbEmail.EMLEMAIL))
                    {
                        ObjFornecedor.TbEmail.EMLID = RetornaSequencial("SEQ_EML");
                        ObjFornecedor.TbEmail.PESID = ObjFornecedor.TbPessoa.PESID;
                        AddListaSalvar(RetornaQueryInclusao(ObjFornecedor.TbEmail, "TB_EML_EMAIL"));
                    }

                    if (!string.IsNullOrEmpty(ObjFornecedor.TbTelefone.TELNUMERO) || !string.IsNullOrEmpty(ObjFornecedor.TbTelefone.TELCELULAR))
                    {
                        ObjFornecedor.TbTelefone.TELID = RetornaSequencial("SEQ_TEL");
                        ObjFornecedor.TbTelefone.PESID = ObjFornecedor.TbPessoa.PESID;
                        AddListaSalvar(RetornaQueryInclusao(ObjFornecedor.TbTelefone, "TB_TEL_TELEFONE"));
                    }

                    if (!string.IsNullOrEmpty(ObjFornecedor.TbEndereco.EDNLOGRADOURO))
                    {
                        ObjFornecedor.TbEndereco.EDNID = RetornaSequencial("SEQ_EDN");
                        ObjFornecedor.TbEndereco.PESID = ObjFornecedor.TbPessoa.PESID;
                        AddListaSalvar(RetornaQueryInclusao(ObjFornecedor.TbEndereco, "TB_EDN_ENDERECO"));
                    }

                    AddListaSalvar("UPDATE TB_PRV_PARAMETROVALOR SET PRVVALOR = PRVVALOR + 1 WHERE PRVCAMPO = 'FORNECEDOR'");


                }
                sRetorno = ExecuteTransacao();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return sRetorno;
        }
        public List<EntityFornecedor> ListarFornecedores()
        {

            List<EntityFornecedor> lista = new List<EntityFornecedor>();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(" SELECT");
            sb.AppendLine("      PES.PESID");
            sb.AppendLine("     ,PES.PESNOME");
            sb.AppendLine("     ,PES.PESDOCFEDERAL");
            sb.AppendLine("     ,PES.PESDOCESTADUAL");
            sb.AppendLine("     ,EML.EMLEMAIL");
            sb.AppendLine("  FROM TB_PES_PESSOA PES");
            sb.AppendLine("    INNER JOIN TB_FOR_FORNECEDOR FORN ON FORN.PESID = PES.PESID");
            sb.AppendLine("    LEFT JOIN TB_EML_EMAIL EML ON EML.PESID = PES.PESID");

            SqlDataReader dr = ListarDadosEntity(sb.ToString());

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    EntityFornecedor obj = new EntityFornecedor();

                    obj.TbPessoa.PESID = int.Parse(dr["PESID"].ToString());
                    obj.TbPessoa.PESNOME = dr["PESNOME"].ToString();
                    obj.TbPessoa.PESDOCESTADUAL = dr["PESDOCESTADUAL"].ToString();
                    obj.TbPessoa.PESDOCFEDERAL = dr["PESDOCFEDERAL"].ToString();
                    obj.TbEmail.EMLEMAIL = dr["EMLEMAIL"].ToString();

                    lista.Add(obj);
                }
            }

            return lista;

        }
        public string ExcluirFornecedor(int idFornecedor)
        {
            string sRetorno = "NOTOK";

            bool bEmail = RetornaObjeto("TB_EML_EMAIL", "PESID", idFornecedor);

            if (bEmail)
            {
                string sQueryEmail = RetornaQueryDelete("TB_EML_EMAIL", "PESID", idFornecedor);
                AddListaSalvar(sQueryEmail);
            }

            bool bTelefone = RetornaObjeto("TB_TEL_TELEFONE", "PESID", idFornecedor);

            if (bTelefone)
            {
                string sQueryTelefone = RetornaQueryDelete("TB_TEL_TELEFONE", "PESID", idFornecedor);
                AddListaSalvar(sQueryTelefone);
            }

            bool bEndereco = RetornaObjeto("TB_EDN_ENDERECO", "PESID", idFornecedor);

            if (bEndereco)
            {
                string sQueryEndereco = RetornaQueryDelete("TB_EDN_ENDERECO", "PESID", idFornecedor);
                AddListaSalvar(sQueryEndereco);
            }

            bool bFornecedor = RetornaObjeto("TB_FOR_FORNECEDOR", "PESID", idFornecedor);

            if (bFornecedor)
            {
                string sFornecedor = RetornaQueryDelete("TB_FOR_FORNECEDOR", "PESID", idFornecedor);
                AddListaSalvar(sFornecedor);

                string sPessoa = RetornaQueryDelete("TB_PES_PESSOA", "PESID", idFornecedor);
                AddListaSalvar(sPessoa);
            }

            sRetorno = ExecuteTransacao();

            return sRetorno;
        }
        public EntityFornecedor EditarFornecedor(int pESID)
        {

            EntityFornecedor objFornecedor = new EntityFornecedor();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(" SELECT");
            sb.AppendLine("              PES.PESID");
            sb.AppendLine("             ,PES.PESNOME");
            sb.AppendLine("             ,PES.PESSOBRENOME");
            sb.AppendLine("             ,PES.PESDOCESTADUAL");
            sb.AppendLine("             ,PES.PESDOCFEDERAL");
            sb.AppendLine("             ,FORN.CCUID");
            sb.AppendLine("             ,FORN.PCTID");
            sb.AppendLine("             ,TEL.TELDDD");
            sb.AppendLine("             ,TEL.TELNUMERO");
            sb.AppendLine("             ,TEL.TELDDDC");
            sb.AppendLine("             ,TEL.TELCELULAR");
            sb.AppendLine("             ,EML.EMLEMAIL");
            sb.AppendLine("             ,EDN.EDNCEP");
            sb.AppendLine("             ,EDN.EDNBAIRRO");
            sb.AppendLine("             ,EDN.EDNCIDADE");
            sb.AppendLine("             ,EDN.EDNLOGRADOURO");
            sb.AppendLine("             ,EDN.EDNNUMERO");
            sb.AppendLine("             ,EDN.EDNUF");
            sb.AppendLine("     FROM TB_PES_PESSOA PES");
            sb.AppendLine("             INNER JOIN TB_FOR_FORNECEDOR FORN ON FORN.PESID = PES.PESID");
            sb.AppendLine("             LEFT JOIN TB_TEL_TELEFONE TEL ON TEL.PESID = PES.PESID");
            sb.AppendLine("             INNER JOIN TB_EDN_ENDERECO EDN ON EDN.PESID = PES.PESID");
            sb.AppendLine("             INNER JOIN TB_EML_EMAIL EML ON EML.PESID = PES.PESID");
            sb.AppendLine("    WHERE PES.PESID = " + pESID);


            SqlDataReader dr = ListarDadosEntity(sb.ToString());

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    objFornecedor.TbPessoa.PESID = int.Parse(dr["PESID"].ToString());
                    objFornecedor.TbPessoa.PESNOME = dr["PESNOME"].ToString();
                    objFornecedor.TbPessoa.PESSOBRENOME = dr["PESSOBRENOME"].ToString();
                    objFornecedor.TbPessoa.PESDOCESTADUAL = dr["PESDOCESTADUAL"].ToString();
                    objFornecedor.TbPessoa.PESDOCFEDERAL = dr["PESDOCFEDERAL"].ToString();
                    objFornecedor.CCUID = int.Parse(dr["CCUID"].ToString());
                    objFornecedor.PCTID = int.Parse(dr["PCTID"].ToString());
                    objFornecedor.TbTelefone.TELDDD = dr["TELDDD"].ToString();
                    objFornecedor.TbTelefone.TELNUMERO = dr["TELNUMERO"].ToString();
                    objFornecedor.TbTelefone.TELDDDC = dr["TELDDDC"].ToString();
                    objFornecedor.TbTelefone.TELCELULAR = dr["TELCELULAR"].ToString();
                    objFornecedor.TbEndereco.EDNCEP = dr["EDNCEP"].ToString();
                    objFornecedor.TbEndereco.EDNBAIRRO = dr["EDNBAIRRO"].ToString();
                    objFornecedor.TbEndereco.EDNCIDADE = dr["EDNCIDADE"].ToString();
                    objFornecedor.TbEndereco.EDNLOGRADOURO = dr["EDNLOGRADOURO"].ToString();
                    objFornecedor.TbEndereco.EDNNUMERO = dr["EDNNUMERO"].ToString();
                    objFornecedor.TbEndereco.EDNUF = dr["EDNUF"].ToString();
                }
            }

            return objFornecedor;
        }
    }
}

