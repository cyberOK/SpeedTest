using SpeedTestUWP.Model;
using SpeedTestUWP.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTestUWP.ViewModel.ViewBoards
{
    public class ServerInformationBoard : ObservableObject
    {
        private ServerInformation _currentServer;

        public ServerInformation CurrentServer
        {
            get { return _currentServer; }
            set { Set(ref _currentServer, value); }
        }
    }
}
