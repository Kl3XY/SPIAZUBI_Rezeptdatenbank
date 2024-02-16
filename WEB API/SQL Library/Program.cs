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
    }
}
