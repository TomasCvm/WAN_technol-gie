using System.Collections.Generic;
using System.Windows.Forms;
using PcapDotNet.Core;
using PcapDotNet.Core.Extensions;
using xcicman_zadanie__WAN.Properties;

namespace xcicman_zadanie__WAN
{
    class FindAdapter
    {
        private IList<LivePacketDevice> _allDevices;
        private Dictionary<string, LivePacketDevice> _adapters;
        private Dictionary<string, string> _switchMac = new Dictionary<string, string>();

        // vyhladanie adapterov a naplnenie comboboxu
        public void Find(ComboBox comb1, ComboBox comb2)
        {
            _allDevices = LivePacketDevice.AllLocalMachine;
            _adapters = new Dictionary<string, LivePacketDevice>();
            _adapters.Add(Resources.chose1, null);
            for (int i = 0; i != _allDevices.Count; ++i)
            {
                LivePacketDevice device = _allDevices[i];
                _adapters.Add((i + 1) + ". " + device.Description, device);
                _switchMac.Add(device.GetMacAddress().ToString(), device.Description);
            }
            
            comb1.DataSource = new BindingSource(_adapters, null);
            comb1.DisplayMember = "Key";
            comb1.ValueMember = "Value";
            comb2.DataSource = new BindingSource(_adapters, null);
            comb2.DisplayMember = "Key";
            comb2.ValueMember = "Value";
        }


        //----------------------- Getters / Setters -----------------------
        public IList<LivePacketDevice> AllDevices
        {
            get { return _allDevices; }
            set { _allDevices = value; }
        }

        public Dictionary<string, LivePacketDevice> Adapters
        {
            get { return _adapters; }
            set { _adapters = value; }
        }
    }
}
