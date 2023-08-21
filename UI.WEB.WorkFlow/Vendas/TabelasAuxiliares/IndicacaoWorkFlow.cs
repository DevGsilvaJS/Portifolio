using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WEB.Query.Venda.TabelasAuxiliares;
using UI.WEB.WorkFlow.Outros;

namespace UI.WEB.WorkFlow.Vendas.TabelasAuxiliares
{
    public class IndicacaoWorkFlow : BaseWeb
    {
        DBComando db = new DBComando();
        public EntityIndicacao RetornaObjInclusao()
        {
            EntityIndicacao objInclusao = new EntityIndicacao();
            return objInclusao;
        }
        public string GravarIndicacao(EntityIndicacao objIndicacao)
        {
            string sRetorno = "NOTOK";

            if (objIndicacao.INDID > 0)
            {
                AddListaAtualizar(objIndicacao);
            }

            else
            {
                AddListaSalvar(objIndicacao);
            }

            sRetorno = ExecuteTransacao();
            return sRetorno;
        }
        public string ExcluirIndicacao(int idnid)
        {
            string sRetorno = "NOTOK";

            string sIndicacao = RetornaObjeto("TB_IND_INDICACAO", "INDID", idnid);

            if (!string.IsNullOrEmpty(sIndicacao))
            {
                AddListaDeletar(RetornaQueryDelete("TB_IND_INDIDICACAO", "INDID", idnid));
                sRetorno = ExecuteTransacao();
            }

            else
            {
                return sRetorno;
            }


            return sRetorno;
        }
        public EntityIndicacao GetIndicacaoID (int indid)
        {

            IndicacaoQuery Query = new IndicacaoQuery();
            EntityIndicacao _Indicacao = new EntityIndicacao();

            SqlCommand _Comando = new SqlCommand(Query.EditarIndicacaoQuery(), db.MinhaConexao());

            SqlParameter parameter = new SqlParameter("@INDID", indid);
            _Comando.Parameters.Add(parameter);
            _Comando.CommandType = CommandType.Text;
            SqlDataReader dr = _Comando.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    _Indicacao.INDID = int.Parse(dr["INDID"].ToString());
                    _Indicacao.INDDESCRICAO = dr["INDDESCRICAO"].ToString();
                    _Indicacao.INDDEFAULTVENDA = dr["INDDEFAULTVENDA"].ToString();
                    _Indicacao.INDSTATUS = dr["INDSTATUS"].ToString();
                }
            }
            return _Indicacao;
        }
        public List<EntityIndicacao> ListaDados()
        {

            List<EntityIndicacao> lista = new List<EntityIndicacao>();

            IndicacaoQuery Query = new IndicacaoQuery();

            SqlDataReader dr = ListarDadosEntity(Query.ListaDadosQuery());

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    EntityIndicacao _Indicacao = new EntityIndicacao();

                    _Indicacao.INDID = int.Parse(dr["INDID"].ToString());
                    _Indicacao.INDDESCRICAO = dr["INDDESCRICAO"].ToString();
                    _Indicacao.INDDEFAULTVENDA = dr["INDDEFAULTVENDA"].ToString();
                    _Indicacao.INDSTATUS = dr["INDSTATUS"].ToString();

                    lista.Add(_Indicacao);
                }
            }

            return lista;

        }
    }
}
