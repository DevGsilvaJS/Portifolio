using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WEB.WorkFlow.Outros
{
    public class ListasGenericasWorkFlow
    {
        public string listaCentroCusto()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT");
            sb.AppendLine("    CCUID");
            sb.AppendLine("  , CCUDESCRICAO");
            sb.AppendLine("FROM TB_CCU_CENTROCUSTO CCU");
            sb.AppendLine("    WHERE CCU.CCUSTATUS = 1");

            return "";
        }
    }
}
