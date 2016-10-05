using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Transport;
using Timer = System.Timers.Timer;

namespace xcicman_zadanie__WAN
{
    public class GetInfo
    {
        
        private string _sourceMac = "";                                                                                      // MAC adresa z ethernet datagramu
        MacAddress _sourceMacByte;                                                                                         // MAC adresa z ethernetu datagramu (rovna sa tej z ethernetu) - aby nebola potrebna konverzia zo stringu
        private string _destinationMac = "";
        MacAddress _destinationMacByte;
        ReadOnlyCollection<byte> _senderMacByte;                                            // MAC adresa z arp aby nebola potrebna konverzia zo stringu
        private string _senderIp = "";                                                                                      // IP adresa v tvare string
        ReadOnlyCollection<byte> _senderIpByte;                                             // IP adresa v tvate byte - aby nebola potrebna konverzia zo stringu
        private string _targetIp = "";
        ReadOnlyCollection<byte> _targetIpByte;
        public static Dictionary<string, Tuple<int, int, DateTimeOffset>> MacTable = new Dictionary<string, Tuple<int, int, DateTimeOffset>>();
        private static Dictionary<string, Tuple<string, char, DateTimeOffset>> _arpTable = new Dictionary<string, Tuple<string, char, DateTimeOffset>>();
        Dictionary<string, int> _statistics = new Dictionary<string, int>();
        private static DataGridView _dataGridView;
        private static ArpTable _arp = new ArpTable();                                                               // premenna typu triedy Arp_table
        private int _sourcePort;                                                                                            // premenna pre ulozenie zdrojoveho portu v TCP/UDP 
        private int _destinationPort;                                                                                       // premenna pre ulozenie cieloveho portu v TCP/UDP 
        private string _actualType = "";
        private static int _arpTimer = 120;
        private readonly Timer _timer = new Timer(5000);

        public GetInfo(DataGridView datagridreceive)
        {
            _dataGridView = datagridreceive;
            _timer.Elapsed += OnTimedEvent;
            _timer.Enabled = true;
            _timer.AutoReset = true;
        }

        public void find_data(Packet packet)
        {
            if (packet.DataLink.Kind == DataLinkKind.Ethernet)                                                              // kontrola ci je to ethernet ramec
            {
                if (packet.Ethernet.EtherType == EthernetType.Arp)                                                          // kontrola ci je to ARP
                {
                    _destinationMac = packet.Ethernet.Destination.ToString();
                    _sourceMac = packet.Ethernet.Source.ToString();
                    _sourceMacByte = packet.Ethernet.Source; 
                    _senderIp = packet.Ethernet.Arp.SenderProtocolAddress[0] + "." + packet.Ethernet.Arp.SenderProtocolAddress[1] + "." + packet.Ethernet.Arp.SenderProtocolAddress[2] + "." + packet.Ethernet.Arp.SenderProtocolAddress[3];
                    _senderIpByte = packet.Ethernet.Arp.SenderProtocolAddress;
                    _senderMacByte = packet.Ethernet.Arp.SenderHardwareAddress;
                    _targetIp = packet.Ethernet.Arp.TargetProtocolAddress[0] + "." + packet.Ethernet.Arp.TargetProtocolAddress[1] + "." + packet.Ethernet.Arp.TargetProtocolAddress[2] + "." + packet.Ethernet.Arp.TargetProtocolAddress[3];
                    _targetIpByte = packet.Ethernet.Arp.TargetProtocolAddress;
                    _actualType = "ARP";
                   // Subnet = ReturnSubnetmask(SenderIp);
                }
                else
                {
                    if (packet.Ethernet.EtherType == EthernetType.IpV4)                                              // kontrola ci je to IP 
                    {
                        
                        _destinationMac = packet.Ethernet.Destination.ToString();
                        _sourceMac = packet.Ethernet.Source.ToString();
                        _senderIp = packet.Ethernet.IpV4.Source.ToString();
                        _targetIp = packet.Ethernet.IpV4.Destination.ToString();

                        UdpDatagram udp = packet.Ethernet.IpV4.Udp;
                        _sourcePort = udp.SourcePort;
                        _destinationPort = udp.DestinationPort;
                        _actualType = "IP";

                        switch (packet.Ethernet.IpV4.Protocol)
                        {
                            case IpV4Protocol.InternetControlMessageProtocol:                                               // kontrola ci je to ICMP
                                _actualType = "ICMP";
                                break;
                            case IpV4Protocol.Tcp:                                                                          // kontrola ci je to TCP
                                _actualType = "TCP";
                                break;
                            case IpV4Protocol.Udp:                                                                          // kontrola ci je to UDP
                                _actualType = "UDP";
                                break;
                        }
                    }
                }
            }
        }

        public void get_statistics(Packet packet, Dictionary<string, int> statisticsReceives)
        {
            _statistics = statisticsReceives;
            if (packet.DataLink.Kind == DataLinkKind.Ethernet)                                                              // kontrola ci je to ethernet ramec
            {
                _statistics["Ethernet II"]++;
                if (packet.Ethernet.EtherType == EthernetType.Arp)                                                          // kontrola ci je to ARP
                {
                    _statistics["ARP"]++;
                }
                else
                    if (packet.Ethernet.EtherType == EthernetType.IpV4)                                                     // kontrola ci je to IP 
                    {
                        switch (packet.Ethernet.IpV4.Protocol)
                        {
                            case IpV4Protocol.InternetControlMessageProtocol:                                               // kontrola ci je to ICMP
                                if (_statistics.ContainsKey("ICMP"))
                                {
                                    _statistics["ICMP"]++;
                                }
                                else
                                    break;
                                break;
                            case IpV4Protocol.Tcp:                                                                          // kontrola ci je to TCP
                                if (_statistics.ContainsKey("TCP"))
                                {
                                    _statistics["TCP"]++;
                                }
                                else
                                    break;
                                break;
                            case IpV4Protocol.Udp:                                                                          // kontrola ci je to UDP
                                if (_statistics.ContainsKey("UDP"))
                                {
                                    _statistics["UDP"]++;
                                }
                                else
                                    break;
                                break;
                        }
                }
            }
        }

        public void prefill_statistics(Dictionary<string, int> statisticsReceive)
        {
            statisticsReceive.Add("Ethernet II", 0);
            //statistics_receive.Add("802.3/LLC", 0);
            //statistics_receive.Add("802.3/SNAP", 0);
            //statistics_receive.Add("802.3/RAW", 0);
            statisticsReceive.Add("ARP", 0);
            statisticsReceive.Add("ICMP", 0);
            statisticsReceive.Add("IP", 0);
            statisticsReceive.Add("TCP", 0);
            statisticsReceive.Add("UDP", 0);
        }
   
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            DateTimeOffset actual = DateTimeOffset.Now;
            foreach (var item in _arpTable.ToList())
            {
                DateTimeOffset stored = item.Value.Item3;
                double sd = (actual - stored).TotalSeconds;
                if (sd > _arpTimer)
                 {
                     _arpTable.Remove(item.Key);
                     _dataGridView.BeginInvoke(new Action(() => _dataGridView.DataSource = _arp.fill_arp_table(_arpTable)));
                     Console.WriteLine("Odstranene: " + item.Key + "\n \r");
                 }
            }
        }


        public string ActualType
        {
            get{ return _actualType; }
            set{ _actualType = value; }
        }

        public string DestinationMac
        {
            get { return _destinationMac; }
            set { _destinationMac = value; }
        }

        public string SourceMac
        {
            get { return _sourceMac; }
            set { _sourceMac = value; }
        }

        public string SenderIp
        {
            get { return _senderIp; }
            set { _senderIp = value; }
        }

        public string TargetIp
        {
            get { return _targetIp; }
            set { _targetIp = value; }
        }

        public ReadOnlyCollection<byte> SenderIpByte
        {
            get { return _senderIpByte; }
            set { _senderIpByte = value; }
        }
        public ReadOnlyCollection<byte> TargetIpByte
        {
            get { return _targetIpByte; }
            set { _targetIpByte = value; }
        }
        public MacAddress SourceMacByte
        {
            get { return _sourceMacByte; }
            set { _sourceMacByte = value; }
        }
        public ReadOnlyCollection<byte> SenderMacByte
        {
            get { return _senderMacByte; }
            set { _senderMacByte = value; }
        }
        public MacAddress DestinationMacByte
        {
            get { return _destinationMacByte; }
            set { _destinationMacByte = value; }
        }

        public Dictionary<string, Tuple<string, char, DateTimeOffset>> ArpTable
        {
            get
            {
                return _arpTable;
            }

            set
            {
                _arpTable = value;
            }
        }

        public int ArpTimer
        {
            get { return _arpTimer; }
            set { _arpTimer = value; }
        }
    }
}
