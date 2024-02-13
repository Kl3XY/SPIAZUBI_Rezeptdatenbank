using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datenbank
{
    internal static class dishTable
    {
        public static void edit(SqlConnection sqlConnection)
        {
            Console.Clear();
            query.queryDraw("exec show_dish", sqlConnection);
            Console.WriteLine("You are editing the main dish list");
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("(e)dit");
            Console.WriteLine("(r)emove");
            Console.WriteLine("(a)dd");
            var selection = Console.ReadLine();
            switch (selection)
            {
                case "e":
                case "edit":
                    var cmd = prepared_statement.getStatement("editDish");

                    var qr = query.queryDraw("select * from step", sqlConnection, null, true);

                   

                    Console.WriteLine("Enter the id of the dish you want to edit");
                    var dish_ID = Convert.ToInt32(Console.ReadLine());

                    Program.version = -1;
                    foreach (DataRow row in qr.Tables[0].Rows)
                    {
                        if (row["Dish_ID"].ToString() == dish_ID.ToString())
                        {
                            Program.version = Convert.ToInt32(row["ver"].ToString());
                            Console.WriteLine("found");
                            break;
                        }
                    }
                    if (Program.version == -1) { throw new Exception("Table Entry not found"); }


                    cmd.Parameters[0].Value = dish_ID;
                    Console.WriteLine("Enter the new name!");
                    var newDishName = Console.ReadLine();
                    cmd.Parameters[1].Value = newDishName;

                    Console.WriteLine("Enter the new description!");
                    var newDishDesc = Console.ReadLine();
                    cmd.Parameters[2].Value = newDishDesc;
                    cmd.Parameters[3].Value = Program.version;

                    cmd.ExecuteNonQuery();
                    break;
                case "r":
                case "remove":
                    var cmd2 = prepared_statement.getStatement("remDish");
                    Console.WriteLine("Enter the id of the dish you want to remove");
                    var dish_ID2 = Convert.ToInt32(Console.ReadLine());
                    cmd2.Parameters[0].Value = dish_ID2;
                    Console.WriteLine("Do you want to delete the current dish?");
                    var confirm = Console.ReadLine();

                    if (confirm == "y")
                    {
                        cmd2.ExecuteNonQuery();
                    }
                    break;
                case "a":
                case "add":
                    var cmd3 = prepared_statement.getStatement("addDish");
                    Console.WriteLine("Enter the name of the new dish!");
                    var addDishName = Console.ReadLine();
                    cmd3.Parameters[0].Value = addDishName;
                    Console.WriteLine("Enter the description of the new dish!");
                    var addDishDescription = Console.ReadLine();
                    cmd3.Parameters[1].Value = addDishDescription;
                    break;
                default:
                    Console.WriteLine("Unrecognized Command.");
                    break;
            }
            Console.Clear();
        }
    }
}
