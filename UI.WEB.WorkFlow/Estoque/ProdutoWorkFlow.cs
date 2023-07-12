using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WEB.Model.Estoque;
using UI.WEB.WorkFlow.Outros;

namespace UI.WEB.WorkFlow.Estoque
{
    public class ProdutoWorkFlow : BaseWeb
    {

        public EntityProduto RetornaObjInclusao()
        {
            EntityProduto objInclusao = new EntityProduto();

            return objInclusao;

        }
        public string GravarProduto(EntityProduto objProduto)
        {

            objProduto.TbGrife.ARGID = RetornaSequencial("SEQ_ARG");
            AddListaSalvar(RetornaQueryInclusao(objProduto.TbGrife, "TB_ARG_ATRGRIFE"));

            objProduto.TbGrupo.ATPID = RetornaSequencial("SEQ_ATP");
            AddListaSalvar(RetornaQueryInclusao(objProduto.TbGrupo, "TB_ATP_ATPGRUPO"));

            objProduto.TbLinha.ARLID = RetornaSequencial("SEQ_ARL");
            AddListaSalvar(RetornaQueryInclusao(objProduto.TbLinha.ARLID, "TB_ARL_ATRLINHAPROD"));

            objProduto.TbModelo.ARMID = RetornaSequencial("SEQ_ARM");
            AddListaSalvar(RetornaQueryInclusao(objProduto.TbModelo, "TB_ARM_ATRMODELO"));

            objProduto.TbSublinha1.AS1ID = RetornaSequencial("SEQ_AS1");
            AddListaSalvar(RetornaQueryInclusao(objProduto.TbSublinha1, "TB_AS1_ATRSUBLINHA1"));

            objProduto.TbSublinha2.AS2ID = RetornaSequencial("SEQ_AS2");
            AddListaSalvar(RetornaQueryInclusao(objProduto.TbSublinha2, "TB_AS2_ATRSUBLINHA2"));

            objProduto.TbTamanho.ATOID = RetornaSequencial("SEQ_ATO");
            AddListaSalvar(RetornaQueryInclusao(objProduto.TbTamanho, "TB_ATO_ATRTAMANHO"));

            string sRetorno = ExecuteTransacao();

            return sRetorno;
        }
    }
}
