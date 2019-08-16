using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTest.ViewModel.HelpfullCollections
{
    public class ServerCollectionViewModel
    {
        public ObservableCollection<ServerViewModel> ServerDataCollection { get; private set; }

        public ObservableCollection<string> ServerNamesCollection { get; private set; }

        public ServerCollectionViewModel()
        {
            this.ServerDataCollection = new ObservableCollection<ServerViewModel>();

            this.ServerDataCollection.Add(new ServerViewModel { IPerf3Server = "iperf.volia.net", Location = "Ukraine", ProviderName = "Volia Kiev", Port = "5201", IsCurrent = true });
            this.ServerDataCollection.Add(new ServerViewModel { IPerf3Server = "bouygues.iperf.fr", Location = "France", ProviderName = "Telehouse 2", Port = "5200", IsCurrent = false });
            this.ServerDataCollection.Add(new ServerViewModel { IPerf3Server = "ping.online.net", Location = "France", ProviderName = "Online.net", Port = "5200", IsCurrent = false });
            this.ServerDataCollection.Add(new ServerViewModel { IPerf3Server = "speedtest.serverius.net", Location = "Netherlands", ProviderName = "Serverius", Port = "5002", IsCurrent = false });
            this.ServerDataCollection.Add(new ServerViewModel { IPerf3Server = "iperf.eenet.ee", Location = "Estonia", ProviderName = "EENet Tartu", Port = "5201", IsCurrent = false });           
            this.ServerDataCollection.Add(new ServerViewModel { IPerf3Server = "iperf.it-north.net", Location = "Kazakhstan", ProviderName = "Petropavl", Port = "5201", IsCurrent = false });
            this.ServerDataCollection.Add(new ServerViewModel { IPerf3Server = "iperf.biznetnetworks.com", Location = "Indonesia", ProviderName = "Biznet", Port = "5201", IsCurrent = false });
            this.ServerDataCollection.Add(new ServerViewModel { IPerf3Server = "iperf.scottlinux.com", Location = "USA, California", ProviderName = "Hurricane Fremont 2", Port = "5201", IsCurrent = false });
            this.ServerDataCollection.Add(new ServerViewModel { IPerf3Server = "iperf.he.net", Location = "USA, California", ProviderName = "Hurricane Fremont 1", Port = "5201", IsCurrent = false });

            this.ServerNamesCollection = GetServerNames(ServerDataCollection);

            // Realization of getting data from Model ServerCollection
        }

        private ObservableCollection<string> GetServerNames(ObservableCollection<ServerViewModel> serversCollection)
        {
            ObservableCollection<string> serverNames = new ObservableCollection<string>();

            foreach (ServerViewModel server in serversCollection)
            {
                string serverName = server.ProviderName;
                serverNames.Add(serverName);
            }

            return serverNames;
        }
    }
}
