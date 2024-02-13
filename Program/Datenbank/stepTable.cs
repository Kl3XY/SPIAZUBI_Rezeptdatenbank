﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datenbank
{
    internal class stepTable
    {
        public void edit(int dishID, SqlConnection sqlConnection)
        {
            var stopped = false;
            while (!stopped)
            {
                Console.Clear();
                var showSteps = prepared_statement.getStatement("showSteps");
                showSteps.Parameters[0].Value = dishID;

                query.queryDraw("", sqlConnection, showSteps);

                var qr = query.queryDraw("select * from step", sqlConnection, null, true);

                Program.version = Convert.ToInt32(qr.Tables[0].Rows[0]["ver"].ToString());

                Console.WriteLine("Running on Version: {0}", Program.version);
                Console.WriteLine("Type in the step you want to edit! type c to exit");
               
                var step = Console.ReadLine();

                if (step == "c")
                {

                    Console.Clear();
                    stopped = true;
                    return;
                }

                Console.WriteLine("Enter a new Description");
                var description = Console.ReadLine();


                var editSteps = prepared_statement.getStatement("editStep");
                editSteps.Parameters[0].Value = dishID;
                editSteps.Parameters[1].Value = Convert.ToInt32(step);
                editSteps.Parameters[2].Value = description;
                editSteps.Parameters[3].Value = Program.version;
                editSteps.ExecuteNonQuery();
                Console.Clear();
                             
            }
        }

        public void add(int dishID, SqlConnection sqlConnection)
        {
            var stopped = false;
            while (!stopped)
            {
                Console.Clear();
                var showSteps = prepared_statement.getStatement("showSteps");
                showSteps.Parameters[0].Value = dishID;

                query.queryDraw("", sqlConnection, showSteps);

                Console.WriteLine("Enter a description for the new step! Type a c and enter to exit!");
                var description = Console.ReadLine();

                if (description == "c")
                {
                    Console.Clear();
                    stopped = true;
                    return;
                }

                var editSteps = prepared_statement.getStatement("addStep");
                editSteps.Parameters[0].Value = dishID;
                editSteps.Parameters[1].Value = description;
                editSteps.ExecuteNonQuery();
                Console.Clear();
            }
        }

        public void remove(int dishID, SqlConnection sqlConnection)
        {
            var stopped = false;
            while (!stopped)
            {
                Console.Clear();
                var showSteps = prepared_statement.getStatement("showSteps");
                showSteps.Parameters[0].Value = dishID;

                var q = query.queryDraw("", sqlConnection, showSteps);

                Console.WriteLine("Enter a Step to be deleted! Type a c and enter to exit!");
                var removeStepNumber = Console.ReadLine();

                if (removeStepNumber == "c")
                {
                    Console.Clear();
                    stopped = true;
                    return;
                }

                var editSteps = prepared_statement.getStatement("removeStep");
                editSteps.Parameters[0].Value = dishID;
                editSteps.Parameters[1].Value = Convert.ToInt32(removeStepNumber);
                editSteps.ExecuteNonQuery();
                Console.Clear();
            }
        }

        public void clear(int dishID, SqlConnection sqlConnection)
        {

                Console.Clear();
                var clearSteps = prepared_statement.getStatement("clearStep");
                clearSteps.Parameters[0].Value = dishID;
                clearSteps.ExecuteNonQuery();
                Console.Clear();
                Console.WriteLine("Cleared all entries for this dish!");
                Console.ReadKey();

        }
    }
}
