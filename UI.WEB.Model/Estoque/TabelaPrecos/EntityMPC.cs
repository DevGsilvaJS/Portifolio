using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WEB.Model.Estoque.TabelaPrecos
{

    [Table("TB_MPC_MATPRECOCUSTO")]
    public class EntityMPC
    {
        public int MPCID { get; set; }
        public int MATID { get; set; }
        public string MPCPRECOCUSTO { get; set; }
        public string MPCDTALTERACAO { get; set; }
    }
}
