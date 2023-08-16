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
                AddListaSalvar(RetornaQueryUpdate(objProduto, "TB_MAT_MATERIAL"));
                objProduto.TbAtributos.MATID = objProduto.MATID;
                AddListaSalvar(RetornaQueryUpdate(objProduto.TbAtributos, "TB_AAT_ATRIBUTOS"));

                string bLinha = RetornaObjeto("TB_ARL_ATRLINHAPROD", "ARLDESCRICAO", objProduto.TbLinha.ARLDESCRICAO);

                if (!string.IsNullOrEmpty(bLinha))
                {
               
                }

                AddListaSalvar(RetornaQueryUpdate(objProduto.TbLinha, "TB_ARL_ATRLINHAPROD"));
                AddListaSalvar(RetornaQueryUpdate(objProduto.TbGrife, "TB_ARG_ATRGRIFE"));
                AddListaSalvar(RetornaQueryUpdate(objProduto.TbModelo, "TB_ARM_ATRMODELO"));
                AddListaSalvar(RetornaQueryUpdate(objProduto.TbCor, "TB_ARC_ATRCOR"));
                AddListaSalvar(RetornaQueryUpdate(objProduto.TbCorNumerica, "TB_ACN_ATRCORNUMERICA"));
                AddListaSalvar(RetornaQueryUpdate(objProduto.TbSublinha1, "TB_AS1_ATRSUBLINHA1"));
                AddListaSalvar(RetornaQueryUpdate(objProduto.TbSublinha2, "TB_AS2_ATRSUBLINHA2"));
                AddListaSalvar(RetornaQueryUpdate(objProduto.TbTamanho, "TB_ATO_ATRTAMANHO"));
                AddListaSalvar(RetornaQueryUpdate(objProduto.TbMpc, "TB_MPC_MATPRECOCUSTO"));
                AddListaSalvar(RetornaQueryUpdate(objProduto.TbMpv, "TB_MPV_MATPRECOVENDA"));
            }
            else
            {
                objProduto.TbLinha.ARLID = RetornaSequencial("SEQ_ARL");
                objProduto.TbLinha.ARLSTATUS = '1'.ToString();
                AddListaSalvar(RetornaQueryInclusao(objProduto.TbLinha, "TB_ARL_ATRLINHAPROD"));

                objProduto.TbGrife.ARGID = RetornaSequencial("SEQ_ARG");
                objProduto.TbGrife.ARGSTATUS = '1'.ToString();
                AddListaSalvar(RetornaQueryInclusao(objProduto.TbGrife, "TB_ARG_ATRGRIFE"));

                objProduto.TbModelo.ARMID = RetornaSequencial("SEQ_ARM");
                objProduto.TbModelo.ARMSTATUS = '1'.ToString();
                AddListaSalvar(RetornaQueryInclusao(objProduto.TbModelo, "TB_ARM_ATRMODELO"));

                objProduto.TbCor.ARCID = RetornaSequencial("SEQ_ARC");
                objProduto.TbCor.ARCSTATUS = '1'.ToString();
                AddListaSalvar(RetornaQueryInclusao(objProduto.TbCor, "TB_ARC_ATRCOR"));

                objProduto.TbCorNumerica.ACNID = RetornaSequencial("SEQ_ACN");
                objProduto.TbCorNumerica.ACNSTATUS = '1'.ToString();
                AddListaSalvar(RetornaQueryInclusao(objProduto.TbCorNumerica, "TB_ACN_ATRCORNUMERICA"));

                objProduto.TbSublinha1.AS1ID = RetornaSequencial("SEQ_AS1");
                objProduto.TbSublinha1.AS1STATUS = '1'.ToString();
                AddListaSalvar(RetornaQueryInclusao(objProduto.TbSublinha1, "TB_AS1_ATRSUBLINHA1"));

                objProduto.TbSublinha2.AS2ID = RetornaSequencial("SEQ_AS2");
                objProduto.TbSublinha2.AS2STATUS = '1'.ToString();
                AddListaSalvar(RetornaQueryInclusao(objProduto.TbSublinha2, "TB_AS2_ATRSUBLINHA2"));

                objProduto.TbTamanho.ATOID = RetornaSequencial("SEQ_ATO");
                objProduto.TbTamanho.ATOSTATUS = '1'.ToString();
                AddListaSalvar(RetornaQueryInclusao(objProduto.TbTamanho, "TB_ATO_ATRTAMANHO"));


                objProduto.MATID = RetornaSequencial("SEQ_MAT");
                objProduto.MATDTCADASTRO = DateTime.Now.ToString();
                AddListaSalvar(RetornaQueryInclusao(objProduto, "TB_MAT_MATERIAL"));

                objProduto.TbAtributos.MATID = objProduto.MATID;
                objProduto.TbAtributos.ARLID = objProduto.TbLinha.ARLID;
                objProduto.TbAtributos.ARCID = objProduto.TbCor.ARCID;
                objProduto.TbAtributos.ACNID = objProduto.TbCorNumerica.ACNID;
                objProduto.TbAtributos.ARMID = objProduto.TbModelo.ARMID;
                objProduto.TbAtributos.ARGID = objProduto.TbGrife.ARGID;
                objProduto.TbAtributos.AS1ID = objProduto.TbSublinha1.AS1ID;
                objProduto.TbAtributos.AS2ID = objProduto.TbSublinha2.AS2ID;
                objProduto.TbAtributos.ATOID = objProduto.TbTamanho.ATOID;


                AddListaSalvar(RetornaQueryInclusao(objProduto.TbAtributos, "TB_AAT_ATRIBUTOS"));



                objProduto.TbMpv.MPVID = RetornaSequencial("SEQ_MPV");
                objProduto.TbMpv.MATID = objProduto.MATID;
                AddListaSalvar(RetornaQueryInclusao(objProduto.TbMpv, "TB_MPV_MATPRECOVENDA"));

                objProduto.TbMpc.MPCID = RetornaSequencial("SEQ_MPC");
                objProduto.TbMpc.MATID = objProduto.MATID;
                AddListaSalvar(RetornaQueryInclusao(objProduto.TbMpc, "TB_MPC_MATPRECOCUSTO"));
                AddListaSalvar("UPDATE TB_PRV_PARAMETROVALOR SET PRVVALOR = PRVVALOR + 1 WHERE PRVCAMPO = 'ARMACAO'");
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
    }
}
