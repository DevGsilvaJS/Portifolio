using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WEB.WorkFlow.Outros;

namespace UI.WEB.WorkFlow.Vendas.TabelasAuxiliares
{
    public class ConvenioWorkFlow : BaseWeb
    {
        DBComando db = new DBComando();

        public List<EntityConvenio> ListaDados()
        {

            List<EntityConvenio> lista = new List<EntityConvenio>();

            EntityConvenio Query = new EntityConvenio();

            SqlDataReader dr = ListarDadosEntity(Query.ListaDadosQuery());

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    EntityConvenio TipoVenda = new EntityConvenio();

                    //TipoVenda.TPVID = int.Parse(dr["TPVID"].ToString());
                    //TipoVenda.TPVDESCRICAO = dr["TPVDESCRICAO"].ToString();
                    //TipoVenda.TPVDEFAULTVENDA = dr["TPVDEFAULTVENDA"].ToString();

                    lista.Add(TipoVenda);
                }
            }

            return lista;

        }
        public EntityConvenio RetornaObjInclusao()
        {
            EntityConvenio objInclusao = new EntityConvenio();
            return objInclusao;
        }
        public string GravarTipoVenda(EntityConvenio ObjConvenio)
        {
            string sRetorno = "NOTOK";

            if (ObjConvenio.CONID > 0)
            {
                AddListaAtualizar(ObjConvenio);
            }
            else
            {
                AddListaSalvar(ObjConvenio);
            }
            sRetorno = ExecuteTransacao();

            return sRetorno;
        }
        public string ExcluirTipoVenda(int conid)
        {
            string sRetorno = "NOTOK";

            AddListaDeletar(RetornaQueryDelete("TB_CON_CONVENIO", "CONID", conid));

            sRetorno = ExecuteTransacao();

            return sRetorno;

        }
        public EntityConvenio GetConvenioByID(int conid)
        {

            //TipoVendaQuery Query = new TipoVendaQuery();
            EntityConvenio _Convenio = new EntityConvenio();

            SqlCommand _Comando = new SqlCommand(Query.EditarTipoVendaQuery(), db.MinhaConexao());

            SqlParameter parameter = new SqlParameter("@TPVID", conid);
            _Comando.Parameters.Add(parameter);
            _Comando.CommandType = CommandType.Text;
            SqlDataReader dr = _Comando.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    //_Convenio.CONID = int.Parse(dr["TPVID"].ToString());
                    //_Convenio.TbEndereco.EDNID = int.Parse(dr["EDNID"].ToString());
                    //_Convenio.TbTelefone. = dr["TPVDEFAULTVENDA"].ToString();
                }
            }
            return _Convenio;
        }
    }
}
