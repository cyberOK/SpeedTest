﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTestModel
{
    public  class ServersCollection
    {
        private static ServersCollection instance;

        public ObservableCollection<ServerInformation> ServerDataCollection { get; private set; }

        public ObservableCollection<string> ServerNamesCollection { get; private set; }

        private ServersCollection()
        {
            this.ServerDataCollection = new ObservableCollection<ServerInformation>
            {
                new ServerInformation { IPerf3Server = "iperf.volia.net", Location = "Ukraine", ProviderName = "Volia Kiev", Port = 5201, IsCurrent = false },
                new ServerInformation { IPerf3Server = "bouygues.iperf.fr", Location = "France", ProviderName = "Telehouse 2", Port = 5205, IsCurrent = true },
                new ServerInformation { IPerf3Server = "ping.online.net", Location = "France", ProviderName = "Online.net", Port = 5205, IsCurrent = false },
                new ServerInformation { IPerf3Server = "speedtest.serverius.net", Location = "Netherlands", ProviderName = "Serverius", Port = 5002, IsCurrent = false },
                new ServerInformation { IPerf3Server = "iperf.eenet.ee", Location = "Estonia", ProviderName = "EENet Tartu", Port = 5201, IsCurrent = false },
                new ServerInformation { IPerf3Server = "iperf.it-north.net", Location = "Kazakhstan", ProviderName = "Petropavl", Port = 5201, IsCurrent = false },
                new ServerInformation { IPerf3Server = "iperf.biznetnetworks.com", Location = "Indonesia", ProviderName = "Biznet", Port = 5201, IsCurrent = false },
                new ServerInformation { IPerf3Server = "iperf.scottlinux.com", Location = "USA, California", ProviderName = "Hurricane Fremont 2", Port = 5201, IsCurrent = false },
                new ServerInformation { IPerf3Server = "iperf.he.net", Location = "USA, California", ProviderName = "Hurricane Fremont 1", Port = 5201, IsCurrent = false }
            };
        }

        public static ServersCollection GetInstance()
        {
            if (instance == null)
            {
                instance = new ServersCollection();
            }

            return instance;
        }

        public ObservableCollection<string> GetServerNames()
        {
            ObservableCollection<string> serverNames = new ObservableCollection<string>();

            foreach (ServerInformation server in this.ServerDataCollection)
            {
                string serverName = server.ProviderName;
                serverNames.Add(serverName);
            }

            return serverNames;
        }
    }
}