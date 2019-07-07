using InternetToolsV6.Networking.HelperClasses;
using InternetToolsV6.Networking.HostnameIdentifier;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
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

                pingDataStructure.SourceAddress = GetIPAddresses.GetLocalAddress;
                pingDataStructure.Data = $"{pingReply.Buffer.Length.ToString()} Bytes";
                pingDataStructure.Protocol = "ICMP";
                pingDataStructure.Destination = Address;
                pingDataStructure.Time = $"{pingReply.RoundtripTime} ms";

                if (pingReply.Status == IPStatus.Success)
                    pingDataStructure.SuccessOrUnsuccess = "Success";
                else
                    pingDataStructure.SuccessOrUnsuccess = "Failed";

                HostnameIdentifierM hi = new HostnameIdentifierM();
                HostnameIdentifierDataStructure hids = new HostnameIdentifierDataStructure();
                hids = hi.GetHostnames(Address);
                pingDataStructure.FoundIPV4 = hids.IPV4Address;
                pingDataStructure.FoundIPV6 = hids.IPV6Address;
            }
            return pingDataStructure;
        }

        public PingDataStructure SendTCPPing(string Address, int Timeout, int Port)
        {
            if (!string.IsNullOrEmpty(Address))
            {
                PingDataStructure pds = new PingDataStructure();
                Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sock.SendBufferSize = 32;
                sock.Blocking = true;

                Stopwatch stopwatch = new Stopwatch();

                // Measure the Connect call only
                stopwatch.Start();
                //IAsyncResult results = sock.BeginConnect(address, 80, null, null);
                bool successs = sock.BeginConnect(Address, Port, null, null).AsyncWaitHandle.WaitOne(Timeout);
                stopwatch.Stop();

                pds.SuccessOrUnsuccess = successs ? "Success" : "Failed";
                pds.SourceAddress = GetIPAddresses.GetLocalAddress;
                pds.Data = $"{sock.SendBufferSize.ToString()} Bytes";
                pds.Protocol = $"TCP -> {Port}";
                pds.Destination = Address;
                pds.Time = $"{stopwatch.Elapsed.TotalMilliseconds} ms";

                sock.Close();

                HostnameIdentifierM hi = new HostnameIdentifierM();
                HostnameIdentifierDataStructure hids = new HostnameIdentifierDataStructure();
                hids = hi.GetHostnames(Address);
                pds.FoundIPV4 = hids.IPV4Address;
                pds.FoundIPV6 = hids.IPV6Address;
                return pds;
            }
            return null;
        }

        public PingDataStructure SendUDPPing(string Address, int Timeout, int Port)
        {
            PingDataStructure pds = new PingDataStructure();
            pds.SuccessOrUnsuccess = "Doesn't work yet :(";
            return pds;
        }
        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        private static extern int SendARP(int DestIP, int SrcIP, byte[] pMacAddr, ref uint PhyAddrLen);

        Stopwatch sw = new Stopwatch();
        private PingDataStructure SendArpRequest(IPAddress dst)
        {
            byte[] macAddr = new byte[6];
            uint macAddrLen = (uint)macAddr.Length;
            //int uintAddress = BitConverter.ToInt32(dst.GetAddressBytes(), 0);

            Task.Run(() => { sw.Start(); });

            int result = SendARP(BitConverter.ToInt32(dst.GetAddressBytes(), 0), 0, macAddr, ref macAddrLen);;

            Task.Run(() => { sw.Stop(); });

            PingDataStructure pds = new PingDataStructure();
            pds.SourceAddress = GetIPAddresses.GetLocalAddress;
            pds.Data = $"42 Bytes";
            pds.Protocol = "TCP";
            pds.Destination = dst.ToString();
            pds.Time = $"{sw.ElapsedMilliseconds} ms";

            if (result == 0)
            {
                pds.SuccessOrUnsuccess = "Success";
                Task.Run(() => { sw.Restart(); });
            }
            else
            {
                pds.SuccessOrUnsuccess = "Failed";
            }
            return pds;
        }
        public IEnumerable<PingDataStructure> SendARPPing(string Address, int Timeout)
        {
            List<IPAddress> ipAddressList = new List<IPAddress>();

            //Generating 192.168.0.1/24 IP Range
            for (int i = 1; i < 255; i++)
            {
                //Obviously you'll want to safely parse user input to catch exceptions.
                ipAddressList.Add(IPAddress.Parse("192.168.1." + i));
            }
            foreach (IPAddress ip in ipAddressList)
            {
                yield return SendArpRequest(ip);
            }
        }
    }
}