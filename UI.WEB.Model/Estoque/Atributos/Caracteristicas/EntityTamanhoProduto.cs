using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WEB.Model.Estoque.Atributos.Caracteristicas
{

    [Table("TB_ATO_ATRTAMANHO")]
    public class EntityTamanhoProduto
    {
        public int ATOID { get; set; }
        public string ATODESCRICAO { get; set; }
        public string ATOSTATUS { get; set; }
    }
}
