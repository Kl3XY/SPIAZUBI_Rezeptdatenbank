using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Datenbank
{
    enum tableEdit
    {
        step,
        ingredient,
        recipe,
        errorTable
    }

    internal static class menu
    {
        public static string drawDishSelectionMenu()
        {
            Console.WriteLine("Select a dish by its ID!");
            Console.WriteLine("or just type e to edit ingredients");
            Console.WriteLine("or just type d to edit the dishes");
            return Console.ReadLine();

        }

        public static tableEdit drawPostSelectionMenu()
        {
            Console.WriteLine("What table do you want to edit?");
            var tableSelect = Console.ReadLine();
            switch (tableSelect)
            {
                case "s":
                case "step":
                    return tableEdit.step;
                    break;
                case "i":
                case "ingredient":
                    return tableEdit.ingredient;
                    break;
                default:
                    Console.WriteLine("Table not recognised. Check what you entered!");
                    return tableEdit.errorTable;
                    break;
            }
        }

        public static void drawStepEditing(int dishID, SqlConnection sqlConnection)
        {
            Console.WriteLine("You've selected the Step List!");
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("(e)dit");
            Console.WriteLine("(r)emove");
            Console.WriteLine("(c)lear");
            Console.WriteLine("(a)dd");
            var instance = new stepTable();
            var selection = Console.ReadLine();
            switch (selection)
            {
                case "e":
                case "edit":
                    instance.edit(dishID, sqlConnection);
                    break;
                case "r":
                case "remove":
                    instance.remove(dishID, sqlConnection);
                    break;
                case "c":
                case "clear":
                    instance.clear(dishID, sqlConnection);
                    break;
                case "a":
                case "add":
                    instance.add(dishID, sqlConnection);
                    break;
                default:
                    Console.WriteLine("Unrecognized Command.");
                    break;
            }
        }

        public static void drawIngredientEditing(int dishID, SqlConnection sqlConnection)
        {
            Console.WriteLine("You've selected the Ingredient List!");
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("(e)dit");
            Console.WriteLine("(r)emove");
            Console.WriteLine("(c)lear");
            Console.WriteLine("(a)dd");
            var instance = new ingredientTable();
            var selection = Console.ReadLine();
            switch (selection)
            {
                case "e":
                case "edit":
                    instance.edit(dishID, sqlConnection);
                    break;
                case "r":
                case "remove":
                    instance.remove(dishID, sqlConnection);
                    break;
                case "c":
                case "clear":
                    var clear = prepared_statement.getStatement("clearDishIngredient");
                    clear.Parameters[0].Value = dishID;
                    clear.ExecuteNonQuery();
                    break;
                case "a":
                case "add":
                    instance.add(dishID, sqlConnection);
                    break;
                default:
                    Console.WriteLine("Unrecognized Command.");
                    break;
            }
        }
    }
}
