using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Timers;

namespace xcicman_zadanie__WAN
{
    public class StaticRouting
    {
        //private Dictionary<IPAddress, Tuple<IPAddress,IPAddress,string>> entries = new Dictionary<IPAddress, Tuple<IPAddress, IPAddress, string>>();
        static readonly StaticRouting _instance = new StaticRouting();
        private List<RoutingTable> _entries = new List<RoutingTable>();
        private List<RipDatabase> _ripDatabase = new List<RipDatabase>();

        private readonly Timer _ripTimer = new Timer(1000);

        public static StaticRouting Instance
        {
            get { return _instance; }
        }

        

        public List<RoutingTable> Entries
        {
            get { return _entries; }
            set { _entries = value; }
        }

        public List<RipDatabase> RipDatabase
        {
            get { return _ripDatabase; }
            set { _ripDatabase = value; }
        }


        public void SetEntries1(string ip, string mask, string hop, string intf)
        {
            RoutingTable rt = new RoutingTable();
            rt.Network = IPAddress.Parse(ip);
            rt.Mask = IPAddress.Parse(mask);
            rt.Nexthop = IPAddress.Parse(hop);
            rt.MyInterface = intf;
            rt.Flag = "S";
            rt.Ad = 1;
            if (!ContainsLoop(_entries, rt))
            {
                _entries.Add(rt);
            }
            else
            {
                foreach (var variable in _entries)
                {
                    if (variable.Network.Equals(IPAddress.Parse(ip)) && variable.Mask.Equals(IPAddress.Parse(mask)))
                    {
                        if (variable.Flag.Equals("R"))
                        {
                            variable.Flag = "S";
                            variable.Ad = 1;
                            variable.MyInterface = intf;
                            variable.Nexthop = IPAddress.Parse(hop);
                        }
                    }
                }
            }
        }

        public void SetEntries2(string ip, string mask, string hop)
        {
            RoutingTable rt = new RoutingTable();
            rt.Network = IPAddress.Parse(ip);
            rt.Mask = IPAddress.Parse(mask);
            rt.Nexthop = IPAddress.Parse(hop);
            rt.Flag = "S";
            rt.Ad = 1;
            if (!ContainsLoop(_entries, rt))
            {
                _entries.Add(rt);
            }
            else
            {
                foreach (var variable in _entries)
                {
                    if (variable.Network.Equals(IPAddress.Parse(ip)) && variable.Mask.Equals(IPAddress.Parse(mask)))
                    {
                        if (variable.Flag.Equals("R"))
                        {
                            variable.Flag = "S";
                            variable.Ad = 1;
                            variable.Nexthop = IPAddress.Parse(hop);
                        }
                    }
                }
            }
        }

        public void SetEntries3(string ip, string mask, string intf)
        {
            RoutingTable rt = new RoutingTable();
            rt.Network = IPAddress.Parse(ip);
            rt.Mask = IPAddress.Parse(mask);
            rt.MyInterface = intf;
            rt.Flag = "S";
            rt.Ad = 1;
            if (!ContainsLoop(_entries, rt))
            {
                _entries.Add(rt);
            }
            else
            {
                foreach (var variable in _entries)
                {
                    if (variable.Network.Equals(IPAddress.Parse(ip)) && variable.Mask.Equals(IPAddress.Parse(mask)))
                    {
                        if (variable.Flag.Equals("R"))
                        {
                            variable.Flag = "S";
                            variable.Ad = 1;
                            variable.MyInterface = intf;
                        }
                    }
                }
            }
        }

        public void SetDirectlyConnected(string ip, string mask, string intf, string ip2, string mask2, string intf2)
        {
            int i = 0;
            foreach (var variable in _entries.ToList())
            {

                if (variable.Flag.Equals("C"))
                {
                    _entries.RemoveAt(i);
                    i--;
                }


                i++;
            }
            i = 0;
            RoutingTable rt = new RoutingTable();
            rt.Network = IPAddress.Parse(ip);
            rt.Mask = IPAddress.Parse(mask);
            rt.MyInterface = intf;
            rt.Flag = "C";
            rt.Ad = 0;
            _entries.Add(rt);
            rt = new RoutingTable();
            rt.Network = IPAddress.Parse(ip2);
            rt.Mask = IPAddress.Parse(mask2);
            rt.MyInterface = intf2;
            rt.Flag = "C";
            rt.Ad = 0;
            _entries.Add(rt);

        }

        public void SetDirectlyConnectedRipDatabase(string ip, string mask, string intf, IPAddress nexthop, DateTime time, int metric, string ip2, string mask2, string intf2, IPAddress nexthop2, DateTime time2, int metric2)
        {
            int i = 0;
            foreach (var variable in _entries.ToList())
            {
                if (variable.Flag.Equals("C"))
                {
                    foreach (var variable2 in _ripDatabase.ToList())
                    {
                        if (variable2.Network.Equals(variable.Network) &&
                            variable2.Mask.Equals(variable.Mask))
                        {
                            _ripDatabase.RemoveAt(i);
                            i--;
                            break;
                        }
                    }
                }


                i++;
            }
            RipDatabase rd = new RipDatabase();
            rd.Network = IPAddress.Parse(ip);
            rd.Mask = IPAddress.Parse(mask);
            rd.Intf = intf;
            rd.Nexthop = nexthop;
            rd.Metric = metric;
            rd.Update = time;
            rd.Status = "";
            _ripDatabase.Add(rd);
            rd = new RipDatabase();
            rd.Network = IPAddress.Parse(ip2);
            rd.Mask = IPAddress.Parse(mask2);
            rd.Intf = intf2;
            rd.Nexthop = nexthop2;
            rd.Metric = metric2;
            rd.Update = time2;
            rd.Status = "";
            _ripDatabase.Add(rd);

        }
        public void SetRipDatabase(string ip, string mask, string intf, IPAddress nexthop, DateTime time, int metric)
        {
            RipDatabase rd = new RipDatabase();
            rd.Status = "";
            rd.Network = IPAddress.Parse(ip);
            rd.Mask = IPAddress.Parse(mask);
            rd.Intf = intf;
            rd.Nexthop = nexthop;
            rd.Metric = metric+1;
            rd.Update = time;
            _ripDatabase.Add(rd);
        }


        static bool ContainsLoop(List<RoutingTable> list, RoutingTable value)
        {
            foreach (RoutingTable t in list)
            {
                if (t.Nexthop != null && value.Nexthop != null && t.MyInterface != null &&
                    value.MyInterface != null)
                {
                    if (t.Flag.Equals(value.Flag) && t.Network.Equals(value.Network) &&
                        t.Mask.Equals(value.Mask) &&
                        t.Nexthop.Equals(value.Nexthop) && t.MyInterface.Equals(value.MyInterface))
                    {
                        return true;
                    }
                }
                if (t.Flag.Equals(value.Flag) && (t.Nexthop == null && value.Nexthop == null) &&
                    (t.MyInterface != null && value.MyInterface != null))
                {
                    if (t.Network.Equals(value.Network) && t.Mask.Equals(value.Mask) &&
                        t.MyInterface.Equals(value.MyInterface))
                    {
                        return true;
                    }
                }
                if (t.Flag.Equals(value.Flag) && (t.MyInterface == null && value.MyInterface == null) &&
                    (t.Nexthop != null && value.Nexthop != null))
                {
                    if (t.Network.Equals(value.Network) && t.Mask.Equals(value.Mask) &&
                        t.Nexthop.Equals(value.Nexthop))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        static bool ContainsLoopRip(List<RipDatabase> list, RipDatabase value)
        {
            for (int i = 0; i < list.Count; i++)
            {

                if (list[i].Nexthop != null && value.Nexthop != null && list[i].Intf != null &&
                    value.Intf != null)
                {
                    if (list[i].Network.Equals(value.Network) &&
                        list[i].Mask.Equals(value.Mask) &&
                        list[i].Nexthop.Equals(value.Nexthop) && 
                        list[i].Metric.Equals(value.Metric) &&
                        list[i].Intf.Equals(value.Intf))
                    {
                        return true;
                    }
                }
                if ((list[i].Nexthop == null && value.Nexthop == null) &&
                    (list[i].Intf != null && value.Intf != null))
                {
                    if (list[i].Network.Equals(value.Network) && 
                        list[i].Mask.Equals(value.Mask) &&
                        list[i].Metric.Equals(value.Metric) &&
                        list[i].Intf.Equals(value.Intf))
                    {
                        return true;
                    }
                }
                if ((list[i].Intf == null && value.Intf == null) &&
                    (list[i].Nexthop != null && value.Nexthop != null))
                {
                    if (list[i].Network.Equals(value.Network) && 
                        list[i].Mask.Equals(value.Mask) &&
                        list[i].Metric.Equals(value.Metric) &&
                        list[i].Nexthop.Equals(value.Nexthop))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool RoutingArp(IPAddress targetIp, string intf)
        {
            foreach (var entry in Entries)
            {
                if (targetIp == null) return false;
                IPAddress targetSubnet = GetSubnet(targetIp, entry.Mask);
                if (entry.Network.Equals(targetSubnet))
                {
                    if (entry.MyInterface != null && !entry.MyInterface.Equals(intf))
                    {
                        return true;
                    }
                    RoutingArp(entry.Nexthop, intf);
                }
            }
            return false;
        }

//------------------------Metoda pre zistenie subnetu-----------------------------------------------------------------------------------------------

        public IPAddress GetSubnet(IPAddress address, IPAddress subnetMask)
        {
            byte[] ipAdressBytes1 = address.GetAddressBytes();
            byte[] subnetMaskBytes1 = subnetMask.GetAddressBytes();
            byte[] broadcastAddress1 = new byte[ipAdressBytes1.Length];
            for (int i = 0; i < broadcastAddress1.Length; i++)
            {
                broadcastAddress1[i] = (byte)(ipAdressBytes1[i] & (subnetMaskBytes1[i]));
            }

            return new IPAddress(broadcastAddress1);
        }
//------------------------Metoda pre nastavenie timeru-----------------------------------------------------------------------------------------------
        public void StartTimer()
        {
            _ripTimer.Elapsed += RipDatabaseTimerEvent;
            _ripTimer.Enabled = true;
            _ripTimer.AutoReset = true;
        }
//------------------------Metoda pre udalost po vzprsani timeru-----------------------------------------------------------------------------------------------
        private void RipDatabaseTimerEvent(Object source, ElapsedEventArgs e)
        {
            StaticRouting st = Instance;
            
            int i = 0;
            foreach (var item in st.RipDatabase)
            {
                if (item.Update != DateTime.MaxValue)
                {
                    DateTimeOffset stored;
                    DateTimeOffset actual = DateTimeOffset.Now;
                    double sd;
                    switch (item.Status)
                    {
                        case "":
                            stored = item.Update;
                            sd = (actual - stored).TotalSeconds;
                            if (sd > 180)
                            {
                                item.Status = "hold down";
                                item.Holddown = DateTime.Now;
                                item.Metric = 16;
                                int itemCount = 0;
                                foreach (var variable in _entries)
                                {
                                    if (variable.Network.Equals(item.Network) && variable.Mask.Equals(item.Mask) &&
                                        variable.Nexthop.Equals(item.Nexthop) && variable.Metric.Equals(item.Metric) &&
                                        variable.MyInterface.Equals(item.Intf))
                                    {
                                        _entries.RemoveAt(itemCount);
                                        break;
                                    }
                                    itemCount++;
                                }
                                
                            }
                            break;
                        case "hold down":
                            stored = item.Flush;
                            sd = (actual - stored).TotalSeconds;
                            if (sd > 240)
                            {
                                st.RipDatabase.RemoveAt(i);
                                break;
                            }
                            stored = item.Holddown;
                            sd = (actual - stored).TotalSeconds;
                            if (sd > 180)
                            {
                                item.Status = "";
                                item.Update = item.Flush;
                            }
                            break;
                    }              
                }      
                i++;
            }
        }

        public void SortTable()
        {
            _entries.Sort((x, y) => String.Compare(x.Mask.ToString(), y.Mask.ToString(), StringComparison.Ordinal));
        }

        public void InsertIntoRoutingTable()
        {
            foreach (var entryDatabase in _ripDatabase)
            {
                if (entryDatabase.Metric != 16 && entryDatabase.Status.Equals(""))
                {
                    IPAddress network = entryDatabase.Network;
                    IPAddress mask = entryDatabase.Mask;
                    int metric = entryDatabase.Metric;
                    string intf = entryDatabase.Intf;
                    IPAddress nexthop = entryDatabase.Nexthop;

                    foreach (var variable in _entries)
                    {
                        if (variable.Network.Equals(network) && variable.Mask.Equals(mask) && variable.Flag.Equals("R"))
                        {
                            variable.Metric = metric;
                            variable.Nexthop = nexthop;
                            variable.MyInterface = intf;
                            break;
                        }
                    }
                }
            }
        }
    }
}
