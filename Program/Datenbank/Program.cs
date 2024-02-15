using ConsoleTables;
using Datenbank;
using Microsoft.Data.SqlClient;
using System.Data;

enum menuStates
{
    query,

}

internal class Program
{
    public static bool isClosingConnection = false;
    public static bool fallback = false;
    public static string input = "";
    public static int version = -1;
    private static void Main(string[] args)
    {
        Console.WriteLine("SQL DATABASE");

        while (!fallback)
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                Console.WriteLine("Enter the connection string (enter a d for the default one)");
                var server = Console.ReadLine();

                if (server == "d")
                {
                    builder.ConnectionString = $"Server=(localDB)\\MSSQLLocaldb;Database=recipes;Integrated Security=True;TrustServerCertificate=true";
                } else
                {
                    builder.ConnectionString = server;
                }

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("Connecting...");

                    connection.Open();

                    Console.Clear();

                    Console.WriteLine("Enter what you wanna do:");
                    Console.WriteLine("(Q)uery");
                    Console.WriteLine("(R)ecipe Menu");

                    if (input == "")
                    {
                        input = Console.ReadLine();
                    }

                    switch (input.Split(" ")[0].ToLower())
                    {
                        case "q":
                            query.queryInit(connection);
                            break;
                        case "r":
                            recipe.recipeInit(connection);
                            break;
                        default:
                            input = "";
                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Something went wrong.");
                Console.WriteLine("{0} @ {1}", ex.Message, ex.Source);
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
                input = "";
            }
        }
    }
}