using SpeedTestModel;
using SpeedTestModel.IPerf;
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
        private string currentHostIpAdress;
        private ServerIPerf currentServer;

        public string CurrentHostIpAdress
        {
            get { return this.currentHostIpAdress; }
            set { Set(ref this.currentHostIpAdress, value); }
        }

        public ServerIPerf CurrentServer
        {
            get { return currentServer; }
            set
            {
                if(value != null)
                {
                    ApplicationData.Current.LocalSettings.Values["HostName"] = value.IPerf3Server;
                    ApplicationData.Current.LocalSettings.Values["HostPort"] = value.Port;

                    Set(ref currentServer, value);
                }
            }
        }
    }
}
