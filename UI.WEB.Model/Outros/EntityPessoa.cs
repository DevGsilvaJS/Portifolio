namespace UI.WEB.Model.Outros
{
    public class EntityPessoa
    {
        public int    PESID { get; set; }
        public string PESNOME { get; set; }
        public string PESSOBRENOME { get; set; }
        public string PESTIPO { get; set; }
        public string PESDOCESTADUAL { get; set; }
        public string PESDOCFEDERAL { get; set; }
        public string PESDTCADASTRO { get; set; }


        public EntityPessoa()
        {
            PESNOME = "";
            PESSOBRENOME = "";
            PESTIPO = "";
            PESDOCESTADUAL = "";
            PESDOCFEDERAL = "";
            PESDTCADASTRO = "";
        }
    }

    
}
