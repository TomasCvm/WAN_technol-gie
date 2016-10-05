using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace xcicman_zadanie__WAN
{
    public class ArpTable
    {


        public DataTable fill_arp_table(Dictionary<string, Tuple<string,char, DateTimeOffset>> arpTableReceive)
        {
            DataTable table = new DataTable();

            table.Columns.Add("Internet Address");
            table.Columns.Add("Physical Address");
            table.Columns.Add("Type");

            while (table.Rows.Count < arpTableReceive.Count)
            {
                table.Rows.Add();
            }


            int i = 0;
            foreach (var item in arpTableReceive.ToList())
            {
                table.Rows[i][0] = item.Key;
                table.Rows[i][1] = item.Value.Item1;
                if (item.Value.Item2 == 'D')
                {
                    table.Rows[i][2] = "dynamic";
                }
                else
                {
                    table.Rows[i][2] = "static";
                }
                i++;
            }

            return table;
        }
    }
}
