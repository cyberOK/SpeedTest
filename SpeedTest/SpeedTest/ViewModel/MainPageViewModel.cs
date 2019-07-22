using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SpeedTest.ViewModel.Helpers;
using SpeedTest.ViewModel.HelpfullCollections;
using Windows.UI.Xaml.Controls;

namespace SpeedTest.ViewModel
{
    public class MainPageViewModel : ObservableObject
    {
        #region Fields

        private string _providerName = "ProviderName";
        private string _ipAdress = "IpAdress";
        private string _serverName = "ServerName";
        private string _serverLocation = "ServerLocation";
        private bool _isServerLoaded;
        private bool _isPaneOpen;
        private SettingSplitViewCollection _settings;

        #endregion

        #region Property binding

        // Main data panel properties
 
        public string ProviderName
        {
            get { return _providerName; }
            set { Set(ref _providerName, value); }
        }

        public string IpAdress
        {
            get { return _ipAdress; }
            set { Set(ref _ipAdress, value); }
        }

        public string ServerName
        {
            get { return _serverName; }
            set { Set(ref _serverName, value); }
        }

        public string ServerLocation
        {
            get { return _serverLocation; }
            set { Set(ref _serverLocation, value); }
        }

        public bool IsServerLoaded
        {
            get { return _isServerLoaded; }
            set { Set(ref _isServerLoaded, value); }
        }

        // Settings panel properties

        public bool IsPaneOpen
        {
            get { return _isPaneOpen; }
            set { Set(ref _isPaneOpen, value); }
        }

        public SettingSplitViewCollection Settings
        {
            get { return this._settings; }
            set { Set(ref this._settings, value); }
        }

        #endregion

        #region Property commands

        // Mainboard properties commands

        public SpeedTestCommand StartButtonPressed { get; private set; }
        public SpeedTestCommand BackButtonPressed { get; private set; }
        public SpeedTestCommand HistoryButtonPressed { get; private set; }
        public SpeedTestCommand SettingsButtonPressed { get; private set; }
        public SpeedTestCommand ChangeServerButtonPressed { get; private set; }

        // Settings panel properties commands

        public SpeedTestCommand SettingSplitViewClosing { get; private set; }
        public SpeedTestCommand LanguageComboBoxChanged { get; private set; }
        public SpeedTestCommand SelectedItemRadioButtonChanged { get; private set; }


        #endregion

        #region Constructors

        public MainPageViewModel()
        {
            this.StartButtonPressed = new SpeedTestCommand(new Action<object>(StartSpeedTest));
            this.BackButtonPressed = new SpeedTestCommand(new Action<object>(BackCalling));
            this.HistoryButtonPressed = new SpeedTestCommand(new Action<object>(HistoryCalling));
            this.SettingsButtonPressed = new SpeedTestCommand(new Action<object>(SettingsCalling));
            this.ChangeServerButtonPressed = new SpeedTestCommand(new Action<object>(ChangeServerCalling));

            this.SettingSplitViewClosing = new SpeedTestCommand(new Action<object>(SettingSplitViewDontClosing));
            this.LanguageComboBoxChanged = new SpeedTestCommand(new Action<object>(LanguageChange));
            this.SelectedItemRadioButtonChanged = new SpeedTestCommand(new Action<object>(ModeChanged));
            
            //this.Settings = 
        }

        #endregion

        #region Mainboard Comands
        
        public async void StartSpeedTest(object param) 
        {
            await new Windows.UI.Popups.MessageDialog("StartSpeedTest()").ShowAsync();
        }

        public async void BackCalling(object param)
        {
            await new Windows.UI.Popups.MessageDialog("BackCalling()").ShowAsync();
        }

        public async void HistoryCalling(object param)
        {
            await new Windows.UI.Popups.MessageDialog("HistoryCalling()").ShowAsync();
        }

        public void SettingsCalling(object param)
        {
            this.IsPaneOpen = true;
        }

        public async void ChangeServerCalling(object param)
        {
            await new Windows.UI.Popups.MessageDialog("ChangeServerCalling()").ShowAsync();
        }

        #endregion

        #region Setting Split View Commands


        public void SettingSplitViewDontClosing(object sender)
        {           
            this.IsPaneOpen = false;            
        }

        public async void LanguageChange(object param)
        {
            ComboBoxItem lang = (ComboBoxItem)param;
            await new Windows.UI.Popups.MessageDialog(lang.Content.ToString()).ShowAsync();
        }

        public async void ModeChanged(object param)
        {
            string par = (string)param;
            await new Windows.UI.Popups.MessageDialog(par.ToString()).ShowAsync();
        }

        #endregion
    }
}
