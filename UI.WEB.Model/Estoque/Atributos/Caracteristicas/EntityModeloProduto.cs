using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WEB.Model.Estoque.Atributos.Caracteristicas
{

    [Table("TB_ARM_ATRMODELO")]
    public class EntityModeloProduto
    {
        public int ARMID { get; set; }
        public string ARMDESCRICAO { get; set; }
        public string ARMSTATUS { get; set; }
    }
}
