﻿using SpeedTest.ViewModel.HelpfullCollections;
using SpeedTest.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTest.ViewModel.ViewBoards
{
    public class ServerPanel : ObservableObject
    {
        private ObservableCollection<Server> _serversCollection;
        private ObservableCollection<string> _serverNamesCollection;
        private ObservableCollection<string> _allServerNamesCollection;
        private bool _isServerPanelOpen;

        public bool IsServerPanelOpen
        {
            get { return this._isServerPanelOpen; }
            set { Set(ref _isServerPanelOpen, value); }
        }

        public ObservableCollection<Server> ServersCollection
        {
            get { return this._serversCollection; }
            set { Set(ref _serversCollection, value); }
        }

        public ObservableCollection<string> ServerNamesCollection
        {
            get { return this._serverNamesCollection; }
            set { Set(ref _serverNamesCollection, value); }
        }

        public ObservableCollection<string> FullServerNamesCollection
        {
            get { return this._allServerNamesCollection; }
            set { Set(ref _allServerNamesCollection, value); }
        }
    }
}