using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WEB.Model.Estoque.Atributos.Caracteristicas
{
    [Table("TB_ARC_ATRCOR")]
    public class EntityCorProduto
    {
        public int ARCID { get; set; }
        public string ARCDESCRICAO { get; set; }
        public string ARCSTATUS { get; set; }
    }
}
