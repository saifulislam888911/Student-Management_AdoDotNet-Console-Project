using AdoDotNetProject;





//string connectionString = "Server=HP-LAPTOP\\SQLEXPRESS;Database=Prac_CSharpB15;User Id=Practice_Project;Password=123456;Trust Server Certificate=True;";
string connectionString = "Server=HP-LAPTOP\\SQLEXPRESS;" +
                          "Database=Prac_CSharpB15;" +
                          "User Id=Practice_Project;" +
                          "Password=123456;" +
                          "Trust Server Certificate=True;";

string insertSql = "INSERT INTO Students(Name, Address, CGPA) VALUES(@name, @address, @cgpa)";
string updateSql = "UPDATE Students SET Name=@name, Address=@address, CGPA=@cgpa WHERE Id=@id";
string deleteSql = "DELETE FROM Students WHERE Id=@id";
string selectByIdSql = "SELECT * FROM Students WHERE Id=@id";
string selectFullTableSql = "SELECT * FROM Students";

bool appRunning = true;


/* .................... Creating DB-Connection Object .................... */

AdonetUtility adonetUtility = new AdonetUtility(connectionString);



/* .................... Calling DB-Connection Object's SQL Methods .................... */

Console.WriteLine("Welcome To \"Student Management System\"\n");

while (appRunning)
{
    Console.Write("\nOperation:" + "\n" +
                  "1.Insert" + "\n" +
                  "2.Update" + "\n" +
                  "3.Delete" + "\n" +
                  "4.Get Data" + "\n" +
                  "5.Display Table" + "\n" +
                  "6.Close" + "\n" +
                  "\nSelect The Operation Number: ");
    
    string operation = Console.ReadLine();

    switch (operation)
    {
        case "1":
        case "Insert":
            try
            {
                Console.WriteLine("Insert Operation:");
                Console.WriteLine("Format:Name,Address,CGPA");
                string[] parts = Console.ReadLine().Split(",");

                Dictionary<string, object> parameters1 = new Dictionary<string, object>();
                parameters1.Add("name", parts[0]);
                parameters1.Add("address", parts[1]);
                parameters1.Add("cgpa", double.Parse(parts[2]));

                adonetUtility.ExecuteSql(insertSql, parameters1);

                Console.WriteLine("\nInsert Successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during Insert operation: " + ex.Message);
            }
            break;



        case "2":
        case "Update":
            try
            {
                Console.WriteLine("Update Operation:");
                Console.Write("Input Id: ");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Format:Name,Address,CGPA");
                string[] parts = Console.ReadLine().Split(",");

                Dictionary<string, object> parameters2 = new Dictionary<string, object>();
                parameters2.Add("id", id);
                parameters2.Add("name", parts[0]);
                parameters2.Add("address", parts[1]);
                parameters2.Add("cgpa", double.Parse(parts[2]));

                adonetUtility.ExecuteSql(updateSql, parameters2);

                Console.WriteLine("\nUpdate Successful." + " Id: " + id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during Update operation: " + ex.Message);
            }
            break;



        case "3":
        case "Delete":
            try
            {
                Console.WriteLine("Delete Operation:");
                Console.Write("Input Id: ");
                int id = int.Parse(Console.ReadLine());

                Dictionary<string, object> parameters3 = new Dictionary<string, object>();
                parameters3.Add("id", id);

                adonetUtility.ExecuteSql(deleteSql, parameters3);

                Console.WriteLine("\nDelete Successful." + " Id: " + id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during Delete operation: " + ex.Message);
            }
            break;



        case "4":
        case "Select":
            try
            {
                Console.WriteLine("Select Operation:");
                Console.Write("Input Id: ");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Format: Id , Name , Address , CGPA");
                Console.WriteLine("Data:");

                Dictionary<string, object> parameters4 = new Dictionary<string, object>();
                parameters4.Add("id", id);

                IList<Dictionary<string, object>> result = adonetUtility.GetData(selectByIdSql, parameters4);
                //var result = adonetUtility.GetData(selectSql, parameters4); 

                foreach (var row in result)
                {
                    foreach (var col in row)
                    {
                        Console.Write(col.Value);

                        Console.Write(" , ");
                    }

                    Console.WriteLine();
                }

                Console.WriteLine("\nFetch Information Successful." + " Id: " + id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during Select operation: " + ex.Message);
            }
            break;



        case "5":
        case "Display Table":
            try
            {
                Console.WriteLine("Display Full Table:");
                Console.WriteLine("Format: Id , Name , Address , CGPA");
                Console.WriteLine("Table Name: Students");
             
                Dictionary<string, object> parameters5 = new Dictionary<string, object>();

                IList<Dictionary<string, object>> result = adonetUtility.GetData(selectFullTableSql, parameters5);
                //var result = adonetUtility.GetData(selectFullTableSql, parameters5); 

                foreach (var row in result)
                {
                    foreach (var col in row)
                    {
                        Console.Write(col.Value);

                        Console.Write(" , ");
                    }

                    Console.WriteLine();
                }

                Console.WriteLine("\nFetch Information Successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during Display Full Table operation: " + ex.Message);
            }
            break;



        case "6":
        case "Close":
            appRunning = false;
            break;



        default:
            Console.WriteLine("Invalid operation! Please select a valid operation.");
            break;
    }
}

Console.WriteLine("\nDone");