using InternetToolsV6.Networking.Ping;
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
    public class PingViewModel : INotifyPropertyChanged
    {
        #region Private Variables
        private string address;
        private double timeout;
        private bool icmpChecked;
        private bool tcpChecked;
        private bool udpChecked;
        private bool arpChecked;
        private int tcpPort;
        private int udpPort;
        private ObservableCollection<PingDataStructure> ipStatusList = new ObservableCollection<PingDataStructure>();
        #endregion

        #region Public Variables
        //Networking classes
        public Ping _Ping = new Ping();


        //Commands
        public ICommand SendPing { get; private set; }
        public ICommand ClearIPStatusList { get; private set; }
        //lists
        public ObservableCollection<PingDataStructure> IpStatusList
        {
            get { return ipStatusList; }
            set
            {
                ipStatusList = value; RaisePropertyChanged();
            }
        }

        //Textbox variables or values... whatever oof
        public string Address
        {
            get => address;
            set
            {
                address = value; RaisePropertyChanged();
            }
        }
        public double Timeout
        {
            get => timeout;
            set
            {
                timeout = value; RaisePropertyChanged();
            }
        }
        public bool ICMPChecked
        {
            get => icmpChecked;
            set
            {
                icmpChecked = value; RaisePropertyChanged();
            }
        }
        public bool TCPChecked
        {
            get => tcpChecked;
            set
            {
                tcpChecked = value; RaisePropertyChanged();
            }
        }
        public bool UDPChecked
        {
            get => udpChecked;
            set
            {
                udpChecked = value; RaisePropertyChanged();
            }
        }
        public bool ARPChecked
        {
            get => arpChecked;
            set
            {
                arpChecked = value; RaisePropertyChanged();
            }
        }
        public int TCPPort
        {
            get => tcpPort;
            set
            {
                tcpPort = value; RaisePropertyChanged();
            }
        }
        public int UDPPort
        {
            get => udpPort;
            set
            {
                udpPort = value; RaisePropertyChanged();
            }
        }

        #endregion

        #region Constructor

        public PingViewModel()
        {
            SendPing = new DelegateCommand(_callPing);
            ClearIPStatusList = new DelegateCommand(_callClearIPstatusList);
            Timeout = 500;
            icmpChecked = true;
            TCPPort = 80;
            UDPPort = 80;
        }

        #endregion

        #region Private Methods

        private void _callPing()
        {
            if (ICMPChecked)
            {
                Task.Run(() =>
                {
                    var oof = _Ping.SendICMPPing(Address, (int)Timeout);
                    App.Current.Dispatcher.InvokeAsync(() =>
                    {
                        IpStatusList.Add(oof);
                    });
                });
            } 
            if (TCPChecked)
            {
                Task.Run(() =>
                {
                    var oof = _Ping.SendTCPPing(Address, (int)Timeout, TCPPort);
                    App.Current.Dispatcher.InvokeAsync(() =>
                    {
                        IpStatusList.Add(oof);
                    });
                });
            }
            if (UDPChecked)
            {
                Task.Run(() =>
                {
                    var oof = _Ping.SendUDPPing(Address, (int)Timeout, UDPPort);
                    App.Current.Dispatcher.InvokeAsync(() =>
                    {
                        IpStatusList.Add(oof);
                    });
                });
            }
            if (ARPChecked)
            {
                Task.Run(() =>
                {
                    var oof = _Ping.SendARPPing(Address, (int)Timeout);
                    foreach (var itm in oof)
                    {
                        App.Current.Dispatcher.InvokeAsync(() =>
                        {
                            IpStatusList.Add(itm);
                        });
                    }
                });
            }
        }
        private void _callClearIPstatusList() => IpStatusList.Clear();

        #endregion

        #region UI Helpers

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName]string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion
    }
}
