using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTest.Model
{
    public class ServersCollection
    {
        private static ServersCollection instance;

        public ObservableCollection<Server> ServerDataCollection { get; private set; }

        public ObservableCollection<string> ServerNamesCollection { get; private set; }

        private ServersCollection()
        {
            this.ServerDataCollection = new ObservableCollection<Server>
            {
                new Server { IPerf3Server = "iperf.volia.net", Location = "Ukraine", ProviderName = "Volia Kiev", Port = "5201", IsCurrent = true },
                new Server { IPerf3Server = "bouygues.iperf.fr", Location = "France", ProviderName = "Telehouse 2", Port = "5200", IsCurrent = false },
                new Server { IPerf3Server = "ping.online.net", Location = "France", ProviderName = "Online.net", Port = "5200", IsCurrent = false },
                new Server { IPerf3Server = "speedtest.serverius.net", Location = "Netherlands", ProviderName = "Serverius", Port = "5002", IsCurrent = false },
                new Server { IPerf3Server = "iperf.eenet.ee", Location = "Estonia", ProviderName = "EENet Tartu", Port = "5201", IsCurrent = false },
                new Server { IPerf3Server = "iperf.it-north.net", Location = "Kazakhstan", ProviderName = "Petropavl", Port = "5201", IsCurrent = false },
                new Server { IPerf3Server = "iperf.biznetnetworks.com", Location = "Indonesia", ProviderName = "Biznet", Port = "5201", IsCurrent = false },
                new Server { IPerf3Server = "iperf.scottlinux.com", Location = "USA, California", ProviderName = "Hurricane Fremont 2", Port = "5201", IsCurrent = false },
                new Server { IPerf3Server = "iperf.he.net", Location = "USA, California", ProviderName = "Hurricane Fremont 1", Port = "5201", IsCurrent = false }
            };

            this.ServerNamesCollection = GetServerNames(ServerDataCollection);
        }

        public static ServersCollection GetInstance()
        {
            if (instance == null)
            {
                instance = new ServersCollection();
            }

            return instance;
        }

        private ObservableCollection<string> GetServerNames(ObservableCollection<Server> serversCollection)
        {
            ObservableCollection<string> serverNames = new ObservableCollection<string>();

            foreach (Server server in serversCollection)
            {
                string serverName = server.ProviderName;
                serverNames.Add(serverName);
            }

            return serverNames;
        }
    }

    public class Server
    {
        public bool IsCurrent { get; set; }
        public string IPerf3Server { get; set; }
        public string Location { get; set; }
        public string ProviderName { get; set; }
        public string Port { get; set; }
    }

}
