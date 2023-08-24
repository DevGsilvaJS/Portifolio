using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WEB.Model.Outros;

namespace UI.WEB.WorkFlow.Vendas.TabelasAuxiliares
{

    [Table("TB_CON_CONVENIO")]
    public class EntityConvenio
    {
        public int CONID { get; set; }
        public string CONEMPRESA { get; set; }
        public string CONNUMCONTRATO { get; set; }
        public string CONPERCDESCONTO { get; set; }
        public string CONOBSERVACOES { get; set; }
        public EntityTelefone TbTelefone { get; set; }
        public EntityEndereco TbEndereco { get; set; }
        public EntityConvenio()
        {
            TbTelefone = new EntityTelefone();
            TbEndereco = new EntityEndereco();
        }
    }
}
