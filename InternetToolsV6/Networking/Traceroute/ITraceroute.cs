using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetToolsV6.Networking.Traceroute
{
    public interface ITraceroute
    {
        IEnumerable<TracerouteDataStructure> SendTraceRoute(string Address, int Timeout, int MaxHops);
    }
}
