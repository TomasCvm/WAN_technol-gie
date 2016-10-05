using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using PcapDotNet.Core;
using PcapDotNet.Core.Extensions;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Transport;

namespace xcicman_zadanie__WAN
{
    public class RipProtocol
    {
        PacketDevice _device;
        StaticRouting _st = StaticRouting.Instance;

        public void RipRequest(PacketCommunicator communicator, PacketDevice deviceReceive, string senderIp)
        {
            _device = deviceReceive;
            string macAdapter = ((LivePacketDevice)_device).GetMacAddress().ToString();

            byte[] commandB = { Convert.ToByte(1) };
            byte[] versionB = { Convert.ToByte(2) };
            byte[] unusedB = BitConverter.GetBytes(0);
            Array.Reverse(unusedB, 0, unusedB.Length);

            byte[] ripDataPom = new byte[commandB.Length + versionB.Length + 2];
            Buffer.BlockCopy(commandB, 0, ripDataPom, 0, 1);
            Buffer.BlockCopy(versionB, 0, ripDataPom, 1, 1);
            Buffer.BlockCopy(unusedB, 0, ripDataPom, 2, 2);

            byte[] entry = RouteEntry(0, 0, IPAddress.Parse("0.0.0.0"), IPAddress.Parse("0.0.0.0"), IPAddress.Parse("0.0.0.0"), 16U);
            byte[] ripData = new byte[ripDataPom.Length+entry.Length];
            Buffer.BlockCopy(ripDataPom, 0, ripData, 0, 4);
            Buffer.BlockCopy(entry, 0, ripData, 4, 20);

            Packet newPacket = PacketBuilder.Build(DateTime.Now,
                                    new EthernetLayer
                                    {
                                        Source = new MacAddress(macAdapter),
                                        Destination = new MacAddress("01:00:5e:00:00:09"),
                                        EtherType = EthernetType.None
                                    },
                                    new IpV4Layer
                                    {
                                        Source = new IpV4Address(senderIp),
                                        CurrentDestination = new IpV4Address("224.0.0.9"),
                                        Fragmentation = IpV4Fragmentation.None,
                                        HeaderChecksum = null, // Will be filled automatically.
                                        Identification = 123,
                                        Options = IpV4Options.None,
                                        Protocol = null, // Will be filled automatically.
                                        Ttl = 128,
                                        TypeOfService = 0
                                    },
                                    new UdpLayer
                                    {
                                        SourcePort = 520,
                                        DestinationPort = 520,
                                        Checksum = null,
                                        CalculateChecksumValue = true
                                    },
                                    new PayloadLayer
                                    {
                                        Data = new Datagram(ripData)
                                    }
                                    );
            try
            {
                communicator.SendPacket(newPacket);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RipResponse(PacketCommunicator communicator, PacketDevice deviceReceive, string senderIp)
        {
            _device = deviceReceive;
            string macAdapter = ((LivePacketDevice)_device).GetMacAddress().ToString();

            byte[] commandB = { Convert.ToByte(2) };
            byte[] versionB = { Convert.ToByte(2) };
            byte[] unusedB = BitConverter.GetBytes(0);
            Array.Reverse(unusedB, 0, unusedB.Length);

            byte[] ripDataPom = new byte[commandB.Length + versionB.Length + 2];
            Buffer.BlockCopy(commandB, 0, ripDataPom, 0, 1);
            Buffer.BlockCopy(versionB, 0, ripDataPom, 1, 1);
            Buffer.BlockCopy(unusedB, 0, ripDataPom, 2, 2);

            List<byte[]> entries = new List<byte[]>();
            foreach (var databaseEntry in _st.RipDatabase)
            {
                int metric = databaseEntry.Metric;
                if (metric != 16)
                {
                    metric += 1;
                }
                entries.Add(RouteEntry(2,0,databaseEntry.Network,databaseEntry.Mask,IPAddress.Parse("0.0.0.0"),(uint)metric));
            }

            byte[] ripData = new byte[ripDataPom.Length + entries.Count * entries[0].Length];
            Buffer.BlockCopy(ripDataPom, 0, ripData, 0, 4);
            int offset = 4;
            foreach (var entry in entries)
            {
                Buffer.BlockCopy(entry,0,ripData,offset,20);
                offset += 20;   
            }

            Packet newPacket = PacketBuilder.Build(DateTime.Now,
                                    new EthernetLayer
                                    {
                                        Source = new MacAddress(macAdapter),
                                        Destination = new MacAddress("01:00:5e:00:00:09"),
                                        EtherType = EthernetType.None
                                    },
                                    new IpV4Layer
                                    {
                                        Source = new IpV4Address(senderIp),
                                        CurrentDestination = new IpV4Address("224.0.0.9"),
                                        Fragmentation = IpV4Fragmentation.None,
                                        HeaderChecksum = null, // Will be filled automatically.
                                        Identification = 123,
                                        Options = IpV4Options.None,
                                        Protocol = null, // Will be filled automatically.
                                        Ttl = 100,
                                        TypeOfService = 0
                                    },
                                    new UdpLayer
                                    {
                                        SourcePort = 520,
                                        DestinationPort = 520,
                                        Checksum = null,
                                        CalculateChecksumValue = true
                                    },
                                    new PayloadLayer
                                    {
                                        Data = new Datagram(ripData)
                                    }
                                    );
            try
            {
                communicator.SendPacket(newPacket);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public byte[] RouteEntry(ushort afi, ushort routeTag, IPAddress ipAddress, IPAddress mask, IPAddress nextHop, uint metric)
        {
            byte[] afiB = BitConverter.GetBytes(afi);
            Array.Reverse(afiB, 0, afiB.Length);
            byte[] routeTagB = BitConverter.GetBytes(routeTag);
            Array.Reverse(routeTagB, 0, routeTagB.Length);
            byte[] ipAddressB = ipAddress.GetAddressBytes();
            byte[] maskB = mask.GetAddressBytes();
            byte[] nextHopB = nextHop.GetAddressBytes();
            byte[] metricB = BitConverter.GetBytes(metric);
            Array.Reverse(metricB, 0, metricB.Length);

            byte[] routeEntry = new byte[afiB.Length + routeTagB.Length + ipAddressB.Length + maskB.Length + nextHopB.Length + metricB.Length];
            Buffer.BlockCopy(afiB, 0, routeEntry, 0, 2);
            Buffer.BlockCopy(routeTagB, 0, routeEntry, 2, 2);
            Buffer.BlockCopy(ipAddressB, 0, routeEntry, 4, 4);
            Buffer.BlockCopy(maskB, 0, routeEntry, 8, 4);
            Buffer.BlockCopy(nextHopB, 0, routeEntry, 12, 4);
            Buffer.BlockCopy(metricB, 0, routeEntry, 16, 4);

            return routeEntry;
        }

        public List<byte[]> GetEntriesFromPacket(PayloadLayer p)
        {
            Datagram data = p.Data;
            List<byte[]> entriesFromPacket = new List<byte[]>();
            int i = 0;
            int pom = 4;
            for (int offset = 4; offset < data.Length; offset+=20)
            {
                entriesFromPacket.Add(new byte[20]);
                byte[] numArray = new byte[2];
                Buffer.BlockCopy(data.ToArray(), offset, entriesFromPacket[i], 0, 20);
                Buffer.BlockCopy(data.ToArray(), pom, numArray, 0, 2);
                Array.Reverse(numArray, 0, numArray.Length);
                ++i;
            }
            return entriesFromPacket;
        }
    }
}
