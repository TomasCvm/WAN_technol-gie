using System;
using System.Net;
using System.Windows.Forms;

namespace xcicman_zadanie__WAN
{
    public partial class StaticRoutingForm : Form
    {
        private readonly StaticRouting _staticRoutingClass;
        StaticRoutingTable _sRoutingTable = new StaticRoutingTable();
        private Receive _r;

        public StaticRoutingForm(StaticRouting received, Receive receive)
        {
            InitializeComponent();
            _staticRoutingClass = received;
            _r = receive;
            dataGridView1.DataSource = _sRoutingTable.fill_staticRouting_table(_staticRoutingClass.Entries);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(IP1_1.Text) > 255 || Convert.ToInt32(IP1_2.Text) > 255 ||
                Convert.ToInt32(IP1_3.Text) > 255 || Convert.ToInt32(IP1_4.Text) > 255 ||
                Convert.ToInt32(Mask1.Text) > 255 || Convert.ToInt32(Mask2.Text) > 255 ||
                Convert.ToInt32(Mask3.Text) > 255 || Convert.ToInt32(Mask4.Text) > 255 ||
                Convert.ToInt32(NextHop1.Text) > 255 || Convert.ToInt32(NextHop2.Text) > 255 ||
                Convert.ToInt32(NextHop3.Text) > 255 || Convert.ToInt32(NextHop4.Text) > 255)
            {
                MessageBox.Show("Maximum pre pole IP adresy je hodnota: 255!");
                return;
            }
            if (checkBox1.Checked == false && checkBox2.Checked == false)
            {
                MessageBox.Show("Minimálne jedna z hodnôt \"Next hop\" alebo \"Interface\" musí byť zadaná!");
                return;
            }
            if (checkBox1.Checked && checkBox2.Checked)
            {
                string intf;
                if (comboBox1.Text.Equals("Adapter 1"))
                {
                    intf = "1";
                }
                else
                {
                    intf = "2";
                }
                string ip = GetSubnet(IPAddress.Parse(IP1_1.Text + "." + IP1_2.Text + "." + IP1_3.Text + "." + IP1_4.Text),IPAddress.Parse(Mask1.Text + "." + Mask2.Text + "." + Mask3.Text + "." + Mask4.Text));
                _staticRoutingClass.SetEntries1(ip,
                    Mask1.Text + "." + Mask2.Text + "." + Mask3.Text + "." + Mask4.Text,
                    NextHop1.Text + "." + NextHop2.Text + "." + NextHop3.Text + "." + NextHop4.Text, intf);
                _staticRoutingClass.SortTable();
                dataGridView1.BeginInvoke(
                new Action(
                    () => dataGridView1.DataSource = _sRoutingTable.fill_staticRouting_table(_staticRoutingClass.Entries)));
                return;
            }
            if (checkBox1.Checked)
            {
                bool set = false;
                foreach (var variable in _staticRoutingClass.Entries)
                {
                    if (variable.Network.Equals(IPAddress.Parse(GetSubnet(IPAddress.Parse(NextHop1.Text + "." + NextHop2.Text + "." + NextHop3.Text + "." +
                                            NextHop4.Text), IPAddress.Parse(Mask1.Text + "." + Mask2.Text + "." + Mask3.Text + "." + Mask4.Text)))))
                    
                    {
                        set = true;
                    }
                }
                if (set == false)
                {
                    //MessageBox.Show("Zadany next hop sa nenachadza v tabulke");
                    return;
                }
                string ip = GetSubnet(IPAddress.Parse(IP1_1.Text + "." + IP1_2.Text + "." + IP1_3.Text + "." + IP1_4.Text), IPAddress.Parse(Mask1.Text + "." + Mask2.Text + "." + Mask3.Text + "." + Mask4.Text));
                _staticRoutingClass.SetEntries2(ip,
                    Mask1.Text + "." + Mask2.Text + "." + Mask3.Text + "." + Mask4.Text,
                    NextHop1.Text + "." + NextHop2.Text + "." + NextHop3.Text + "." + NextHop4.Text);
                _staticRoutingClass.SortTable();
                dataGridView1.BeginInvoke(
                new Action(
                    () => dataGridView1.DataSource = _sRoutingTable.fill_staticRouting_table(_staticRoutingClass.Entries)));
                return;
            }
            if (checkBox2.Checked)
            {
                string intf;
                if (comboBox1.Text.Equals("Adapter 1"))
                {
                    intf = "1";
                }
                else
                {
                    intf = "2";
                }
                string ip = GetSubnet(IPAddress.Parse(IP1_1.Text + "." + IP1_2.Text + "." + IP1_3.Text + "." + IP1_4.Text), IPAddress.Parse(Mask1.Text + "." + Mask2.Text + "." + Mask3.Text + "." + Mask4.Text));
                _staticRoutingClass.SetEntries3(ip,
                    Mask1.Text + "." + Mask2.Text + "." + Mask3.Text + "." + Mask4.Text, intf);
                _staticRoutingClass.SortTable();
                dataGridView1.BeginInvoke(
                new Action(
                    () => dataGridView1.DataSource = _sRoutingTable.fill_staticRouting_table(_staticRoutingClass.Entries)));
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
                return;
            int i = dataGridView1.SelectedCells[0].RowIndex;                                     //get the Row Index             
            DataGridViewRow row = dataGridView1.Rows[i];
            RoutingTable rt = new RoutingTable();
            rt.Network = IPAddress.Parse(row.Cells[1].Value.ToString());
            rt.Mask = IPAddress.Parse(row.Cells[2].Value.ToString());
            if(row.Cells[3].Value.ToString()!="")
            rt.Nexthop = IPAddress.Parse(row.Cells[3].Value.ToString());
            if(row.Cells[4].Value.ToString()!="")
            rt.MyInterface = row.Cells[4].Value.ToString();
            int j = 0;
            foreach (var variable in _staticRoutingClass.Entries)
            {
                if (variable.Nexthop != null && rt.Nexthop != null && variable.MyInterface != null &&
                    rt.MyInterface != null)
                {
                    if (variable.Network.Equals(rt.Network) && variable.Mask.Equals(rt.Mask) &&
                        variable.Nexthop.Equals(rt.Nexthop) && variable.MyInterface.Equals(rt.MyInterface) && (!variable.Flag.Equals("C") || !variable.Flag.Equals("R")))
                    {
                        _staticRoutingClass.Entries.RemoveAt(j);
                        _staticRoutingClass.SortTable();
                        j = 0;
                        dataGridView1.BeginInvoke(new Action(() => dataGridView1.DataSource = _sRoutingTable.fill_staticRouting_table(_staticRoutingClass.Entries)));
                        break;
                    }
                }
                if ((variable.Nexthop == null && rt.Nexthop == null) && (variable.MyInterface != null && rt.MyInterface != null))
                {
                    if (variable.Network.Equals(rt.Network) && variable.Mask.Equals(rt.Mask) &&
                        variable.MyInterface.Equals(rt.MyInterface) && (!variable.Flag.Equals("C") || !variable.Flag.Equals("R")))
                    {
                        _staticRoutingClass.Entries.RemoveAt(j);
                        _staticRoutingClass.SortTable();
                        j = 0;
                        dataGridView1.BeginInvoke(new Action(() => dataGridView1.DataSource = _sRoutingTable.fill_staticRouting_table(_staticRoutingClass.Entries)));
                        break;
                    }
                }
                if ((variable.MyInterface == null && rt.MyInterface == null) && (variable.Nexthop != null && rt.Nexthop != null))
                {
                    if (variable.Network.Equals(rt.Network) && variable.Mask.Equals(rt.Mask) &&
                        variable.Nexthop.Equals(rt.Nexthop) && (!variable.Flag.Equals("C") || !variable.Flag.Equals("R")))
                    {
                        _staticRoutingClass.Entries.RemoveAt(j);
                        _staticRoutingClass.SortTable();
                        j = 0;
                        dataGridView1.BeginInvoke(new Action(() => dataGridView1.DataSource = _sRoutingTable.fill_staticRouting_table(_staticRoutingClass.Entries)));
                        break;
                    }
                }
                j++;
            }
        }
        private void IP_MASK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        public static string GetSubnet(IPAddress address, IPAddress subnetMask)
        {
            byte[] ipAdressBytes1 = address.GetAddressBytes();
            byte[] subnetMaskBytes1 = subnetMask.GetAddressBytes();
            byte[] broadcastAddress1 = new byte[ipAdressBytes1.Length];
            for (int i = 0; i < broadcastAddress1.Length; i++)
            {
                broadcastAddress1[i] = (byte)(ipAdressBytes1[i] & (subnetMaskBytes1[i]));
            }

            return new IPAddress(broadcastAddress1).ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                NextHop1.Enabled = true;
                NextHop2.Enabled = true;
                NextHop3.Enabled = true;
                NextHop4.Enabled = true;
            }
            else
            {
                NextHop1.Enabled = false;
                NextHop2.Enabled = false;
                NextHop3.Enabled = false;
                NextHop4.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _staticRoutingClass.Entries.Clear();
            _staticRoutingClass.SetDirectlyConnected(GetSubnet(_r.Ip1, _r.Mask1), _r.Mask1.ToString(), "1", GetSubnet(_r.Ip2, _r.Mask2), _r.Mask2.ToString(), "2");
            dataGridView1.DataSource = _sRoutingTable.fill_staticRouting_table(_staticRoutingClass.Entries); 
        }
    }
}
