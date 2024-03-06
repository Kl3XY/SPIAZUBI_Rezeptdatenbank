using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Datenbank
{
    internal static class prepared_statement
    {
        public static List<(string,SqlCommand)> statements = new();
        public static void prepareStatements(SqlConnection sqlConnection)
        {
            statements.Clear();

            /* DISPLAY ALL INFO */

            var dishSelectCommand = new SqlCommand("exec show_entire_dish_info @id = @ds", sqlConnection);
            dishSelectCommand.Parameters.Add(new SqlParameter("@ds", System.Data.SqlDbType.Int));
            dishSelectCommand.Parameters[0].Value = 0;
            dishSelectCommand.Prepare();

            statements.Add(("displayAllInfo",dishSelectCommand));

            /* SHOW STEPS */

            var showStepCommand = new SqlCommand("show_step @Dish_ID = @showStep_ID", sqlConnection);
            showStepCommand.Parameters.Add(new SqlParameter("@showStep_ID", System.Data.SqlDbType.Int));
            showStepCommand.Parameters[0].Value = 0;
            showStepCommand.Prepare();

            statements.Add(("showSteps", showStepCommand));

            /* EDIT DISH */

            var editStepCommand = new SqlCommand("exec edit_step @Dish_ID = @editStep_DishID, @stepNumber = @editStep_StepNumber, @description = @editStep_Description, @version = @ver", sqlConnection);
            editStepCommand.Parameters.Add(new SqlParameter("@editStep_DishID", System.Data.SqlDbType.Int));
            editStepCommand.Parameters[0].Value = 0;
            editStepCommand.Parameters.Add(new SqlParameter("@editStep_StepNumber", System.Data.SqlDbType.Int));
            editStepCommand.Parameters[1].Value = 0;
            editStepCommand.Parameters.Add(new SqlParameter("@editStep_Description", System.Data.SqlDbType.VarChar, 8000));
            editStepCommand.Parameters[2].Value = "";
            editStepCommand.Parameters.Add(new SqlParameter("@ver", System.Data.SqlDbType.Int));
            editStepCommand.Parameters[3].Value = 1;

            editStepCommand.Prepare();

            statements.Add(("editStep", editStepCommand));

            /* ADD STEP */

            var addStepCommand = new SqlCommand("exec add_step @dish_id = @addStep_DishID, @description = @addStep_Description", sqlConnection);
            addStepCommand.Parameters.Add(new SqlParameter("@addStep_DishID", System.Data.SqlDbType.Int));
            addStepCommand.Parameters[0].Value = 0;
            addStepCommand.Parameters.Add(new SqlParameter("@addStep_Description", System.Data.SqlDbType.VarChar, 8000));
            addStepCommand.Parameters[1].Value = "";

            addStepCommand.Prepare();

            statements.Add(("addStep", addStepCommand));

            /* REMOVE STEP */

            var removeStepCommand = new SqlCommand("exec delete_step @Dish_ID = @deleteStep_DishID, @Stepnmb = @deleteStep_Stepnmb", sqlConnection);
            removeStepCommand.Parameters.Add(new SqlParameter("@deleteStep_DishID", System.Data.SqlDbType.Int));
            removeStepCommand.Parameters[0].Value = 0;
            removeStepCommand.Parameters.Add(new SqlParameter("@deleteStep_Stepnmb", System.Data.SqlDbType.Int));
            removeStepCommand.Parameters[1].Value = 0;

            removeStepCommand.Prepare();

            statements.Add(("removeStep", removeStepCommand));

            /* CLEAR STEP */

            var clearStepCommand = new SqlCommand("exec clear_step @Dish_ID = @deleteStep_DishID", sqlConnection);
            clearStepCommand.Parameters.Add(new SqlParameter("@deleteStep_DishID", System.Data.SqlDbType.Int));
            clearStepCommand.Parameters[0].Value = 0;

            clearStepCommand.Prepare();

            statements.Add(("clearStep", clearStepCommand));

            /* SHOW RECIPE */

            var showRecipeCommand = new SqlCommand("exec show_Recipe @id = @showRecipe_ID", sqlConnection);
            showRecipeCommand.Parameters.Add(new SqlParameter("@showRecipe_ID", System.Data.SqlDbType.Int));
            showRecipeCommand.Parameters[0].Value = 0;

            showRecipeCommand.Prepare();

            statements.Add(("showRecipe", showRecipeCommand));

            /* SHOW INGREDIENTS */

            var showIngredientCommand = new SqlCommand("exec show_ingredient @id = @showIngredient_ID", sqlConnection);
            showIngredientCommand.Parameters.Add(new SqlParameter("@showIngredient_ID", System.Data.SqlDbType.Int));
            showIngredientCommand.Parameters[0].Value = 0;

            showIngredientCommand.Prepare();

            statements.Add(("showIngredient", showIngredientCommand));

            /* SEARCH INGREDIENTS */

            var searchIngredientCommand = new SqlCommand("exec search_ingredient @name = @searchIngredient_ID", sqlConnection);
            searchIngredientCommand.Parameters.Add(new SqlParameter("@searchIngredient_ID", System.Data.SqlDbType.Int));
            searchIngredientCommand.Parameters[0].Value = 0;

            searchIngredientCommand.Prepare();

            statements.Add(("searchIngredient", searchIngredientCommand));

            /* ADD INGREDIENTS */

            var addIngredientCommand = new SqlCommand("exec add_ingredient @name = @addIngredient_ID", sqlConnection);
            addIngredientCommand.Parameters.Add(new SqlParameter("@addIngredient_ID", System.Data.SqlDbType.VarChar, 64));
            addIngredientCommand.Parameters[0].Value = "";

            addIngredientCommand.Prepare();

            statements.Add(("addIngredient", addIngredientCommand));

            /* EDIT INGREDIENTS */

            var editIngredientCommand = new SqlCommand("exec edit_ingredient @id = @on, @newname = @nn, @version = @ver", sqlConnection);
            editIngredientCommand.Parameters.Add(new SqlParameter("@on", System.Data.SqlDbType.Int));
            editIngredientCommand.Parameters[0].Value = 0;
            editIngredientCommand.Parameters.Add(new SqlParameter("@nn", System.Data.SqlDbType.VarChar, 64));
            editIngredientCommand.Parameters[1].Value = "";
            editIngredientCommand.Parameters.Add(new SqlParameter("@ver", System.Data.SqlDbType.Int));
            editIngredientCommand.Parameters[2].Value = 1;

            editIngredientCommand.Prepare();

            statements.Add(("editIngredient", editIngredientCommand));

            /* DELETE INGREDIENTS */

            var deleteIngredientsCommand = new SqlCommand("exec delete_ingredient @id = @o", sqlConnection);
            var cmd = deleteIngredientsCommand;
            cmd.Parameters.Add(new SqlParameter("@o", System.Data.SqlDbType.Int));
            cmd.Parameters[0].Value = 0;

            cmd.Prepare();

            statements.Add(("deleteIngredient", cmd));

            /* ADD DISH */

            var addDishCommand = new SqlCommand("exec add_dish @name = @n, @description = @o", sqlConnection);
            cmd = addDishCommand;
            cmd.Parameters.Add(new SqlParameter("@n", System.Data.SqlDbType.VarChar, 64));
            cmd.Parameters[0].Value = 0;
            cmd.Parameters.Add(new SqlParameter("@o", System.Data.SqlDbType.VarChar, 8000));
            cmd.Parameters[1].Value = 0;

            cmd.Prepare();

            statements.Add(("addDish", cmd));

            /* EDIT DISH */

            var editDishCommand = new SqlCommand("exec edit_dish @id = @i, @name = @n, @description = @de, @version = @ver", sqlConnection);
            cmd = editDishCommand;
            cmd.Parameters.Add(new SqlParameter("@i", System.Data.SqlDbType.Int, 64));
            cmd.Parameters[0].Value = 0;
            cmd.Parameters.Add(new SqlParameter("@n", System.Data.SqlDbType.VarChar, 8000));
            cmd.Parameters[1].Value = 0;
            cmd.Parameters.Add(new SqlParameter("@de", System.Data.SqlDbType.VarChar, 8000));
            cmd.Parameters[2].Value = 0;
            cmd.Parameters.Add(new SqlParameter("@ver", System.Data.SqlDbType.Int));
            cmd.Parameters[3].Value = 1;

            cmd.Prepare();

            statements.Add(("editDish", cmd));

            /* REMOVE DISH */

            var remDishCommand = new SqlCommand("exec delete_dish @id = @i", sqlConnection);
            cmd = remDishCommand;
            cmd.Parameters.Add(new SqlParameter("@n", System.Data.SqlDbType.VarChar, 64));
            cmd.Parameters[0].Value = 0;

            cmd.Prepare();

            statements.Add(("remDish", cmd));

            /* SHOW DISH INGREDIENTS */

            var showDishIngredientsCommand = new SqlCommand("exec show_dish_ingredients @id = @o", sqlConnection);
            cmd = showDishIngredientsCommand;
            cmd.Parameters.Add(new SqlParameter("@o", System.Data.SqlDbType.Int));
            cmd.Parameters[0].Value = 0;

            cmd.Prepare();

            statements.Add(("showDishIngredients", cmd));

            /* DELETE DISH INGREDIENTS */

            var deleteDishIngredientsCommand = new SqlCommand("exec delete_dish_ingredient @Dish_ID = @o, @Ingredient_ID = @a", sqlConnection);
            cmd = deleteDishIngredientsCommand;
            cmd.Parameters.Add(new SqlParameter("@o", System.Data.SqlDbType.Int));
            cmd.Parameters[0].Value = 0;
            cmd.Parameters.Add(new SqlParameter("@a", System.Data.SqlDbType.Int));
            cmd.Parameters[1].Value = 0;

            cmd.Prepare();

            statements.Add(("deleteDishIngredients", cmd));

            /* ADD INGREDIENT TO DISH */

            var addIngredientToDishCommand = new SqlCommand("exec add_ingredient_to_recipe @Dish_ID = @di, @Ingredient_ID = @ii, @amount = @a, @unitID = @u", sqlConnection);
            addIngredientToDishCommand.Parameters.Add(new SqlParameter("@di", System.Data.SqlDbType.Int));
            addIngredientToDishCommand.Parameters[0].Value = 0;
            addIngredientToDishCommand.Parameters.Add(new SqlParameter("@ii", System.Data.SqlDbType.Int));
            addIngredientToDishCommand.Parameters[1].Value = 0;
            addIngredientToDishCommand.Parameters.Add(new SqlParameter("@a", System.Data.SqlDbType.Int));
            addIngredientToDishCommand.Parameters[2].Value = 0;
            addIngredientToDishCommand.Parameters.Add(new SqlParameter("@u", System.Data.SqlDbType.Int));
            addIngredientToDishCommand.Parameters[3].Value = 0;

            addIngredientToDishCommand.Prepare();

            statements.Add(("addIngredientToDish", addIngredientToDishCommand));

            /* CLEAR DISH INGREDIENTS */

            var clearDishingredientsCommand = new SqlCommand("exec clear_dish_ingredients @Dish_ID = @o", sqlConnection);
            cmd = clearDishingredientsCommand;
            cmd.Parameters.Add(new SqlParameter("@o", System.Data.SqlDbType.Int));
            cmd.Parameters[0].Value = 0;

            cmd.Prepare();

            statements.Add(("clearDishIngredient", cmd));

            /* EDIT INGREDIENT OF DISH */

            var editIngredDishCommand = new SqlCommand("exec edit_dish_ingredients @ownDish_ID = @di, @ownIngredient_ID = @ii, @newIngID = @a, @newMeasurement = @u, @newUnit = @nu, @version = @ver", sqlConnection);
            editIngredDishCommand.Parameters.Add(new SqlParameter("@di", System.Data.SqlDbType.Int));
            editIngredDishCommand.Parameters[0].Value = 0;
            editIngredDishCommand.Parameters.Add(new SqlParameter("@ii", System.Data.SqlDbType.Int));
            editIngredDishCommand.Parameters[1].Value = 0;
            editIngredDishCommand.Parameters.Add(new SqlParameter("@a", System.Data.SqlDbType.Int));
            editIngredDishCommand.Parameters[2].Value = 0;
            editIngredDishCommand.Parameters.Add(new SqlParameter("@u", System.Data.SqlDbType.Int));
            editIngredDishCommand.Parameters[3].Value = 0;
            editIngredDishCommand.Parameters.Add(new SqlParameter("@nu", System.Data.SqlDbType.Int));
            editIngredDishCommand.Parameters[4].Value = 0;
            editIngredDishCommand.Parameters.Add(new SqlParameter("@ver", System.Data.SqlDbType.Int));
            editIngredDishCommand.Parameters[5].Value = 1;

            editIngredDishCommand.Prepare();

            statements.Add(("editIngredientOfDish", editIngredDishCommand));
        }

        public static SqlCommand getStatement(string searchTerm)
        {
            var find = statements.FindIndex(n => n.Item1 == searchTerm);

            return statements[find].Item2;
        }
    }
}
