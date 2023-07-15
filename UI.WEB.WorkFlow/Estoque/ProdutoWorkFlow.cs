using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WEB.WorkFlow.Outros;
using UI.WEB.Model.Estoque;

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
            objProduto.TbMpv.MATID = objProduto.MATID;
            AddListaSalvar(RetornaQueryInclusao(objProduto.TbMpc, "TB_MPC_MATPRECOCUSTO"));

            string sRetorno = ExecuteTransacao();

            return sRetorno;
        }

        public string RetornaSequencial()
        {
            string retorno = "";
            string query = "SELECT PRV.PRVVALOR FROM TB_PRV_PARAMETROSVALOR PRV WHERE PRV.PRVCAMPO = 'ARMACAO'";

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
    }
}
