using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datenbank
{
    internal class ingredientTable
    {
        public void add(int dishID, SqlConnection sqlConnection)
        {
            var stopped = false;
            while (!stopped)
            {
                Console.Clear();
                var showSteps = prepared_statement.getStatement("showRecipe");
                showSteps.Parameters[0].Value = dishID;

                query.queryDraw("", sqlConnection, showSteps);

                Console.WriteLine("All Ingredients:");

                query.queryDraw("select * from ingredient", sqlConnection);


                Console.WriteLine("Enter the ID of the Ingredient you want to add!\n Type a c and enter to exit!");
                var description = Console.ReadLine();

                if (description == "c")
                {
                    Console.Clear();
                    stopped = true;
                    return;
                }
                var initSearch = prepared_statement.getStatement("searchIngredient");
                initSearch.Parameters[0].Value = Convert.ToInt32(description);
                var ingredientSearch = query.queryDraw("", sqlConnection, initSearch, true);
                if (ingredientSearch.Tables[0].Rows.Count > 0)
                {
                    var addIngredient = prepared_statement.getStatement("addIngredientToDish");
                    addIngredient.Parameters[0].Value = dishID;
                    addIngredient.Parameters[1].Value = Convert.ToInt32(description);

                    Console.WriteLine("How much? (unit gets decided after this step.)");
                    var amountAsk = Console.ReadLine();

                    Console.WriteLine("Select Unit by id");

                    query.queryDraw("select * from unit", sqlConnection);
                    var unitAsk = Convert.ToInt32(Console.ReadLine());

                    addIngredient.Parameters[2].Value = amountAsk;
                    addIngredient.Parameters[3].Value = unitAsk;
                    addIngredient.ExecuteNonQuery();
                }
                Console.Clear();
            }
        }

        public void edit(int dishID, SqlConnection sqlConnection)
        {
            var stopped = false;
            while (!stopped)
            {
                Console.Clear();
                var showSteps = prepared_statement.getStatement("showDishIngredients");
                showSteps.Parameters[0].Value = dishID;

                query.queryDraw("", sqlConnection, showSteps);

                Console.WriteLine("All Ingredients:");

                query.queryDraw("select * from ingredient", sqlConnection);

                var qr = query.queryDraw("select * from ingredient", sqlConnection, null, true);

                Program.version = Convert.ToInt32(qr.Tables[0].Rows[0]["ver"].ToString());

                Console.WriteLine("Running on Version: {0}", Program.version);
                Console.WriteLine("Enter the ID of the Ingredient you want to edit!\n Type a c and enter to exit!");
                var description = Console.ReadLine();

                if (description == "c")
                {
                    Console.Clear();
                    stopped = true;
                    return;
                }
                var initSearch = prepared_statement.getStatement("searchIngredient");
                initSearch.Parameters[0].Value = Convert.ToInt32(description);
                var ingredientSearch = query.queryDraw("", sqlConnection, initSearch, true);
                if (ingredientSearch.Tables[0].Rows.Count > 0)
                {
                    var editIngredient = prepared_statement.getStatement("editIngredientOfDish");
                    editIngredient.Parameters[0].Value = dishID;
                    editIngredient.Parameters[1].Value = Convert.ToInt32(description);

                    Console.Clear(); 

                    query.queryDraw("select * from ingredient", sqlConnection);
                    Console.WriteLine("To what ingredient do you want to change it? (select via id)");
                    var ingredSelect = Console.ReadLine();

                    Console.Clear();

                    editIngredient.Parameters[2].Value = Convert.ToInt32(ingredSelect);

                    Console.WriteLine("How much? (unit gets selected after this)");
                    var measurementInput = Console.ReadLine();
                    editIngredient.Parameters[3].Value = Convert.ToInt32(measurementInput);

                    Console.Clear();

                    query.queryDraw("select * from unit", sqlConnection);
                    Console.WriteLine("What unit? select by id");
                    var unitInput = Console.ReadLine();
                    editIngredient.Parameters[4].Value = Convert.ToInt32(unitInput);
                    editIngredient.Parameters[5].Value = Program.version;

                    editIngredient.ExecuteNonQuery();
                }
                Console.Clear();
            }
        }

        public void remove(int dishID, SqlConnection sqlConnection)
        {
            var stopped = false;
            while (!stopped)
            {
                Console.Clear();
                var showSteps = prepared_statement.getStatement("showDishIngredients");
                showSteps.Parameters[0].Value = dishID;

                query.queryDraw("", sqlConnection, showSteps);

                Console.WriteLine("All Ingredients:");

                query.queryDraw("select * from ingredient", sqlConnection);


                Console.WriteLine("Enter the ID of the Ingredient you want to remove\n Type a c and enter to exit!");
                var description = Console.ReadLine();

                if (description == "c")
                {
                    Console.Clear();
                    stopped = true;
                    return;
                }
                var initSearch = prepared_statement.getStatement("searchIngredient");
                initSearch.Parameters[0].Value = Convert.ToInt32(description);
                var ingredientSearch = query.queryDraw("", sqlConnection, initSearch, true);
                if (ingredientSearch.Tables[0].Rows.Count > 0)
                {
                    var deleteIngredient = prepared_statement.getStatement("deleteDishIngredients");
                    deleteIngredient.Parameters[0].Value = dishID;
                    deleteIngredient.Parameters[1].Value = Convert.ToInt32(description);

                    deleteIngredient.ExecuteNonQuery();
                }
                Console.Clear();
            }
        }
    }
}
