using System;
using System.Net;

namespace xcicman_zadanie__WAN
{
    public class RipDatabase
    {
        private IPAddress _network;
        private IPAddress _mask;
        private IPAddress _nexthop;
        private int _metric;
        private string _intf;
        private DateTime _update;
        private DateTime _flush;
        private DateTime _holddown;
        private string _status;

        public IPAddress Network
        {
            get { return _network; }
            set { _network = value; }
        }

        public IPAddress Mask
        {
            get { return _mask; }
            set { _mask = value; }
        }

        public IPAddress Nexthop
        {
            get { return _nexthop; }
            set { _nexthop = value; }
        }

        public int Metric
        {
            get { return _metric; }
            set { _metric = value; }
        }

        public string Intf
        {
            get { return _intf; }
            set { _intf = value; }
        }

        public DateTime Update
        {
            get { return _update; }
            set { _update = value; }
        }
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public DateTime Flush
        {
            get { return _flush; }
            set { _flush = value; }
        }

        public DateTime Holddown
        {
            get { return _holddown; }
            set { _holddown = value; }
        }
    }
}