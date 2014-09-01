using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using VarcalSys_System.Util.Properties;

namespace VarcalSys_System.Util
{
    public class Contexto
    {
        private readonly MySqlConnection _connection;

        public Contexto()
        {
            _connection = new MySqlConnection("server=mysql01.varcalsys1.hospedagemdesites.ws;user id=varcalsys1;password=galleguy35;database=varcalsys1;");
        }

        private readonly MySqlParameterCollection _sqlParameterCollection = new MySqlCommand().Parameters;

        protected void CleanParameter()
        {
            _sqlParameterCollection.Clear();
        }

        protected void AddParameter(string parameterName, object parameterValue)
        {
            _sqlParameterCollection.AddWithValue(parameterName, parameterValue);
        }

        private MySqlCommand CreateCommand(CommandType cmdType, string cmdSql)
        {
           
            _connection.Open();
            var cmd = _connection.CreateCommand();
            cmd.CommandType = cmdType;
            cmd.CommandText = cmdSql;
            cmd.CommandTimeout = 7200;
            foreach (MySqlParameter sqlParameter in _sqlParameterCollection)
            {
                cmd.Parameters.Add(new MySqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
            }
            return cmd;
        }

        protected object ExecCommand(CommandType cmdType, string cmdSql)
        {
            try
            {
                var cmd = CreateCommand(cmdType, cmdSql);
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        protected DataTable ExecQuery(CommandType cmdType, string cmdSql)
        {
            try
            {
                var cmd = CreateCommand(cmdType, cmdSql);
                var dt = new DataTable();
                var sqlDataAdapter = new MySqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dt);
                return dt;
            } 
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

    }
}
