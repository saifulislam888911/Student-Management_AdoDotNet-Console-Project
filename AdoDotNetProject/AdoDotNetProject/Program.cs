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
string selectSql = "SELECT * FROM Students WHERE Id=@id";



Console.WriteLine("\n" + "Insert Operation:");
Console.WriteLine("Format:Name,Address,CGPA");
string[] parts = Console.ReadLine().Split(",");

Console.WriteLine("\n" + "Select Operation:");
Console.WriteLine("Format:(Select By ID)");
int id = int.Parse(Console.ReadLine());

Dictionary<string, object> parameters1 = new Dictionary<string, object>();
parameters1.Add("name", parts[0]);
parameters1.Add("address", parts[1]);
parameters1.Add("cgpa", double.Parse(parts[2]));

Dictionary<string, object> parameters2 = new Dictionary<string, object>();
parameters2.Add("id", id);



/* .................... Creating DB-Connection Object .................... */

AdonetUtility adonetUtility = new AdonetUtility(connectionString);



/* .................... Calling DB-Connection Object's SQL Methods .................... */

/* ..... Insert Operation .....*/

adonetUtility.ExecuteSql(insertSql, parameters1);



/* ..... Select Operation .....*/

IList<Dictionary<string, object>> result = adonetUtility.GetData(selectSql, parameters2);
//var result = adonetUtility.GetData(selectSql, parameters2); 

foreach (var row in result)
{
    foreach(var col in row)
    {
        Console.Write(col.Value);

        Console.Write(" , ");
    }

    Console.WriteLine();
}


Console.WriteLine("\n" + "Done");