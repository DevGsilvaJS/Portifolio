﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WEB.Model.Estoque;
using UI.WEB.Model.Estoque.TabelaPrecos;
using UI.WEB.Model.Fiscal.Tabelas_Auxiliares;
using UI.WEB.Query.Estoque;
using UI.WEB.Query.Fiscal;
using UI.WEB.WorkFlow.Outros;

namespace UI.WEB.WorkFlow.Estoque
{
    public class EntradaEstoqueWorkFlow : BaseWeb
    {

        DBComando db = new DBComando();

        public object EntityMPC { get; private set; }

        public List<EntityItensEntrada> RetornaListaEntrada()
        {
            List<EntityItensEntrada> listaEntrada = new List<EntityItensEntrada>();

            return listaEntrada;
        }
        public EntityNotaFiscal RetornaObjInclusao()
        {
            EntityNotaFiscal obj = new EntityNotaFiscal();

            return obj;
        }
        public List<EntityFornecedor> RetornaComboFornecedores()
        {
            List<EntityFornecedor> listaFornecedores = new List<EntityFornecedor>();
            EntradaEstoqueQuery Query = new EntradaEstoqueQuery();

            SqlDataReader dr = ListarDadosEntity(Query.listaFornecedoresQuery());


            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    EntityFornecedor Fornecedor = new EntityFornecedor();

                    Fornecedor.FORID = int.Parse(dr["FORID"].ToString());
                    Fornecedor.TbPessoa.PESNOME = dr["PESNOME"].ToString();

                    listaFornecedores.Add(Fornecedor);
                }
            }

            return listaFornecedores;
        }
        public List<EntityCFOP> RetornaComboCfops()
        {
            List<EntityCFOP> listaCfops = new List<EntityCFOP>();
            EntradaEstoqueQuery Query = new EntradaEstoqueQuery();

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
        public EntityProduto RetornaProduto(string produto)
        {
            EntityProduto Produto = new EntityProduto();




            return Produto;
        }
        public List<EntityProduto> RetornaEntityProduto(string produto)
        {
            List<EntityProduto> listaProduto = new List<EntityProduto>();

            EntradaEstoqueQuery Query = new EntradaEstoqueQuery();

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
                    Produto.TbGrife.ARGDESCRICAO = dr["ARGDESCRICAO"].ToString();
                    Produto.TbCor.ARCDESCRICAO = dr["ARCDESCRICAO"].ToString();
                    
                    listaProduto.Add(Produto);
                }
            }


            return listaProduto;


        }
        public string GravarEntradaEstoque(EntityNotaFiscal EntradaEstoque)
        {
            string sRetorno = "";
            EntradaEstoqueQuery Query = new EntradaEstoqueQuery();


            AddListaSalvar(EntradaEstoque);

            foreach (var item in EntradaEstoque.ListaEntrada)
            {
                item.MVNID = EntradaEstoque.MVNID;
                item.MVMVALCUSTO = item.MVMVALCUSTO.Replace(",", ".");
                item.MVMVALIPI = item.MVMVALIPI.Replace(",", ".");
                item.MVMVALVENDA = item.MVMVALVENDA.Replace(",", ".");
                item.MVMVALUNITARIO = item.MVMVALUNITARIO.Replace(",", ".");

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
                        Estoque.MECQUANTIDADE = int.Parse(dr["MECQUANTIDADE"].ToString());       

                    }
                }

                Estoque.MATID = item.MATID;
                Estoque.MECQUANTIDADE = Estoque.MECQUANTIDADE + int.Parse(item.MVMQUANTIDADE);

                AddListaAtualizar(Estoque);

                EntityMPV TB_MPV_MATPRECOVENDA = new EntityMPV();

                TB_MPV_MATPRECOVENDA.MATID = item.MATID;
                TB_MPV_MATPRECOVENDA.MPVMARKUP = item.MVMMARKUP;
                TB_MPV_MATPRECOVENDA.MPVPRECOVENDA = item.MVMVALVENDA;

                AddListaAtualizar(TB_MPV_MATPRECOVENDA);

                EntityMPC TB_MPC_MATPRECOCUSTO = new EntityMPC();

                string dataAtual = DateTime.Now.ToString("dd/MM/yyyy");


                TB_MPC_MATPRECOCUSTO.MATID = item.MATID;
                TB_MPC_MATPRECOCUSTO.MPCPRECOCUSTO = item.MVMVALUNITARIO;
                TB_MPC_MATPRECOCUSTO.MPCDTALTERACAO = dataAtual;

                AddListaAtualizar(TB_MPC_MATPRECOCUSTO);


            }

            ExecuteTransacao();

            return sRetorno;
        }

    }
}
