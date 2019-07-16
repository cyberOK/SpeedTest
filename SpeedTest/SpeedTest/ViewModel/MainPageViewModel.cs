using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpeedTest.ViewModel.Helpers;

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

        #endregion

        #region Property binding

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

        #endregion

        #region Property commands

        public SpeedTestCommands StartButtonPressed { get; private set; }
        public SpeedTestCommands BackButtonPressed { get; private set; }
        public SpeedTestCommands HistoryButtonPressed { get; private set; }
        public SpeedTestCommands SettingsButtonPressed { get; private set; }
        public SpeedTestCommands ChangeServerButtonPressed { get; private set; }

        #endregion

        #region Constructors

        public MainPageViewModel()
        {
            this.StartButtonPressed = new SpeedTestCommands(new Action(StartSpeedTest));
            this.BackButtonPressed = new SpeedTestCommands(new Action(BackCalling));
            this.HistoryButtonPressed = new SpeedTestCommands(new Action(HistoryCalling));
            this.SettingsButtonPressed = new SpeedTestCommands(new Action(SettingsCalling));
            this.ChangeServerButtonPressed = new SpeedTestCommands(new Action(ChangeServerCalling));
        }

        #endregion

        #region Comands
        // need realisation// need realisation// need realisation// need realisation// need realisation// need realisation// need realisation// need realisation// need realisation
        public async void StartSpeedTest() 
        {
            await new Windows.UI.Popups.MessageDialog("StartSpeedTest()").ShowAsync();
        }

        public async void BackCalling()
        {
            await new Windows.UI.Popups.MessageDialog("BackCalling()").ShowAsync();
        }

        public async void HistoryCalling()
        {
            await new Windows.UI.Popups.MessageDialog("HistoryCalling()").ShowAsync();
        }

        public async void SettingsCalling()
        {
            await new Windows.UI.Popups.MessageDialog("SettingsCalling()").ShowAsync();
        }

        public async void ChangeServerCalling()
        {
            await new Windows.UI.Popups.MessageDialog("ChangeServerCalling()").ShowAsync();
        }

        #endregion
    }
}
