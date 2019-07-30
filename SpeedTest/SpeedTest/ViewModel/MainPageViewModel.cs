using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SpeedTest.ViewModel.Helpers;
using SpeedTest.ViewModel.HelpfullCollections;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace SpeedTest.ViewModel
{
    public class MainPageViewModel : ObservableObject
    {
        #region Fields

        private SpeedDataViewModel _oldHistoryValue = null;

        private string _providerName = "ProviderName";
        private string _ipAdress = "IpAdress";
        private string _serverName;
        private string _serverLocation;
        private bool _isServerLoaded;
        private bool _isSettingsPaneOpen;
        private SettingViewModel _settings;
        private int _selectedMode;
        private bool _isHistoryPanelOpen;
        private bool _isHistorySelected;
        private ObservableCollection<SpeedDataViewModel> _speedDataCollection;
        private ObservableCollection<ServerViewModel> _serversCollection;
        private ObservableCollection<string> _serverNamesCollection;
        private ObservableCollection<string> _allServerNamesCollection;
        public bool _isServerPanelOpen;

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

        public bool IsSettingsPaneOpen
        {
            get { return _isSettingsPaneOpen; }
            set { Set(ref _isSettingsPaneOpen, value); }
        }       

        public SettingViewModel Settings
        {
            get { return this._settings; }
            private set { Set(ref _settings, value ); }
        }

        public int SelectedMode
        {
            get { return this._selectedMode; }
            private set { Set(ref _selectedMode, value); }
        }

        // History panel properties

        public bool IsHistoryPanelOpen
        {
            get { return this._isHistoryPanelOpen; }
            private set { Set(ref _isHistoryPanelOpen, value); }
        }

        
        public bool IsHistorySelected
        {
            get { return this._isHistorySelected; }
            private set { Set(ref _isHistorySelected, value); }
        }

        public ObservableCollection<SpeedDataViewModel> SpeedDataCollection
        {
            get { return this._speedDataCollection; }
            private set { Set(ref _speedDataCollection, value); }
        }

        // Server panel properties
        
        public bool IsServerPanelOpen
        {
            get { return this._isServerPanelOpen; }
            private set { Set(ref _isServerPanelOpen, value); }
        }

        public ObservableCollection<ServerViewModel> ServersCollection
        {
            get { return this._serversCollection; }
            private set { Set(ref _serversCollection, value); }
        }

        public ObservableCollection<string> ServerNamesCollection
        {
            get { return this._serverNamesCollection; }
            private set { Set(ref _serverNamesCollection, value); }
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

        // History panel properties commands

        public SpeedTestCommand DeleteHistoryButtonPressed { get; private set; }       
        public SpeedTestCommand CloseHistoryButtonPressed { get; private set; }
        public SpeedTestCommand SingleHistoryDeletedButtonPressed { get; private set; }
        public SpeedTestCommand SingleHistorySelected { get; private set; }

        // Server panel properties commands

        public SpeedTestCommand ServerSuggestBoxTextChanged { get; private set; }
        public SpeedTestCommand SingleServerSelected { get; private set; }
        public SpeedTestCommand CloseServerPanelButtonPressed { get; private set; }
     
        #endregion

        #region Constructors

        public MainPageViewModel()
        {
            // Main panel commands assing

            this.StartButtonPressed = new SpeedTestCommand(new Action<object>(StartSpeedTest));
            this.BackButtonPressed = new SpeedTestCommand(new Action<object>(BackCalling));
            this.HistoryButtonPressed = new SpeedTestCommand(new Action<object>(HistoryCalling));
            this.SettingsButtonPressed = new SpeedTestCommand(new Action<object>(SettingsCalling));
            this.ChangeServerButtonPressed = new SpeedTestCommand(new Action<object>(ChangeServerCalling));

            // Settings panel commands assing

            this.SettingSplitViewClosing = new SpeedTestCommand(new Action<object>(SettingSplitViewDontClosing));
            this.LanguageComboBoxChanged = new SpeedTestCommand(new Action<object>(LanguageChange));
            this.SelectedItemRadioButtonChanged = new SpeedTestCommand(new Action<object>(ModeChanged));

            // History panel commands assing

            this.DeleteHistoryButtonPressed = new SpeedTestCommand(new Action<object>(DeleteHistory));
            this.CloseHistoryButtonPressed = new SpeedTestCommand(new Action<object>(CloseHistory));
            this.SingleHistoryDeletedButtonPressed = new SpeedTestCommand(new Action<object>(SingleHistoryDeleted));
            this.SingleHistorySelected = new SpeedTestCommand(new Action<object>(SingleHistorySelecting));

            // Server panel commands assing

            this.ServerSuggestBoxTextChanged = new SpeedTestCommand(new Action<object>(ServerNameTextChanged));
            this.SingleServerSelected = new SpeedTestCommand(new Action<object>(SingleServerSelecting));
            this.CloseServerPanelButtonPressed = new SpeedTestCommand(new Action<object>(CloseServerPanel));

            // Initialization MainPageViewModel

            this.Settings = SettingViewModel.GetInstance();
            this.SelectedMode = (int)Mode.Light;
            this.IsHistoryPanelOpen = false;
            this.IsHistorySelected = false;
            this.IsServerPanelOpen = false;

            ///////////////
            // Testing data

            SpeedDataCollectionViewModel speedData = new SpeedDataCollectionViewModel();
            this.SpeedDataCollection = speedData.SpeedDataCollection;

            ServerCollectionViewModel servers = new ServerCollectionViewModel();
            this.ServersCollection = servers.ServerDataCollection;
            this.ServerNamesCollection = servers.ServerNamesCollection;
            this._allServerNamesCollection = servers.ServerNamesCollection;

            this.NewServerNameLocationAssign();
            ///////////////
        }

        #endregion

        #region Mainboard Actions from Comands 

        private async void StartSpeedTest(object param) 
        {
            await new Windows.UI.Popups.MessageDialog("StartSpeedTest()").ShowAsync();
        }

        private async void BackCalling(object param)
        {
            await new Windows.UI.Popups.MessageDialog("BackCalling()").ShowAsync();
        }

        private void HistoryCalling(object param)
        {
            if (this.IsHistoryPanelOpen)
            {
                this.IsHistoryPanelOpen = false;
            }
            else
            {
                this.IsHistoryPanelOpen = true;
            }                    
        }

        private void SettingsCalling(object param)
        {
            this.IsSettingsPaneOpen = true;
        }

        private void ChangeServerCalling(object param)
        {
            this.IsServerPanelOpen = true;
        }

        #endregion

        #region Settings Actions from Commands

        private void SettingSplitViewDontClosing(object sender)
        {           
            this.IsSettingsPaneOpen = false;            
        }

        private async void LanguageChange(object param)
        {
            await new Windows.UI.Popups.MessageDialog(param.ToString()).ShowAsync();
        }

        private async void ModeChanged(object param)
        {
            await new Windows.UI.Popups.MessageDialog(param.ToString()).ShowAsync();
        }

        #endregion

        #region History Actions from Commands

        private async void DeleteHistory(object param)
        {                                   
            Style buttonStyle = CreateButtonStyle();
            ContentDialog deleteFileDialog = CreateContentDialog(buttonStyle);
            ContentDialogResult result = await deleteFileDialog.ShowAsync();

                // Delete the file if the user clicked the primary button.
                /// Otherwise, do nothing.
            if (result == ContentDialogResult.Primary)
                {
                    // Delete the file.
                }
            else
                {
                    // The user clicked the CLoseButton, pressed ESC, Gamepad B, or the system back button.
                    // Do nothing.
                }
            
        }
        
        private void CloseHistory(object param)
        {
            this.IsHistoryPanelOpen = false;
        }

        private async void SingleHistoryDeleted(object param)
        {
            await new Windows.UI.Popups.MessageDialog("SingleHistoryDeleted()").ShowAsync();
        }

        private void SingleHistorySelecting(object param)
        {
            SpeedDataViewModel newSingleHistorySelected = (SpeedDataViewModel)param;
                        
            SpeedDataViewModel filteredHistory = SpeedDataCollection.FirstOrDefault(h => h.Id == newSingleHistorySelected.Id);
            
            filteredHistory.IsSelected = true;

            if (this._oldHistoryValue == null)
            {
                this._oldHistoryValue = filteredHistory;
            }
            else
            {
                this._oldHistoryValue.IsSelected = false;
                this._oldHistoryValue = filteredHistory;
            }
        }

        #endregion

        #region Server Actions from Commands

        private void ServerNameTextChanged(object param)
        {
            string inputText = (string)param;

            ObservableCollection<string> serverResults = FindServerInCollection(inputText);            

            if (serverResults != null)
            {
                this.ServerNamesCollection = serverResults;
            }            
        }

        private void SingleServerSelecting(object param)
        {
            string selectingServer = (string)param;

            this.UnsetCurrentServer();
            this.SetCurrentServer(selectingServer);
            this.NewServerNameLocationAssign();
        }

        private void CloseServerPanel(object param)
        {
            this.IsServerPanelOpen = false;
        }

        #endregion

        #region Helpful methods

        private ObservableCollection<string> FindServerInCollection(string inputText)
        {
            var serversResults = _allServerNamesCollection.Where(s => s.ToLower().Contains(inputText.ToLower()));

            ObservableCollection<string> castServersResults = new ObservableCollection<string>(serversResults);

            return castServersResults;
        }

        private void UnsetCurrentServer()
        {
            ServerViewModel currentServer = this.ServersCollection.FirstOrDefault(s => s.IsCurrent == true);
            if (currentServer != null)
            {
                currentServer.IsCurrent = false;
            }
        }

        private void SetCurrentServer(string selectingServer)
        {
            ServerViewModel newCurrentServer = this.ServersCollection.FirstOrDefault(s => s.ProviderName == selectingServer);
            if (newCurrentServer != null)
            {
                newCurrentServer.IsCurrent = true;
            }
        }

        private void SetCurrentServer(ServerViewModel selectingServer)
        {
            ServerViewModel newCurrentServer = this.ServersCollection.FirstOrDefault(s => s == selectingServer);
            if (newCurrentServer != null)
            {
                newCurrentServer.IsCurrent = true;
            }
        }

        private void NewServerNameLocationAssign()
        {
            this.ServerName = this.ServersCollection.FirstOrDefault(s => s.IsCurrent == true)?.ProviderName;
            this.ServerLocation = this.ServersCollection.FirstOrDefault(s => s.IsCurrent == true)?.Location;
        }

        private Style CreateButtonStyle()
        {
            LinearGradientBrush gradientBrush = new LinearGradientBrush();
            GradientStop firstGradient = new GradientStop();
            GradientStop secondGradient = new GradientStop();
            firstGradient.Color = Color.FromArgb(255, 20, 206, 236);
            firstGradient.Offset = 1;
            secondGradient.Color = Color.FromArgb(255, 16, 23, 87);
            secondGradient.Offset = 0;

            gradientBrush.GradientStops.Add(firstGradient);
            gradientBrush.GradientStops.Add(secondGradient);
            gradientBrush.EndPoint = new Point(0, 1);
            gradientBrush.StartPoint = new Point(0.6, 0);

            var buttonStyle = new Style(typeof(Button));
            buttonStyle.Setters.Add(new Setter(Button.BackgroundProperty, gradientBrush));
            buttonStyle.Setters.Add(new Setter(Button.ForegroundProperty, Colors.White));

            return buttonStyle;
        }

        private ContentDialog CreateContentDialog(Style buttonStyle)
        {
            ContentDialog FileDialog = new ContentDialog
            {
                Title = "Clear Application history?",
                Content = "Do you really want to delete the history?" + "\n" + "To Continue press 'Clear history', this may take a while.",
                PrimaryButtonText = "Clear history",
                CloseButtonText = "Cancel",
                PrimaryButtonStyle = buttonStyle
            };

            return FileDialog;
        }

        #endregion
    }
}
