using InternetToolsV6.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace InternetToolsV6.Networking.HelperClasses
{
    public class GetIPAddresses
    {
        public static string GetLocalAddress
        {
            //when something needs to get it, it will do this:
            get
            {
                try
                {
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
                    if (socket != null && !socket.Connected) try { socket.Connect("8.8.8.8", 65530); } catch { }
                    string InternalIP = (socket.LocalEndPoint as IPEndPoint).Address.ToString();
                    if (socket.Connected) try { socket.Disconnect(false); } catch { }
                    return InternalIP;
                }
                catch
                {
                    BalloonMessage.SideMessage("Couldnt get Local IP", "Could not get local IP Address from", 5, SystemIcons.Error);
                    return "Could not get Public Address";
                }
            }
        }
        public static string GetPublicAddress
        {
            get
            {
                try
                {
                    return new WebClient().DownloadString("http://icanhazip.com");
                }
                catch
                {
                    BalloonMessage.SideMessage("Couldnt get Public IP", "Could not get public IP Address from" + "http://icanhazip.com.", 5, SystemIcons.Error);
                    return "Could not get Public Address";
                }
            }
        }
    }
}
