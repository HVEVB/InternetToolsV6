using InternetToolsV6.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InternetToolsV6.Networking.HelperClasses
{
    public class ExtraTools : IExtraTools
    {
        [DllImport("dnsapi.dll", EntryPoint = "DnsFlushResolverCache")]
        private static extern uint DnsFlushResolverCache();

        public void FlushDNSCache()
        {
            BalloonMessage.SimpleSideMessage("DNS Resolver Cleared", $"Status: {DnsFlushResolverCache()}");
        }
    }
}
