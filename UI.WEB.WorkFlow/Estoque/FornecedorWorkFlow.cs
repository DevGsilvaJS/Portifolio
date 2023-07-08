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
        public string RetornaSequencial()
        {
            string retorno = "";
            string query = "SELECT PRV.PRVVALOR FROM TB_PRV_PARAMETROSVALOR PRV WHERE PRV.PRVCAMPO = 'FORNECEDOR'";

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

                    if (!string.IsNullOrEmpty(ObjFornecedor.TbEmail.EMLEMAIL))
                    {
                        ObjFornecedor.TbEmail.PESID = ObjFornecedor.TbPessoa.PESID;
                        AddListaSalvar(RetornaQueryInclusao(ObjFornecedor.TbEmail, "TB_EML_EMAIL"));
                    }

                    if (!string.IsNullOrEmpty(ObjFornecedor.TbTelefone.TELNUMERO) || !string.IsNullOrEmpty(ObjFornecedor.TbTelefone.TELCELULAR))
                    {
                        ObjFornecedor.TbTelefone.PESID = ObjFornecedor.TbPessoa.PESID;
                        AddListaSalvar(RetornaQueryInclusao(ObjFornecedor.TbTelefone, "TB_TEL_TELEFONE"));
                    }

                    if (!string.IsNullOrEmpty(ObjFornecedor.TbEndereco.EDNLOGRADOURO))
                    {
                        ObjFornecedor.TbTelefone.PESID = ObjFornecedor.TbPessoa.PESID;
                        AddListaSalvar(RetornaQueryInclusao(ObjFornecedor.TbEndereco, "TB_EDN_ENDERECO"));
                    }

                    AddListaSalvar(RetornaQueryInclusao(ObjFornecedor.TbCentroCusto, "TB_CCU_CENTROCUSTO"));
                    AddListaSalvar(RetornaQueryInclusao(ObjFornecedor.TbPlanoContas, "TB_PCT_PLANOCONTAS"));

                    sRetorno = ExecuteTransacao();
                }
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
    }
}
