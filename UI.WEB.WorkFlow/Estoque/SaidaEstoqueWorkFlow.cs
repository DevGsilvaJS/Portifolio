using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WEB.Model.Estoque;
using UI.WEB.Model.Fiscal.Tabelas_Auxiliares;
using UI.WEB.Query.Estoque;
using UI.WEB.WorkFlow.Outros;

namespace UI.WEB.WorkFlow.Estoque
{
    public class SaidaEstoqueWorkFlow : BaseWeb
    {
        DBComando db = new DBComando();
        public EntityNotaFiscal RetornaObjInclusao()
        {
            EntityNotaFiscal obj = new EntityNotaFiscal();

            return obj;
        }

        public List<EntityCFOP> RetornaComboCfops()
        {
            List<EntityCFOP> listaCfops = new List<EntityCFOP>();
            SaidaEstoqueQuery Query = new SaidaEstoqueQuery();

            SqlDataReader dr = ListarDadosEntity(Query.listaCfopQuery());

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    EntityCFOP Cfop = new EntityCFOP();

                    Cfop.COPID = int.Parse(dr["COPID"].ToString());
                    Cfop.COPDESCRICAO = dr["COPDESCRICAO"].ToString();

                    listaCfops.Add(Cfop);
                }
            }

            return listaCfops;
        }

        public List<EntityProduto> RetornaListaSaida (string produto)
        {

            List<EntityProduto> lista = new List<EntityProduto>();


            SaidaEstoqueQuery Query = new SaidaEstoqueQuery();

            SqlCommand _Comando = new SqlCommand(Query.buscaProdutoQuery(), db.MinhaConexao());

            SqlParameter parameter = new SqlParameter("@produto", produto);
            _Comando.Parameters.Add(parameter);
            _Comando.CommandType = CommandType.Text;
            SqlDataReader dr = _Comando.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    EntityProduto Produto = new EntityProduto();

                    Produto.MATID = int.Parse(dr["MATID"].ToString());
                    Produto.MATSEQUENCIAL = dr["MATSEQUENCIAL"].ToString();
                    Produto.MATFANTASIA = dr["MATFANTASIA"].ToString();
                    Produto.TbMec.MECQUANTIDADE = dr["MECQUANTIDADE"].ToString();

                    lista.Add(Produto);
                }
            }



            return lista;
        }
    }
}
