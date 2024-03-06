using System.Data;
using Microsoft.Data.SqlClient;

namespace SQL
{
    public static class Commands
    {
        public static DataSet recipeGet(string query, SqlConnection connection, SqlCommand cmd = null)
        {
            if (cmd != null)
            {
                using (cmd)
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    return ds;
                }
            } else
            {
                using (SqlCommand cmdRecipe = new SqlCommand(query, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmdRecipe);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    return ds;
                }
            }
            
        }
    }
}
