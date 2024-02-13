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
            prepared_statement.prepareStatements(sqlConnection);
            

            while (true)
            {
                Console.WriteLine("All Dishes");
                query.queryDraw("exec show_dish", sqlConnection);
                
                Console.WriteLine();
                Console.WriteLine("All Ingredients");
                query.queryDraw("exec show_ingredient", sqlConnection);

                var dishID = menu.drawDishSelectionMenu();

                switch (dishID[0])
                {
                    case 'e':
                        Console.Clear();
                        generalIngredientTable.edit(0, sqlConnection);
                        break;
                    case 'd':
                        dishTable.edit(sqlConnection);
                        break;
                    default:
                        Console.Clear();
                        var infoDisplayAll = prepared_statement.getStatement("displayAllInfo");
                        infoDisplayAll.Parameters[0].Value = dishID;

                        query.queryDraw("", sqlConnection, infoDisplayAll);
                        var selectedTable = menu.drawPostSelectionMenu();
                        switch (selectedTable)
                        {
                            case tableEdit.step:
                                menu.drawStepEditing(Convert.ToInt32(dishID), sqlConnection);
                                break;
                            case tableEdit.ingredient:
                                menu.drawIngredientEditing(Convert.ToInt32(dishID), sqlConnection);
                                break;
                            case tableEdit.recipe:
                                break;
                            case tableEdit.errorTable:
                                break;
                        }
                        break;
                }
            }
        }
    }
}
