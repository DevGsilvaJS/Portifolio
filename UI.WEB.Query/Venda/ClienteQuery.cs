using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.WEB.Query.Venda
{
    public class ClienteQuery
    {
        public string EditarClienteQuery()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(" SELECT                                                                  ");
            sb.AppendLine("	    CLI.CLIID,                                                          ");
            sb.AppendLine("	    PES.PESID,                                                          ");
            sb.AppendLine("	    COALESCE(CLI.CLISALARIO, 0) AS CLISALARIO,                          ");
            sb.AppendLine("	    COALESCE(CLI.CLISTATUS, 0) AS CLISTATUS,                            ");
            sb.AppendLine("	    COALESCE(CLI.CLISEXO, 0) AS CLISEXO,                                ");
            sb.AppendLine("	    COALESCE(CLI.CLIESTADOCIVIL, 0) AS CLIESTADOCIVIL,                  ");
            sb.AppendLine("	    PES.PESNOME,                                                        ");
            sb.AppendLine("	    PES.PESSOBRENOME,                                                   ");
            sb.AppendLine("	    PES.PESSOBRENOME,                                                   ");
            sb.AppendLine("	    PES.PESDOCESTADUAL,                                                 ");
            sb.AppendLine("	    PES.PESDOCESTADUAL,                                                 ");
            sb.AppendLine("	    PES.PESDOCFEDERAL,                                                  ");
            sb.AppendLine("	    EML.EMLEMAIL,                                                       ");
            sb.AppendLine("	    COALESCE(EDN.EDNCEP, 0) AS EDNCEP,                                  ");
            sb.AppendLine("	    COALESCE(EDN.EDNCIDADE, 0) AS EDNCIDADE,                            ");
            sb.AppendLine("	    COALESCE(EDN.EDNLOGRADOURO, 0) AS EDNLOGRADOURO,                    ");
            sb.AppendLine("	    COALESCE(EDN.EDNBAIRRO, 0) AS EDNBAIRRO,                            ");
            sb.AppendLine("	    COALESCE(EDN.EDNNUMERO, 0) AS EDNNUMERO,                            ");
            sb.AppendLine("	    COALESCE(EDN.EDNCOMPLEMENTO, 0) AS EDNCOMPLEMENTO,                  ");
            sb.AppendLine("	    COALESCE(TEL.TELDDD, 0) AS TELDDD,                                  ");
            sb.AppendLine("	    COALESCE(TEL.TELNUMERO, 0) AS TELNUMERO,                            ");
            sb.AppendLine("	    COALESCE(TEL.TELDDDC, 0) AS TELDDDC,                                ");
            sb.AppendLine("	    COALESCE(TEL.TELCELULAR, 0) AS TELCELULAR                           ");
            sb.AppendLine(" FROM TB_CLI_CLIENTE CLI                                                 ");
            sb.AppendLine(" JOIN TB_PES_PESSOA PES ON PES.PESID = CLI.PESID                         ");
            sb.AppendLine(" LEFT JOIN TB_EML_EMAIL EML ON EML.PESID = PES.PESID                     ");
            sb.AppendLine(" LEFT JOIN TB_EDN_ENDERECO EDN ON EDN.PESID = PES.PESID                  ");
            sb.AppendLine(" LEFT JOIN TB_TEL_TELEFONE TEL ON TEL.PESID = PES.PESID                  ");
            sb.AppendLine("     WHERE CLI.CLIID = @CLIID                                            ");


            return sb.ToString();
        }

        public string ListaDadosClienteQuery()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(" SELECT                                                      ");
            sb.AppendLine("     CLI.CLIID,                                              ");
            sb.AppendLine("	    CLI.CLISEQUENCIAL,                                      ");
            sb.AppendLine("	    CONCAT(PES.PESNOME, ' ', PES.PESSOBRENOME) AS PESNOME,  ");
            sb.AppendLine("	    PES.PESDOCESTADUAL,                                     ");
            sb.AppendLine("	    PES.PESDOCFEDERAL,                                      ");
            sb.AppendLine("	    CLI.CLISEXO                                             ");
            sb.AppendLine(" FROM TB_CLI_CLIENTE CLI                                     ");
            sb.AppendLine(" JOIN TB_PES_PESSOA PES ON PES.PESID = CLI.PESID             ");
            sb.AppendLine(" LEFT JOIN TB_EML_EMAIL EML ON EML.PESID = PES.PESID         ");
            sb.AppendLine(" LEFT JOIN TB_EDN_ENDERECO EDN ON EDN.PESID = PES.PESID      ");
            sb.AppendLine(" LEFT JOIN TB_TEL_TELEFONE TEL ON TEL.PESID = PES.PESID      ");                                                                                      

            return sb.ToString();
        }
    }
}
