using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datenbank
{
    internal static class generalIngredientTable
    {
        public static void edit(int dishID, SqlConnection sqlConnection)
        {
            Console.Clear();
            query.queryDraw("select * from ingredient", sqlConnection);
            Console.WriteLine("You are editing the main ingreidnet list");
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("(e)dit");
            Console.WriteLine("(r)emove");
            Console.WriteLine("(a)dd");
            var selection = Console.ReadLine();
            switch (selection)
            {
                case "e":
                case "edit":
                    Console.WriteLine("enter an id to edit");
                    var editIngredient = Console.ReadLine();

                    var qr = query.queryDraw("select * from ingredient", sqlConnection, null, true);
 
                    Program.version = -1;
                    foreach (DataRow row in qr.Tables[0].Rows)
                    {
                        if (row["Ingredient_ID"].ToString() == editIngredient)
                        {
                            Program.version = Convert.ToInt32(row["ver"].ToString());
                            Console.WriteLine("found");
                            break;
                        }
                    }
                    if (Program.version == -1) { throw new Exception("Table Entry not found"); }

                    Console.WriteLine("Now the new name for it!");
                    var newIngredientName = Console.ReadLine();
                    var editCommand = prepared_statement.getStatement("editIngredient");
                    editCommand.Parameters[0].Value = Convert.ToInt32(editIngredient);
                    editCommand.Parameters[1].Value = newIngredientName;
                    editCommand.Parameters[2].Value = Program.version;
                    editCommand.ExecuteNonQuery();
                    break;
                case "r":
                case "remove":
                    Console.WriteLine("enter an id to remove");
                    var id = Console.ReadLine();
                    var deleteCommand = prepared_statement.getStatement("editIngredient");
                    deleteCommand.Parameters[0].Value = Convert.ToInt32(id);
                    deleteCommand.ExecuteNonQuery();
                    break;
                case "a":
                case "add":
                    Console.WriteLine("enter the name of the ingredient");
                    var ingname = Console.ReadLine();
                    var addCommand = prepared_statement.getStatement("addIngredient");
                    addCommand.Parameters[0].Value = ingname;
                    addCommand.ExecuteNonQuery();
                    break;
                default:
                    Console.WriteLine("Unrecognized Command.");
                    break;
            }
            Console.Clear();
        }
    }
}
