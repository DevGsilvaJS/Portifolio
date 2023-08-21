using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WEB.WorkFlow.Vendas.TabelasAuxiliares
{

    [Table("TB_IND_INDICACAO")]
    public class EntityIndicacao
    {
        public int INDID { get; set; }
        public string INDDESCRICAO { get; set; }
        public string INDDEFAULTVENDA { get; set; }
        public string INDSTATUS { get; set; }
    }
}
