﻿using SpeedTestIPerf.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTestIPerf.ViewModel.ViewBoards
{
    public class ServerInformationBoard : ObservableObject
    {
        private string _providerName;
        private string _ipAdress = "IpAdress";
        private string _serverName;
        private string _serverLocation;

        public string ProviderName
        {
            get { return _providerName; }
            set { Set(ref _providerName, value); }
        }

        public string IpAdress
        {
            get { return _ipAdress; }
            set { Set(ref _ipAdress, value); }
        }

        public string CurrentServerName
        {
            get { return _serverName; }
            set { Set(ref _serverName, value); }
        }

        public string CurrentServerLocation
        {
            get { return _serverLocation; }
            set { Set(ref _serverLocation, value); }
        }
    }
}
