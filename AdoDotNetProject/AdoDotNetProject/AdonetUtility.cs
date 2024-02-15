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

        #region Write : step 1 : Commit No.1 : (Commented) (This Code is Modified in next Commit No.2)

        //public void ExecuteSql(string sql)
        //{
        //    /* [Note: * If the clase impliments "Idisposal" interface ,
        //                then keyword "using" will dispose automatically.]
        //    */

        //    using SqlConnection connection = new SqlConnection(_connectionString);      
        //    using SqlCommand command = new SqlCommand(sql, connection);     /*[Note: here sql=sql query, connection=connection object.]*/


        //    if (connection.State != System.Data.ConnectionState.Open) 
        //        connection.Open();


        //    int effection = command.ExecuteNonQuery();      /*[Note: It will count - How many rows are affected.]*/

        //    Console.WriteLine("Row is effected : " + " " + effection);


        //    /* [Note: * SqlConnection, SqlCommand These classes's methods are heavy & occupied the ram ,
        //                That is why after every operation it must be destroyed.
        //              * if "using" keyword is used , dont need to write below codes.]
        //    */

        //    //connection.Close();
        //    //command.Dispose();
        //    //connection.Dispose();
        //}

        #endregion

        private SqlCommand CreateCommand(string sql)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(sql, connection);

            if(connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            return command; 
        }

        public void ExecuteSql(string sql)
        {
            /* [Note: * "command" object's method Calling - "CreateCommand(sql)"
                        & throwing "sql" argument in the parameter of "CreateCommand(sql)".
                      * "using" keyword will automatically Dispose "SqlCommand command" object.
                      * "SqlCommand(sql, connection)" inside "command" there is passed "connection" object,
                        both of them will be disposed, Dont need to do separately.]
            */

            using SqlCommand command = CreateCommand(sql);   
            
            int effection = command.ExecuteNonQuery();

            Console.WriteLine("Row is effected : " + effection + "\n");
        }

        public IList<Dictionary<string, object>> GetData(string sql)        /*[Note: use "interface" as Return Data type.]*/
        {
            using SqlCommand command = CreateCommand(sql);

            using SqlDataReader reader = command.ExecuteReader();

            /* [Note: * when calling "command.ExecuteReader()" it will give us "SqlDataReader",
                        it have not fetch data yet.
                      * when we run a loop and call "Read" method ,
                        the "reader" object will read/bring row data from the database.
                      * "reader" bring data as object,
                        we can typeCast the object if it is needed.
            */

            //List<Dictionary<string, object>> items = new List<Dictionary<string, object>>();      /*[Note: Both code line are same.]*/
            var items = new List<Dictionary<string, object>>();     
            while (reader.Read())       /*[Note: it will "True"  and go on untill it vists all rows, after visits all rows it will "Flase".]*/
            {
                Dictionary<string, object> data = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)     /*[Note: Finding Columns' value in a row.]*/
                {
                    data.Add(reader.GetName(i), reader.GetValue(i));        /*[Note: all Column's values are inserted in a row.]*/
                }

                items.Add(data);        /*[Note: a row's value are inserted in a list<>.]*/
            }

            return items;
        }
    }
}
