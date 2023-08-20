using System;
using System.Data.SqlClient;
using System.Data;
using UI.WEB.WorkFlow.Outros;
using UI.WEB.Model.Estoque;
using UI.WEB.Query.Estoque;
using System.Collections.Generic;

namespace UI.WEB.WorkFlow.Estoque
{
    public class ProdutoWorkFlow : BaseWeb
    {
        DBComando db = new DBComando();
        public EntityProduto RetornaObjInclusao()
        {
            EntityProduto objInclusao = new EntityProduto();

            return objInclusao;

        }
        public string GravarProduto(EntityProduto objProduto)
        {

            if (objProduto.MATID > 0)
            {
                AddListaAtualizar(objProduto);
                objProduto.TbAtributos.MATID = objProduto.MATID;
                AddListaAtualizar(objProduto.TbAtributos);

                string bLinha = RetornaObjeto("TB_ARL_ATRLINHAPROD", "ARLDESCRICAO", objProduto.TbLinha.ARLDESCRICAO);

                if (!string.IsNullOrEmpty(bLinha))
                {
               
                }

                AddListaAtualizar(objProduto.TbLinha);
                AddListaAtualizar(objProduto.TbGrife);
                AddListaAtualizar(objProduto.TbModelo);
                AddListaAtualizar(objProduto.TbCor);
                AddListaAtualizar(objProduto.TbCorNumerica);
                AddListaAtualizar(objProduto.TbSublinha1);
                AddListaAtualizar(objProduto.TbSublinha2);
                AddListaAtualizar(objProduto.TbTamanho);
                AddListaAtualizar(objProduto.TbMpc);
                AddListaAtualizar(objProduto.TbMpv);
            }
            else
            {
                objProduto.TbLinha.ARLSTATUS = '1'.ToString();
                AddListaSalvar(objProduto.TbLinha);

                objProduto.TbGrife.ARGSTATUS = '1'.ToString();
                AddListaSalvar(objProduto.TbGrife);

                objProduto.TbModelo.ARMSTATUS = '1'.ToString();
                AddListaSalvar(objProduto.TbModelo);

                objProduto.TbCor.ARCSTATUS = '1'.ToString();
                AddListaSalvar(objProduto.TbCor);

                objProduto.TbCorNumerica.ACNSTATUS = '1'.ToString();
                AddListaSalvar(objProduto.TbCorNumerica);

                objProduto.TbSublinha1.AS1STATUS = '1'.ToString();
                AddListaSalvar(objProduto.TbSublinha1);

                objProduto.TbSublinha2.AS2STATUS = '1'.ToString();
                AddListaSalvar(objProduto.TbSublinha2);


                objProduto.TbTamanho.ATOSTATUS = '1'.ToString();
                AddListaSalvar(objProduto.TbTamanho);


                objProduto.MATDTCADASTRO = DateTime.Now.ToString();
                AddListaSalvar(objProduto);

                objProduto.TbAtributos.MATID = objProduto.MATID;
                objProduto.TbAtributos.ARLID = objProduto.TbLinha.ARLID;
                objProduto.TbAtributos.ARCID = objProduto.TbCor.ARCID;
                objProduto.TbAtributos.ACNID = objProduto.TbCorNumerica.ACNID;
                objProduto.TbAtributos.ARMID = objProduto.TbModelo.ARMID;
                objProduto.TbAtributos.ARGID = objProduto.TbGrife.ARGID;
                objProduto.TbAtributos.AS1ID = objProduto.TbSublinha1.AS1ID;
                objProduto.TbAtributos.AS2ID = objProduto.TbSublinha2.AS2ID;
                objProduto.TbAtributos.ATOID = objProduto.TbTamanho.ATOID;


                AddListaSalvar(objProduto.TbAtributos);




                objProduto.TbMpv.MATID = objProduto.MATID;

                if (!string.IsNullOrEmpty(objProduto.TbMpv.MPVPRECOVENDA))
                {
                    objProduto.TbMpv.MPVPRECOVENDA = objProduto.TbMpv.MPVPRECOVENDA.Replace(',', '.');
                    AddListaSalvar(objProduto.TbMpv);
                }

                else
                {
                    objProduto.TbMpv.MPVPRECOVENDA = "0";
                    AddListaSalvar(objProduto.TbMpv);
                }



                objProduto.TbMpc.MATID = objProduto.MATID;


                if (!string.IsNullOrEmpty(objProduto.TbMpc.MPCPRECOCUSTO))
                {
                    objProduto.TbMpc.MPCPRECOCUSTO = objProduto.TbMpc.MPCPRECOCUSTO.Replace(',', '.');
                    AddListaSalvar(objProduto.TbMpc);
                }
                else
                {
                    objProduto.TbMpc.MPCPRECOCUSTO = "0";
                    AddListaSalvar(objProduto.TbMpc);
                }


                AddListaParametros("UPDATE TB_PRV_PARAMETROVALOR SET PRVVALOR = PRVVALOR + 1 WHERE PRVCAMPO = 'ARMACAO'");
            }
            string sRetorno = ExecuteTransacao();

            return sRetorno;
        }
        public List<EntityProduto> ListaProdutos()
        {
            ProdutoQuery Query = new ProdutoQuery();

            List<EntityProduto> lista = new List<EntityProduto>();

            SqlDataReader dr = ListarDadosEntity(Query.ListaProdutosQuery());

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    EntityProduto Produto = new EntityProduto();

                    Produto.MATID = int.Parse(dr["MATID"].ToString());
                    Produto.MATSEQUENCIAL = dr["MATSEQUENCIAL"].ToString();
                    Produto.MATFANTASIA = dr["MATFANTASIA"].ToString();
                    Produto.TbMpc.MPCPRECOCUSTO = dr["MPCPRECOCUSTO"].ToString();
                    Produto.TbMpv.MPVPRECOVENDA = dr["MPVPRECOVENDA"].ToString();
                    Produto.TbGrife.ARGDESCRICAO = dr["ARGDESCRICAO"].ToString();
                    Produto.MATDTCADASTRO = dr["MATDTCADASTRO"].ToString();

                    lista.Add(Produto);
                }
            }
            return lista;
        }
        public string RetornaSequencial()
        {
            string retorno = "";
            string query = "SELECT PRV.PRVVALOR FROM TB_PRV_PARAMETROVALOR PRV WHERE PRV.PRVCAMPO = 'ARMACAO'";

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
        public EntityProduto GetProdutoByID (int matid)
        {
            EntityProduto Produto = new EntityProduto();
            ProdutoQuery Query = new ProdutoQuery();


            SqlCommand _Comando = new SqlCommand(Query.EditarProdutoQuery(), db.MinhaConexao());

            SqlParameter parameter = new SqlParameter("@MATID", matid);
            _Comando.Parameters.Add(parameter);
            _Comando.CommandType = CommandType.Text;
            SqlDataReader dr = _Comando.ExecuteReader();


            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    Produto.MATID = int.Parse(dr["MATID"].ToString());
                    Produto.MATSEQUENCIAL = dr["MATSEQUENCIAL"].ToString();
                    Produto.MATRECSOL = dr["MATRECSOL"].ToString();
                    Produto.NCMID = int.Parse(dr["NCMID"].ToString());
                    Produto.MATDESCRICAO = dr["MATDESCRICAO"].ToString();
                    Produto.MATDESCRICAOECF = dr["MATDESCRICAOECF"].ToString();
                    Produto.FORID = int.Parse(dr["FORID"].ToString());
                    Produto.MATCONTROLAEST = dr["MATCONTROLAEST"].ToString();
                    Produto.MATVENDA = dr["MATVENDA"].ToString();
                    Produto.MATACEITANEGATIVO = dr["MATACEITANEGATIVO"].ToString();
                    Produto.TbLinha.ARLDESCRICAO = dr["ARLDESCRICAO"].ToString();
                    Produto.TbGrife.ARGDESCRICAO = dr["ARGDESCRICAO"].ToString();
                    Produto.TbModelo.ARMDESCRICAO = dr["ARMDESCRICAO"].ToString();
                    Produto.TbCor.ARCDESCRICAO = dr["ARCDESCRICAO"].ToString();
                    Produto.TbCorNumerica.ACNDESCRICAO = dr["ACNDESCRICAO"].ToString();
                    Produto.TbSublinha1.AS1DESCRICAO = dr["AS1DESCRICAO"].ToString();
                    Produto.TbSublinha2.AS2DESCRICAO = dr["AS2DESCRICAO"].ToString();
                    Produto.TbTamanho.ATODESCRICAO = dr["ATODESCRICAO"].ToString();
                    Produto.TbMpc.MPCPRECOCUSTO = dr["MPCPRECOCUSTO"].ToString();
                    Produto.TbMpv.MPVPRECOVENDA = dr["MPVPRECOVENDA"].ToString();
                    Produto.TbMpv.MPVMARKUP = dr["MPVMARKUP"].ToString();
                }
            }

            return Produto;
        }
        public string ExcluirProduto(int matid)
        {
            string sRetorno = "NOTOK";

            string sAtributo = RetornaObjeto("TB_AAT_ATRIBUTOS", "MATID", matid);

            if (!string.IsNullOrEmpty(sAtributo))
            {
                string sQueryAtributos = RetornaQueryDelete("TB_AAT_ATRIBUTOS", "MATID", matid);
                AddListaDeletar(sQueryAtributos);
            }

            string sPrecoVenda = RetornaObjeto("TB_MPV_MATPRECOVENDA", "MATID", matid);

            if (!string.IsNullOrEmpty(sPrecoVenda))
            {
                string sQueryPrecoVenda = RetornaQueryDelete("TB_MPV_MATPRECOVENDA", "MATID", matid);
                AddListaDeletar(sQueryPrecoVenda);
            }

            string sPrecoCusto = RetornaObjeto("TB_MPC_MATPRECOCUSTO", "MATID", matid);

            if (!string.IsNullOrEmpty(sPrecoCusto))
            {
                string sQueryPrecoCusto = RetornaQueryDelete("TB_MPC_MATPRECOCUSTO", "MATID", matid);
                AddListaDeletar(sQueryPrecoCusto);
            }

            string sMaterial = RetornaObjeto("TB_MAT_MATERIAL", "MATID", matid);

            if (!string.IsNullOrEmpty(sMaterial))
            {
                string sQueryMaterial = RetornaQueryDelete("TB_MAT_MATERIAL", "MATID", matid);
                AddListaDeletar(sQueryMaterial);
            }



            sRetorno = ExecuteTransacao();


            return sRetorno;
        }
    }
}
