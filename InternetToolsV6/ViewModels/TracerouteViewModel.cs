using InternetToolsV6.Networking.Traceroute;
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
    public class TracerouteViewModel : INotifyPropertyChanged
    {
        #region private values
        private string address;
        private int maxhops;
        private int timeout;
        private ObservableCollection<TracerouteDataStructure> tracerouteDataStructures = new ObservableCollection<TracerouteDataStructure>();
        #endregion

        #region public variables

        public Traceroute traceroute = new Traceroute();

        public ICommand SendTraceroute { get; private set; }
        public ICommand ClearTracerouteTable { get; private set; }
        //lists
        public ObservableCollection<TracerouteDataStructure> TracerouteMessages
        {
            get { return tracerouteDataStructures; }
            set
            {
                tracerouteDataStructures = value; RaisePropertyChanged();
            }
        }

        public string Address
        {
            get => address;
            set { address = value; RaisePropertyChanged(); }
        }

        public int MaxHops
        {
            get => maxhops;
            set { maxhops = value;RaisePropertyChanged(); }
        }
        public int Timeout
        {
            get => timeout;
            set { timeout = value; RaisePropertyChanged(); }
        }
        #endregion

        #region constructor
        public TracerouteViewModel()
        {
            SendTraceroute = new DelegateCommand(_sendTracert);
            ClearTracerouteTable = new DelegateCommand(clrTracertTable);
            MaxHops = 50;
            Timeout = 500;
        }
        #endregion

        #region private methods

        private void _sendTracert()
        {
            Task.Run(() =>
            {
                var oof = traceroute.SendTraceRoute(Address, Timeout, MaxHops);
                foreach (var itm in oof)
                {
                    App.Current.Dispatcher.InvokeAsync(() =>
                    {
                        TracerouteMessages.Add(itm);
                    });
                }
            });
        }

        private void clrTracertTable() => TracerouteMessages.Clear();

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
