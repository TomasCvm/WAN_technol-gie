using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace xcicman_zadanie__WAN
{
    public class StatisticsTable
    {
        public DataTable fill_statistics_table(Dictionary<string, int> d1I, Dictionary<string, int> d1O, Dictionary<string, int> d2I, Dictionary<string, int> d2O)
        {
            DataTable table = new DataTable();

            table.Columns.Add(" ");
            table.Columns.Add("IN-1");
            table.Columns.Add("OUT-1");
            table.Columns.Add("  ");
            table.Columns.Add("IN-2");
            table.Columns.Add("OUT-2");

            while (table.Rows.Count < d1I.Count)
            {
                table.Rows.Add();
            }

            table.Rows[0][0] = "Ethernet II";
            //table.Rows[1][0] = "802.3/LLC";
           // table.Rows[2][0] = "802.3/SNAP";
            //table.Rows[3][0] = "802.3/RAW";
            table.Rows[1][0] = "ARP";
            table.Rows[2][0] = "ICMP";
            table.Rows[3][0] = "IP";
            table.Rows[4][0] = "TCP";
            table.Rows[5][0] = "UDP";

            int i = 0;
            foreach (var item in d1I.ToList())
            {
                table.Rows[i][1] = item.Value.ToString();
                i++;
            }

            i = 0;
            foreach (var item in d1O.ToList())
            {
                table.Rows[i][2] = item.Value;
                i++;
            }

            i = 0;
            foreach (var item in d2I.ToList())
            {
                table.Rows[i][4] = item.Value;
                i++;
            }

            i = 0;
            foreach (var item in d2O.ToList())
            {
                table.Rows[i][5] = item.Value;
                i++;
            }

            return table;
        }
    }
}
