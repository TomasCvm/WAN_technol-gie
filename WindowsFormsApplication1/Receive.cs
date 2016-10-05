using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using PcapDotNet.Base;
using PcapDotNet.Core;
using PcapDotNet.Core.Extensions;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Arp;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.Icmp;
using PcapDotNet.Packets.IpV4;
using xcicman_zadanie__WAN.Properties;

namespace xcicman_zadanie__WAN
{
    public class Receive
    {
        private static string _packetString;                                                                                        //premenna do ktorej sa ulozi prijaty packet ako string
        private static PacketCommunicator _communicator1, _communicator2;
        private static RichTextBox _richTextBox1, _richTextBox2;
        private static PacketDevice _device1, _device2;                                                                               
        private static DataGridView _dataGridView1;
        private static DataGridView _dataGridView2;
        private static GetInfo _getInfo;                                                                                           // premenna typu truedy Get_Info
        private static StatisticsTable _statisticsTable;                                                   // premenna typu triedy Statistics_table
        private static ArpTable _arpTable;                                                                        // premenna typu triedy Arp_table
        private static Dictionary<string, int> _statisticsD1I;                                     // statistika pre adapter1 v smere in
        private static Dictionary<string, int> _statisticsD1O;                                     // statistika pre adapter1 v smere out
        private static Dictionary<string, int> _statisticsD2I;                                     // statistika pre adapter2 v smere in
        private static Dictionary<string, int> _statisticsD2O;                                     // statistika pre adapter2 v smere out
        private static IPAddress _ip1;
        private static IPAddress _ip2;
        private static IPAddress _mask1;
        private static IPAddress _mask2;
        private static List<Tuple<IPAddress, int, Packet>> _myQueue;
        private static RipProtocol _ripProtocol;
        private static bool _ripEnabled;



        public Receive(RichTextBox richTextBox1Receive, RichTextBox richTextBox2Receive, PacketDevice device1Receive, int d1IndexReceive, PacketDevice device2Receive, int d2IndexReceive, DataGridView dataGridView1Receive, DataGridView dataGridView2Receive)
        {
            _richTextBox1 = richTextBox1Receive;
            _richTextBox2 = richTextBox2Receive;
            _device1 = device1Receive;
            _device2 = device2Receive;
            _dataGridView2 = dataGridView2Receive;
            _getInfo = new GetInfo(dataGridView1Receive);
            _getInfo.prefill_statistics(_statisticsD1I);
            _getInfo.prefill_statistics(_statisticsD1O);
            _getInfo.prefill_statistics(_statisticsD2I);
            _getInfo.prefill_statistics(_statisticsD2O);

            _communicator1 = _device1.Open(65536, PacketDeviceOpenAttributes.NoCaptureLocal | PacketDeviceOpenAttributes.Promiscuous, 1);
            _communicator2 = _device2.Open(65536, PacketDeviceOpenAttributes.NoCaptureLocal | PacketDeviceOpenAttributes.Promiscuous, 1);
            _dataGridView1 = dataGridView1Receive;

            _dataGridView1.BeginInvoke(new Action(() => _dataGridView1.DataSource = _arpTable.fill_arp_table(_getInfo.ArpTable)));                                                                               // vypisanie arp tabulky pri prvom spusteni
            _dataGridView2.BeginInvoke(new Action(() => _dataGridView2.DataSource = _statisticsTable.fill_statistics_table(_statisticsD1I, _statisticsD1O, _statisticsD2I, _statisticsD2O)));                 // vypisanie tabulky statistik pri prvom spusteni

        }

        static Receive()
        {
            _ripEnabled = false;
            _ripProtocol = new RipProtocol();
            _myQueue = new List<Tuple<IPAddress, int, Packet>>();
            _mask2 = IPAddress.Parse("255.255.255.0");
            _mask1 = IPAddress.Parse("255.255.255.0");
            _ip2 = IPAddress.Parse("20.20.20.2");
            _ip1 = IPAddress.Parse("10.10.10.2");
            _statisticsD2O = new Dictionary<string, int>();
            _statisticsD2I = new Dictionary<string, int>();
            _statisticsD1O = new Dictionary<string, int>();
            _statisticsD1I = new Dictionary<string, int>();
            _arpTable = new ArpTable();
            _statisticsTable = new StatisticsTable();
            _richTextBox2 = null;
            _richTextBox1 = null;
        }

//---------------------------------------------------------------------------------------------------------------------------------------------------
        public void a1_receive()
        {
            using (_communicator1)
            {
                // start the capture
                _communicator1.ReceivePackets(0, PacketHandler1);
                _communicator1.Break();
            }
        }
//---------------------------------------------------------------------------------------------------------------------------------------------------
        public void a2_receive()
        {
            using (_communicator2)
            {
                // start the capture
                _communicator2.ReceivePackets(0, PacketHandler2);
                _communicator2.Break();

            }
        }
//---------------------------------------------------------------------------------------------------------------------------------------------------
        public static void PacketHandler1(Packet packet)
        {
            var packetBytes = new byte[packet.Length];
            packetBytes = packet.Buffer;
            _packetString = BitConverter.ToString(packetBytes);
            string[] asdasd = _packetString.Split('-');
            _packetString = string.Join("", asdasd);
            string text = "\r\n" + packet.Timestamp.ToString("yyyy-MM-dd hh:mm:ss.fff") + " length:" + packet.Length +
                          "\r\n" + "\r\n" + _packetString + "\r\n";

            _getInfo.find_data(packet);
            _getInfo.get_statistics(packet, _statisticsD1I);
            _dataGridView2.BeginInvoke(new Action(() => _dataGridView2.DataSource = _statisticsTable.fill_statistics_table(_statisticsD1I, _statisticsD1O, _statisticsD2I, _statisticsD2O)));
            Sending s = new Sending();
                if (
                packet.Ethernet.Destination.Equals(
                    ((LivePacketDevice)_device1).GetMacAddress()))
            {
                if (packet.Ethernet.EtherType.ToString().Equals("IpV4"))
                {
                    //---------------------------------------------------------------------------------------------------------------------------------------------------
                    if (packet.Ethernet.IpV4.Protocol.Equals(IpV4Protocol.InternetControlMessageProtocol) &&
                        packet.Ethernet.IpV4.Destination.ToString().Equals(_ip1.ToString()))
                    {
                        Console.WriteLine(Resources.ICMP___R);
                        int ad = 10000;
                        int i = 0;
                        int selected = -1;
                        if (packet.Ethernet.IpV4.Icmp.MessageType == IcmpMessageType.Echo)
                        {
                            Console.WriteLine(Resources.ICMP___echo);
                            StaticRouting st = StaticRouting.Instance;
                            foreach (var entry in st.Entries)
                            {
                                if (IsInSameSubnet(IPAddress.Parse(_getInfo.SenderIp), entry.Network, entry.Mask))
                                {
                                    if (entry.Ad < ad)
                                    {
                                        ad = entry.Ad;
                                        selected = i;
                                    }
                                }
                            }
                            if (selected != -1)
                            {

                                Console.WriteLine(Resources.ZhodaN);
                                if (st.Entries[selected].MyInterface != null)
                                {
                                    if (st.Entries[selected].MyInterface.Equals("1"))
                                    {
                                        MacAddress destmac;
                                        if (st.Entries[selected].Nexthop != null)
                                        {
                                            if (_getInfo.ArpTable.ContainsKey(st.Entries[selected].Nexthop.ToString()))
                                            {
                                                destmac =
                                                    new MacAddress(
                                                        _getInfo.ArpTable[st.Entries[selected].Nexthop.ToString()].Item1);
                                                s.IcmpReply(packet, _ip1.ToString(), _device1, _communicator1, destmac, _getInfo, _statisticsD1O);
                                                return;
                                            }
                                            s.ArpRequest(
                                                st.Entries[selected].Nexthop.GetAddressBytes().AsReadOnly(),
                                                _ip1.GetAddressBytes().AsReadOnly(), _device1, _communicator1, _getInfo, _statisticsD1O);
                                            return;
                                        }
                                        if (_getInfo.ArpTable.ContainsKey(_getInfo.SenderIp))
                                        {
                                            Console.WriteLine(Resources.ICMP___zhoda_ARP);
                                            destmac = new MacAddress(_getInfo.ArpTable[_getInfo.SenderIp].Item1);
                                            s.IcmpReply(packet, _ip1.ToString(), _device1, _communicator1, destmac, _getInfo, _statisticsD1O);
                                            return;
                                        }
                                        Console.WriteLine(
                                            Resources.ICMP___zhodaN);
                                        s.ArpRequest(
                                            IPAddress.Parse(_getInfo.SenderIp).GetAddressBytes().AsReadOnly(),
                                            _ip1.GetAddressBytes().AsReadOnly(), _device1, _communicator1, _getInfo, _statisticsD1O);
                                        return;
                                    }
                                    else
                                    {
                                        MacAddress destmac;
                                        if (st.Entries[selected].Nexthop != null)
                                        {
                                            if (_getInfo.ArpTable.ContainsKey(st.Entries[selected].Nexthop.ToString()))
                                            {
                                                destmac =
                                                    new MacAddress(
                                                        _getInfo.ArpTable[st.Entries[selected].Nexthop.ToString()].Item1);
                                                s.IcmpReply(packet, _ip2.ToString(), _device2, _communicator2, destmac, _getInfo, _statisticsD2O);
                                                return;
                                            }
                                            s.ArpRequest(
                                                st.Entries[selected].Nexthop.GetAddressBytes().AsReadOnly(),
                                                _ip2.GetAddressBytes().AsReadOnly(), _device2, _communicator2, _getInfo, _statisticsD2O);
                                            return;
                                        }
                                        if (_getInfo.ArpTable.ContainsKey(_getInfo.SenderIp))
                                        {
                                            destmac = new MacAddress(_getInfo.ArpTable[_getInfo.SenderIp].Item1);
                                            s.IcmpReply(packet, _ip2.ToString(), _device2, _communicator2, destmac, _getInfo, _statisticsD2O);
                                            return;
                                        }
                                        s.ArpRequest(
                                            IPAddress.Parse(_getInfo.SenderIp).GetAddressBytes().AsReadOnly(),
                                            _ip2.GetAddressBytes().AsReadOnly(), _device2, _communicator2, _getInfo, _statisticsD2O);
                                        return;
                                    }
                                }
                                if (st.Entries[selected].Nexthop != null)
                                {
                                    IPAddress pom = st.Entries[selected].Nexthop;
                                    foreach (var nextHopentry in st.Entries)
                                    {
                                        if (
                                            IsInSameSubnet(pom, st.Entries[selected].Network,
                                                st.Entries[selected].Mask) &&
                                            nextHopentry.Flag.Equals("C"))
                                        {
                                            if (nextHopentry.MyInterface.Equals("1"))
                                            {
                                                MacAddress destmac;
                                                if (_getInfo.ArpTable.ContainsKey(pom.ToString()))
                                                {
                                                    destmac = new MacAddress(_getInfo.ArpTable[pom.ToString()].Item1);
                                                    s.IcmpReply(packet, _ip1.ToString(), _device1, _communicator1,
                                                        destmac, _getInfo, _statisticsD1O);
                                                    return;
                                                }
                                                s.ArpRequest(pom.GetAddressBytes().AsReadOnly(),
                                                    _ip1.GetAddressBytes().AsReadOnly(), _device1, _communicator1, _getInfo, _statisticsD1O);
                                                return;
                                            }
                                            else
                                            {
                                                MacAddress destmac;
                                                if (_getInfo.ArpTable.ContainsKey(pom.ToString()))
                                                {
                                                    destmac = new MacAddress(_getInfo.ArpTable[pom.ToString()].Item1);
                                                    s.IcmpReply(packet, _ip2.ToString(), _device2, _communicator2,
                                                        destmac, _getInfo, _statisticsD2O);
                                                    return;
                                                }
                                                s.ArpRequest(pom.GetAddressBytes().AsReadOnly(),
                                                    _ip2.GetAddressBytes().AsReadOnly(), _device2, _communicator2, _getInfo, _statisticsD2O);
                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine(Resources.smerovanie);
                        Routing(packet, s);
                    }
                    //---------------------------------------------------------------------------------------------------------------------------------------------------
                }
                else
                {
                    if (packet.Ethernet.EtherType.Equals(EthernetType.Arp))
                    {
                        bool flag = false;

                        if (_getInfo.ArpTable.ContainsKey(_getInfo.SenderIp))
                        // kontrola, ci sa IP odosielatela nachadza v ARP tabulke. Ak ano, prepise sa MAC adresa a zresetuje cas.
                        {
                            _getInfo.ArpTable[_getInfo.SenderIp] =
                                new Tuple<string, char, DateTimeOffset>(_getInfo.SourceMac, 'D', DateTimeOffset.Now);
                            // aktualizovanie zaznamu v ARP tabulke
                            flag = true;
                        }
                        if (packet.Ethernet.Arp.TargetProtocolIpV4Address.Equals(new IpV4Address(_ip1.ToString())))
                        {
                            if (!flag)
                            {
                                add_to_arp_table(); // pridanie zaznamu do ARP tabulky
                            }

                            if (packet.Ethernet.Arp.Operation == ArpOperation.Request)
                            {
                                s.ArpReply(_getInfo.SourceMacByte, _getInfo.SenderMacByte, _getInfo.SenderIpByte,
                                    _ip1.GetAddressBytes().AsReadOnly(), _device1, _communicator1, _getInfo, _statisticsD1O);
                                // odoslanie reply odpovede
                            }
                        }
                        return;
                    }
                }
            }
            //---------------------------------------------------------------------------------------------------------------------------------------------------
            else
            {
                if (_ripEnabled && packet.Ethernet.IpV4.Destination.ToString().Equals("224.0.0.9") && packet.Ethernet.Destination.ToString().Equals("01:00:5E:00:00:09"))
                {
                    if (packet.Ethernet.IpV4.Udp.SourcePort.Equals(520) &&
                        packet.Ethernet.IpV4.Udp.DestinationPort.Equals(520))
                    {
                        PayloadLayer p = (PayloadLayer) packet.Ethernet.IpV4.Udp.Payload.ExtractLayer();
                        if (p.Data[0] == 1 && p.Data[1] == 2)
                        {
                            List<byte[]> entries = _ripProtocol.GetEntriesFromPacket(p);
                            byte[] numArray = new byte[2];
                            Buffer.BlockCopy(entries[0], 0, numArray, 0, 2); // skopirujem si sem AFI
                            Array.Reverse(numArray, 0, numArray.Length);
                            ushort uint16 = BitConverter.ToUInt16(value: new byte[0x2] // tu bude AFI bez nul
                            {
                                numArray[0],
                                numArray[1]
                            }, startIndex: 0);
                            byte[] numArray2 = new byte[4];
                            Buffer.BlockCopy(entries[0], 16, numArray2, 0, 4);
                                // sem si kopirujem metriku
                            Array.Reverse(numArray2, 0, numArray2.Length);
                            uint uint32 = BitConverter.ToUInt32(numArray2, 0); // tu bude metrika bez nul
                            if (entries.Count == 1 && uint16 == 0 && (int) uint32 == 16)
                                // porovnam metriku a AFI a pocet zaznamov ak je zaznam 1, AFI 2 a metrika 16 tak posielam celu databazu
                            {
                                _ripProtocol.RipResponse(_communicator1, _device1, _ip1.ToString());
                            }
                        }
                        else
                        {
                            if (p.Data[0] == 2 && p.Data[1] == 2)
                            {
                                StaticRouting st = StaticRouting.Instance;
                                List<byte[]> entries = _ripProtocol.GetEntriesFromPacket(p);
                                foreach (var entryReceived in entries)
                                {
                                    byte[] networkByte = new byte[4];
                                    byte[] mask = new byte[4];
                                    byte[] metricByte = new byte[4];

                                    Buffer.BlockCopy(entryReceived, 4, networkByte, 0, 4);
                                    Buffer.BlockCopy(entryReceived, 8, mask, 0, 4);
                                    Buffer.BlockCopy(entryReceived, 16, metricByte, 0, 4);
                                    Array.Reverse(metricByte, 0, metricByte.Length);
                                    int metric = BitConverter.ToInt32(metricByte, 0);
                                    IPAddress network = st.GetSubnet(new IPAddress(networkByte), new IPAddress(mask));
                                    bool insertNewNetwork = false;
                                    foreach (var entry in st.RipDatabase)
                                    {
                                        if (entry.Network.Equals(network) && entry.Mask.Equals(new IPAddress(mask)) && metric == 16)
                                        {
                                            if (entry.Update != DateTime.MaxValue && entry.Status.Equals(""))
                                            {
                                                entry.Status = "hold down";
                                                entry.Holddown = DateTime.Now;
                                                int i = 0;
                                                foreach (var routeEntry in st.Entries)
                                                {
                                                    if (routeEntry.Network.Equals(entry.Network) &&
                                                        routeEntry.Mask.Equals(entry.Mask) && routeEntry.Flag.Equals("R"))
                                                    {
                                                        st.Entries.RemoveAt(i);
                                                    }
                                                    i++;
                                                }
                                                _ripProtocol.RipResponse(_communicator1,_device1,_ip1.ToString());
                                                _ripProtocol.RipResponse(_communicator2, _device2, _ip2.ToString());
                                            }
                                            insertNewNetwork = true;
                                        }
                                        else
                                        {
                                            // ak sa zhoduje siet, maska, metrika aj sused tak aktualizujem len casovac
                                            if (entry.Network.Equals(network) && entry.Mask.Equals(new IPAddress(mask)) &&
                                                entry.Metric.Equals(metric) &&
                                                entry.Nexthop.Equals(
                                                    IPAddress.Parse(packet.Ethernet.IpV4.Source.ToString())))
                                            {
                                                // ak je status "" cize siet je v normalnom stave
                                                if (entry.Status.Equals(""))
                                                {
                                                    entry.Update = DateTime.Now;
                                                    entry.Flush = DateTime.Now;
                                                }
                                                else
                                                {
                                                    // ak je status "hold down" cize siet je v stave invalid
                                                    if (entry.Status.Equals("hold down"))
                                                    {
                                                        //ak mi este neprisiela ziadny update pocas hold down stavu
                                                        if (entry.Flush.Equals(entry.Update))
                                                        {
                                                            entry.Flush = DateTime.Now;
                                                            entry.Metric = metric;
                                                        }
                                                    }
                                                }
                                                insertNewNetwork = true;
                                            }
                                            else
                                            {
                                                // ak je metrika v update lepsia  alebo rovnaka
                                                if (entry.Network.Equals(network) && entry.Mask.Equals(new IPAddress(mask)) &&
                                                    entry.Metric >= metric)
                                                {
                                                    // ak je status "" cize siet je v normalnom stave
                                                    if (entry.Status.Equals(""))
                                                    {
                                                        entry.Nexthop =
                                                            IPAddress.Parse(packet.Ethernet.IpV4.Source.ToString());
                                                        entry.Metric = metric;
                                                        entry.Intf = "1";
                                                        entry.Update = DateTime.Now;
                                                        entry.Flush = DateTime.Now;
                                                        foreach (var routeEntry in st.Entries)
                                                        {
                                                            if (routeEntry.Network.Equals(entry.Network) &&
                                                                routeEntry.Mask.Equals(entry.Mask) &&
                                                                routeEntry.Flag.Equals("R"))
                                                            {
                                                                routeEntry.Metric = metric;
                                                                routeEntry.MyInterface = "1";
                                                            }
                                                        }
                                                        st.InsertIntoRoutingTable();
                                                        _ripProtocol.RipResponse(_communicator1, _device1, _ip1.ToString());
                                                        _ripProtocol.RipResponse(_communicator2, _device2, _ip2.ToString());
                                                    }
                                                    else
                                                    {
                                                        // ak je status "hold down" cize siet je v stave invalid
                                                        if (entry.Status.Equals("hold down"))
                                                        {
                                                            entry.Nexthop =
                                                                IPAddress.Parse(packet.Ethernet.IpV4.Source.ToString());
                                                            entry.Metric = metric;
                                                            entry.Intf = "1";
                                                            entry.Update = DateTime.Now;
                                                        }
                                                    }
                                                    insertNewNetwork = true;
                                                }
                                                else
                                                {
                                                    if (entry.Network.Equals(network) && metric > entry.Metric)
                                                    {
                                                        insertNewNetwork = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (!insertNewNetwork && metric != 16)
                                    {
                                        st.SetRipDatabase(st.GetSubnet(network, new IPAddress(mask)).ToString(),
                                            new IPAddress(mask).ToString(), "1",
                                            IPAddress.Parse(packet.Ethernet.IpV4.Source.ToString()), DateTime.Now,
                                            metric);
                                        RoutingTable rt = new RoutingTable();
                                        rt.Network = st.GetSubnet(network, new IPAddress(mask));
                                        rt.Mask = new IPAddress(mask);
                                        rt.Nexthop = IPAddress.Parse(packet.Ethernet.IpV4.Source.ToString());
                                        rt.MyInterface = "1";
                                        rt.Flag = "R";
                                        rt.Ad = 120;
                                        st.Entries.Add(rt);
                                    }
                                    else
                                    {
                                        if (!insertNewNetwork && metric == 16)
                                        {
                                           /* st.setRIPDatabase(st.GetSubnet(network, new IPAddress(mask)).ToString(),
                                                new IPAddress(mask).ToString(), "1",
                                                IPAddress.Parse(packet.Ethernet.IpV4.Source.ToString()), DateTime.Now,
                                                metric);*/
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (packet.Ethernet.Destination.ToString().Equals("FF:FF:FF:FF:FF:FF"))
                    {
                        if (packet.Ethernet.EtherType.Equals(EthernetType.Arp))
                        {
                            Console.WriteLine("ARP FFFF");
                            bool flag = false;

                            if (_getInfo.ArpTable.ContainsKey(_getInfo.SenderIp))
                            // kontrola, ci sa IP odosielatela nachadza v ARP tabulke. Ak ano, prepise sa MAC adresa a zresetuje cas.
                            {
                                _getInfo.ArpTable[_getInfo.SenderIp] =
                                    new Tuple<string, char, DateTimeOffset>(_getInfo.SourceMac, 'D', DateTimeOffset.Now);
                                // aktualizovanie zaznamu v ARP tabulke
                                flag = true;
                            }
                            if (packet.Ethernet.Arp.TargetProtocolIpV4Address.Equals(new IpV4Address(_ip1.ToString())))
                            {
                                if (!flag)
                                {
                                    add_to_arp_table(); // pridanie zaznamu do ARP tabulky
                                }

                                if (packet.Ethernet.Arp.Operation == ArpOperation.Request)
                                {
                                    s.ArpReply(_getInfo.SourceMacByte, _getInfo.SenderMacByte, _getInfo.SenderIpByte,
                                        _ip1.GetAddressBytes().AsReadOnly(), _device1, _communicator1, _getInfo, _statisticsD1O);
                                    // odoslanie reply odpovede
                                }
                            }
                            else
                            {
                                Console.WriteLine("ARP - proxy");
                                IPAddress targetIp = IPAddress.Parse(packet.Ethernet.Arp.TargetProtocolIpV4Address.ToString());
                                StaticRouting st = StaticRouting.Instance;

                                if (st.RoutingArp(targetIp, "1"))
                                {
                                    string macAdapter = ((LivePacketDevice)_device1).GetMacAddress().ToString();
                                    string[] macAdapterParts = macAdapter.Split(':');
                                    Packet newPacket = PacketBuilder.Build(DateTime.Now,
                                                new EthernetLayer
                                                {
                                                    Source = new MacAddress(macAdapter),
                                                    Destination = packet.Ethernet.Source,
                                                    EtherType = EthernetType.None
                                                },
                                                new ArpLayer
                                                {

                                                    ProtocolType = EthernetType.IpV4,
                                                    SenderHardwareAddress = new[] { (byte)Convert.ToInt32(macAdapterParts[0], 16), (byte)Convert.ToInt32(macAdapterParts[1], 16), (byte)Convert.ToInt32(macAdapterParts[2], 16), (byte)Convert.ToInt32(macAdapterParts[3], 16), (byte)Convert.ToInt32(macAdapterParts[4], 16), (byte)Convert.ToInt32(macAdapterParts[5], 16) }.AsReadOnly(),
                                                    SenderProtocolAddress = packet.Ethernet.Arp.TargetProtocolAddress,
                                                    TargetHardwareAddress = packet.Ethernet.Arp.SenderHardwareAddress,
                                                    TargetProtocolAddress = packet.Ethernet.Arp.SenderProtocolAddress,
                                                    Operation = ArpOperation.Reply
                                                });
                                    _communicator1.SendPacket(newPacket);
                                }
                            }
                        }
                    }
                }
            }
            _richTextBox1.BeginInvoke(new Action(() => _richTextBox1.AppendText(text)));
            _dataGridView1.BeginInvoke(new Action(() => _dataGridView1.DataSource = _arpTable.fill_arp_table(_getInfo.ArpTable)));                                                                               
        }

//---------------------------------------------------------------------------------------------------------------------------------------------------        
        public static void PacketHandler2(Packet packet)
        {
            byte[] packetBytes = new byte[packet.Length];
            packetBytes = packet.Buffer;
            _packetString = BitConverter.ToString(packetBytes);
            string[] asdasd = _packetString.Split('-');
            _packetString = string.Join("", asdasd);
            String text = "\r\n" + packet.Timestamp.ToString("yyyy-MM-dd hh:mm:ss.fff") + " length:" + packet.Length + "\r\n" + "\r\n" + _packetString + "\r\n";

            _getInfo.find_data(packet);
            _getInfo.get_statistics(packet, _statisticsD2I);
            _dataGridView2.BeginInvoke(new Action(() => _dataGridView2.DataSource = _statisticsTable.fill_statistics_table(_statisticsD1I, _statisticsD1O, _statisticsD2I, _statisticsD2O)));

            Sending s = new Sending();
            if (
                _getInfo.DestinationMac.Equals(
                    ((LivePacketDevice) _device2).GetMacAddress().ToString()))
            {
            if (packet.Ethernet.EtherType.ToString().Equals("IpV4"))
                {
                    //---------------------------------------------------------------------------------------------------------------------------------------------------
                    if (packet.Ethernet.IpV4.Protocol.Equals(IpV4Protocol.InternetControlMessageProtocol) && _getInfo.TargetIp.Equals(_ip2.ToString()))
                    {
                        if (packet.Ethernet.IpV4.Icmp.MessageType == IcmpMessageType.Echo)
                        {
                            if (packet.Ethernet.IpV4.Icmp.MessageType == IcmpMessageType.Echo)
                            {
                                StaticRouting st = StaticRouting.Instance;
                                int ad = 10000;
                                int i = 0;
                                int selected = -1;
                                foreach (var entry in st.Entries)
                                {
                                    if (IsInSameSubnet(IPAddress.Parse(_getInfo.SenderIp), entry.Network, entry.Mask))
                                    {
                                        if (entry.Ad < ad)
                                        {
                                            ad = entry.Ad;
                                            selected = i;
                                        }
                                    }
                                }
                                if (selected != -1)
                                {
                                    if (st.Entries[selected].MyInterface != null)
                                    {
                                        if (st.Entries[selected].MyInterface.Equals("1"))
                                        {
                                            MacAddress destmac;
                                            if (st.Entries[selected].Nexthop != null)
                                            {
                                                if (
                                                    _getInfo.ArpTable.ContainsKey(
                                                        st.Entries[selected].Nexthop.ToString()))
                                                {
                                                    destmac =
                                                        new MacAddress(
                                                            _getInfo.ArpTable[st.Entries[selected].Nexthop.ToString()]
                                                                .Item1);
                                                    s.IcmpReply(packet, _ip1.ToString(), _device1, _communicator1,
                                                        destmac, _getInfo, _statisticsD1O);
                                                    return;
                                                }
                                                s.ArpRequest(
                                                    st.Entries[selected].Nexthop.GetAddressBytes().AsReadOnly(),
                                                    _ip1.GetAddressBytes().AsReadOnly(), _device1, _communicator1, _getInfo, _statisticsD1O);
                                                return;
                                            }
                                            if (_getInfo.ArpTable.ContainsKey(_getInfo.SenderIp))
                                            {
                                                destmac = new MacAddress(_getInfo.ArpTable[_getInfo.SenderIp].Item1);
                                                s.IcmpReply(packet, _ip1.ToString(), _device1, _communicator1,
                                                    destmac, _getInfo, _statisticsD1O);
                                                return;
                                            }
                                            s.ArpRequest(
                                                IPAddress.Parse(_getInfo.TargetIp)
                                                    .GetAddressBytes()
                                                    .AsReadOnly(),
                                                _ip1.GetAddressBytes().AsReadOnly(), _device1, _communicator1, _getInfo, _statisticsD1O);
                                            return;
                                        }
                                        else
                                        {
                                            MacAddress destmac;
                                            if (st.Entries[selected].Nexthop != null)
                                            {
                                                if (
                                                    _getInfo.ArpTable.ContainsKey(
                                                        st.Entries[selected].Nexthop.ToString()))
                                                {
                                                    destmac =
                                                        new MacAddress(
                                                            _getInfo.ArpTable[st.Entries[selected].Nexthop.ToString()]
                                                                .Item1);
                                                    s.IcmpReply(packet, _ip2.ToString(), _device2, _communicator2,
                                                        destmac, _getInfo, _statisticsD2O);
                                                    return;
                                                }
                                                s.ArpRequest(
                                                    st.Entries[selected].Nexthop.GetAddressBytes().AsReadOnly(),
                                                    _ip2.GetAddressBytes().AsReadOnly(), _device2, _communicator2, _getInfo, _statisticsD2O);
                                                return;
                                            }
                                            if (_getInfo.ArpTable.ContainsKey(_getInfo.SenderIp))
                                            {
                                                destmac = new MacAddress(_getInfo.ArpTable[_getInfo.SenderIp].Item1);
                                                s.IcmpReply(packet, _ip2.ToString(), _device2, _communicator2,
                                                    destmac, _getInfo, _statisticsD2O);
                                                return;
                                            }
                                            s.ArpRequest(
                                                IPAddress.Parse(_getInfo.TargetIp)
                                                    .GetAddressBytes()
                                                    .AsReadOnly(),
                                                _ip2.GetAddressBytes().AsReadOnly(), _device2, _communicator2, _getInfo, _statisticsD2O);
                                            return;
                                        }
                                    }
                                    if (st.Entries[selected].Nexthop != null)
                                    {
                                        IPAddress pom = st.Entries[selected].Nexthop;
                                        foreach (var nextHopentry in st.Entries)
                                        {
                                            if (
                                                IsInSameSubnet(pom, st.Entries[selected].Network,
                                                    st.Entries[selected].Mask) && nextHopentry.Flag.Equals("C"))
                                            {
                                                if (nextHopentry.MyInterface.Equals("1"))
                                                {
                                                    MacAddress destmac;
                                                    if (_getInfo.ArpTable.ContainsKey(pom.ToString()))
                                                    {
                                                        destmac =
                                                            new MacAddress(_getInfo.ArpTable[pom.ToString()].Item1);
                                                        s.IcmpReply(packet, _ip1.ToString(), _device1, _communicator1,
                                                            destmac, _getInfo, _statisticsD1O);
                                                        return;
                                                    }
                                                    s.ArpRequest(pom.GetAddressBytes().AsReadOnly(),
                                                        _ip1.GetAddressBytes().AsReadOnly(), _device1,
                                                        _communicator1, _getInfo, _statisticsD1O);
                                                    return;
                                                }
                                                else
                                                {
                                                    MacAddress destmac;
                                                    if (_getInfo.ArpTable.ContainsKey(pom.ToString()))
                                                    {
                                                        destmac =
                                                            new MacAddress(_getInfo.ArpTable[pom.ToString()].Item1);
                                                        s.IcmpReply(packet, _ip2.ToString(), _device2, _communicator2,
                                                            destmac, _getInfo, _statisticsD2O);
                                                        return;
                                                    }
                                                    s.ArpRequest(pom.GetAddressBytes().AsReadOnly(),
                                                        _ip2.GetAddressBytes().AsReadOnly(), _device2,
                                                        _communicator2, _getInfo, _statisticsD1O);
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Routing(packet, s);
                    }
                    //---------------------------------------------------------------------------------------------------------------------------------------------------
                }
                else
                {
                    if (packet.Ethernet.EtherType.Equals(EthernetType.Arp))
                    {
                      
                        bool flag = false;

                        if (_getInfo.ArpTable.ContainsKey(_getInfo.SenderIp))
                        // kontrola, ci sa IP odosielatela nachadza v ARP tabulke. Ak ano, prepise sa MAC adresa a zresetuje cas.
                        {
                            _getInfo.ArpTable[_getInfo.SenderIp] =
                                new Tuple<string, char, DateTimeOffset>(_getInfo.SourceMac, 'D', DateTimeOffset.Now);
                            // aktualizovanie zaznamu v ARP tabulke
                            flag = true;
                        }
                        if (packet.Ethernet.Arp.TargetProtocolIpV4Address.Equals(new IpV4Address(_ip2.ToString())))
                        {
                            if (!flag)
                            {
                                add_to_arp_table(); // pridanie zaznamu do ARP tabulky
                            }

                            if (packet.Ethernet.Arp.Operation == ArpOperation.Request)
                            {
                                s.ArpReply(_getInfo.SourceMacByte, _getInfo.SenderMacByte, _getInfo.SenderIpByte,
                                    _ip2.GetAddressBytes().AsReadOnly(), _device2, _communicator2, _getInfo, _statisticsD2O);
                                // odoslanie reply odpovede
                            }
                        }
                        return;
                    }
                }
            }
            //---------------------------------------------------------------------------------------------------------------------------------------------------
            else
            {
                if (_ripEnabled && packet.Ethernet.IpV4.Destination.ToString().Equals("224.0.0.9") && packet.Ethernet.Destination.ToString().Equals("01:00:5E:00:00:09"))
                {
                    if (packet.Ethernet.IpV4.Udp.SourcePort.Equals(520) &&
                        packet.Ethernet.IpV4.Udp.DestinationPort.Equals(520))
                    {
                        PayloadLayer p = (PayloadLayer)packet.Ethernet.IpV4.Udp.Payload.ExtractLayer();
                        if (p.Data[0] == 1 && p.Data[1] == 2)
                        {
                            List<byte[]> entries = _ripProtocol.GetEntriesFromPacket(p);
                            byte[] numArray = new byte[2];
                            Buffer.BlockCopy(entries[0], 0, numArray, 0, 2); // skopirujem si sem AFI
                            Array.Reverse(numArray, 0, numArray.Length);
                            ushort uint16 = BitConverter.ToUInt16(new byte[2] // tu bude AFI bez nul
                            {
                                numArray[0],
                                numArray[1]
                            }, 0);
                            byte[] numArray2 = new byte[4];
                            Buffer.BlockCopy(entries[0], 16, numArray2, 0, 4);
                            // sem si kopirujem metriku
                            Array.Reverse(numArray2, 0, numArray2.Length);
                            uint uint32 = BitConverter.ToUInt32(numArray2, 0); // tu bude metrika bez nul
                            if (entries.Count == 1 && uint16 == 0 && (int)uint32 == 16)
                            // porovnam metriku a AFI a pocet zaznamov ak je zaznam 1, AFI 2 a metrika 16 tak posielam celu databazu
                            {
                                _ripProtocol.RipResponse(_communicator2, _device2, _ip2.ToString());
                            }
                        }
                        else
                        {
                            if (p.Data[0] == 2 && p.Data[1] == 2)
                            {
                                StaticRouting st = StaticRouting.Instance;
                                List<byte[]> entries = _ripProtocol.GetEntriesFromPacket(p);
                                foreach (var entryReceived in entries)
                                {
                                    byte[] networkByte = new byte[4];
                                    byte[] mask = new byte[4];
                                    byte[] metricByte = new byte[4];

                                    Buffer.BlockCopy(entryReceived, 4, networkByte, 0, 4);
                                    Buffer.BlockCopy(entryReceived, 8, mask, 0, 4);
                                    Buffer.BlockCopy(entryReceived, 16, metricByte, 0, 4);
                                    Array.Reverse(metricByte, 0, metricByte.Length);
                                    int metric = BitConverter.ToInt32(metricByte, 0);
                                    IPAddress network = st.GetSubnet(new IPAddress(networkByte), new IPAddress(mask));
                                    bool insertNewNetwork = false;
                                    foreach (var entry in st.RipDatabase)
                                    {
                                        if (entry.Network.Equals(network) && entry.Mask.Equals(new IPAddress(mask)) && metric == 16)
                                        {
                                            if (entry.Update != DateTime.MaxValue && entry.Status.Equals(""))
                                            {
                                                entry.Status = "hold down";
                                                entry.Holddown = DateTime.Now;
                                                int i = 0;
                                                foreach (var routeEntry in st.Entries)
                                                {
                                                    if (routeEntry.Network.Equals(entry.Network) &&
                                                        routeEntry.Mask.Equals(entry.Mask) && routeEntry.Flag.Equals("R"))
                                                    {
                                                        st.Entries.RemoveAt(i);
                                                    }
                                                    i++;
                                                }
                                                _ripProtocol.RipResponse(_communicator1, _device1, _ip1.ToString());
                                                _ripProtocol.RipResponse(_communicator2, _device2, _ip2.ToString());
                                            }
                                            insertNewNetwork = true;
                                        }
                                        else
                                        {
                                            // ak sa zhoduje siet, maska, metrika aj sused tak aktualizujem len casovac
                                            if (entry.Network.Equals(network) && entry.Mask.Equals(new IPAddress(mask)) &&
                                                entry.Metric.Equals(metric) &&
                                                entry.Nexthop.Equals(
                                                    IPAddress.Parse(packet.Ethernet.IpV4.Source.ToString())))
                                            {
                                                // ak je status "" cize siet je v normalnom stave
                                                if (entry.Status.Equals(""))
                                                {
                                                    entry.Update = DateTime.Now;
                                                    entry.Flush = DateTime.Now;
                                                }
                                                else
                                                {
                                                    // ak je status "hold down" cize siet je v stave invalid
                                                    if (entry.Status.Equals("hold down"))
                                                    {
                                                        //ak mi este neprisiela ziadny update pocas hold down stavu
                                                        if (entry.Flush.Equals(entry.Update))
                                                        {
                                                            entry.Flush = DateTime.Now;
                                                            entry.Metric = metric;
                                                        }
                                                    }
                                                }
                                                insertNewNetwork = true;
                                            }
                                            else
                                            {
                                                // ak je metrika v update lepsia alebo rovnaka
                                                if (entry.Network.Equals(network) && entry.Mask.Equals(new IPAddress(mask)) && entry.Metric >= metric)
                                                {
                                                    // ak je status "" cize siet je v normalnom stave
                                                    if (entry.Status.Equals(""))
                                                    {
                                                        entry.Nexthop =
                                                            IPAddress.Parse(packet.Ethernet.IpV4.Source.ToString());
                                                        entry.Metric = metric;
                                                        entry.Intf = "2";
                                                        entry.Update = DateTime.Now;
                                                        entry.Flush = DateTime.Now;
                                                        foreach (var routeEntry in st.Entries)
                                                        {
                                                            if (routeEntry.Network.Equals(entry.Network) &&
                                                                routeEntry.Mask.Equals(entry.Mask) && routeEntry.Flag.Equals("R"))
                                                            {
                                                                routeEntry.Metric = metric;
                                                                routeEntry.MyInterface = "2";
                                                            }
                                                        }
                                                        st.InsertIntoRoutingTable();
                                                        _ripProtocol.RipResponse(_communicator1, _device1, _ip1.ToString());
                                                        _ripProtocol.RipResponse(_communicator2, _device2, _ip2.ToString());
                                                    }
                                                    else
                                                    {
                                                        // ak je status "hold down" cize siet je v stave invalid
                                                        if (entry.Status.Equals("hold down"))
                                                        {
                                                            entry.Nexthop =
                                                                IPAddress.Parse(packet.Ethernet.IpV4.Source.ToString());
                                                            entry.Metric = metric;
                                                            entry.Intf = "2";
                                                            entry.Update = DateTime.Now;
                                                        }
                                                    }
                                                    insertNewNetwork = true;
                                                }
                                            }
                                        }
                                    }
                                    if (!insertNewNetwork && metric != 16)
                                    {
                                        st.SetRipDatabase(st.GetSubnet(network, new IPAddress(mask)).ToString(),
                                            new IPAddress(mask).ToString(), "2",
                                            IPAddress.Parse(packet.Ethernet.IpV4.Source.ToString()), DateTime.Now,
                                            metric);
                                        RoutingTable rt = new RoutingTable();
                                        rt.Network = st.GetSubnet(network, new IPAddress(mask));
                                        rt.Mask = new IPAddress(mask);
                                        rt.Nexthop = IPAddress.Parse(packet.Ethernet.IpV4.Source.ToString());
                                        rt.MyInterface = "2";
                                        rt.Flag = "R";
                                        rt.Ad = 120;
                                        st.Entries.Add(rt);
                                    }
                                    else
                                    {
                                        if (!insertNewNetwork && metric == 16)
                                        {
                                            /*st.setRIPDatabase(st.GetSubnet(network, new IPAddress(mask)).ToString(),
                                                new IPAddress(mask).ToString(), "2",
                                                IPAddress.Parse(packet.Ethernet.IpV4.Source.ToString()), DateTime.Now,
                                                metric);*/
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (_getInfo.DestinationMac.Equals("FF:FF:FF:FF:FF:FF"))
                    {
                        if (packet.Ethernet.EtherType.Equals(EthernetType.Arp))
                        {
                            bool flag = false;

                            if (_getInfo.ArpTable.ContainsKey(_getInfo.SenderIp))
                            // kontrola, ci sa IP odosielatela nachadza v ARP tabulke. Ak ano, prepise sa MAC adresa a zresetuje cas.
                            {
                                _getInfo.ArpTable[_getInfo.SenderIp] =
                                    new Tuple<string, char, DateTimeOffset>(_getInfo.SourceMac, 'D', DateTimeOffset.Now);
                                // aktualizovanie zaznamu v ARP tabulke
                                flag = true;
                            }
                            if (packet.Ethernet.Arp.TargetProtocolIpV4Address.Equals(new IpV4Address(_ip2.ToString())))
                            {
                                if (!flag)
                                {
                                    add_to_arp_table(); // pridanie zaznamu do ARP tabulky
                                }

                                if (packet.Ethernet.Arp.Operation == ArpOperation.Request)
                                {
                                    s.ArpReply(_getInfo.SourceMacByte, _getInfo.SenderMacByte, _getInfo.SenderIpByte,
                                        _ip2.GetAddressBytes().AsReadOnly(), _device2, _communicator2, _getInfo, _statisticsD2O);
                                    // odoslanie reply odpovede
                                }
                            }
                            else
                            {
                                IPAddress targetIp = IPAddress.Parse(packet.Ethernet.Arp.TargetProtocolIpV4Address.ToString());
                                StaticRouting st = StaticRouting.Instance;

                                if (st.RoutingArp(targetIp, "2"))
                                {
                                    string macAdapter = ((LivePacketDevice)_device2).GetMacAddress().ToString();
                                    string[] macAdapterParts = macAdapter.Split(':');
                                    Packet newPacket = PacketBuilder.Build(DateTime.Now,
                                                new EthernetLayer
                                                {
                                                    Source = new MacAddress(macAdapter),
                                                    Destination = packet.Ethernet.Source,
                                                    EtherType = EthernetType.None
                                                },
                                                new ArpLayer
                                                {

                                                    ProtocolType = EthernetType.IpV4,
                                                    SenderHardwareAddress = new[] { (byte)Convert.ToInt32(macAdapterParts[0], 16), (byte)Convert.ToInt32(macAdapterParts[1], 16), (byte)Convert.ToInt32(macAdapterParts[2], 16), (byte)Convert.ToInt32(macAdapterParts[3], 16), (byte)Convert.ToInt32(macAdapterParts[4], 16), (byte)Convert.ToInt32(macAdapterParts[5], 16) }.AsReadOnly(),
                                                    SenderProtocolAddress = packet.Ethernet.Arp.TargetProtocolAddress,
                                                    TargetHardwareAddress = packet.Ethernet.Arp.SenderHardwareAddress,
                                                    TargetProtocolAddress = packet.Ethernet.Arp.SenderProtocolAddress,
                                                    Operation = ArpOperation.Reply
                                                });
                                    _communicator2.SendPacket(newPacket);
                                }
                            }
                        }
                    }

                }
            }

            _richTextBox2.BeginInvoke(new Action(() => _richTextBox2.AppendText(text)));
            _dataGridView1.BeginInvoke(new Action(() => _dataGridView1.DataSource = _arpTable.fill_arp_table(_getInfo.ArpTable)));
        }

//------------------------Metoda pre pridanie IP+MASK do ARP tabulky-----------------------------------------------------------------------------------------------------
        public static void add_to_arp_table()
        {
            _getInfo.ArpTable.Add(_getInfo.SenderIp, new Tuple<string, char, DateTimeOffset>(_getInfo.SourceMac, 'D', DateTimeOffset.Now));
        }
//------------------------Metoda pre zistenie ci su IP z rovnakeho subnetu-----------------------------------------------------------------------------------------------
        public static bool IsInSameSubnet(IPAddress address, IPAddress address2, IPAddress subnetMask)
        {
            byte[] ipAdressBytes1 = address.GetAddressBytes();
            byte[] subnetMaskBytes1 = subnetMask.GetAddressBytes();
            byte[] broadcastAddress1 = new byte[ipAdressBytes1.Length];
            for (int i = 0; i < broadcastAddress1.Length; i++)
            {
                broadcastAddress1[i] = (byte) (ipAdressBytes1[i] & (subnetMaskBytes1[i]));
            }


            byte[] ipAdressBytes2 = address2.GetAddressBytes();
            byte[] subnetMaskBytes2 = subnetMask.GetAddressBytes();
            byte[] broadcastAddress2 = new byte[ipAdressBytes2.Length];
            for (int i = 0; i < broadcastAddress1.Length; i++)
            {
                broadcastAddress2[i] = (byte) (ipAdressBytes2[i] & (subnetMaskBytes2[i]));
            }

            return new IPAddress(broadcastAddress1).Equals(new IPAddress(broadcastAddress2));
        }
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static void Routing(Packet packet, Sending s)
        {
            StaticRouting st = StaticRouting.Instance;
            int ad = 10000;
            int i = 0;
            int selected = -1;
            foreach (var entry in st.Entries)
            {
                if (IsInSameSubnet(IPAddress.Parse(_getInfo.TargetIp), entry.Network, entry.Mask))
                {
                    if (entry.Ad < ad)
                    {
                        ad = entry.Ad;
                        selected = i;
                    }
                }
                i++;
            }
            if (selected != -1)
            {
                if (st.Entries[selected].MyInterface != null)
                {
                    if (st.Entries[selected].MyInterface.Equals("1"))
                    {
                        MacAddress destmac;
                        if (st.Entries[selected].Nexthop != null)
                        {
                            if (_getInfo.ArpTable.ContainsKey(st.Entries[selected].Nexthop.ToString()))
                            {
                                destmac =
                                    new MacAddress(_getInfo.ArpTable[st.Entries[selected].Nexthop.ToString()].Item1);
                                s.Send(packet, _communicator1, destmac, _device1, _getInfo, _statisticsD1O);
                            }
                            else
                            {
                                s.ArpRequest(st.Entries[selected].Nexthop.GetAddressBytes().AsReadOnly(),
                                    _ip1.GetAddressBytes().AsReadOnly(), _device1, _communicator1, _getInfo, _statisticsD1O);
                            }
                        }
                        else
                        {
                            if (_getInfo.ArpTable.ContainsKey(_getInfo.TargetIp))
                            {
                                destmac = new MacAddress(_getInfo.ArpTable[_getInfo.TargetIp].Item1);
                                s.Send(packet, _communicator1, destmac, _device1, _getInfo, _statisticsD1O);
                            }
                            else
                            {
                                s.ArpRequest(IPAddress.Parse(_getInfo.TargetIp).GetAddressBytes().AsReadOnly(),
                                    _ip1.GetAddressBytes().AsReadOnly(), _device1, _communicator1, _getInfo, _statisticsD1O);
                            }
                        }
                    }
                    else
                    {
                        MacAddress destmac;
                        if (st.Entries[selected].Nexthop != null)
                        {
                            if (_getInfo.ArpTable.ContainsKey(st.Entries[selected].Nexthop.ToString()))
                            {
                                destmac =
                                    new MacAddress(_getInfo.ArpTable[st.Entries[selected].Nexthop.ToString()].Item1);
                                s.Send(packet, _communicator2, destmac, _device2, _getInfo, _statisticsD2O);
                            }
                            else
                            {
                                s.ArpRequest(st.Entries[selected].Nexthop.GetAddressBytes().AsReadOnly(),
                                    _ip2.GetAddressBytes().AsReadOnly(), _device2, _communicator2, _getInfo, _statisticsD2O);
                            }
                        }
                        else
                        {
                            if (_getInfo.ArpTable.ContainsKey(_getInfo.TargetIp))
                            {
                                destmac = new MacAddress(_getInfo.ArpTable[_getInfo.TargetIp].Item1);
                                s.Send(packet, _communicator2, destmac, _device2, _getInfo, _statisticsD2O);
                            }
                            else
                            {
                                s.ArpRequest(IPAddress.Parse(_getInfo.TargetIp).GetAddressBytes().AsReadOnly(),
                                    _ip2.GetAddressBytes().AsReadOnly(), _device2, _communicator2, _getInfo, _statisticsD2O);
                            }
                        }
                    }
                }
                else
                {
                    if (st.Entries[selected].Nexthop != null)
                    {
                        IPAddress pom = st.Entries[selected].Nexthop;
                        foreach (var nextHopentry in st.Entries)
                        {
                            if (IsInSameSubnet(pom, st.Entries[selected].Network, st.Entries[selected].Mask) &&
                                nextHopentry.Flag.Equals("C"))
                            {
                                if (nextHopentry.MyInterface.Equals("1"))
                                {
                                    MacAddress destmac;
                                    if (_getInfo.ArpTable.ContainsKey(pom.ToString()))
                                    {
                                        destmac = new MacAddress(_getInfo.ArpTable[pom.ToString()].Item1);
                                        s.Send(packet, _communicator1, destmac, _device1, _getInfo, _statisticsD1O);
                                        return;
                                    }
                                    s.ArpRequest(pom.GetAddressBytes().AsReadOnly(),
                                        _ip1.GetAddressBytes().AsReadOnly(), _device1, _communicator1, _getInfo, _statisticsD1O);
                                    return;
                                }
                                else
                                {
                                    MacAddress destmac;
                                    if (_getInfo.ArpTable.ContainsKey(pom.ToString()))
                                    {
                                        destmac = new MacAddress(_getInfo.ArpTable[pom.ToString()].Item1);
                                        s.Send(packet, _communicator2, destmac, _device2, _getInfo, _statisticsD2O);
                                        return;
                                    }
                                    s.ArpRequest(pom.GetAddressBytes().AsReadOnly(),
                                        _ip2.GetAddressBytes().AsReadOnly(), _device2, _communicator2, _getInfo, _statisticsD2O);
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        //----------------------- Getters / Setters -----------------------
        public GetInfo GetInfo
        {
            get { return _getInfo; }
            set { _getInfo = value; }
        }
        public Dictionary<string, int> StatisticsD1I
        {
            get { return _statisticsD1I; }
            set { _statisticsD1I = value; }
        }
        public Dictionary<string, int> StatisticsD1O
        {
            get { return _statisticsD1O; }
            set { _statisticsD1O = value; }
        }
        public Dictionary<string, int> StatisticsD2I
        {
            get { return _statisticsD2I; }
            set { _statisticsD2I = value; }
        }
        public Dictionary<string, int> StatisticsD2O
        {
            get { return _statisticsD2O; }
            set { _statisticsD2O = value; }
        }

        public StatisticsTable StatisticsTable
        {
            get { return _statisticsTable; }
            set { _statisticsTable = value; }
        }

        public ArpTable ArpTable
        {
            get { return _arpTable;  }
            set { _arpTable = value; }
        }

        public PacketCommunicator Communicator1
        {
            get { return _communicator1; }
            set { _communicator1 = value; }
        }
        public PacketCommunicator Communicator2
        {
            get { return _communicator2; }
            set { _communicator2 = value; }
        }

        public IPAddress Ip1
        {
            get { return _ip1; }
            set { _ip1 = value; }
        }
        public IPAddress Ip2
        {
            get { return _ip2; }
            set { _ip2 = value; }
        }

        public IPAddress Mask1
        {
            get { return _mask1; }
            set { _mask1 = value; }
        }

        public IPAddress Mask2
        {
            get { return _mask2; }
            set { _mask2 = value; }
        }

        public bool RipEnabled
        {
            get { return _ripEnabled; }
            set { _ripEnabled = value; }
        }
    }
}
