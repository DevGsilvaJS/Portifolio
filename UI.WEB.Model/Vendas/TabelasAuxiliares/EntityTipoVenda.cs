using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WEB.Model.Vendas.TabelasAuxiliares
{
    [Table("TB_TPV_TIPOVENDA")]
    public class EntityTipoVenda
    {
        public int TPVID { get; set; }
        public string TPVDESCRICAO { get; set; }
        public string TPVDEFAULTVENDA { get; set; }
    }
}
