﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.WEB.Model.Outros;
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


            if (_Cliente.CLIID > 0)
            {
                AtualizarCliente(_Cliente);
            }

            else
            {
                AddListaSalvar(_Cliente.TbPessoa);

                _Cliente.PESID = _Cliente.TbPessoa.PESID;
                _Cliente.CLISTATUS = "1";


                if (_Cliente.CLISALARIO != null)
                {
                    _Cliente.CLISALARIO = _Cliente.CLISALARIO.Replace(',', '.');
                }

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
            }

            ExecuteTransacao();

            db.FechaConexao(db.MinhaConexao());



            return sRetorno;
        }
        public string AtualizarCliente(EntityCliente _Cliente)
        {
            string sRetorno = "";


            AddListaAtualizar(_Cliente.TbPessoa);
            _Cliente.CLISALARIO = _Cliente.CLISALARIO.Replace(',', '.');
            _Cliente.PESID = _Cliente.TbPessoa.PESID;
            AddListaAtualizar(_Cliente);

            if (_Cliente.TbEmail.EMLID > 0)
            {
                _Cliente.TbEmail.PESID = _Cliente.TbPessoa.PESID;
                AddListaAtualizar(_Cliente.TbEmail);
            }
            else if (!string.IsNullOrEmpty(_Cliente.TbEmail.EMLEMAIL) && !(_Cliente.TbEmail.EMLID > 0))
            {
                _Cliente.TbEmail.PESID = _Cliente.TbPessoa.PESID;
                AddListaSalvar(_Cliente.TbEmail);
            }

            if (_Cliente.TbTelefone.TELID > 0)
            {
                _Cliente.TbTelefone.PESID = _Cliente.TbPessoa.PESID;
                AddListaAtualizar(_Cliente.TbTelefone);
            }
            else if (!string.IsNullOrEmpty(_Cliente.TbTelefone.TELNUMERO) || (!string.IsNullOrEmpty(_Cliente.TbTelefone.TELCELULAR)) && !(_Cliente.TbTelefone.TELID > 0))
            {
                _Cliente.TbTelefone.PESID = _Cliente.TbPessoa.PESID;
                AddListaSalvar(_Cliente.TbTelefone);
            }

            if (_Cliente.TbEndereco.EDNID > 0)
            {
                _Cliente.TbEndereco.PESID = _Cliente.TbPessoa.PESID;
                AddListaAtualizar(_Cliente.TbEndereco);
            }
            else if (!string.IsNullOrEmpty(_Cliente.TbEndereco.EDNLOGRADOURO) && !(_Cliente.TbEndereco.EDNID > 0))
            {
                _Cliente.TbEndereco.PESID = _Cliente.TbPessoa.PESID;
                AddListaSalvar(_Cliente.TbEndereco);
            }

            return sRetorno;
        }
        public EntityCliente GetClienteByID(int pesid)
        {

            EntityCliente _Cliente = new EntityCliente();
            ClienteQuery Query = new ClienteQuery();

            try
            {

                SqlCommand _Comando = new SqlCommand(Query.EditarClienteQuery(), db.MinhaConexao());

                SqlParameter parameter = new SqlParameter("@PESID", pesid);
                _Comando.Parameters.Add(parameter);
                _Comando.CommandType = CommandType.Text;

                SqlDataReader dr = _Comando.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())

                    {
                        _Cliente.TbEmail.EMLID = int.Parse(dr["EMLID"].ToString());
                        _Cliente.TbEndereco.EDNID = int.Parse(dr["EDNID"].ToString());
                        _Cliente.TbTelefone.TELID = int.Parse(dr["TELID"].ToString());
                        _Cliente.CLIID = int.Parse(dr["CLIID"].ToString());
                        _Cliente.CLISALARIO = dr["CLISALARIO"].ToString();
                        _Cliente.CLIESTADOCIVIL = dr["CLIESTADOCIVIL"].ToString();
                        _Cliente.CLISEXO = dr["CLISEXO"].ToString();
                        _Cliente.CLISTATUS = dr["CLISTATUS"].ToString();
                        _Cliente.CLISEQUENCIAL = dr["CLISEQUENCIAL"].ToString();
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

                        _Cliente.TbPessoa.PESID = int.Parse(dr["PESID"].ToString());
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
        public string ExcluirCliente(int pesid)
        {

            string sRetorno = "";
            EntityCliente TB_CLI_CLIENTE = RetornaObjeto<EntityCliente>("TB_CLI_CLIENTE", pesid, "PESID");
            EntityPessoa TB_PES_PESSOA = RetornaObjeto<EntityPessoa>("TB_PES_PESSOA", TB_CLI_CLIENTE.PESID);
            EntityEndereco TB_EDN_ENDERECO = RetornaObjeto<EntityEndereco>("TB_EDN_ENDERECO", TB_CLI_CLIENTE.PESID, "PESID");
            EntityEmail TB_EML_EMAIL = RetornaObjeto<EntityEmail>("TB_EML_EMAIL", TB_CLI_CLIENTE.PESID, "PESID");
            EntityTelefone TB_TEL_TELEFONE = RetornaObjeto<EntityTelefone>("TB_TEL_TELEFONE", TB_CLI_CLIENTE.PESID, "PESID");


            if (TB_EDN_ENDERECO != null)
            {
                string sQueryEndereco = RetornaQueryDelete("TB_EDN_ENDERECO", "PESID", pesid);
                AddListaDeletar(sQueryEndereco);
            }

            if (TB_EML_EMAIL != null)
            {
                string sQueryEmail = RetornaQueryDelete("TB_EML_EMAIL", "PESID", pesid);
                AddListaDeletar(sQueryEmail);
            }

            if (TB_TEL_TELEFONE != null)
            {
                string sQueryTelefone = RetornaQueryDelete("TB_TEL_TELEFONE", "PESID", pesid);
                AddListaDeletar(sQueryTelefone);
            }

            string sQueryCliente = RetornaQueryDelete("TB_CLI_CLIENTE", "PESID", pesid);
            string sQueryPessoa = RetornaQueryDelete("TB_PES_PESSOA", "PESID", pesid);

            AddListaDeletar(sQueryCliente);
            AddListaDeletar(sQueryPessoa);

            ExecuteTransacao();

            db.FechaConexao(db.MinhaConexao());

            return sRetorno;
        }
    }
}
