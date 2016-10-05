using System.Net;

namespace xcicman_zadanie__WAN
{
    public class RoutingTable
    {
        private string _flag;
        private IPAddress _network;
        private IPAddress _mask;
        private IPAddress _nexthop;
        private string _myInterface;
        private int _metric;
        private int _ad;

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

        public string MyInterface
        {
            get { return _myInterface; }
            set { _myInterface = value; }
        }

        public string Flag
        {
            get { return _flag; }
            set { _flag = value; }
        }

        public int Metric
        {
            get { return _metric; }
            set { _metric = value; }
        }

        public int Ad
        {
            get { return _ad; }
            set { _ad = value; }
        }
    }
}
