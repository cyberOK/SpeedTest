using SpeedTestUWP.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTestUWP.ViewModel.ViewBoards
{
    public class HistoryPanel : ObservableObject
    {
        private SpeedDataViewModel _oldSelectedHistoryValue = null;
        private bool _isHistoryPanelOpen;
        private bool _isHistorySelected;
        private string _currentPing;
        private ObservableCollection<SpeedDataViewModel> _speedDataCollection;

        public string CurrentPing
        {
            get { return this._currentPing; }
            set { Set(ref this._currentPing, value); }
        }

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

        public ObservableCollection<SpeedDataViewModel> SpeedDataCollection
        {
            get { return this._speedDataCollection; }
            set { Set(ref _speedDataCollection, value); }
        }

        public SpeedDataViewModel OldSelectedHistoryValue
        {
            get { return this._oldSelectedHistoryValue; }
            set { Set(ref _oldSelectedHistoryValue, value); }
        }
    }
}
