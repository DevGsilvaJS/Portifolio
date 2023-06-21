using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WEB.Model.Outros
{
    public class EntityEndereco
    {
        public int EDNID { get; set; }
        public int PESID { get; set; }
        public string EDNCEP { get; set; }
        public string EDNUF { get; set; }
        public string EDNCIDADE { get; set; }
        public string EDNLOGRADOURO { get; set; }
        public string EDNBAIRRO { get; set; }
        public string EDNNUMERO { get; set; }
        public string EDNCOMPLEMENTO { get; set; }

        public EntityEndereco()
        {
            EDNCEP = "";
            EDNUF = "";
            EDNCIDADE = "";
            EDNLOGRADOURO = "";
            EDNBAIRRO = "";
            EDNNUMERO = "";
            EDNCOMPLEMENTO = "";
        }
    }
}
