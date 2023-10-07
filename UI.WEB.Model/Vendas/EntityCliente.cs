using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WEB.Model.Outros;

namespace UI.WEB.WorkFlow.Vendas
{
    [Table("TB_CLI_CLIENTE")]
    public class EntityCliente
    {
        public int CLIID { get; set; }
        public int PESID { get; set; }
        public string CLISEQUENCIAL { get; set; }
        public string CLISEXO { get; set; }
        public string CLISTATUS { get; set; }
        public string CLIESTADOCIVIL { get; set; }
        public string CLISALARIO { get; set; }
        public string CLINASCIMENTO { get; set; }
        public EntityPessoa TbPessoa { get; set; }
        public EntityEndereco TbEndereco { get; set; }
        public EntityEmail TbEmail { get; set; }
        public EntityTelefone TbTelefone { get; set; }

        public EntityCliente()
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
