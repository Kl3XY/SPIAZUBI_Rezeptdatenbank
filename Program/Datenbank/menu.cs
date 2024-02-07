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

    public static class Validator
    {
        public static bool Validate(object Input)
        {
            var errMsg = new List<ValidationResult>();
            var ctxt = new ValidationContext(Input);
            var rv = System.ComponentModel.DataAnnotations.Validator.TryValidateObject(Input, ctxt, errMsg, true);
            if (rv == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The input you've given is invalid.\nPlease correct the following things:");
                errMsg.ForEach(n => Console.WriteLine($"-{n}"));
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
                return false;
            }
            return true;
        }
    }

    class commandSelection
    {
        [Required(ErrorMessage = "Missing Command")]
        public string givenCommand { get; set; }

        public commandSelection(string commandName)
        {
            this.givenCommand = commandName;
        }
    }

    internal static class menu
    {
        public static int drawDishSelectionMenu()
        {
            Console.WriteLine("Select a dish by its ID!");
            return Convert.ToInt32(Console.ReadLine());

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
                    instance.editStep(dishID, sqlConnection);
                    break;
                case "r":
                case "remove":
                    instance.removeStep(dishID, sqlConnection);
                    break;
                case "c":
                case "clear":
                    instance.clearStep(dishID, sqlConnection);
                    break;
                case "a":
                case "add":
                    instance.addStep(dishID, sqlConnection);
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
            var instance = new stepTable();
            var selection = Console.ReadLine();
            switch (selection)
            {
                case "e":
                case "edit":
                    
                    break;
                case "r":
                case "remove":
                    
                    break;
                case "c":
                case "clear":
                    
                    break;
                case "a":
                case "add":
                    
                    break;
                default:
                    Console.WriteLine("Unrecognized Command.");
                    break;
            }
        }
    }
}
