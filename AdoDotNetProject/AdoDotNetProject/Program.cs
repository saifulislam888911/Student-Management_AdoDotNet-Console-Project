using AdoDotNetProject;



Console.WriteLine("Welcome To \"Student Management System\"." + "\n");

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



/* .................... Creating DB-Connection Object .................... */

AdonetUtility adonetUtility = new AdonetUtility(connectionString);



/* .................... Calling DB-Connection Object's SQL Methods .................... */

bool appRunning = true;

while (appRunning)
{
    Console.WriteLine("\n" + "Operation:" + "\n" +
                      "1.Insert" + "\n" +
                      "2.Update" + "\n" +
                      "3.Delete" + "\n" +
                      "4.Select" + "\n" +
                      "5.Display Full Table" + "\n" +
                      "6.Close" + "\n" + "\n" +
                      "Select The Querry:(Write The Serial Number)");
    
    string operation = Console.ReadLine();

    if (operation == "1" || operation == "Insert")
    {
        try
        {
            Console.WriteLine("\n" + "Insert Operation:");
            Console.WriteLine("Format:Name,Address,CGPA");
            string[] parts = Console.ReadLine().Split(",");

            Dictionary<string, object> parameters1 = new Dictionary<string, object>();
            parameters1.Add("name", parts[0]);
            parameters1.Add("address", parts[1]);
            parameters1.Add("cgpa", double.Parse(parts[2]));

            adonetUtility.ExecuteSql(insertSql, parameters1);

            Console.WriteLine("\n" + "Insert Successful.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error during Insert operation: " + ex.Message);
        }
    }

    if (operation == "2" || operation == "Update")
    {
        try
        {
            Console.WriteLine("\n" + "Update Operation:");
            Console.WriteLine("Input ID :");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Format:Name,Address,CGPA");
            string[] parts = Console.ReadLine().Split(",");

            Dictionary<string, object> parameters2 = new Dictionary<string, object>();
            parameters2.Add("id", id);
            parameters2.Add("name", parts[0]);
            parameters2.Add("address", parts[1]);
            parameters2.Add("cgpa", double.Parse(parts[2]));

            adonetUtility.ExecuteSql(updateSql, parameters2);

            Console.WriteLine("\n" + "Update Successful." + " Id : " + id);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error during Update operation: " + ex.Message);
        }
    }

    if (operation == "3" || operation == "Delete")
    {
        try
        {
            Console.WriteLine("\n" + "Delete Operation:");
            Console.WriteLine("Input Id :");
            int id = int.Parse(Console.ReadLine());

            Dictionary<string, object> parameters3 = new Dictionary<string, object>();
            parameters3.Add("id", id);

            adonetUtility.ExecuteSql(deleteSql, parameters3);

            Console.WriteLine("\n" + "Delete Successful." + " Id : " + id);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error during Delete operation: " + ex.Message);
        }
    }

    if (operation == "4" || operation == "Select")
    {
        try
        {
            Console.WriteLine("\n" + "Select Operation:");
            Console.WriteLine("Format:(Select By ID)");
            Console.WriteLine("Input Id :");
            int id = int.Parse(Console.ReadLine());
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

            Console.WriteLine("\n" + "Fetch Information Successful." + " Id : " + id);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error during Select operation: " + ex.Message);
        }
    }

    if (operation == "5" || operation == "Display Full Table")
    {
        try
        {
            Console.WriteLine("\n" + "Display Full Table:(Select Entire Table Operation)" + "\n" +
              "Format: Id , Name , Address , CGPA" + "\n" +
              "Table : Students" + "\n");

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

            Console.WriteLine("\n" + "Fetch Information Successful.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error during Display Full Table operation: " + ex.Message);
        }
    }

    if (operation == "6" || operation == "Close")
    {
        appRunning = false;
    }
}



Console.WriteLine("\n" + "Done");