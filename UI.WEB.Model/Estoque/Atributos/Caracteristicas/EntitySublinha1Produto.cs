using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WEB.Model.Estoque.Atributos.Caracteristicas
{

    [Table("TB_AS1_ATRSUBLINHA1")]
    public class EntitySublinha1Produto
    {
        public int AS1ID { get; set; }
        public string AS1DESCRICAO { get; set; }
        public string AS1STATUS { get; set; }
    }
}
