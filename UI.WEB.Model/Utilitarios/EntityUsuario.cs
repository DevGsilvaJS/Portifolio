using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WEB.Model.Outros;

namespace UI.WEB.Model.Utilitarios
{
    public class EntityUsuario
    {
        public int USUID { get; set; }
        public int PESID { get; set; }
        public string USUSENHA { get; set; }
        public string USUSTATUS { get; set; }
        public EntityPessoa TbPessoa { get; set; }
        public EntityEmail TbEmail { get; set; }
        public EntityEndereco TbEndereco { get; set; }
        public EntityTelefone TbTelefone { get; set; }

        public EntityUsuario()
        {
            TbPessoa = new EntityPessoa();
            TbEmail = new EntityEmail();
            TbEndereco = new EntityEndereco();
            TbTelefone = new EntityTelefone();
            
        }
    }
}
