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

        public List<EntityProduto> RetornaListaSaida(string produto)
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

        public string GravarSaidaEstoque(EntityNotaFiscal _SaidaEstoque)
        {
            string sRetorno = "";
            EntradaEstoqueQuery Query = new EntradaEstoqueQuery();

            _SaidaEstoque.FORID = null;

            AddListaSalvar(_SaidaEstoque);

            foreach (var item in _SaidaEstoque.ListaEntrada)
            {
                item.MVNID = _SaidaEstoque.MVNID;
                AddListaSalvar(item);

                EntityEstoque Estoque = new EntityEstoque();

                SqlCommand _Comando = new SqlCommand(Query.retornaQuantidadeQuery(), db.MinhaConexao());

                SqlParameter parametro = new SqlParameter("MATID", item.MATID);
                _Comando.Parameters.Add(parametro);
                _Comando.CommandType = CommandType.Text;

                SqlDataReader dr = _Comando.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Estoque.MECQUANTIDADE = dr["MECQUANTIDADE"].ToString();

                    }
                }


                Estoque.MATID = item.MATID;
                Estoque.MECQUANTIDADE = (int.TryParse(Estoque.MECQUANTIDADE, out int mecQuantidade) && int.TryParse(item.MVMQUANTIDADE, out int mvmQuantidade))
                                                     ? (mecQuantidade - mvmQuantidade).ToString()
                                                     : Estoque.MECQUANTIDADE;

                EntityEstoque TB_MEC_MATESTCONTROLE = RetornaObjeto<EntityEstoque>("TB_MEC_MATESTCONTROLE", item.MATID, "MATID");

                if (TB_MEC_MATESTCONTROLE != null)
                {
                    Estoque.MECID = TB_MEC_MATESTCONTROLE.MECID;
                    AddListaAtualizar(Estoque);
                }

                else
                {
                    AddListaSalvar(Estoque);
                }
            }



            ExecuteTransacao();
            db.FechaConexao(db.MinhaConexao());

            return sRetorno;
        }
    }
}
