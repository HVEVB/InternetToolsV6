using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetToolsV6.Networking.HostnameIdentifier
{
    public interface IHostnameIdentifier
    {
        /// <summary>
        /// Gets the IPV4 and IPV6 (if it exists) from a hostname. 
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        HostnameIdentifierDataStructure GetHostnames(string Address);
    }
}
