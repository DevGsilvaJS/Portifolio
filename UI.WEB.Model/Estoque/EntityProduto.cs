using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WEB.Model.Estoque.Atributos;
using UI.WEB.Model.Estoque.Atributos.Caracteristicas;
using UI.WEB.Model.Estoque.TabelaPrecos;

namespace UI.WEB.Model.Estoque
{
    public class EntityProduto
    {
        public int MATID { get; set; }
        public int NCMID { get; set; }
        public int FORID { get; set; }
        public string MATSEQUENCIAL { get; set; }
        public string MATRECSOL { get; set; }
        public string MATDESCRICAO { get; set; }
        public string MATDESCRICAOECF { get; set; }
        public string MATVENDA { get; set; }
        public string MATFANTASIA { get; set; }
        public string MATCONTROLAEST { get; set; }
        public string MATDTCADASTRO { get; set; }
        public string MATACEITANEGATIVO { get; set; }

        public EntityAtributosProduto TbAtributos { get; set; }
        public EntityGrifeProduto TbGrife { get; set; }
        public EntityGrupoProduto TbGrupo { get; set; }
        public EntityLinhaProduto TbLinha { get; set; }
        public EntityModeloProduto TbModelo { get; set; }
        public EntitySublinha1Produto TbSublinha1 { get; set; }
        public EntitySublinha2Produto TbSublinha2 { get; set; }
        public EntityTamanhoProduto TbTamanho { get; set; }
        public EntityCorNumericaProduto TbCorNumerica { get; set; }
        public EntityCorProduto TbCor { get; set; }
        public EntityMPV  TbMpv { get; set; }
        public EntiyMPC TbMpc { get; set; }

        public EntityProduto()
        {
            TbAtributos = new EntityAtributosProduto();
            TbGrife = new EntityGrifeProduto();
            TbGrupo = new EntityGrupoProduto();
            TbLinha = new EntityLinhaProduto();
            TbModelo = new EntityModeloProduto();
            TbSublinha1 = new EntitySublinha1Produto();
            TbSublinha2 = new EntitySublinha2Produto();
            TbCor = new EntityCorProduto();
            TbTamanho = new EntityTamanhoProduto();
            TbCorNumerica = new EntityCorNumericaProduto();
            TbMpv = new EntityMPV();
            TbMpc = new EntiyMPC();
        }
    }
}
