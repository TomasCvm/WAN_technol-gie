using System;
using System.Collections.Generic;
using System.Data;

namespace xcicman_zadanie__WAN
{
    class RipDatabaseTable
    {

        public static DataTable fill_ripDatabase_table(List<RipDatabase> ripDatabase)
        {
            DataTable table = new DataTable();

            table.Columns.Add(" ");
            table.Columns.Add("Network");
            table.Columns.Add("Mask");
            table.Columns.Add("Next hop");
            table.Columns.Add("Interface");
            table.Columns.Add("Metric");
            table.Columns.Add("Time");
            table.Columns.Add("Status");

            int rowsNum = 0;
            foreach (var variable in ripDatabase)
            {
                table.Rows.Add();
                rowsNum++;
                table.Rows[rowsNum - 1][1] = variable.Network;
                table.Rows[rowsNum - 1][2] = variable.Mask;
                table.Rows[rowsNum - 1][3] = variable.Nexthop;
                table.Rows[rowsNum - 1][4] = variable.Intf;
                table.Rows[rowsNum - 1][5] = variable.Metric;
                if(!variable.Update.Equals(DateTime.MaxValue))
                    table.Rows[rowsNum - 1][6] = variable.Update;
                table.Rows[rowsNum - 1][7] = variable.Status;

            }
            return table;
        }
    }
}
