using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WEB.Model.Estoque.Atributos.Caracteristicas
{


    [Table("TB_AS2_ATRSUBLINHA2")]
    public class EntitySublinha2Produto
    {
        public int AS2ID { get; set; }
        public string AS2DESCRICAO { get; set; }
        public string AS2STATUS { get; set; }
    }
}
