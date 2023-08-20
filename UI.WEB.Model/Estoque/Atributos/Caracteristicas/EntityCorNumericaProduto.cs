using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WEB.Model.Estoque.Atributos.Caracteristicas
{
    [Table("TB_ACN_ATRCORNUMERICA")]
    public class EntityCorNumericaProduto
    {
        public int ACNID { get; set; }
        public string ACNDESCRICAO { get; set; }
        public string ACNSTATUS { get; set; }
    }
}
