using System.Data;
using Microsoft.Data.SqlClient;

namespace SQL
{
    public static class Commands
    {
        public static DataSet recipeGet(string query, SqlConnection connection, SqlCommand cmd = null)
        {
            if (cmd == null)
            {
                cmd = new SqlCommand(query, connection);
            }
            using (cmd)
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds;
            }

        }
    }
}
