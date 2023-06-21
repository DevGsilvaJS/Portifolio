using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WEB.Model.Outros
{
    public class EntityTelefone
    {
        public int TELID { get; set; }
        public int PESID { get; set; }
        public string TELNUMERO { get; set; }
        public string TELDDD { get; set; }
        public string TELCELULAR { get; set; }
        public string TELDDDC { get; set; }

        public EntityTelefone()
        {
            TELNUMERO = "";
            TELDDD = "";
            TELCELULAR = "";
            TELDDDC = "";
        }
    }
}
