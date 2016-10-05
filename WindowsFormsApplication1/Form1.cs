using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using PcapDotNet.Base;
using PcapDotNet.Core;
using PcapDotNet.Core.Extensions;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.Icmp;
using PcapDotNet.Packets.IpV4;
using xcicman_zadanie__WAN.Properties;
using Timer = System.Timers.Timer;

namespace xcicman_zadanie__WAN
{
    public partial class Form1 : Form
    {
        private Receive _r;
        private FindAdapter _findAdapter = new FindAdapter();
        private HelpForm _help = new HelpForm();
        private ExitForm _exit = new ExitForm();
        PacketDevice _device1, _device2;
        private bool _started;
        private StaticRoutingForm _staticRoutingForm;
        private RipDatabaseForm _ripDatabaseForm;
        private StaticRouting _staticRouting = StaticRouting.Instance;
        private Timer _ripUpdateTimer = new Timer(30000);
        private bool _passiveInterface1;
        private bool _passiveInterface2;


        public Form1()
        {
            InitializeComponent();
            _findAdapter.Find(comboBox1, comboBox2);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void start_Click(object sender, EventArgs e)
        {
            if (_started)
            {
                return;
            }
            _started = true;
            _device1 = ((KeyValuePair<string, LivePacketDevice>)comboBox1.SelectedItem).Value;
            _device2 = ((KeyValuePair<string, LivePacketDevice>)comboBox2.SelectedItem).Value;

            if ((_device1 != null) && (_device2 != null) && (IP1_1.Text != string.Empty) && (IP1_2.Text != string.Empty) &&
                (IP1_3.Text != string.Empty) && (IP1_4.Text != string.Empty) && (IP2_1.Text != string.Empty) &&
                (IP2_2.Text != string.Empty) && (IP2_3.Text != string.Empty) && (IP2_4.Text != string.Empty) &&
                (MASK1_1.Text != string.Empty) && (MASK1_2.Text != string.Empty) && (MASK1_3.Text != string.Empty) &&
                (MASK1_4.Text != string.Empty) && (MASK2_1.Text != string.Empty) && (MASK2_2.Text != string.Empty) &&
                (MASK2_3.Text != string.Empty) && (MASK2_3.Text != string.Empty))
            {
               
                _r = new Receive(richTextBox1, richTextBox2, _device1,
                    _findAdapter.AllDevices.IndexOf((LivePacketDevice) _device1) + 1, _device2,
                    _findAdapter.AllDevices.IndexOf((LivePacketDevice) _device2) + 1, dataGridView1, dataGridView2);

                SetDirectlyConnectedNetworks();
               // staticRouting.startTimer();

                Thread adapter1Thread = new Thread(_r.a1_receive);
                adapter1Thread.IsBackground = true;
                adapter1Thread.Start();
                Thread adapter2Thread = new Thread(_r.a2_receive);
                adapter2Thread.IsBackground = true;
                adapter2Thread.Start();
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                if (radioButton2.Checked) // ak je zvolene dynamicke smerovanie tak sa zapne
                {
                    RipProtocol ripProtocol = new RipProtocol();
                    if (!_passiveInterface1)
                        ripProtocol.RipRequest(_r.Communicator1, _device1, _r.Ip1.ToString());
                    if (!_passiveInterface2)
                        ripProtocol.RipRequest(_r.Communicator2, _device2, _r.Ip2.ToString());

                    _ripUpdateTimer.Elapsed += RipUpdate;
                    _ripUpdateTimer.Enabled = true;
                    _ripUpdateTimer.AutoReset = true;
                }
            }
            else
            {
                MessageBox.Show(Resources.Allert1);
                _started = false;
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
        private void SetDirectlyConnectedNetworks()
        {
            _staticRouting.SetDirectlyConnectedRipDatabase(GetSubnet(_r.Ip1, _r.Mask1), _r.Mask1.ToString(), "1", IPAddress.Parse("0.0.0.0"), DateTime.MaxValue, 0, GetSubnet(_r.Ip2, _r.Mask2), _r.Mask1.ToString(), "2", IPAddress.Parse("0.0.0.0"), DateTime.MaxValue, 0);
            _staticRouting.SetDirectlyConnected(GetSubnet(_r.Ip1, _r.Mask1), _r.Mask1.ToString(), "1", GetSubnet(_r.Ip2, _r.Mask2), _r.Mask2.ToString(), "2");
            
        }

        private void ResetStat_Click(object sender, EventArgs e)
        {
            _r.StatisticsD1I.Clear();
            _r.StatisticsD1O.Clear();
            _r.StatisticsD2I.Clear();
            _r.StatisticsD2O.Clear();
            _r.GetInfo.prefill_statistics(_r.StatisticsD1I);
            _r.GetInfo.prefill_statistics(_r.StatisticsD1O);
            _r.GetInfo.prefill_statistics(_r.StatisticsD2I);
            _r.GetInfo.prefill_statistics(_r.StatisticsD2O);
            dataGridView2.BeginInvoke(new Action(() => dataGridView2.DataSource = _r.StatisticsTable.fill_statistics_table(_r.StatisticsD1I, _r.StatisticsD1O, _r.StatisticsD2I, _r.StatisticsD2O)));
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboBox1.Focused || comboBox1.Text == Resources.chose1)
            {
                return;
            }
            Dictionary<string, LivePacketDevice> copyOfAdapters = _findAdapter.Adapters.ToDictionary(entry => entry.Key, entry => entry.Value);
            copyOfAdapters.Remove(comboBox1.Text);
            string text = comboBox2.Text;
            comboBox2.DataSource = null;
            comboBox2.DataSource = new BindingSource(copyOfAdapters, null);
            comboBox2.DisplayMember = "Key";
            comboBox2.ValueMember = "Value";
            comboBox2.SelectedIndex = get_index(text, copyOfAdapters);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboBox2.Focused || comboBox2.Text == Resources.chose1)
            {
                return;
            }
            Dictionary<string, LivePacketDevice> copyOfAdapters = _findAdapter.Adapters.ToDictionary(entry => entry.Key, entry => entry.Value);
            copyOfAdapters.Remove(comboBox2.Text);
            string text = comboBox1.Text;
            comboBox1.DataSource = null;
            comboBox1.DataSource = new BindingSource(copyOfAdapters, null);
            comboBox1.DisplayMember = "Key";
            comboBox1.ValueMember = "Value";
            comboBox1.SelectedIndex = get_index(text, copyOfAdapters);
        }

        public int get_index(String text, Dictionary<string, LivePacketDevice> copyOfAdapters)
        {
            int index = 0;
            foreach (var pair in copyOfAdapters)
            {
                if (pair.Key == text)
                {
                    return index;
                }
                index++;
            }
            return index;

        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Autor: Tomas Cicman" + Environment.NewLine + "AIS cislo: 60327" + Environment.NewLine + "Odbor: PKSS 4" + Environment.NewLine + "Rocnik: 2015/2016" + Environment.NewLine + "Predmet: WAN");
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_help == null)
            {
                _help = new HelpForm();
                _help.Disposed += help_set_null;
                _help.Show();
                _help.MaximizeBox = false;
                _help.MinimizeBox = false;

            }
            else
            {
                _help.Activate();
            }
        }
        private void help_set_null(object sender, EventArgs e)
        {
            _help = null;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_exit.ShowDialog() == DialogResult.Yes)
            {
                Close();
            }
        }

        private void staticRoutingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_staticRoutingForm == null)
            {
                _staticRoutingForm = new StaticRoutingForm(_staticRouting, _r);
                _staticRoutingForm.Disposed += staticRouting_set_null;
                _staticRoutingForm.Show();
            }
        }
        private void staticRouting_set_null(object sender, EventArgs e)
        {
            _staticRoutingForm = null;
        }

        private void ResetARP_Click(object sender, EventArgs e)
        {
            _r.GetInfo.ArpTable.Clear();
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            
            dataGridView1.BeginInvoke(new Action(() => dataGridView1.DataSource = _r.ArpTable.fill_arp_table(_r.GetInfo.ArpTable)));                                                                               // vypisanie arp tabulky pri prvom spusteni
        }

        private void pingBtn_Click(object sender, EventArgs e)
        {
            if (_r == null)
            {
                _device1 = ((KeyValuePair<string, LivePacketDevice>) comboBox1.SelectedItem).Value;
                _device2 = ((KeyValuePair<string, LivePacketDevice>) comboBox2.SelectedItem).Value;
                string ip1Str = IP1_1.Text + "." + IP1_2.Text + "." + IP1_3.Text + "." + IP1_4.Text;
                    // nacitanie ip adresy z textboxov
                string ip2Str = IP2_1.Text + "." + IP2_2.Text + "." + IP2_3.Text + "." + IP2_4.Text;
                    // nacitanie ip adresy z textboxov
                IPAddress ip1 = IPAddress.Parse(ip1Str); // parsovanie ip adresz zo stringu na IPAddress
                IPAddress ip2 = IPAddress.Parse(ip2Str); // parsovanie ip adresz zo stringu na IPAddress

                if ((pingIP_1.Text != string.Empty) && (pingIP_2.Text != string.Empty) &&
                    (pingIP_3.Text != string.Empty) &&
                    (pingIP_4.Text != string.Empty) && (_device1 != null) && (_device2 != null))
                {

                    IPAddress ip =
                        IPAddress.Parse(pingIP_1.Text + "." + pingIP_2.Text + "." + pingIP_3.Text + "." +
                                        pingIP_4.Text);

                    PacketCommunicator communicator1 = _device1.Open(65536,
                        PacketDeviceOpenAttributes.NoCaptureLocal | PacketDeviceOpenAttributes.Promiscuous, 1000);

                    PacketCommunicator communicator2 = _device2.Open(65536,
                        PacketDeviceOpenAttributes.NoCaptureLocal | PacketDeviceOpenAttributes.Promiscuous, 1000);

                    Sending s = new Sending();
                    s.ArpRequest(ip.GetAddressBytes().AsReadOnly(), ip1.GetAddressBytes().AsReadOnly(), _device1,
                        communicator1, getInfo: _r.GetInfo, statistics: _r.StatisticsD1O);
                    // odoslanie arp requestu na oba porty
                    s.ArpRequest(ip.GetAddressBytes().AsReadOnly(), ip2.GetAddressBytes().AsReadOnly(), _device2,
                        communicator2, _r.GetInfo, _r.StatisticsD2O);

                    communicator1.Dispose();
                    communicator2.Dispose();
                }
                else
                {
                    MessageBox.Show(Resources.Allert2);
                }
            }
            else
            {
                IPAddress ip =
                    IPAddress.Parse(pingIP_1.Text + "." + pingIP_2.Text + "." + pingIP_3.Text + "." +
                                    pingIP_4.Text);
                
                string mac= "";
                string intf = "";
                bool request = true;
                foreach (var routeEntry in _staticRouting.Entries)
                {
                    if (routeEntry.Network.Equals(IPAddress.Parse(GetSubnet(ip, routeEntry.Mask))))
                    {
                        if (routeEntry.MyInterface != null)
                        {
                            if (routeEntry.Nexthop == null)
                            {
                                foreach (var arp in _r.GetInfo.ArpTable)
                                {
                                    if (arp.Key.Equals(ip.ToString()))
                                    {
                                        mac = arp.Value.Item1;
                                        Sending s = new Sending();
                                        Packet echo = IcmpEcho(mac, (routeEntry.MyInterface.Equals("1")) ? _device1 : _device2,
                                            (routeEntry.MyInterface.Equals("1")) ? _r.Ip1 : _r.Ip2, ip);
                                        PacketCommunicator cm = (routeEntry.MyInterface.Equals("1")) ? _r.Communicator1 : _r.Communicator2;
                                        cm.SendPacket(echo);
                                        request = false;
                                        break;
                                    }
                                }
                                intf = routeEntry.MyInterface;
                                break;
                            }
                            // ak nexthop nieje nul musim si ho vyhladat 
                            foreach (var routeEntry2 in _staticRouting.Entries)
                            {
                                if (routeEntry2.Network.Equals(routeEntry.Nexthop))
                                {
                                    foreach (var arp in _r.GetInfo.ArpTable)
                                    {
                                        if (arp.Key.Equals(routeEntry.Nexthop))
                                        {
                                            mac = arp.Value.Item1;
                                            request = false;
                                            Sending s = new Sending();
                                            Packet echo = IcmpEcho(mac, (routeEntry2.MyInterface.Equals("1")) ? _device1 : _device2,
                                                (routeEntry2.MyInterface.Equals("1")) ? _r.Ip1 : _r.Ip2, routeEntry.Nexthop);
                                            PacketCommunicator cm = (routeEntry2.MyInterface.Equals("1")) ? _r.Communicator1 : _r.Communicator2;
                                            cm.SendPacket(echo);
                                            break;
                                        }
                                    }
                                    intf = routeEntry2.MyInterface;
                                    break;
                                }
                            }
                            break;
                        }
                        // ak my interface je null tak je tma nexthop a musim si ho vyhladat
                        foreach (var routeEntry2 in _staticRouting.Entries)
                        {
                            if (routeEntry2.Network.Equals(routeEntry.Nexthop))
                            {
                                foreach (var arp in _r.GetInfo.ArpTable)
                                {
                                    if (arp.Key.Equals(routeEntry.Nexthop))
                                    {
                                        mac = arp.Value.Item1;
                                        request = false;
                                        Sending s = new Sending();
                                        Packet echo = IcmpEcho(mac, (routeEntry2.MyInterface.Equals("1")) ? _device1 : _device2,
                                            (routeEntry2.MyInterface.Equals("1")) ? _r.Ip1 : _r.Ip2, routeEntry.Nexthop);
                                        PacketCommunicator cm = (routeEntry2.MyInterface.Equals("1")) ? _r.Communicator1 : _r.Communicator2;
                                        cm.SendPacket(echo);
                                        break;
                                    }
                                }
                                intf = routeEntry2.MyInterface;
                                break;
                            }
                        }
                        break;
                    }
                }
                if (request)
                {
                    Sending s = new Sending();
                    if (intf.Equals("1"))
                    {
                        s.ArpRequest(ip.GetAddressBytes().AsReadOnly(), _r.Ip1.GetAddressBytes().AsReadOnly(), _device1,
                            _r.Communicator1, _r.GetInfo, _r.StatisticsD1O);
                    }
                    else
                    {
                        if (intf.Equals("2"))
                        {
                            s.ArpRequest(ip.GetAddressBytes().AsReadOnly(), _r.Ip2.GetAddressBytes().AsReadOnly(),
                                _device2,
                                _r.Communicator2, _r.GetInfo, _r.StatisticsD2O);
                        }
                    }
                }
            }
        }

        private Packet IcmpEcho(string destMac, PacketDevice device, IPAddress sourceIp, IPAddress destinationIp)
        {
            string macAdapter = ((LivePacketDevice)device).GetMacAddress().ToString();

            Packet newPacket = PacketBuilder.Build(DateTime.Now,
                                                new EthernetLayer
                                                {
                                                    Source = new MacAddress(macAdapter),
                                                    Destination = new MacAddress(destMac),
                                                    EtherType = EthernetType.None
                                                },
                                                new IpV4Layer
                                                {
                                                    Source = new IpV4Address(sourceIp.ToString()),
                                                    CurrentDestination = new IpV4Address(destinationIp.ToString()),
                                                    Fragmentation = IpV4Fragmentation.None,
                                                    HeaderChecksum = null, // Will be filled automatically.
                                                    Identification = 123,
                                                    Options = IpV4Options.None,
                                                    Protocol = null, // Will be filled automatically.
                                                    Ttl = 100,
                                                    TypeOfService = 0
                                                },
                                                new IcmpEchoLayer
                                                {
                                                    Checksum = null,
                                                    Identifier = 456,
                                                    SequenceNumber = 800
                                                });
            return newPacket;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_r == null)
                return;
            _r.Ip1 = IPAddress.Parse(IP1_1.Text + "." + IP1_2.Text + "." + IP1_3.Text + "." + IP1_4.Text);
            SetDirectlyConnectedNetworks();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (_r == null)
                return;
            _r.Ip2 = IPAddress.Parse(IP2_1.Text + "." + IP2_2.Text + "." + IP2_3.Text + "." + IP2_4.Text);
            SetDirectlyConnectedNetworks();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _r.Mask1 = IPAddress.Parse(MASK1_1.Text + "." + MASK1_2.Text + "." + MASK1_3.Text + "." + MASK1_4.Text);
            SetDirectlyConnectedNetworks();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _r.Mask2 = IPAddress.Parse(MASK2_1.Text + "." + MASK2_2.Text + "." + MASK2_3.Text + "." + MASK2_4.Text);
            SetDirectlyConnectedNetworks();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int time = Convert.ToInt32(textBox1.Text);
            _r.GetInfo.ArpTimer = time;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked && _r != null)
            {
                RipProtocol ripProtocol = new RipProtocol();
                if (!_passiveInterface1)
                    ripProtocol.RipRequest(_r.Communicator1, _device1, _r.Ip1.ToString());
                if (!_passiveInterface2)
                    ripProtocol.RipRequest(_r.Communicator2, _device2, _r.Ip2.ToString());

                _ripUpdateTimer.Elapsed += RipUpdate;
                _ripUpdateTimer.Enabled = true;
                _ripUpdateTimer.AutoReset = true;
                _r.RipEnabled = true;
            }
        }

        private void RipUpdate(Object source, ElapsedEventArgs e)
        {
            RipProtocol ripProtocol = new RipProtocol();
            if (!_passiveInterface1)
                ripProtocol.RipResponse(_r.Communicator1, _device1, _r.Ip1.ToString());
            if (!_passiveInterface2)
                ripProtocol.RipResponse(_r.Communicator2, _device2, _r.Ip2.ToString());
        }

        private void rIPDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ripDatabaseForm = new RipDatabaseForm();
            _ripDatabaseForm.Disposed += RipDatabaseForm_set_null;
            _ripDatabaseForm.Show();

        }

        private void RipDatabaseForm_set_null(object sender, EventArgs e)
        {
            _ripDatabaseForm = null;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                _ripUpdateTimer.Enabled = false;
                _r.RipEnabled = false;
                _staticRouting.RipDatabase.Clear();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (_passiveInterface1)
            {
                _passiveInterface1 = false;
                button6.BackColor = SystemColors.ControlDark;
            }
            else
            {
                _passiveInterface1 = true;
                button6.BackColor = SystemColors.HotTrack;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (_passiveInterface2)
            {
                _passiveInterface2 = false;
                button7.BackColor = SystemColors.ControlDark;
            }
            else
            {
                _passiveInterface2 = true;
                button7.BackColor = SystemColors.HotTrack;
            }
        }

        // metoda pre kontrolu ci do IP a MASK zadavam iba cisla
        private void IP_MASK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
