using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpeedTest.ViewModel.Helpers;
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

        #endregion

        #region Property commands

        public SpeedTestCommands StartButtonPressed { get; private set; }
        public SpeedTestCommands BackButtonPressed { get; private set; }
        public SpeedTestCommands HistoryButtonPressed { get; private set; }
        public SpeedTestCommands SettingsButtonPressed { get; private set; }
        public SpeedTestCommands ChangeServerButtonPressed { get; private set; }

        // Settings panel Properties commands

        public SpeedTestCommands SettingSplitViewClosing { get; private set; }

        #endregion

        #region Constructors

        public MainPageViewModel()
        {
            this.StartButtonPressed = new SpeedTestCommands(new Action<object>(StartSpeedTest));
            this.BackButtonPressed = new SpeedTestCommands(new Action<object>(BackCalling));
            this.HistoryButtonPressed = new SpeedTestCommands(new Action<object>(HistoryCalling));
            this.SettingsButtonPressed = new SpeedTestCommands(new Action<object>(SettingsCalling));
            this.ChangeServerButtonPressed = new SpeedTestCommands(new Action<object>(ChangeServerCalling));
            this.ChangeServerButtonPressed = new SpeedTestCommands(new Action<object>(SettingSplitViewDontClosing));
        }

        #endregion

        #region Comands
        // need realisation// need realisation// need realisation// need realisation// need realisation// need realisation// need realisation// need realisation// need realisation
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
            this.IsPaneOpen = !this.IsPaneOpen;
        }

        public async void ChangeServerCalling(object param)
        {
            await new Windows.UI.Popups.MessageDialog("ChangeServerCalling()").ShowAsync();
        }

        // Setting Split View Commands

        public async void SettingSplitViewDontClosing(object sender)
        {
            this.IsPaneOpen = true;
            await new Windows.UI.Popups.MessageDialog("SettingSplitViewDontClosing()").ShowAsync();
        }

        #endregion
    }
}
