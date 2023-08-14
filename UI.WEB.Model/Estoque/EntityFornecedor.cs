using UI.WEB.Model.Financeiro.Tabelas_Auxiliares;
using UI.WEB.Model.Outros;

namespace UI.WEB.Model.Estoque
{
    public class EntityFornecedor
    {
        public int FORID { get; set; }
        public int PESID { get; set; }
        public int CCUID { get; set; }
        public int PCTID { get; set; }
        public string FORSEQUENCIAL { get; set; }
        public string FOROBSERVACAO { get; set; }
        public string FORSITE { get; set; }
        public string FORSTATUS { get; set; }
        public EntityPessoa TbPessoa { get; set; }
        public EntityEndereco TbEndereco { get; set; }
        public EntityEmail TbEmail { get; set; }
        public EntityTelefone TbTelefone { get; set; }
        public EntityPlanoContas TbPlanoContas { get; set; }
        public EntityCentroCusto TbCentroCusto { get; set; }

        public EntityFornecedor()
        {
            TbPessoa = new EntityPessoa();
            TbEndereco = new EntityEndereco();
            TbEmail = new EntityEmail();
            TbTelefone = new EntityTelefone();
            TbPlanoContas = new EntityPlanoContas();
            TbCentroCusto = new EntityCentroCusto();
        }
    }
}
