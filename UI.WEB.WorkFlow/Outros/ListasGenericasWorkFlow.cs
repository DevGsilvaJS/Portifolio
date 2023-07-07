using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WEB.Model.Financeiro.Tabelas_Auxiliares;

namespace UI.WEB.WorkFlow.Outros
{
    public class ListasGenericasWorkFlow : BaseWeb
    {
        public List<EntityCentroCusto> listaCentroCusto()
        {
            List<EntityCentroCusto> lista = new List<EntityCentroCusto>();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT");
            sb.AppendLine("    CCUID");
            sb.AppendLine("  , CCUDESCRICAO");
            sb.AppendLine("FROM TB_CCU_CENTROCUSTO CCU");
            sb.AppendLine("    WHERE CCU.CCUSTATUS = 1");

            SqlDataReader dr = ListarDadosEntity(sb.ToString());

            while (dr.Read())
            {
                lista.Add(new EntityCentroCusto
                {
                    CCUID = int.Parse(dr["CCUID"].ToString()),
                    CCUDESCRICAO = dr["CCUDESCRICAO"].ToString()
                });
            }
            return lista;
        }

        public List<EntityPlanoContas> listaPlanoContas()
        {
            List<EntityPlanoContas> lista = new List<EntityPlanoContas>();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT");
            sb.AppendLine("    PCTID");
            sb.AppendLine("  , PCTDESCRICAO");
            sb.AppendLine("FROM TB_PCT_PLANOCONTAS PCT");
            sb.AppendLine("    WHERE PCT.PCTSTATUS = 1");

            SqlDataReader dr = ListarDadosEntity(sb.ToString());

            while (dr.Read())
            {
                lista.Add(new EntityPlanoContas
                {
                    PCTID = int.Parse(dr["PCTID"].ToString()),
                    PCTDESCRICAO = dr["PCTDESCRICAO"].ToString()
                });
            }
            return lista;
        }
    }
}
