using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetToolsV6.Networking.Ping
{
    public class PingDataStructure
    {
        public string SuccessOrUnsuccess { get; set; }
        public string SourceAddress { get; set; }
        public string Data { get; set; }
        public string Protocol { get; set; }
        public string Destination { get; set; }
        public string Time { get; set; }

        public string FoundIPV4 { get; set; }
        public string FoundIPV6 { get; set; }

        public override string ToString()
        {
            return $"{SuccessOrUnsuccess} -> ({SourceAddress} : {Data} Bytes) {Protocol} ({Destination} Time: {Time} ms)";
        }
    }
}