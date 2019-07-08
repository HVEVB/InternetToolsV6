using InternetToolsV6.Networking.HelperClasses;
using InternetToolsV6.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InternetToolsV6.ViewModels
{
    public class MainViewModel
    {
        public ICommand FlushDNSResolver   { get; set; }
        public ICommand GetPublicIPAddress { get; set; }
        public ICommand GetLocalIPAddress  { get; set; }
        public ICommand ExitProgram        { get; set; }
        public ExtraTools etools = new ExtraTools();

        public MainViewModel()
        {
            FlushDNSResolver   = new DelegateCommand(_flushDNS);
            GetPublicIPAddress = new DelegateCommand(_getPubIP);
            GetLocalIPAddress  = new DelegateCommand(_getprivIP);
            ExitProgram        = new DelegateCommand(_extApp);
        }

        private void _flushDNS()
        {
            etools.FlushDNSCache();
        }

        private void _getPubIP()
        {
            BalloonMessage.SimpleSideMessage("Public IP Address", GetIPAddresses.GetPublicAddress);
        }

        private void _getprivIP()
        {
            BalloonMessage.SimpleSideMessage("Local IP Address", GetIPAddresses.GetLocalAddress);
        }

        private void _extApp()
        {
            Environment.Exit(0);
        }
    }
}
