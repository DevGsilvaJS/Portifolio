using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WEB.Model.Outros;

namespace UI.WEB.WorkFlow.Vendas.TabelasAuxiliares
{

    [Table("TB_CVN_CONVENIO")]
    public class EntityConvenio
    {
        public int CVNID { get; set; }
        public int PESID { get; set; }
        public string CVNCONTRATO { get; set; }
        public string CVNDESCONTO { get; set; }
        public string CVNOBSERVACAO { get; set; }
        public String CVNNAOAPARECEVENDA { get; set; }
        public EntityPessoa TbPessoa { get; set; }
        public EntityTelefone TbTelefone { get; set; }
        public EntityEndereco TbEndereco { get; set; }
        public EntityConvenio()
        {
            TbPessoa = new EntityPessoa();
            TbTelefone = new EntityTelefone();
            TbEndereco = new EntityEndereco();
        }
    }
}
