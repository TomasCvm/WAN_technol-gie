using System.Collections.Generic;
using System.Data;

namespace xcicman_zadanie__WAN
{
    class StaticRoutingTable
    {
        public DataTable fill_staticRouting_table(List<RoutingTable> entries)
        {
            DataTable table = new DataTable();

            table.Columns.Add(" ");
            table.Columns.Add("Network");
            table.Columns.Add("Mask");
            table.Columns.Add("Next hop");
            table.Columns.Add("Interface");
            table.Columns.Add("AD");

            int rowsNum = 0;
            foreach (var variable in entries)
            {
                table.Rows.Add();
                rowsNum++;
                table.Rows[rowsNum - 1][0] = variable.Flag;
                table.Rows[rowsNum - 1][1] = variable.Network;
                table.Rows[rowsNum - 1][2] = variable.Mask;
                table.Rows[rowsNum - 1][3] = variable.Nexthop;
                table.Rows[rowsNum - 1][4] = variable.MyInterface;
                table.Rows[rowsNum - 1][5] = variable.Ad;
            }
            return table;
        }
    }
}
