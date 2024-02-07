using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Datenbank
{
    internal class recipe
    {
        public static void recipeInit(SqlConnection sqlConnection)
        {
            Console.Clear();
            string[] commands = new string[]
            {
                "Find (SYNTAX = Name)",
                "Add (SYNTAX = Dish Name;Description)",
                "Delete (SYNTAX = id)",
                "Select (SYNTAX = id)"
            };
            var flipper = 3;

            prepared_statement.prepareStatements(sqlConnection);
            

            while (true)
            {
                flipper++;
                if (flipper > commands.Length-1)
                {
                    flipper = 0;
                }

                query.queryDraw("exec show_dish", sqlConnection);
                var dishID = menu.drawDishSelectionMenu();

                Console.Clear();

                var infoDisplayAll = prepared_statement.getStatement("displayAllInfo");
                infoDisplayAll.Parameters[0].Value = dishID;

                query.queryDraw("", sqlConnection, infoDisplayAll);
                var selectedTable = menu.drawPostSelectionMenu();
                switch (selectedTable)
                {
                    case tableEdit.step:
                        menu.drawStepEditing(dishID, sqlConnection);
                        break;
                    case tableEdit.ingredient:
                        break;
                    case tableEdit.recipe:
                        break;
                    case tableEdit.errorTable:
                        break;
                }
            }
        }
    }
}
