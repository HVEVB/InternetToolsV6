using InternetToolsV6.Networking.HelperClasses;
using InternetToolsV6.Networking.HostnameIdentifier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InternetToolsV6.Networking.Ping
{
    public class Ping : IPing
    {
        public PingDataStructure SendICMPPing(string Address, int Timeout)
        {
            PingDataStructure pingDataStructure = new PingDataStructure();
            if (!string.IsNullOrEmpty(Address))
            {
                byte[] buffer =
                {
                        101, 108, 108, 111, 32, 116, 104, 101,
                        114, 101, 32, 99, 97, 110, 32, 121,
                        111, 117, 32, 115, 101, 101, 32, 116,
                        104, 105, 115, 32, 112, 105, 110, 103
                     };

                System.Net.NetworkInformation.Ping pingIP = new System.Net.NetworkInformation.Ping();
                PingReply pingReply = null;

                try
                {
                    pingReply = pingIP.Send(Address, Timeout + 1, buffer);
                }
                catch (PingException d) { MessageBox.Show($"{d.Message}", $"Ping exception"); return null; }
                catch (Exception g) { MessageBox.Show(g.Message); }

                if (pingReply.Status == IPStatus.Success)
                {
                    pingDataStructure.SuccessOrUnsuccess = "Success";
                    pingDataStructure.SourceAddress = GetIPAddresses.GetLocalAddress;
                    pingDataStructure.Data = $"{pingReply.Buffer.Length.ToString()} Bytes";
                    pingDataStructure.Protocol = "ICMP";
                    pingDataStructure.Destination = Address;
                    pingDataStructure.Time = $"{pingReply.RoundtripTime} ms";
                }
                else
                {
                    pingDataStructure.SuccessOrUnsuccess = "Failed";
                    pingDataStructure.SourceAddress = GetIPAddresses.GetLocalAddress;
                    pingDataStructure.Data = $"{pingReply.Buffer.Length.ToString()} Bytes";
                    pingDataStructure.Protocol = "ICMP";
                    pingDataStructure.Destination = Address;
                    pingDataStructure.Time = $"{pingReply.RoundtripTime} ms";
                }

                HostnameIdentifierM hi = new HostnameIdentifierM();
                HostnameIdentifierDataStructure hids = new HostnameIdentifierDataStructure();
                hids = hi.GetHostnames(Address);
                pingDataStructure.FoundIPV4 = hids.IPV4Address;
                pingDataStructure.FoundIPV6 = hids.IPV6Address;
            }
            return pingDataStructure;
        }

        public PingDataStructure SendTCPPing(string Address, int Timeout)
        {
            throw new NotImplementedException();
        }

        public PingDataStructure SendUDPPing(string Address, int Timeout)
        {
            throw new NotImplementedException();
        }
        public PingDataStructure SendARPPing(string Address, int Timeout)
        {
            throw new NotImplementedException();
        }
    }
}