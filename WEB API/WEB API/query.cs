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
        public static List<Recipe> recipeGet(SqlConnection connection)
        {
            var list = new List<Recipe>();

            using (SqlCommand cmdRecipe = new SqlCommand("select Dish_ID, dish_name, dish_description from dish", connection))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmdRecipe);
                DataTable DataSet = new DataTable();
                adapter.Fill(DataSet);

                foreach (DataRow row in DataSet.Rows) {

                    var baseRecipe = new Recipe();

                    baseRecipe.recipeName = row["dish_name"].ToString();
                    baseRecipe.recipeDescription = row["dish_description"].ToString();
                    
                    using (SqlCommand getIngredient = new SqlCommand($"exec show_dish_ingredients @id = {row["Dish_ID"]}", connection))
                    {
                        SqlDataAdapter IngredientAdapter = new SqlDataAdapter(getIngredient);
                        DataTable IngredientDataSet = new DataTable();
                        IngredientAdapter.Fill(IngredientDataSet);

                        foreach (DataRow ingredients in IngredientDataSet.Rows)
                        {
                            baseRecipe.ingredients.Add(new Ingredient(ingredients["ingredient_name"].ToString(), ingredients["amount"].ToString()));
                        }
                    }

                    using (SqlCommand getSteps = new SqlCommand($"exec show_step @Dish_ID = {row["Dish_ID"]}", connection))
                    {
                        SqlDataAdapter StepsAdapter = new SqlDataAdapter(getSteps);
                        DataTable StepsDataSet = new DataTable();
                        StepsAdapter.Fill(StepsDataSet);

                        foreach (DataRow step in StepsDataSet.Rows)
                        {
                            baseRecipe.steps.Add(new steps(Convert.ToInt32(step["Step"].ToString()), step["step_description"].ToString()));
                        }
                    }
                    list.Add(baseRecipe);
                }

            }

            return list;
        }
    }
}
