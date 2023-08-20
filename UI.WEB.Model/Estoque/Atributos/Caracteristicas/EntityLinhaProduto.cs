using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WEB.Model.Estoque.Atributos.Caracteristicas
{
    [Table("TB_ARL_ATRLINHAPROD")] // Especifica o nome da tabela
    public class EntityLinhaProduto
    {
        public int ARLID { get; set; }
        public string ARLDESCRICAO { get; set; }
        public string ARLSTATUS { get; set; }
    }
}
