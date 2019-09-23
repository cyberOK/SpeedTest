using SpeedTestModel;
using SpeedTestUWP.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace SpeedTestUWP.ViewModel.ViewBoards
{
    public class ServerInformationBoard : ObservableObject
    {
        private ServerInformation _currentServer;

        public ServerInformation CurrentServer
        {
            get { return _currentServer; }
            set
            {
                if(value != null)
                {
                    ApplicationData.Current.LocalSettings.Values["HostName"] = value.IPerf3Server;
                    ApplicationData.Current.LocalSettings.Values["HostPort"] = value.Port;

                    Set(ref _currentServer, value);
                }
            }
        }
    }
}
