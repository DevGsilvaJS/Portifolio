using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WEB.Query.Venda.TabelasAuxiliares;
using UI.WEB.WorkFlow.Outros;

namespace UI.WEB.WorkFlow.Vendas.TabelasAuxiliares
{
    public class ConvenioWorkFlow : BaseWeb
    {
        DBComando db = new DBComando();
        ConvenioQuery Query = new ConvenioQuery();

        public List<EntityConvenio> ListaDados()
        {

            List<EntityConvenio> lista = new List<EntityConvenio>();


            SqlDataReader dr = ListarDadosEntity(Query.ListaDadosQuery());

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    EntityConvenio Convenio = new EntityConvenio();

                    Convenio.CVNID = int.Parse(dr["CVNID"].ToString());
                    Convenio.CVNCONTRATO = dr["CVNCONTRATO"].ToString();
                    Convenio.TbPessoa.PESNOME = dr["PESNOME"].ToString();
                    Convenio.CVNDESCONTO = dr["CVNDESCONTO"].ToString();
                    Convenio.TbTelefone.TELCELULAR = dr["TELEFONE"].ToString();
                    Convenio.CVNNAOAPARECEVENDA = dr["CVNNAOAPARECEVENDA"].ToString();


                    lista.Add(Convenio);
                }
            }

            return lista;

        }
        public EntityConvenio RetornaObjInclusao()
        {
            EntityConvenio objInclusao = new EntityConvenio();
            return objInclusao;
        }
        public string GravarConvenio(EntityConvenio _Convenio)
        {
            string sRetorno = "NOTOK";

            if (_Convenio.CVNID > 0)
            {
                AtualizarConvenio(_Convenio);
            }
            else
            {

                AddListaSalvar(_Convenio.TbPessoa);

                int PESID = _Convenio.TbPessoa.PESID;


                if (!string.IsNullOrEmpty(_Convenio.TbTelefone.TELNUMERO) || !string.IsNullOrEmpty(_Convenio.TbTelefone.TELCELULAR))
                {
                    AddListaSalvar(_Convenio.TbTelefone);
                }

                if (!string.IsNullOrEmpty(_Convenio.TbEndereco.EDNLOGRADOURO))
                {
                    AddListaSalvar(_Convenio.TbEndereco);
                }

                AddListaSalvar(_Convenio);
            }
            sRetorno = ExecuteTransacao();

            db.FechaConexao(db.MinhaConexao());

            return sRetorno;
        }
        public string ExcluirConvenio(int cvnid)
        {
            string sRetorno = "NOTOK";

            AddListaDeletar(RetornaQueryDelete("TB_CVN_CONVENIO", "CVNID", cvnid));

            sRetorno = ExecuteTransacao();

            return sRetorno;

        }
        public EntityConvenio GetConvenioByID(int cvnid)
        {

            SqlCommand _Comando = new SqlCommand(Query.EditarConvenioQuery(), db.MinhaConexao());
            EntityConvenio Convenio = new EntityConvenio();

            SqlParameter parameter = new SqlParameter("@CVNID", cvnid);
            _Comando.Parameters.Add(parameter);
            _Comando.CommandType = CommandType.Text;
            SqlDataReader dr = _Comando.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Convenio.CVNID = int.Parse(dr["CVNID"].ToString());
                    Convenio.TbPessoa.PESID = int.Parse(dr["PESID"].ToString());
                    Convenio.TbEndereco.EDNID = int.Parse(dr["EDNID"].ToString());
                    Convenio.TbTelefone.TELID = int.Parse(dr["TELID"].ToString());
                    Convenio.CVNCONTRATO = dr["CVNCONTRATO"].ToString();
                    Convenio.CVNDESCONTO = dr["CVNDESCONTO"].ToString();
                    Convenio.CVNOBSERVACAO = dr["CVNOBSERVACAO"].ToString();
                    Convenio.CVNNAOAPARECEVENDA = dr["CVNNAOAPARECEVENDA"].ToString();
                    Convenio.TbPessoa.PESNOME = dr["PESNOME"].ToString();
                    Convenio.TbEndereco.EDNCEP = dr["EDNCEP"].ToString();
                    Convenio.TbEndereco.EDNCIDADE = dr["EDNCIDADE"].ToString();
                    Convenio.TbEndereco.EDNLOGRADOURO = dr["EDNLOGRADOURO"].ToString();
                    Convenio.TbEndereco.EDNUF = dr["EDNUF"].ToString();
                    Convenio.TbEndereco.EDNBAIRRO = dr["EDNBAIRRO"].ToString();
                    Convenio.TbEndereco.EDNNUMERO = dr["EDNNUMERO"].ToString();
                    Convenio.TbEndereco.EDNCOMPLEMENTO = dr["EDNCOMPLEMENTO"].ToString();
                    Convenio.TbTelefone.TELNUMERO = dr["TELNUMERO"].ToString();
                    Convenio.TbTelefone.TELDDD = dr["TELDDD"].ToString();
                    Convenio.TbTelefone.TELCELULAR = dr["TELCELULAR"].ToString();
                    Convenio.TbTelefone.TELDDDC = dr["TELDDDC"].ToString();
                }
            }
            return Convenio;
        }
        public string AtualizarConvenio(EntityConvenio _Convenio)
        {
            string sRetorno = "NOTOK";
            int PESID = _Convenio.TbPessoa.PESID;


            _Convenio.PESID = PESID;

            AddListaAtualizar(_Convenio);
            AddListaAtualizar(_Convenio.TbPessoa);


            string sEndereco = RetornaObjeto("TB_EDN_ENDERECO", "PESID", _Convenio.TbPessoa.PESID);

            if (!string.IsNullOrEmpty(sEndereco))
            {
                _Convenio.TbEndereco.PESID = _Convenio.TbPessoa.PESID;
                AddListaAtualizar(_Convenio.TbEndereco);
            }
            else
            {
                AddListaSalvar(_Convenio.TbEndereco);
            }

            string sTelefone = RetornaObjeto("TB_TEL_TELEFONE", "PESID", _Convenio.TbPessoa.PESID);

            if (!string.IsNullOrEmpty(sTelefone))
            {
                _Convenio.TbTelefone.PESID = _Convenio.TbPessoa.PESID;
                AddListaAtualizar(_Convenio.TbTelefone);
            }
            else
            {
                AddListaSalvar(_Convenio.TbTelefone);
            }


            return sRetorno;
        }
    }
}
