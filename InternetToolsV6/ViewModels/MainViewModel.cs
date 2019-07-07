using InternetToolsV6.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetToolsV6.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Variables
        //Commands
        public CommandRunner SendPing           { get; private set; }
        public CommandRunner ClearIPStatusList  { get; private set; }
        public CommandRunner ClearDNSStatusList { get; private set; }

        //Textbox variables or values... whatever oof
        private string address;
        public string Address
        {
            get => address; 
            set
            {
                address = value; RaisePropertyChanged();
            }
        }

        private double timeout;
        public double Timeout
        {
            get => timeout;
            set
            {
                timeout = value; RaisePropertyChanged();
            }
        }

        private bool icmpChecked;
        public bool ICMPChecked
        {
            get => icmpChecked;
            set
            {
                icmpChecked = value; RaisePropertyChanged();
            }
        }


        private bool tcpChecked;
        public bool TCPChecked
        {
            get => tcpChecked;
            set
            {
                tcpChecked = value; RaisePropertyChanged();
            }
        }


        private bool udpChecked;
        public bool UDPChecked
        {
            get => udpChecked;
            set
            {
                udpChecked = value; RaisePropertyChanged();
            }
        }


        private bool arpChecked;
        public bool ARPChecked
        {
            get => arpChecked;
            set
            {
                arpChecked = value; RaisePropertyChanged();
            }
        }

        private int tcpPort;
        public int TCPPort
        {
            get => tcpPort;
            set
            {
                tcpPort = value; RaisePropertyChanged();
            }
        }

        private int udpPort;
        public int UDPPort
        {
            get => udpPort;
            set
            {
                udpPort = value; RaisePropertyChanged();
            }
        }
        #endregion







        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName]string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
