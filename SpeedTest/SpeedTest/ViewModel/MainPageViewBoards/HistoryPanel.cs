using SpeedTest.Model;
using SpeedTest.ViewModel.Helpers;
using SpeedTest.ViewModel.HelpfullCollections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTest.ViewModel.ViewBoards
{
    public class HistoryPanel : ObservableObject
    {
        private SpeedData _oldSelectedHistoryValue = null;
        private bool _isHistoryPanelOpen;
        private bool _isHistorySelected;
        private ObservableCollection<SpeedData> _speedDataCollection;

        public bool IsHistoryPanelOpen
        {
            get { return this._isHistoryPanelOpen; }
            set { Set(ref _isHistoryPanelOpen, value); }
        }

        public bool IsHistorySelected
        {
            get { return this._isHistorySelected; }
            set { Set(ref _isHistorySelected, value); }
        }

        public ObservableCollection<SpeedData> SpeedDataCollection
        {
            get { return this._speedDataCollection; }
            set { Set(ref _speedDataCollection, value); }
        }

        public SpeedData OldSelectedHistoryValue
        {
            get { return this._oldSelectedHistoryValue; }
            set { Set(ref _oldSelectedHistoryValue, value); }
        }
    }
}
