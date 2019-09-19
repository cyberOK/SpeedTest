using SpeedTestUWP.ViewModel.Helpers;
using SpeedTestUWP.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTestUWP.ViewModel.ViewBoards
{
    public class SettingsPanel : ObservableObject
    {
        private bool _isSettingsPaneOpen;
        private bool _isBackgroundTestEnable;
        private AppSetting _settings;
        private int _selectedMode;

        public bool IsSettingsPaneOpen
        {
            get { return _isSettingsPaneOpen; }
            set { Set(ref _isSettingsPaneOpen, value); }
        }

        public bool IsBackgroundTestEnable
        {
            get { return _isBackgroundTestEnable; }
            set { Set(ref _isBackgroundTestEnable, value); }
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
