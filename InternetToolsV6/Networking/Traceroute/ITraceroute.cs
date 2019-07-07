using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetToolsV6.Networking.Traceroute
{
    public interface ITraceroute
    {
        void SendTraceRoute(string Address, int Timeout);
    }
}
