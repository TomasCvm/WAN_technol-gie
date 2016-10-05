using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PcapDotNet.Base;
using PcapDotNet.Core;
using PcapDotNet.Core.Extensions;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Arp;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.Icmp;
using PcapDotNet.Packets.IpV4;

namespace xcicman_zadanie__WAN
{
    public class Sending
    {
        PacketDevice _device;

        public void ArpReply(MacAddress source, ReadOnlyCollection<byte> senderMacByte, ReadOnlyCollection<byte> senderIpByte, ReadOnlyCollection<byte> targetIpByte, PacketDevice deviceReceive, PacketCommunicator communicator, GetInfo getInfo, Dictionary<string, int> statistics)
        {
            _device = deviceReceive;
            string macAdapter = ((LivePacketDevice)_device).GetMacAddress().ToString();
            string[] macAdapterParts = macAdapter.Split(':');


            Packet newPacket = PacketBuilder.Build(DateTime.Now,
                                                new EthernetLayer
                                                {
                                                    Source = new MacAddress(macAdapter),
                                                    Destination = source,
                                                    EtherType = EthernetType.None
                                                },
                                                new ArpLayer
                                                {

                                                    ProtocolType = EthernetType.IpV4,                                           // aby islo AsReadOnly treba pridat ... using PcapDotNet.Base;
                                                    SenderHardwareAddress = new[] { (byte)Convert.ToInt32(macAdapterParts[0], 16), (byte)Convert.ToInt32(macAdapterParts[1], 16), (byte)Convert.ToInt32(macAdapterParts[2], 16), (byte)Convert.ToInt32(macAdapterParts[3], 16), (byte)Convert.ToInt32(macAdapterParts[4], 16), (byte)Convert.ToInt32(macAdapterParts[5], 16) }.AsReadOnly(),
                                                    SenderProtocolAddress = targetIpByte,
                                                    TargetHardwareAddress = senderMacByte,
                                                    TargetProtocolAddress = senderIpByte,
                                                    Operation = ArpOperation.Reply
                                                });

            getInfo.get_statistics(newPacket, statistics);
            try
            {
                communicator.SendPacket(newPacket);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ArpRequest(ReadOnlyCollection<byte> targetIpByte, ReadOnlyCollection<byte> senderIpByte, PacketDevice deviceReceive, PacketCommunicator communicator, GetInfo getInfo, Dictionary<string, int> statistics)
        {
            _device = deviceReceive;
            string macAdapter = ((LivePacketDevice)_device).GetMacAddress().ToString();
            string[] macAdapterParts = macAdapter.Split(':');

            Packet newPacket = PacketBuilder.Build(DateTime.Now,
                                                new EthernetLayer
                                                {
                                                    Source = new MacAddress(macAdapter),
                                                    Destination = new MacAddress("FF:FF:FF:FF:FF:FF"),
                                                    EtherType = EthernetType.None
                                                },
                                                new ArpLayer
                                                {
                                                    ProtocolType = EthernetType.IpV4,
                                                    SenderHardwareAddress = new[] { (byte)Convert.ToInt32(macAdapterParts[0], 16), (byte)Convert.ToInt32(macAdapterParts[1], 16), (byte)Convert.ToInt32(macAdapterParts[2], 16), (byte)Convert.ToInt32(macAdapterParts[3], 16), (byte)Convert.ToInt32(macAdapterParts[4], 16), (byte)Convert.ToInt32(macAdapterParts[5], 16) }.AsReadOnly(),
                                                    SenderProtocolAddress = senderIpByte,
                                                    TargetHardwareAddress = new byte[] { 0, 0, 0, 0, 0, 0 }.AsReadOnly(),
                                                    TargetProtocolAddress = targetIpByte,
                                                    Operation = ArpOperation.Request
                                                });

            getInfo.get_statistics(newPacket, statistics);
            try
            {
                communicator.SendPacket(newPacket);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IcmpReply(Packet packet, string myIp, PacketDevice deviceReceive, PacketCommunicator communicator, MacAddress destmac, GetInfo getInfo, Dictionary<string, int> statistics )
        {
            _device = deviceReceive;
            string macAdapter = ((LivePacketDevice)_device).GetMacAddress().ToString();
            IcmpEchoLayer myicmp = (IcmpEchoLayer) packet.Ethernet.IpV4.Icmp.ExtractLayer();
            
            Packet newPacket = PacketBuilder.Build(DateTime.Now, 
                                                new EthernetLayer
                                                {
                                                    Source = new MacAddress(macAdapter),
                                                    Destination = destmac,
                                                    EtherType = EthernetType.None
                                                }, 
                                                new IpV4Layer
                                                {
                                                    Source = new IpV4Address(myIp),
                                                    CurrentDestination = packet.Ethernet.IpV4.Source,
                                                    Fragmentation = IpV4Fragmentation.None,
                                                    HeaderChecksum = null,
                                                    Identification = 123,
                                                    Options = IpV4Options.None,
                                                    Protocol = null,
                                                    Ttl = 100,
                                                    TypeOfService = 0
                                                },
                                                new IcmpEchoReplyLayer
                                                {
                                                    Checksum = null,
                                                    Identifier = myicmp.Identifier,
                                                    SequenceNumber = myicmp.SequenceNumber
                                                },
                                                packet.Ethernet.IpV4.Icmp.Payload.ExtractLayer());

            getInfo.get_statistics(newPacket, statistics);
            try
            {
                communicator.SendPacket(newPacket);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Send(Packet packet, PacketCommunicator communicator, MacAddress dest, PacketDevice device, GetInfo getInfo, Dictionary<string, int> statistics)
        {
            string macAdapter = ((LivePacketDevice)device).GetMacAddress().ToString();

            IpV4Layer ip = (IpV4Layer) packet.Ethernet.IpV4.ExtractLayer();
            EthernetLayer et = (EthernetLayer) packet.Ethernet.ExtractLayer();
            PayloadLayer pl = (PayloadLayer) packet.Ethernet.IpV4.Payload.ExtractLayer();
            et.Destination = dest;
            et.Source = new MacAddress(macAdapter);
            ip.Ttl -= 1;
            ip.HeaderChecksum = null;
            Packet newpacket = PacketBuilder.Build(DateTime.Now, et, ip, pl);

            getInfo.get_statistics(newpacket, statistics);
            try
            {
                communicator.SendPacket(newpacket);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
