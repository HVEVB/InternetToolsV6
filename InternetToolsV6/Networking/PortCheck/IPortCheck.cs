using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetToolsV6.Networking.PortCheck
{
    public interface IPortCheck
    {
        void SendPortCheck(string Address, int Timeout);
    }
}
