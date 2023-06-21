using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WEB.Model.Outros;

namespace UI.WEB.WorkFlow.Vendas
{
    public class EntitiesCliente
    {
        public int CLIID { get; set; }
        public int PESID { get; set; }
        public string CLISEXO { get; set; }
        public string CLISTATUS { get; set; }
        public string CLIESTADOCIVIL { get; set; }
        public string CLISALARIO { get; set; }
        public EntityPessoa TbPessoa { get; set; }
        public EntityEndereco TbEndereco { get; set; }
        public EntityEmail TbEmail { get; set; }
        public EntityTelefone TbTelefone { get; set; }

        public EntitiesCliente()
        {
            CLISEXO = "";
            CLISTATUS = "";
            CLIESTADOCIVIL = "";
            CLISALARIO = "";
            CLISALARIO = "";
            TbPessoa = new EntityPessoa();
            TbEndereco = new EntityEndereco();
            TbEmail = new EntityEmail();
            TbTelefone = new EntityTelefone();
        }

    }
}
