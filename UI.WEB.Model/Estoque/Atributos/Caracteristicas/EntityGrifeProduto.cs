using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WEB.Model.Estoque.Atributos.Caracteristicas
{

    [Table("TB_ARG_ATRGRIFE")]
    public class EntityGrifeProduto
    {
        public int ARGID { get; set; }
        public string ARGDESCRICAO { get; set; }
        public string ARGSTATUS { get; set; }
    }
}
