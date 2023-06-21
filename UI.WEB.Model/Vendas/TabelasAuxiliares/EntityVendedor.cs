using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WEB.Model.Outros;

namespace UI.WEB.WorkFlow.Vendas.TabelasAuxiliares
{
    public class EntityVendedor
    {
        public int VNDID { get; set; }
        public int PESID { get; set; }
        public string VNDSTATUS { get; set; }
        public string VNDNASCIMENTO { get; set; }
        public string VNDSEQUENCIAL { get; set; }
        public EntityTelefone TbTelefone { get; set; }
        public EntityEmail TbEmail { get; set; }
        public EntityEndereco TbEndereco { get; set; }
        public EntityPessoa TbPessoa { get; set; }

        public EntityVendedor()
        {
            VNDSTATUS = "";
            VNDNASCIMENTO = "";
            VNDSEQUENCIAL = "";
            TbTelefone = new EntityTelefone();
            TbEmail = new EntityEmail();
            TbEndereco = new EntityEndereco();
            TbPessoa = new EntityPessoa();
        }

    }
}
