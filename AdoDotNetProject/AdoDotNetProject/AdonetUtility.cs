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

        public void ExecuteSql(string sql)
        {
            /* [Note: * If the clase impliments "Idisposal" interface ,
                        then keyword "using" will dispose automatically.]
            */

            using SqlConnection connection = new SqlConnection(_connectionString);      
            using SqlCommand command = new SqlCommand(sql, connection);     /*[Note: here sql=sql query, connection=connection object.]*/


            if (connection.State != System.Data.ConnectionState.Open) 
                connection.Open();


            int effection = command.ExecuteNonQuery();      /*[Note: It will count - How many rows are affected.]*/
            Console.WriteLine("Row is effected : " + " " + effection);

            /* [Note: * SqlConnection, SqlCommand These classes's methods are heavy & occupied the ram ,
                        That is why after every operation it must be destroyed.
                      * if "using" keyword is used , dont need to write below codes.]
            */

            //connection.Close();
            //command.Dispose();
            //connection.Dispose();
        }
    }
}
