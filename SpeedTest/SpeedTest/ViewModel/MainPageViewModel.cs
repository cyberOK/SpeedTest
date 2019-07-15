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

        private string _providerName;
        private string _ipAdress;
        private string _serverName;
        private string _serverLocation;
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

        public SpeedTestCommands StartCommand { get; private set; }

        #endregion

        #region Constructors

        public MainPageViewModel()
        {
            this.StartCommand = new SpeedTestCommands(new Action(StartSpeedTest));
        }

        #endregion

        #region Comands

        public void StartSpeedTest() // need realisation
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
