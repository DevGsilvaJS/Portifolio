using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WEB.Model.Estoque.TabelaPrecos;
using UI.WEB.Model.Fiscal.Tabelas_Auxiliares;

namespace UI.WEB.Model.Estoque
{
    [Table("TB_MVN_MOVMATNOTA")]
    public class EntityNotaFiscal
    {

        public int MVNID { get; set; }
        public int FORID { get; set; }
        public string MVNDATAENTRADA { get; set; }
        public string MVNNUMNOTA { get; set; }
        public string MVNMODELONOTA { get; set; }
        public string MVNSERIENOTA { get; set; }
        public string MVNSUBSERIENOTA { get; set; }
        public string MVNTOTALNOTA { get; set; }
        public EntityItensEntrada TbItensEntrada { get; set; }
        public List<EntityItensEntrada> ListaEntrada { get; set; }
        public EntityNotaFiscal()
        {
            ListaEntrada = new List<EntityItensEntrada>();
            TbItensEntrada = new EntityItensEntrada();
        }
    }
}
