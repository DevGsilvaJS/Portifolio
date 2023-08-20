using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WEB.Model.Vendas.TabelasAuxiliares;
using UI.WEB.Query.Venda.TabelasAuxiliares;
using UI.WEB.WorkFlow.Outros;

namespace UI.WEB.WorkFlow.Vendas.TabelasAuxiliares
{
    public class TipoVendaWorkFlow : BaseWeb
    {
        DBComando db = new DBComando();

        public List<EntityTipoVenda> ListaDados()
        {

            List<EntityTipoVenda> lista = new List<EntityTipoVenda>();

            TipoVendaQuery Query = new TipoVendaQuery();

            SqlDataReader dr = ListarDadosEntity(Query.ListaDadosQuery());

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    EntityTipoVenda TipoVenda = new EntityTipoVenda();

                    TipoVenda.TPVID = int.Parse(dr["TPVID"].ToString());
                    TipoVenda.TPVDESCRICAO = dr["TPVDESCRICAO"].ToString();
                    TipoVenda.TPVDEFAULTVENDA = dr["TPVDEFAULTVENDA"].ToString();

                    lista.Add(TipoVenda);
                }
            }

            return lista;

        }

        public EntityTipoVenda RetornaObjInclusao()
        {
            EntityTipoVenda objInclusao = new EntityTipoVenda();
            return objInclusao;
        }
        public string GravarTipoVenda(EntityTipoVenda ObjTipoVenda)
        {
            string sRetorno = "NOTOK";

            if (ObjTipoVenda.TPVID > 0)
            {
                AddListaAtualizar(ObjTipoVenda);
            }
            else
            {
                AddListaSalvar(ObjTipoVenda);
            }
            sRetorno = ExecuteTransacao();

            return sRetorno;
        }
        public string ExcluirTipoVenda(int tpvid)
        {
            string sRetorno = "NOTOK";

            AddListaDeletar(RetornaQueryDelete("TB_TPV_TIPOVENDA", "TPVID", tpvid));

            sRetorno = ExecuteTransacao();

            return sRetorno;

        }

        public EntityTipoVenda GetTipoVendaByID(int tpvid)
        {

            TipoVendaQuery Query = new TipoVendaQuery();
            EntityTipoVenda _TipoVenda = new EntityTipoVenda();

            SqlCommand _Comando = new SqlCommand(Query.EditarTipoVendaQuery(), db.MinhaConexao());

            SqlParameter parameter = new SqlParameter("@TPVID", tpvid);
            _Comando.Parameters.Add(parameter);
            _Comando.CommandType = CommandType.Text;
            SqlDataReader dr = _Comando.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {      
                    _TipoVenda.TPVID = int.Parse(dr["TPVID"].ToString());
                    _TipoVenda.TPVDESCRICAO = dr["TPVDESCRICAO"].ToString();
                    _TipoVenda.TPVDEFAULTVENDA = dr["TPVDEFAULTVENDA"].ToString();
                }
            }
            return _TipoVenda;
        }
    }
}
