using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WEB.Query.Venda;
using UI.WEB.WorkFlow.Outros;

namespace UI.WEB.WorkFlow.Vendas
{
    public class ClienteWorkFlow : BaseWeb
    {
        DBComando db = new DBComando();
        public EntityCliente RetornaObjInclusao()
        {
            EntityCliente obj = new EntityCliente();

            return obj;
        }
        public string RetornaSequencial()
        {
            string retorno = "";
            string query = "SELECT PRV.PRVVALOR FROM TB_PRV_PARAMETROVALOR PRV WHERE PRV.PRVCAMPO = 'CLIENTE'";

            SqlCommand Comando = new SqlCommand(query, db.MinhaConexao());
            Comando.CommandType = CommandType.Text;
            SqlDataReader dr = Comando.ExecuteReader();

            while (dr.Read())
            {
                retorno = dr["PRVVALOR"].ToString();
            }

            db.FechaConexao(db.MinhaConexao());
            return retorno;
        }
        public string GravarCLiente(EntityCliente _Cliente)
        {
            string sRetorno = "";


            AddListaSalvar(_Cliente.TbPessoa);

            _Cliente.PESID = _Cliente.TbPessoa.PESID;
            _Cliente.CLISTATUS = "1";
            _Cliente.CLISALARIO = _Cliente.CLISALARIO.Replace(',', '.');

            AddListaSalvar(_Cliente);

            string sEndereco = RetornaObjeto("TB_EDN_ENDERECO", "PESID", _Cliente.TbPessoa.PESID);

            if (!string.IsNullOrEmpty(sEndereco))
            {
                AddListaAtualizar(_Cliente.TbEndereco);
            }
            else
            {
                if (!string.IsNullOrEmpty(_Cliente.TbEndereco.EDNLOGRADOURO))
                {
                    _Cliente.TbEndereco.PESID = _Cliente.PESID;
                    AddListaSalvar(_Cliente.TbEndereco);
                }
            }

            string sTelefone = RetornaObjeto("TB_TEL_TELEFONE", "PESID", _Cliente.TbPessoa.PESID);

            if (!string.IsNullOrEmpty(sTelefone))
            {
                AddListaAtualizar(_Cliente.TbTelefone);
            }
            else
            {
                if (!string.IsNullOrEmpty(_Cliente.TbTelefone.TELNUMERO) || !string.IsNullOrEmpty(_Cliente.TbTelefone.TELCELULAR))
                {
                    _Cliente.TbTelefone.PESID = _Cliente.PESID;
                    AddListaSalvar(_Cliente.TbTelefone);
                }
            }

            string sEmail = RetornaObjeto("TB_EML_EMAIL", "PESID", _Cliente.TbPessoa.PESID);

            if (!string.IsNullOrEmpty(sEmail))
            {
                AddListaAtualizar(_Cliente.TbEmail);
            }
            else
            {
                if (!string.IsNullOrEmpty(_Cliente.TbEmail.EMLEMAIL))
                {
                    _Cliente.TbEmail.PESID = _Cliente.PESID;
                    AddListaSalvar(_Cliente.TbEmail);
                }
            }

            ExecuteTransacao();

            db.FechaConexao(db.MinhaConexao());



            return sRetorno;
        }
        public EntityCliente GetClienteByID(int cliid)
        {

            EntityCliente _Cliente = new EntityCliente();
            ClienteQuery Query = new ClienteQuery();

            try
            {               

                SqlCommand _Comando = new SqlCommand(Query.EditarClienteQuery(), db.MinhaConexao());

                SqlParameter parameter = new SqlParameter("@CLIID", cliid);
                _Comando.Parameters.Add(parameter);
                _Comando.CommandType = CommandType.Text;   

                SqlDataReader dr = _Comando.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())

                    {

                        _Cliente.CLIID = int.Parse(dr["CLIID"].ToString());
                        _Cliente.CLISALARIO = dr["CLISALARIO"].ToString();
                        _Cliente.CLIESTADOCIVIL = dr["CLIESTADOCIVIL"].ToString();
                        _Cliente.CLISEXO = dr["CLISEXO"].ToString();
                        _Cliente.CLISTATUS = dr["CLISTATUS"].ToString();
                        _Cliente.TbPessoa.PESID = int.Parse(dr["PESID"].ToString());
                        _Cliente.TbPessoa.PESNOME = dr["PESNOME"].ToString();
                        _Cliente.TbPessoa.PESSOBRENOME = dr["PESSOBRENOME"].ToString();
                        _Cliente.TbPessoa.PESDOCESTADUAL = dr["PESDOCESTADUAL"].ToString();
                        _Cliente.TbPessoa.PESDOCFEDERAL = dr["PESDOCFEDERAL"].ToString();
                        _Cliente.TbEmail.EMLEMAIL = dr["EMLEMAIL"].ToString();
                        _Cliente.TbTelefone.TELDDD = dr["TELDDD"].ToString();
                        _Cliente.TbTelefone.TELNUMERO = dr["TELNUMERO"].ToString();
                        _Cliente.TbTelefone.TELDDDC = dr["TELDDDC"].ToString();
                        _Cliente.TbTelefone.TELCELULAR = dr["TELCELULAR"].ToString();
                        _Cliente.TbEndereco.EDNCEP = dr["EDNCEP"].ToString();
                        _Cliente.TbEndereco.EDNBAIRRO = dr["EDNBAIRRO"].ToString();
                        _Cliente.TbEndereco.EDNCIDADE = dr["EDNCIDADE"].ToString();
                        _Cliente.TbEndereco.EDNLOGRADOURO = dr["EDNLOGRADOURO"].ToString();
                        _Cliente.TbEndereco.EDNNUMERO = dr["EDNNUMERO"].ToString();
                        _Cliente.TbEndereco.EDNUF = dr["EDNUF"].ToString();

                    }
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            return _Cliente;
        }
        public List<EntityCliente> ListaDados()
        {

            List<EntityCliente> lsClientes = new List<EntityCliente>();
            ClienteQuery Query = new ClienteQuery();

            try
            {


                SqlCommand _Comando = new SqlCommand(Query.ListaDadosClienteQuery(), db.MinhaConexao());

                //Tipo do comando, texto
                _Comando.CommandType = CommandType.Text;

                SqlDataReader dr = _Comando.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        EntityCliente _Cliente = new EntityCliente();

                        _Cliente.CLIID = int.Parse(dr["CLIID"].ToString());
                        _Cliente.CLISEQUENCIAL = dr["CLISEQUENCIAL"].ToString();
                        _Cliente.TbPessoa.PESNOME = dr["PESNOME"].ToString();
                        _Cliente.TbPessoa.PESDOCESTADUAL = dr["PESDOCESTADUAL"].ToString();
                        _Cliente.TbPessoa.PESDOCFEDERAL = dr["PESDOCFEDERAL"].ToString();
                        _Cliente.CLISEXO = dr["CLISEXO"].ToString();                       

                        lsClientes.Add(_Cliente);

                    }
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            return lsClientes;
        }
    }
}
