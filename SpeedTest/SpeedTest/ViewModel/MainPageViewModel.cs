using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SpeedTestUWP.Tlles;
using SpeedTestUWP.ViewModel.Helpers;
using SpeedTestUWP.ViewModel.ViewBoards;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Globalization;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Microsoft.Toolkit.Uwp.UI;
using SpeedTestModel.HistoryManager;
using SpeedTestModel.ServerIPerfProvider;
using SpeedTestUWP.BackgroundSpeedTest;
using Windows.Storage;
using Windows.Foundation.Metadata;
using Windows.ApplicationModel;
using Windows.UI.StartScreen;
using SpeedTestModel;
using SpeedTestModel.Iperf;
using SpeedTestModel.IPerf;
using System.ComponentModel;
using Windows.ApplicationModel.Resources.Core;

namespace SpeedTestUWP.ViewModel
{
    public class MainPageViewModel : ObservableObject
    {
        #region Fields

        private int id = 0;
        private bool isPopupGridRaise;
        private bool isPhoneMainPanelOpen;
        private IperfWrapper iPerfInstance;
        private DataBoard dataBoard;
        private ServerInformationBoard serverInformationBoard;
        private ArcBoard arcBoard;
        private SettingsPanel settingsPanel;
        private HistoryPanel historyPanel;
        private ServerPanel serverPanel;

        #endregion

        #region Properties

        public ResourceLoader ResourceManager { get; private set; }
        public BackgroundHelper BackgroundTestManager { get; private set; }
        public HistoryManager HistoryManager { get; private set; }
        public ServerIPerfManager ServerIPerfManager { get; private set; }

        #endregion

        #region Property binding

        public bool IsPopupGridRaise
        {
            get { return this.isPopupGridRaise; }
            private set { Set(ref this.isPopupGridRaise, value); }
        }

        public bool IsPhoneMainPanelOpen
        {
            get { return this.isPhoneMainPanelOpen; }
            private set { Set(ref this.isPhoneMainPanelOpen, value); }
        }

        public IperfWrapper IPerfInstance
        {
            get { return this.iPerfInstance; }
            private set { Set(ref this.iPerfInstance, value); }
        }

        public ArcBoard ArcBoard
        {
            get { return arcBoard; }
            private set { Set(ref arcBoard, value); }
        }

        public DataBoard DataBoard
        {
            get { return dataBoard; }
            private set { Set(ref dataBoard, value); }
        }

        public ServerInformationBoard ServerInformationBoard
        {
            get { return serverInformationBoard; }
            private set { Set(ref serverInformationBoard, value); }
        }

        public SettingsPanel SettingsPanel
        {
            get { return settingsPanel; }
            private set { Set(ref settingsPanel, value); }
        }

        public HistoryPanel HistoryPanel
        {
            get { return historyPanel; }
            private set { Set(ref historyPanel, value); }
        }

        public ServerPanel ServerPanel
        {
            get { return serverPanel; }
            private set { Set(ref serverPanel, value); }
        }

        #endregion

        #region Property commands

        // MainPage properties commands
        public SpeedTestCommand MainPageLoadedCommand { get; private set; }
        
        // Mainboard properties commands
        public SpeedTestCommand StartButtonPressed { get; private set; }
        public SpeedTestCommand BackButtonPressed { get; private set; }
        public SpeedTestCommand HistoryButtonPressed { get; private set; }
        public SpeedTestCommand SettingsButtonPressed { get; private set; }
        public SpeedTestCommand ChangeServerButtonPressed { get; private set; }
        public SpeedTestCommand GamburgerButtonPressed { get; private set; }

        // Settings panel properties commands
        public SpeedTestCommand BackgroundTestToggleSwitch { get; private set; }
        public SpeedTestCommand SettingSplitViewClosing { get; private set; }
        public SpeedTestCommand LanguageComboBoxChanged { get; private set; }
        public SpeedTestCommand SelectedItemRadioButtonChanged { get; private set; }

        // History panel properties commands
        public SpeedTestCommand DeleteHistoryButtonPressed { get; private set; }
        public SpeedTestCommand CloseHistoryButtonPressed { get; private set; }
        public SpeedTestCommand SingleHistoryDeletedButtonPressed { get; private set; }
        public SpeedTestCommand SingleHistorySelected { get; private set; }
        public SpeedTestCommand DeleteHistoryContentDialogButtonPressed { get; private set; }

        // Server panel properties commands
        public SpeedTestCommand ServerSuggestBoxTextChanged { get; private set; }
        public SpeedTestCommand SingleServerSelected { get; private set; }
        public SpeedTestCommand CloseServerPanelButtonPressed { get; private set; }

        #endregion

        #region Constructor

        public MainPageViewModel()
        {
            // Initialization Helpers
            this.ResourceManager = new ResourceLoader();
            this.BackgroundTestManager = new BackgroundHelper();
            this.HistoryManager = new HistoryManager();
            this.ServerIPerfManager = new ServerIPerfManager();

            // IperfInstance Initialization
            this.IPerfInstance = new IperfWrapper();
            this.IPerfInstance.ErrorRecieved += Model_ErrorRecieved; ;
            this.IPerfInstance.ConnectingDataRecieved += Model_ConnectingDataRecieved;
            this.IPerfInstance.ConnectedDataRecieved += Model_ConnectedDataRecieved;
            this.IPerfInstance.PingDataRecieved += Model_PingDataRecieved;
            this.IPerfInstance.DownloadDataRecieved += Model_DownloadDataRecieved;
            this.IPerfInstance.UploadDataRecieved += Model_UploadDataRecieved;
            this.IPerfInstance.AverageDowloadDataRecieved += Model_AverageDowloadDataRecieved;
            this.IPerfInstance.AverageUploadDataRecieved += Model_AverageUploadDataRecieved;

            // Initialization MainPageViewModel
            this.DataBoard = new DataBoard();
            this.ArcBoard = new ArcBoard();
            this.ServerInformationBoard = new ServerInformationBoard();
            this.HistoryPanel = new HistoryPanel();

            // Set ViewModel from database
            //this.ServerIPerfManager.SaveRange(this.ServerIPerfManager.BaseServerCollection());  // FIT DATABASE 
            List<ServerIPerf> serversIPerf = this.ServerIPerfManager.GetServersList();
            this.ServerPanel = new ServerPanel(new AdvancedCollectionView(serversIPerf, true));
            this.ServerPanel.ServersCollection.CurrentChanged += ServersCollectionView_CurrentChanged;
            this.ServerPanel.ServersCollection.CurrentItem = serversIPerf.FirstOrDefault(s => s.IsCurrent == true);

            this.SettingsPanel = new SettingsPanel
            {
                Settings = new AppSetting(),
                SelectedMode = 2,                 // Set Windows Default Theme when app start
                IsBackgroundTestEnable = true     // Enable BackgroundSpeedTest when app start 
            };

            // MainPage commands assigning
            this.MainPageLoadedCommand = new SpeedTestCommand(new Action<object>(InitializePage));          
            // Main panel commands assigning
            this.StartButtonPressed = new SpeedTestCommand(new Action<object>(StartSpeedTest));
            this.BackButtonPressed = new SpeedTestCommand(new Action<object>(ShareCalling));
            this.HistoryButtonPressed = new SpeedTestCommand(new Action<object>(HistoryCalling));
            this.SettingsButtonPressed = new SpeedTestCommand(new Action<object>(SettingsCalling));
            this.ChangeServerButtonPressed = new SpeedTestCommand(new Action<object>(ChangeServerCalling));
            this.GamburgerButtonPressed = new SpeedTestCommand(new Action<object>(PhoneMainPanelCalling));
            // Settings panel commands assigning         
            this.BackgroundTestToggleSwitch = new SpeedTestCommand(new Action<object>(BackgroundTestToggle));
            this.SettingSplitViewClosing = new SpeedTestCommand(new Action<object>(SettingsClosing));
            this.LanguageComboBoxChanged = new SpeedTestCommand(new Action<object>(LanguageChange));
            this.SelectedItemRadioButtonChanged = new SpeedTestCommand(new Action<object>(ModeChanged));
            // History panel commands assigning
            this.DeleteHistoryButtonPressed = new SpeedTestCommand(new Action<object>(CallDeleteHistoryDialog));
            this.CloseHistoryButtonPressed = new SpeedTestCommand(new Action<object>(CloseHistory));
            this.SingleHistoryDeletedButtonPressed = new SpeedTestCommand(new Action<object>(SingleHistoryDeleting));
            this.SingleHistorySelected = new SpeedTestCommand(new Action<object>(SingleHistorySelecting));
            this.DeleteHistoryContentDialogButtonPressed = new SpeedTestCommand(new Action<object>(DeleteHistory));
            // Server panel commands assigning
            this.ServerSuggestBoxTextChanged = new SpeedTestCommand(new Action<object>(ServerNameTextChanged));
            this.SingleServerSelected = new SpeedTestCommand(new Action<object>(SingleServerSelecting));
            this.CloseServerPanelButtonPressed = new SpeedTestCommand(new Action<object>(CloseServerPanel));
        }

        #endregion

        #region MainPage Load Actions for Delegates

        private async void InitializePage(object param)
        {
            this.FirstStartAppSuggestingAddLiveTile();
            this.LoadTestsHistory();
        }        

        #endregion

        #region Mainboard Actions for Delegates

        private void StartSpeedTest(object param)
        {
            // Set View Components

            this.ArcBoard.IsStartButtonPressed = true;
            this.ArcBoard.IsTryConnect = true;
            this.ArcBoard.IsSpeedMeterBackgroundVisible = true;
            this.ArcBoard.IsDownloadSpeedDataRecieved = false;
            this.ArcBoard.IsUploadSpeedDataRecieved = false;
            this.ArcBoard.IsStartArcVisible = false;
            this.DataBoard.IsPingFieldsGridVisible = false;
            this.DataBoard.IsDownloadSpeedFieldsGridVisible = false;
            this.DataBoard.IsUploadSpeedFieldsGridVisible = false;

            // Start Download Test

            this.StartSpeedTest();         
        }

        private async void ShareCalling(object param)
        {
            this.ClosePhoneGrid();
            this.IsPopupGridRaise = false;
            await new Windows.UI.Popups.MessageDialog("ShareCalling()").ShowAsync();
        }

        private void HistoryCalling(object param)
        {
            this.ClosePhoneGrid();
            this.IsPopupGridRaise = true;
            this.HistoryPanel.IsHistoryPanelOpen = true;
        }

        private void SettingsCalling(object param)
        {
            this.ClosePhoneGrid();
            this.IsPopupGridRaise = true;
            this.SettingsPanel.IsSettingsPaneOpen = true;
        }

        private void ChangeServerCalling(object param)
        {
            this.IsPopupGridRaise = true;
            this.ServerPanel.IsServerPanelOpen = true;

            // Rollback filtration           
            this.ServerPanel.ServersCollection.Filter = null;
            this.ServerPanel.ServersCollection.RefreshFilter();
            this.serverPanel.IsNoresults = false;
        }

        private void PhoneMainPanelCalling(object param)
        {
            if (this.IsPhoneMainPanelOpen)
            {
                this.IsPopupGridRaise = false;
                this.IsPhoneMainPanelOpen = false;
            }
            else
            {
                this.IsPopupGridRaise = true;
                this.IsPhoneMainPanelOpen = true;
            }
        }

        #endregion

        #region Settings Actions for Delegates
        
        private void BackgroundTestToggle(object param)
        {
            bool isBackgroundTestOn = (bool)param;

            if (isBackgroundTestOn)
            {
                this.BackgroundTestManager.StartBackgroundSpeedTest();
            }

            else
            {
                this.BackgroundTestManager.StopBackgroundSpeedTest();
            }
        }

        private void LanguageChange(object param)
        {
            Helpers.Language chosenLanguage = (Helpers.Language)param;

            string langCode = chosenLanguage.LanguageCode;

            ApplicationLanguages.PrimaryLanguageOverride = langCode;            

            Frame mainPage = Window.Current.Content as Frame;             
            mainPage.Navigate(typeof(MainPage), null, new SuppressNavigationTransitionInfo());
        }

        private void ModeChanged(object param)
        {
            string selectedMode = (string)param;

            if (selectedMode == "Dark")
            {
                this.SettingsPanel.SelectedMode = 1;
                this.SettingsPanel.Settings.Theme = "Dark";
            }

            else if (selectedMode == "Light")
            {
                this.SettingsPanel.SelectedMode = 0;
                this.SettingsPanel.Settings.Theme = "Light";
            }

            else if (selectedMode == "Default")
            {
                this.SettingsPanel.SelectedMode = 2;
                this.SettingsPanel.Settings.Theme = this.SettingsPanel.Settings.GetUserTheme();
            }
        }

        private void SettingsClosing(object param)
        {
            this.SettingsPanel.IsSettingsPaneOpen = false;
            this.IsPopupGridRaise = false;
        }

        #endregion

        #region History Actions for Delegates

        private async void SingleHistoryDeleting(object param)
        {
            SpeedDataViewModel singleHistoryForDeleting = (SpeedDataViewModel)param;

            this.HistoryPanel.SpeedDataCollection?.Remove(singleHistoryForDeleting);
            await this.HistoryManager.DeleteSingleSample(singleHistoryForDeleting.Id);
        }

        private async void DeleteHistory(object param)
        {
            this.HistoryPanel.SpeedDataCollection.Clear();
            await this.HistoryManager.Delete();
            this.id = 0;
        }

        private void SingleHistorySelecting(object param)
        {
            SpeedDataViewModel newSingleHistorySelected = (SpeedDataViewModel)param;

            SpeedDataViewModel filteredHistory = this.HistoryPanel.SpeedDataCollection.FirstOrDefault(h => h.Id == newSingleHistorySelected?.Id);

            if (filteredHistory != null)
            {
                filteredHistory.IsSelected = true;
            }

            if (this.HistoryPanel.OldSelectedHistoryValue == null)
            {
                this.HistoryPanel.OldSelectedHistoryValue = filteredHistory;
            }
            else
            {
                this.HistoryPanel.OldSelectedHistoryValue.IsSelected = false;
                this.HistoryPanel.OldSelectedHistoryValue = filteredHistory;
            }
        }

        private async void CallDeleteHistoryDialog(object param)
        {
            if (this.HistoryPanel.SpeedDataCollection.Count != 0)
            {
                DeleteDialog dD = new DeleteDialog();

                await dD.ShowAsync();
            }
        }

        private void CloseHistory(object param)
        {
            this.IsPopupGridRaise = false;
            this.HistoryPanel.IsHistoryPanelOpen = false;
        }

        #endregion

        #region Server Actions from Commands

        private void ServerNameTextChanged(object param)
        {
            string inputText = (string)param;

            this.ServerPanel.ServersCollection.Filter = s => ((ServerIPerf)s).ProviderName.ToLower().Contains(inputText.ToLower()); 

            if (this.ServerPanel.ServersCollection.Count == 0)
            {
                this.serverPanel.IsNoresults = true;
            }

            else
            {
                this.serverPanel.IsNoresults = false;
            }
        }

        private void SingleServerSelecting(object param)
        {
            ServerIPerf selectingServer = (ServerIPerf)param;

            this.ServerPanel.ServersCollection.CurrentItem = selectingServer;
        }

        private void ServersCollectionView_CurrentChanged(object sender, object e)
        {
            this.ServerInformationBoard.CurrentServer = (ServerIPerf)this.ServerPanel.ServersCollection.CurrentItem;
        }

        private void CloseServerPanel(object param)
        {
            this.IsPopupGridRaise = false;
            this.ServerPanel.IsServerPanelOpen = false;
        }

        #endregion

        #region IPerfInstance Methods

        private void StartSpeedTest()
        {
            this.ArcBoard.IsTryConnect = true;

            string currentHostName = this.ServerInformationBoard.CurrentServer.IPerf3Server;
            int currentHostPort = this.ServerInformationBoard.CurrentServer.Port;

            this.IPerfInstance.StartSpeedTest(currentHostName, currentHostPort);
        }

        private async void Model_ErrorRecieved(object sender, SpeedTestModel.SpeedTestEventArgs.ErrorsEventArgs e)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                TestMode testMode = e.TestMode;

                switch (testMode)
                {
                    case TestMode.Download:

                        this.ArcBoard.IsStartButtonPressed = false;
                        this.ArcBoard.IsStartArcVisible = true;
                        this.ArcBoard.IsTryConnect = false;
                        this.ArcBoard.IsSpeedMeterBackgroundVisible = false;
                        this.ArcBoard.IsSpeedDataNumbersReceiving = false;
                        this.ArcBoard.IsDownloadSpeedDataRecieved = false;
                        this.DataBoard.IsPingFieldsGridVisible = false;
                        this.DataBoard.IsDownloadSpeedFieldsGridVisible = false;

                        break;

                    case TestMode.Upload:

                        this.ArcBoard.IsStartButtonPressed = true;
                        this.ArcBoard.IsStartArcVisible = false;

                        break;
                }
            });
        }

        private async void Model_ConnectingDataRecieved(object sender, SpeedTestModel.SpeedTestEventArgs.ConnectingEventArgs e)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                TestMode testMode = e.TestMode;

                switch (testMode)
                {
                    case TestMode.Download:

                        this.ArcBoard.IsTryConnect = false;
                        this.ArcBoard.IsSpeedMeterBackgroundVisible = true;

                        break;

                    case TestMode.Upload:

                        this.ArcBoard.IsStartButtonPressed = true;
                        this.ArcBoard.IsTryConnect = false;

                        break;
                }
            });
        }

        private async void Model_ConnectedDataRecieved(object sender, SpeedTestModel.SpeedTestEventArgs.ConnectedEventArgs e) 
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                TestMode testMode = e.TestMode;

                switch (testMode)
                {
                    case TestMode.Download:

                        this.ServerInformationBoard.CurrentHostIpAdress = e.HostIp;

                        break;

                    case TestMode.Upload:

                        break;
                }
            });
        }

        private async void Model_PingDataRecieved(object sender, SpeedTestModel.SpeedTestEventArgs.PingEventArgs e)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                int ping = e.Ping;

                // Set View
                this.DataBoard.IsPingFieldsGridVisible = true;
                this.DataBoard.PingData = ping.ToString();

                // Set Current SpeedData Sample
                this.HistoryPanel.CurrentSpeedDataSample = new SpeedDataViewModel
                {
                    Date = DateTime.Now,
                    Ping = ping.ToString(),
                    Server = this.ServerInformationBoard.CurrentServer.IPerf3Server,
                    IsSelected = false,
                    Id = ++this.id
                };
            });
        }

        private async void Model_DownloadDataRecieved(object sender, SpeedTestModel.SpeedTestEventArgs.DownloadSpeedEventArgs e)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                // Set View Elements

                this.ArcBoard.IsDownloadSpeedDataRecieved = true;
                this.ArcBoard.IsSpeedDataNumbersReceiving = true;
                this.ArcBoard.IsStartButtonPressed = true;
                this.ArcBoard.IsStartArcVisible = false;
                this.ArcBoard.IsTryConnect = false;
                this.ArcBoard.IsSpeedMeterBackgroundVisible = true;

                // Set Arc Board from Recieving Data

                string measureValue = this.ResourceManager.GetString("MeasureSpeedValue");

                this.ArcBoard.DownloadSpeedArcValue = e.DownloadSpeed;
                this.ArcBoard.SpeedDataNumbers = e.DownloadSpeed + " " + measureValue;
            });                      
        }

        private async void Model_UploadDataRecieved(object sender, SpeedTestModel.SpeedTestEventArgs.UploadSpeedEventArgs e)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if (e.IsTestEnd == false)
                {
                    // Set View Elements   

                    this.ArcBoard.IsUploadSpeedDataRecieved = true;
                    this.ArcBoard.IsStartButtonPressed = true;
                    this.ArcBoard.IsStartArcVisible = false;
                    this.ArcBoard.IsTryConnect = false;
                    this.ArcBoard.IsSpeedMeterBackgroundVisible = true;

                    // Set Arc Board from Recieving Data

                    string measureValue = this.ResourceManager.GetString("MeasureSpeedValue");

                    this.ArcBoard.UploadSpeedArcValue = e.UploadSpeed;
                    this.ArcBoard.SpeedDataNumbers = e.UploadSpeed + " " + measureValue;
                }

                else // If Test Ended set View elements
                {
                    this.ArcBoard.IsSpeedDataNumbersReceiving = false;
                    this.ArcBoard.IsStartButtonPressed = false;
                }
            });
        }

        private async void Model_AverageDowloadDataRecieved(object sender, SpeedTestModel.SpeedTestEventArgs.AverageDownloadDataEventArgs e)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {                         
                // Set View
                this.DataBoard.DownloadSpeedData = ((int)(e.AverageDownloadSpeed)).ToString();
                this.DataBoard.IsDownloadSpeedFieldsGridVisible = true;

                // Set Current SpeedData Sample
                this.HistoryPanel.CurrentSpeedDataSample.DownloadSpeed = Math.Truncate(e.AverageDownloadSpeed * 100) / 100;
            });
        }

        private async void Model_AverageUploadDataRecieved(object sender, SpeedTestModel.SpeedTestEventArgs.AverageUploadDataEventArgs e)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                // Set View
                this.DataBoard.UploadSpeedData = ((int)(e.AverageUploadSpeed)).ToString();
                this.DataBoard.IsUploadSpeedFieldsGridVisible = true;

                // Set Current SpeedData Sample
                this.HistoryPanel.CurrentSpeedDataSample.UploadSpeed = Math.Truncate(e.AverageUploadSpeed * 100) / 100;

                // Add Speed Sample to History Collection
                this.HistoryPanel.SpeedDataCollection.Add(this.HistoryPanel.CurrentSpeedDataSample);
                this.HistoryManager.Save(this.SpeedDataConversion(this.HistoryPanel.CurrentSpeedDataSample));

                // Create LiveTile
                TileSpeedTest.CreateTile(this.DataBoard.PingData, this.DataBoard.DownloadSpeedData, this.DataBoard.UploadSpeedData, this.ResourceManager);
            });
        }

        #endregion

        #region Helpful methods

        private async void FirstStartAppSuggestingAddLiveTile()
        {
            //ApplicationData.Current.LocalSettings.Values.Remove("IsNotFirstStart"); // remove after testing
            if (!ApplicationData.Current.LocalSettings.Values.ContainsKey("IsNotFirstStart")) 
            {
                if (ApiInformation.IsTypePresent("Windows.UI.StartScreen.StartScreenManager"))
                {
                    AppListEntry entry = (await Package.Current.GetAppListEntriesAsync())[0];
                    bool isSupportedStartScreen = StartScreenManager.GetDefault().SupportsAppListEntry(entry);

                    if (isSupportedStartScreen)
                    {
                        // Check if your app is currently pinned
                        bool isPinned = await StartScreenManager.GetDefault().ContainsAppListEntryAsync(entry);

                        if (!isPinned)
                        {
                            // And pin it to Start
                            await StartScreenManager.GetDefault().RequestAddAppListEntryAsync(entry);
                        }
                    }
                }
                ApplicationData.Current.LocalSettings.Values["IsNotFirstStart"] = true;
                //ApplicationData.Current.LocalSettings.Values.Remove("IsNotFirstStart"); // remove after testing
            }
        }

        private void LoadTestsHistory()
        {
            int historyCount = this.HistoryManager.Count();

            if (historyCount != 0 && !this.HistoryPanel.SpeedDataCollection.Any())
            {
                foreach (SpeedData sd in this.HistoryManager.GetHistoryList())
                {
                    
                    this.HistoryPanel.SpeedDataCollection.Add(new SpeedDataViewModel
                    {
                        Id = sd.Id,
                        IsSelected = false,
                        Server = sd.Server,
                        Date = sd.Date,
                        Ping = sd.Ping,
                        DownloadSpeed = sd.DownloadSpeed,
                        UploadSpeed = sd.UploadSpeed
                    });
                }
                this.id = this.HistoryPanel.SpeedDataCollection.Last().Id;
            }
        }

        private SpeedData SpeedDataConversion(SpeedDataViewModel sample)
        {
            return new SpeedData
            {
                Id = sample.Id,
                Server = sample.Server,
                Date = sample.Date,
                Ping = sample.Ping,
                DownloadSpeed = sample.DownloadSpeed,
                UploadSpeed = sample.UploadSpeed
            };
        }

        private void ClosePhoneGrid()
        {
            if (this.IsPhoneMainPanelOpen)
            {
                this.IsPhoneMainPanelOpen = false;
            }
        }

        #endregion
    }
}
