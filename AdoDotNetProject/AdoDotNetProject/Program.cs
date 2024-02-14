// See https://aka.ms/new-console-template for more information

using AdoDotNetProject;



string connectionString = "Server=HP-LAPTOP\\SQLEXPRESS;Database=Prac_CSharpB15;User Id=Practice_Project;Password=123456;Trust Server Certificate=True;";


string insertSql = "INSERT INTO Students(Name, Address, CGPA) VALUES('Mr. E', 'Hamburg, Germany', 4.00)";
string updateSql = "UPDATE Students SET Name='Mr. CCC', Address='India' WHERE Id=3";
string deleteSql = "DELETE FROM Students WHERE Id=3";


AdonetUtility adonetUtility = new AdonetUtility(connectionString);
adonetUtility.ExecuteSql(insertSql);
Console.WriteLine("Done");