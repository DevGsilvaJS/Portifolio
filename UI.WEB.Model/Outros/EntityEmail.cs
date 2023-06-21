using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WEB.Model.Outros
{
    public class EntityEmail
    {
        public int EMLID { get; set; }
        public int PESID { get; set; }
        public string EMLEMAIL { get; set; }

        public EntityEmail()
        {
            EMLEMAIL = "";
        }
    }
}
