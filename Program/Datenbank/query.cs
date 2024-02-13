using ConsoleTables;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datenbank
{
    internal static class query
    {
        public static void queryInit(SqlConnection connection)
        {
            while (!Program.isClosingConnection)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.WriteLine();
                Console.WriteLine("->>SQL QUERY DATABASE");
                Console.WriteLine("------------------");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("Query:");
                Console.ForegroundColor = ConsoleColor.Yellow;
                var cmd = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;

                if (cmd.ToString().ToLower() == "c") { Program.input = ""; return; }

                queryDraw(cmd, connection);

                Console.WriteLine("press any key to continue");
                Console.WriteLine("or press c to terminate the connection");
                var key = Console.ReadKey();
                if (key.ToString().ToLower() == "c") { Program.input = ""; return; }

            }
        }

        public static DataSet queryDraw(string query, SqlConnection connection, SqlCommand cmd = null, bool onlyTables = false)
        {
            using (SqlCommand cmd2 = new SqlCommand(query, connection))
            {
                var useCMD = cmd2;
                if (cmd != null) { useCMD = cmd; }
                SqlDataAdapter adapter = new SqlDataAdapter(useCMD);
                DataSet DataSet = new DataSet();
                adapter.Fill(DataSet);
                if (onlyTables) { return DataSet; }
                foreach(DataTable dt in DataSet.Tables)
                {
                    var resultTable = new ConsoleTable();
                    resultTable.Options.EnableCount = false;

                    var collist = new List<string>();
                    var rowlist = new string[dt.Columns.Count];

                    for (var i = 0; i < rowlist.Length; i++)
                        rowlist[i] = "";

                    foreach (DataColumn col in dt.Columns) { collist.Add(col.ColumnName); } //Get & Set Column Names
                    resultTable.AddColumn(collist);

                    foreach (DataRow row in dt.Rows)
                    {
                        rowlist = new string[dt.Columns.Count];
                        for (var i = 0; i < rowlist.Length; i++)
                            rowlist[i] = "";

                        foreach (DataColumn col in dt.Columns)
                        {
                            for (var i = 0; i < rowlist.Length; i++)
                                if (rowlist[i] == "") { rowlist[i] = row[col].ToString(); break; };
                        }

                        resultTable.AddRow(rowlist);
                    }

                    resultTable.Write();
                    Console.WriteLine();
                }
                return DataSet;
            }
        }
    }
}
