using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WEB.Model.Estoque
{
    [Table("TB_MEC_MATESTCONTROLE")]
    public class EntityEstoque
    {
        public int MECID { get; set; }
        public int MATID { get; set; }
        public string MECQUANTIDADE { get; set; }

    }
}
