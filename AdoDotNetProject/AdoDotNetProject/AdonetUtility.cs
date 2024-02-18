using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace AdoDotNetProject
{
    public class AdonetUtility
    {
        private readonly string _connectionString;

        public AdonetUtility(string connectionString)
        {
            _connectionString = connectionString;
        }



        /* .................... DB-Connection Method .................... */
        private SqlCommand CreateCommand(string sql, Dictionary<string, object> parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(sql, connection);

            foreach(var parameter in parameters)
            {
                command.Parameters.Add(new SqlParameter(parameter.Key, parameter.Value));
            }

            if(connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            return command; 
        }



        /* .................... ExecuteSql Method .................... */
        public void ExecuteSql(string sql, Dictionary<string, object> parameters)
        {
            using SqlCommand command = CreateCommand(sql, parameters);   
            
            int effection = command.ExecuteNonQuery();

            Console.WriteLine("Row is effected : " + effection + "\n");
        }



        /* .................... GetData Method .................... */
        public IList<Dictionary<string, object>> GetData(string sql, Dictionary<string, object> parameters)        
        {
            using SqlCommand command = CreateCommand(sql, parameters);
            using SqlDataReader reader = command.ExecuteReader();

            //List<Dictionary<string, object>> items = new List<Dictionary<string, object>>();      
            var items = new List<Dictionary<string, object>>();  
            
            while (reader.Read())      
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)     
                {
                    data.Add(reader.GetName(i), reader.GetValue(i));        
                }

                items.Add(data);       
            }

            return items;
        }
    }
}
