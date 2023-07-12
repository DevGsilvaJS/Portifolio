using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UI.WEB.WorkFlow.Vendas;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace UI.WEB.WorkFlow.Outros
{
    public class BaseWeb
    {
        DBComando _Conexao = new DBComando();
        SqlCommand _Comando = new SqlCommand();
        List<string> listaSalvar = new List<string>();
        public string RetornaQueryInclusao(object entity, string Tabela)
        {

            // Cria uma instância do StringBuilder para construir o script de inserção
            var insertScriptBuilder = new StringBuilder();
            string inicioPropriedade = Tabela.Substring(3, 3);

            // Adiciona o nome da tabela ao script
            insertScriptBuilder.Append($"INSERT INTO {Tabela} (");

            // Obtém as propriedades do objeto
            PropertyInfo[] propriedades = entity.GetType().GetProperties();

            // Adiciona o nome das colunas ao script
            for (int i = 0; i < propriedades.Length; i++)
            {
                if (propriedades[i].GetCustomAttribute<ColumnAttribute>() != null)
                {
                    string nomeColuna = propriedades[i].GetCustomAttribute<ColumnAttribute>().Name;
                }
                else
                {
                    if (propriedades[i].Name.Contains(inicioPropriedade) || propriedades[i].Name.Contains("ID"))
                    {
                        insertScriptBuilder.Append($"{propriedades[i].Name}, ");
                    }
                }
            }

            // Remove a vírgula extra no final da lista de colunas
            insertScriptBuilder.Length -= 2;

            // Adiciona os valores ao script
            insertScriptBuilder.Append(") VALUES (");
            for (int i = 0; i < propriedades.Length; i++)
            {
                object value = propriedades[i].GetValue(entity);

                if (value == null)
                {
                    string formattedValue = FormatValue(value);
                    insertScriptBuilder.Append($"{formattedValue}, ");
                }

                else if (value.GetType() == typeof(string) || value.GetType() == typeof(int) || value == null)
                {
                    string formattedValue = FormatValue(value);
                    insertScriptBuilder.Append($"{formattedValue}, ");
                }
            }

            // Remove a vírgula extra no final da lista de valores e adiciona o ponto-e-vírgula
            insertScriptBuilder.Length -= 2;
            insertScriptBuilder.Append(")" + " SELECT SCOPE_IDENTITY()");

            // Retorna o script de inserção como uma string
            return insertScriptBuilder.ToString();
        }
        public string RetornaQueryUpdate(object obj, string tableName)
        {
            StringBuilder updateCommand = new StringBuilder();
            updateCommand.Append("UPDATE ");
            updateCommand.Append(tableName);
            updateCommand.Append(" SET ");
            string where = "";

            Type objectType = obj.GetType();
            PropertyInfo[] properties = objectType.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name;
                object propertyValue = property.GetValue(obj);

                if (IsPropertyClass(property))
                {
                    continue;
                }
                if (updateCommand.Length >= 2 && propertyName.ToString().EndsWith("ID"))
                {
                    where = " WHERE " + propertyName + " = " + propertyValue;
                }

                else
                {
                    updateCommand.Append(propertyName);
                    updateCommand.Append(" = ");

                    if (propertyValue is string || propertyValue is DateTime)
                    {
                        updateCommand.Append("'");
                        updateCommand.Append(propertyValue);
                        updateCommand.Append("'");
                        updateCommand.Append(",");

                    }
                    else
                    {

                        if (propertyValue == null)
                        {
                            string SETNULL = "NULL";
                            updateCommand.Append(SETNULL);
                            updateCommand.Append(",");
                        }
                        else
                        {
                            updateCommand.Append(propertyValue);
                            updateCommand.Append(",");
                        }
                    }
                }
            }

            if (updateCommand.Length > 0 && updateCommand[updateCommand.Length - 1] == ',')
            {
                updateCommand.Length--;  // Remove o último caractere
            }

            updateCommand.Append(where);



            return updateCommand.ToString();
        }
        public string RetornaQueryDelete(string tabela, string coluna, int id)
        {

            string idFormatado = tabela.Substring(3, 3);

            string deleteScript = $"DELETE FROM {tabela} WHERE " + coluna + " = " + id;
            return deleteScript;
        }
        public bool IsPropertyClass(PropertyInfo property)
        {
            Type propertyType = property.PropertyType;
            return propertyType.IsClass && propertyType != typeof(string);
        }
        private static string FormatValue(object value)
        {
            if (value == null)
            {
                return "NULL";
            }
            else if (value is string || value is DateTime)
            {
                return $"'{value.ToString()}'";
            }
            else if (value is bool)
            {
                return ((bool)value) ? "1" : "0";
            }
            else
            {
                return value.ToString();
            }

        }
        public SqlDataReader ListarDadosEntity(string Query)
        {
            try
            {
                SqlCommand _Comando = new SqlCommand(Query, _Conexao.MinhaConexao());
                _Comando.CommandType = CommandType.Text;
                SqlDataReader _DataReader = _Comando.ExecuteReader();

                _Conexao.FechaConexao(_Conexao.MinhaConexao());

                return _DataReader;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public int RetornaSequencial(string SEQ)
        {
            EntitiesCliente _usu = new EntitiesCliente();

            SqlCommand command = new SqlCommand($"SELECT NEXT VALUE FOR {SEQ}", _Conexao.MinhaConexao());

            object nextValue = command.ExecuteScalar();

            int sequencial = Convert.ToInt32(nextValue);


            return sequencial;
        }
        public void AddListaSalvar(string indice)
        {
            listaSalvar.Add(indice);
        }
        public string ExecuteTransacao()
        {
            string retorno = "NOTOK";

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {

                    foreach (string insert in listaSalvar)
                    {
                        _Comando = new SqlCommand(insert, _Conexao.MinhaConexao());
                        _Comando.CommandType = CommandType.Text;
                        int Result = (int)_Comando.ExecuteNonQuery();
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return retorno;
            }
        }
        public bool RetornaObjeto(string tableName, string columnName, object value)
        {
            string query = $"SELECT 1 FROM {tableName} WHERE {columnName} =" + value;

            SqlCommand _Comando = new SqlCommand(query, _Conexao.MinhaConexao());
            _Comando.CommandType = CommandType.Text;
            SqlDataReader dr = _Comando.ExecuteReader();

            if (dr.Read())
            {
                return true; // O valor existe na tabela
            }
            return false;  // O valor não existe na tabela
        }

    }
}

