using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB_API;

namespace Datenbank
{
    internal static class query
    {
        public static DataSet recipeGet(string query, SqlConnection connection, SqlCommand cmd = null)
        {
            if (cmd != null)
            {
                query = cmd.CommandText;
            }
            using (SqlCommand cmdRecipe = new SqlCommand(query, connection))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmdRecipe);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds;
            }
        }
        public static List<Recipe> postDB (SqlConnection connection)
        {
            var list = new List<Recipe>();

            DataSet DataSet = recipeGet("select Dish_ID, dish_name, dish_description from, dish", connection):
            foreach (DataRow row in DataSet.Rows) {

                var baseRecipe = new Recipe();

                baseRecipe.recipeName = row["dish_name"].ToString();
                baseRecipe.recipeDescription = row["dish_description"].ToString();

                var IngredientDataSet = recipeGet($"exec show_dish_ingredients @id = {row["Dish_ID"]}", connection);

                foreach (DataRow ingredients in IngredientDataSet.Rows)
                {
                    baseRecipe.ingredients.Add(new Ingredient(ingredients["ingredient_name"].ToString(), ingredients["amount"].ToString()));
                }

                var StepsDataSet = recipeGet($"exec show_step @Dish_ID = {row["Dish_ID"]}", connection);
                foreach (DataRow step in StepsDataSet.Rows)
                {
                    baseRecipe.steps.Add(new steps(Convert.ToInt32(step["Step"].ToString()), step["step_description"].ToString()));
                }
                list.Add(baseRecipe);
            }
            return list;
        }
    }
}
