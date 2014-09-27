using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;



namespace VarcalSys_System.Util
{
    public class Contexto
    {

        
        private readonly SqlParameterCollection _sqlParameterCollection = new SqlCommand().Parameters;

        private SqlConnection CreateConnection()
        {           
            var connString = ConfigurationManager.ConnectionStrings["strConnection"].ConnectionString;
            return new SqlConnection(connString);
        }

        protected void CleanParameter()
        {
            _sqlParameterCollection.Clear();
        }

        protected void AddParameter(string parameterName, object parameterValue)
        {
            _sqlParameterCollection.AddWithValue(parameterName, parameterValue);
        }

        private SqlCommand CreateCommand(CommandType cmdType, string cmdSql)
        {
            var connection = CreateConnection();
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandType = cmdType;
            cmd.CommandText = cmdSql;
            cmd.CommandTimeout = 7200;
            foreach (SqlParameter sqlParameter in _sqlParameterCollection)
            {
                cmd.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
            }
            return cmd;
        }

        protected object ExecCommand(CommandType cmdType, string cmdSql)
        {
            try
            {
                SqlCommand cmd = CreateCommand(cmdType, cmdSql);
                return cmd.ExecuteScalar();            
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        protected DataTable ExecQuery(CommandType cmdType, string cmdSql)
        {
            try
            {
                var cmd = CreateCommand(cmdType, cmdSql);
                var dt = new DataTable();
                var sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dt);
                return dt;
            } 
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

    }
}
