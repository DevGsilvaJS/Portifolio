using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WEB.Query.Estoque
{
    public class SaidaEstoqueQuery
    {
        public string listaCfopQuery()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("   SELECT                                                                             ");
            sb.AppendLine("     COP.COPID,                                                                       ");
            sb.AppendLine("     CAST(COP.COPCODIGO AS VARCHAR(10)) + ' - ' + COP.COPDESCRICAO AS COPDESCRICAO    ");
            sb.AppendLine("   FROM TB_COP_CFOP COP                                                               ");
            sb.AppendLine("     WHERE COP.COPTIPO = 'S' and COP.COPSTATUS = '1'                                  ");

            return sb.ToString();
        }

        public string buscaProdutoQuery()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("  SELECT                                                             ");
            sb.AppendLine("     MAT.MATID,                                                      ");
            sb.AppendLine("     MAT.MATSEQUENCIAL,                                              ");
            sb.AppendLine("     MAT.MATFANTASIA,                                                ");
            sb.AppendLine("     MEC.MECQUANTIDADE                                               ");
            sb.AppendLine("  FROM TB_MAT_MATERIAL MAT                                           ");
            sb.AppendLine("  JOIN TB_MEC_MATESTCONTROLE MEC ON MEC.MATID = MAT.MATID            ");
            sb.AppendLine("     WHERE MEC.MECQUANTIDADE > 0 AND MAT.MATCONTROLAEST = 1          ");

            return sb.ToString();
        }
    }
}
