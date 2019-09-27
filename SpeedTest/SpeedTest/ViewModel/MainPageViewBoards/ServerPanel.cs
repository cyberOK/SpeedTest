using SpeedTestModel;
using SpeedTestUWP.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.UI;

namespace SpeedTestUWP.ViewModel.ViewBoards
{
    public class ServerPanel : ObservableObject
    {       
        private bool isServerPanelOpen;
        private bool isNoresults;

        public bool IsServerPanelOpen
        {
            get { return this.isServerPanelOpen; }
            set { Set(ref isServerPanelOpen, value); }
        }

        public bool IsNoresults
        {
            get { return this.isNoresults; }
            set { Set(ref isNoresults, value); }
        }

        public AdvancedCollectionView ServersCollection { get; private set; }

        public ServerPanel(AdvancedCollectionView serversCollection)
        {
            this.ServersCollection = serversCollection;
            this.ServersCollection?.SortDescriptions.Add(new SortDescription("ProviderName", SortDirection.Ascending));
        }
    }
}
