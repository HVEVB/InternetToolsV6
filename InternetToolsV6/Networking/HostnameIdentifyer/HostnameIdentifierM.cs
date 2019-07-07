using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InternetToolsV6.Networking.HostnameIdentifier
{
    public class HostnameIdentifierM : IHostnameIdentifier
    {
        public HostnameIdentifierDataStructure GetHostnames(string Address)
        {
            HostnameIdentifierDataStructure hids = new HostnameIdentifierDataStructure();
            try
            {
                IPAddress[] iPAddresses = Dns.GetHostAddresses(Address);
                foreach (IPAddress hostAddress in iPAddresses)
                {
                    switch (hostAddress.AddressFamily)
                    {
                        case System.Net.Sockets.AddressFamily.InterNetwork:
                            hids.IPV4Address = hostAddress.ToString();
                            break;
                        case System.Net.Sockets.AddressFamily.InterNetworkV6:
                            hids.IPV6Address = hostAddress.ToString();
                            break;
                    }
                }
                return hids;
            }
            catch (Exception g) { MessageBox.Show(g.Message); return hids; }
        }
    }
}