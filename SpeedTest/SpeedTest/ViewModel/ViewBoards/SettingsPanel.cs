using SpeedTest.ViewModel.Helpers;
using SpeedTest.ViewModel.HelpfullCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTest.ViewModel.ViewBoards
{
    public class SettingsPanel : ObservableObject
    {
        private bool _isSettingsPaneOpen;
        private AppSetting _settings;
        private int _selectedMode;

        public bool IsSettingsPaneOpen
        {
            get { return _isSettingsPaneOpen; }
            set { Set(ref _isSettingsPaneOpen, value); }
        }

        public AppSetting Settings
        {
            get { return this._settings; }
            set { Set(ref _settings, value); }
        }

        public int SelectedMode
        {
            get { return this._selectedMode; }
            set { Set(ref _selectedMode, value); }
        }
    }
}
