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

            var dishSelectCommand = new SqlCommand("exec show_entire_dish_info @id = @dishSelect_ID", sqlConnection);
            dishSelectCommand.Parameters.Add(new SqlParameter("@dishSelect_ID", System.Data.SqlDbType.Int));
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

            var editStepCommand = new SqlCommand("exec edit_step @Dish_ID = @editStep_DishID, @stepNumber = @editStep_StepNumber, @description = @editStep_Description", sqlConnection);
            editStepCommand.Parameters.Add(new SqlParameter("@editStep_DishID", System.Data.SqlDbType.Int));
            editStepCommand.Parameters[0].Value = 0;
            editStepCommand.Parameters.Add(new SqlParameter("@editStep_StepNumber", System.Data.SqlDbType.Int));
            editStepCommand.Parameters[1].Value = 0;
            editStepCommand.Parameters.Add(new SqlParameter("@editStep_Description", System.Data.SqlDbType.VarChar, 8000));
            editStepCommand.Parameters[2].Value = "";
            
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
        }

        public static SqlCommand getStatement(string searchTerm)
        {
            var find = statements.FindIndex(n => n.Item1 == searchTerm);

            return statements[find].Item2;
        }
    }
}
