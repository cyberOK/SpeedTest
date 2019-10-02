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
        private SpeedDataViewModel oldSelectedHistoryValue = null;
        private bool isHistoryPanelOpen;
        private bool isHistorySelected;
        private ObservableCollection<SpeedDataViewModel> speedDataCollection;

        public SpeedDataViewModel CurrentSpeedDataSample { get; set; } 

        public bool IsHistoryPanelOpen
        {
            get { return this.isHistoryPanelOpen; }
            set { Set(ref isHistoryPanelOpen, value); }
        }

        public bool IsHistorySelected
        {
            get { return this.isHistorySelected; }
            set { Set(ref isHistorySelected, value); }
        }

        public ObservableCollection<SpeedDataViewModel> SpeedDataCollection
        {
            get { return this.speedDataCollection; }
            private set { Set(ref speedDataCollection, value); }
        }

        public SpeedDataViewModel OldSelectedHistoryValue
        {
            get { return this.oldSelectedHistoryValue; }
            set { Set(ref oldSelectedHistoryValue, value); }
        }

        public HistoryPanel()
        {
            this.SpeedDataCollection = new ObservableCollection<SpeedDataViewModel>();
            this.CurrentSpeedDataSample = new SpeedDataViewModel();
        }
    }
}
