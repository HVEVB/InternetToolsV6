using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetToolsV6.Networking.Traceroute
{
    public class TracerouteDataStructure
    {
        public string AddressPosition { get; set; }
        public string Interval { get; set; }
        public string Status { get; set; }
        public string Route { get; set; }
        public string TotalTime { get; set; }
        public void clearData()
        {
            AddressPosition = "";
            Interval = "";
            Status = "";
            Route = "";
            TotalTime = "";
        }
        public override string ToString()
        {
            return $"[{AddressPosition}] -- {Status} -> {Route} | Interval: {Interval} --> Total Time: {TotalTime} ms";
        }
    }
}
