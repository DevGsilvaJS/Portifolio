using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WEB.Query.Utilitarios
{
    public class UsuarioQuery
    {
        public string ListaDadosQuery()
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendLine("	SELECT                                                       ");
            sb.AppendLine("	    USU.USUID,                                               ");
            sb.AppendLine("	    PES.PESID,                                               ");
            sb.AppendLine("	    TEL.TELID,                                               ");
            sb.AppendLine("	    EDN.EDNID,                                               ");
            sb.AppendLine("	    EML.EMLID,                                               ");
            sb.AppendLine("	    USU.USUSEQUENCIAL,                                       ");
            sb.AppendLine("	    USU.USUSTATUS,                                           ");
            sb.AppendLine("	    USU.USUSENHA,                                            ");
            sb.AppendLine("	    PES.PESNOME,                                             ");
            sb.AppendLine("	    PES.PESSOBRENOME,                                        ");
            sb.AppendLine("	    EML.EMLEMAIL,                                            ");
            sb.AppendLine("	    EDN.EDNCEP,                                              ");
            sb.AppendLine("	    EDN.EDNUF,                                               ");
            sb.AppendLine("	    EDN.EDNCIDADE,                                           ");
            sb.AppendLine("	    EDN.EDNLOGRADOURO,                                       ");
            sb.AppendLine("	    EDN.EDNBAIRRO,                                           ");
            sb.AppendLine("	    EDN.EDNNUMERO,                                           ");
            sb.AppendLine("	    EDN.EDNCOMPLEMENTO,                                      ");
            sb.AppendLine("	    TEL.TELNUMERO,                                           ");
            sb.AppendLine("	    TEL.TELDDD,                                              ");
            sb.AppendLine("	    TEL.TELCELULAR,                                          ");
            sb.AppendLine("	    TEL.TELDDDC                                              ");
            sb.AppendLine(" FROM TB_USU_USUARIO USU                                      ");
            sb.AppendLine("     INNER JOIN TB_PES_PESSOA PES ON PES.PESID = USU.PESID    ");
            sb.AppendLine("     INNER JOIN TB_EML_EMAIL EML ON EML.PESID = PES.PESID     ");
            sb.AppendLine("     LEFT JOIN TB_EDN_ENDERECO EDN ON EDN.PESID = PES.PESID   ");
            sb.AppendLine("     LEFT JOIN TB_TEL_TELEFONE TEL ON TEL.PESID = PES.PESID   ");

            return sb.ToString();

        }

        public string GetUsuarioByIDQuery()
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendLine("  SELECT                                                                                    ");
            sb.AppendLine("    USU.USUID,                                                                              ");
            sb.AppendLine("    PES.PESID,                                                                              ");
            sb.AppendLine("    COALESCE(TEL.TELID, 0) AS TELID,                                                        ");
            sb.AppendLine("    COALESCE(EDN.EDNID, 0) AS EDNID,                                                        ");
            sb.AppendLine("    COALESCE(EML.EMLID, 0) AS EMLID,                                                        ");
            sb.AppendLine("    USU.USUSEQUENCIAL,                                                                      ");
            sb.AppendLine("    USU.USUSTATUS,                                                                          ");
            sb.AppendLine("    USU.USUSENHA,                                                                           ");
            sb.AppendLine("    PES.PESNOME,                                                                            ");
            sb.AppendLine("    PES.PESSOBRENOME,                                                                       ");
            sb.AppendLine("    COALESCE(CAST(EML.EMLEMAIL AS VARCHAR(255)), '0') AS EMLEMAIL,                          ");
            sb.AppendLine("    COALESCE(CAST(EDN.EDNCEP AS VARCHAR(255)), '0') AS EDNCEP,                              ");
            sb.AppendLine("    COALESCE(CAST(EDN.EDNUF AS VARCHAR(255)), '0') AS EDNUF,                                ");
            sb.AppendLine("    COALESCE(CAST(EDN.EDNCIDADE AS VARCHAR(255)), '0') AS EDNCIDADE,                        ");
            sb.AppendLine("    COALESCE(CAST(EDN.EDNLOGRADOURO AS VARCHAR(255)), '0') AS EDNLOGRADOURO,                ");
            sb.AppendLine("    COALESCE(CAST(EDN.EDNBAIRRO AS VARCHAR(255)), '0') AS EDNBAIRRO,                        ");
            sb.AppendLine("    COALESCE(CAST(EDN.EDNNUMERO AS VARCHAR(255)), '0') AS EDNNUMERO,                        ");
            sb.AppendLine("    COALESCE(CAST(EDN.EDNCOMPLEMENTO AS VARCHAR(255)), '0') AS EDNCOMPLEMENTO,              ");
            sb.AppendLine("    COALESCE(CAST(TEL.TELNUMERO AS VARCHAR(255)), '0') AS TELNUMERO,                        ");
            sb.AppendLine("    COALESCE(CAST(TEL.TELDDD AS VARCHAR(255)), '0') AS TELDDD,                              ");
            sb.AppendLine("    COALESCE(CAST(TEL.TELCELULAR AS VARCHAR(255)), '0') AS TELCELULAR,                      ");
            sb.AppendLine("    COALESCE(CAST(TEL.TELDDDC AS VARCHAR(255)), '0') AS TELDDDC                             ");
            sb.AppendLine("  FROM TB_USU_USUARIO USU                                                                   ");
            sb.AppendLine("  INNER JOIN TB_PES_PESSOA PES ON PES.PESID = USU.PESID                                     ");
            sb.AppendLine("  LEFT JOIN TB_EML_EMAIL EML ON EML.PESID = PES.PESID                                       ");
            sb.AppendLine("  LEFT JOIN TB_EDN_ENDERECO EDN ON EDN.PESID = PES.PESID                                    ");
            sb.AppendLine("  LEFT JOIN TB_TEL_TELEFONE TEL ON TEL.PESID = PES.PESID                                    ");
            sb.AppendLine("     WHERE USU.PESID = @PESID;                                                              ");


            return sb.ToString();

        }
    }
}
