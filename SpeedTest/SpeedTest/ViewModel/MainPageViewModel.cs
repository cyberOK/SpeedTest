﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SpeedTest.Model;
using SpeedTestIPerf.Model;
using SpeedTestIPerf.Tlles;
using SpeedTestIPerf.ViewModel.Helpers;
using SpeedTestIPerf.ViewModel.ViewBoards;
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

namespace SpeedTestIPerf.ViewModel
{
    public class MainPageViewModel : ObservableObject
    {
        #region Fields

        ResourceLoader resources; 
        private int _id = 0;
        private bool _isPopupGridRaise = false;
        private bool _isPhoneMainPanelOpen = false;
        private Model.SpeedTest _model;
        private DataBoard _dataBoard;
        private ServerInformationBoard _serverInformationBoard;
        private ArcBoard _arcBoard;
        private SettingsPanel _settingsPanel;
        private HistoryPanel _historyPanel;
        private ServerPanel _serverPanel;

        int timeOut = 0;
        int numberOfPingTests = 1;
        int numberOfDownloadTests = 10, numberOfUploadTests = 10;
        TimeSpan interval = TimeSpan.FromMilliseconds(200);
        bool downloadMode = false, uploadMode = true, testMode = false;

        #endregion

        #region Property binding

        public bool IsPopupGridRaise
        {
            get { return this._isPopupGridRaise; }
            private set { Set(ref this._isPopupGridRaise, value); }
        }

        public bool IsPhoneMainPanelOpen
        {
            get { return this._isPhoneMainPanelOpen; }
            private set { Set(ref this._isPhoneMainPanelOpen, value); }
        }

        public Model.SpeedTest Model
        {
            get { return this._model; }
            private set { Set(ref this._model, value); }
        }

        public ArcBoard ArcBoard
        {
            get { return _arcBoard; }
            private set { Set(ref _arcBoard, value); }
        }

        public DataBoard DataBoard
        {
            get { return _dataBoard; }
            private set { Set(ref _dataBoard, value); }
        }

        public ServerInformationBoard ServerInformationBoard
        {
            get { return _serverInformationBoard; }
            private set { Set(ref _serverInformationBoard, value); }
        }

        public SettingsPanel SettingsPanel
        {
            get { return _settingsPanel; }
            private set { Set(ref _settingsPanel, value); }
        }

        public HistoryPanel HistoryPanel
        {
            get { return _historyPanel; }
            private set { Set(ref _historyPanel, value); }
        }

        public ServerPanel ServerPanel
        {
            get { return _serverPanel; }
            private set { Set(ref _serverPanel, value); }
        }

        #endregion

        #region Property commands
        
        // MainPage properties commands
        public SpeedTestCommand MainPageLoadedCommand { get; private set; }
        public SpeedTestCommand MainPageUnloadedCommand { get; private set; }

        // Mainboard properties commands

        public SpeedTestCommand StartButtonPressed { get; private set; }
        public SpeedTestCommand BackButtonPressed { get; private set; }
        public SpeedTestCommand HistoryButtonPressed { get; private set; }
        public SpeedTestCommand SettingsButtonPressed { get; private set; }
        public SpeedTestCommand ChangeServerButtonPressed { get; private set; }
        public SpeedTestCommand GamburgerButtonPressed { get; private set; }
        
        // Settings panel properties commands

        public SpeedTestCommand SettingSplitViewClosing { get; private set; }
        public SpeedTestCommand LanguageComboBoxChanged { get; private set; }
        public SpeedTestCommand SelectedItemRadioButtonChanged { get; private set; }

        // History panel properties commands

        public SpeedTestCommand DeleteHistoryButtonPressed { get; private set; }       
        public SpeedTestCommand CloseHistoryButtonPressed { get; private set; }
        public SpeedTestCommand SingleHistoryDeletedButtonPressed { get; private set; }
        public SpeedTestCommand SingleHistorySelected { get; private set; }
        public SpeedTestCommand PhoneSingleHistoryDeletedButtonPressed { get; private set; }

        public SpeedTestCommand DeleteHistoryContentDialogButtonPressed { get; private set; }

        // Server panel properties commands

        public SpeedTestCommand ServerSuggestBoxTextChanged { get; private set; }
        public SpeedTestCommand SingleServerSelected { get; private set; }
        public SpeedTestCommand CloseServerPanelButtonPressed { get; private set; }
     
        #endregion

        #region Constructors

        public MainPageViewModel()
        {
            // Model Initialization

            this.Model =  new Model.SpeedTest();

            ServersCollection serversCollection = ServersCollection.GetInstance();

            this.Model.DownloudDataRecieved += Model_DownloudDataRecieved;
            this.Model.UploadDataRecieved += Model_UploadDataRecieved;

            this.Model.IPerf.ConnectingDataUpdated += IPerf_ConnectingDataUpdated;
            this.Model.IPerf.ConnectedDataUpdated += IPerf_ConnectedDataUpdated;
            this.Model.IPerf.StartOfTestDataUpdated += IPerf_StartOfTestDataUpdated;
            this.Model.IPerf.SpeedDataUpdated += IPerf_SpeedDataUpdated;
            this.Model.IPerf.PingTestDataUpdated += IPerf_PingTestDataUpdated;
            this.Model.IPerf.ErrorDataUpdated += IPerf_ErrorDataUpdated;

            // Initialization Helpers

            this.resources = new ResourceLoader();

            // Initialization MainPageViewModel

            this.DataBoard = new DataBoard();
            this.ArcBoard = new ArcBoard();

            this.SettingsPanel = new SettingsPanel
            {
                Settings = new AppSetting(),
                SelectedMode = 2 // Set Windows Default Theme when start app
            };

            this.HistoryPanel = new HistoryPanel
            {
                SpeedDataCollection = new ObservableCollection<SpeedDataViewModel>()
            };

            this.ServerPanel = new ServerPanel
            {
                ServersCollection = serversCollection.ServerDataCollection,
                ServerNamesCollection = serversCollection.GetServerNames(),
                FullServerNamesCollection = serversCollection.GetServerNames()
            };

            this.ServerInformationBoard = new ServerInformationBoard
            {
                CurrentServer = this.ServerPanel.ServersCollection.FirstOrDefault(s => s.IsCurrent == true)               
            };

            // MainPage commands assigning

            this.MainPageLoadedCommand = new SpeedTestCommand(new Action<object>(LoadingHistoryWhenAppStarting));
            this.MainPageUnloadedCommand = new SpeedTestCommand(new Action<object>(SaveHistoryWhenAppClosing));

            // Main panel commands assigning

            this.StartButtonPressed = new SpeedTestCommand(new Action<object>(StartSpeedTest));
            this.BackButtonPressed = new SpeedTestCommand(new Action<object>(ShareCalling));
            this.HistoryButtonPressed = new SpeedTestCommand(new Action<object>(HistoryCalling));
            this.SettingsButtonPressed = new SpeedTestCommand(new Action<object>(SettingsCalling));
            this.ChangeServerButtonPressed = new SpeedTestCommand(new Action<object>(ChangeServerCalling));
            this.GamburgerButtonPressed = new SpeedTestCommand(new Action<object>(PhoneMainPanelCalling));

            // Settings panel commands assigning

            this.SettingSplitViewClosing = new SpeedTestCommand(new Action<object>(SettingsClosing));
            this.LanguageComboBoxChanged = new SpeedTestCommand(new Action<object>(LanguageChange));
            this.SelectedItemRadioButtonChanged = new SpeedTestCommand(new Action<object>(ModeChanged));

            // History panel commands assigning

            this.DeleteHistoryButtonPressed = new SpeedTestCommand(new Action<object>(CallDeleteHistoryDialog));
            this.CloseHistoryButtonPressed = new SpeedTestCommand(new Action<object>(CloseHistory));
            this.SingleHistoryDeletedButtonPressed = new SpeedTestCommand(new Action<object>(SingleHistoryDeleting));
            this.SingleHistorySelected = new SpeedTestCommand(new Action<object>(SingleHistorySelecting));
            this.PhoneSingleHistoryDeletedButtonPressed = new SpeedTestCommand(new Action<object>(PhoneSingleHistoryDeleting));
            this.DeleteHistoryContentDialogButtonPressed = new SpeedTestCommand(new Action<object>(DeleteHistory));

            // Server panel commands assigning

            this.ServerSuggestBoxTextChanged = new SpeedTestCommand(new Action<object>(ServerNameTextChanged));
            this.SingleServerSelected = new SpeedTestCommand(new Action<object>(SingleServerSelecting));
            this.CloseServerPanelButtonPressed = new SpeedTestCommand(new Action<object>(CloseServerPanel));
        }

        #endregion

        #region MainPage Actions for Delegates

        private void LoadingHistoryWhenAppStarting(object param)
        {
            using (SpeedDataContext db = new SpeedDataContext())
            {
                ObservableCollection<SpeedDataViewModel> speedDataFromDatabase = new ObservableCollection<SpeedDataViewModel>();

                foreach(SpeedData sd in db.SpeedDatas.ToList())
                {
                    speedDataFromDatabase.Add(new SpeedDataViewModel
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

                this.HistoryPanel.SpeedDataCollection = speedDataFromDatabase;

                SpeedDataViewModel lastHistory;

                if (HistoryPanel.SpeedDataCollection.Any())
                {
                    lastHistory = this.HistoryPanel.SpeedDataCollection.Last();

                    if (lastHistory != null)
                    {
                        this._id = lastHistory.Id;
                    }
                }
               
            }
        }

        private void SaveHistoryWhenAppClosing(object param)
        {
            
        }

        #endregion

        #region Mainboard Actions for Delegates

        private async void StartSpeedTest(object param) 
        {
            this.ArcBoard.IsStartButtonPressed = true;
            //this.ArcBoard.IsTryConnect = true;
            //this.ArcBoard.IsSpeedMeterBackgroundVisible = true;
            //this.DataBoard.IsPingFieldsGridVisible = false;
            //this.DataBoard.IsDownloadSpeedFieldsGridVisible = false;
            //this.DataBoard.IsUploadSpeedFieldsGridVisible = false;

            //this.Model.StartTest();

            // Download Test Initialization
           
            string hostName = this.ServerInformationBoard.CurrentServer.IPerf3Server;
            int port = this.ServerInformationBoard.CurrentServer.Port;

            await this.Model.IPerf.TestNTimesAsync(hostName, port, this.numberOfDownloadTests, interval, downloadMode);
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

        private void LanguageChange(object param)
        {
            Helpers.Language chosenLanguage = (Helpers.Language)param;
            
            string langCode = chosenLanguage.LanguageCode;

            ApplicationLanguages.PrimaryLanguageOverride = langCode;

            Frame mainPage = Window.Current.Content as Frame;
            mainPage.Navigate(typeof(MainPage), null, new SuppressNavigationTransitionInfo());
        }

        private  void ModeChanged(object param)
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

        private async void CallDeleteHistoryDialog(object param)
        {       
            if (this.HistoryPanel.SpeedDataCollection.Count != 0)
            {
                DeleteDialog dD = new DeleteDialog();

                await dD.ShowAsync();
            }
        }

        private void DeleteHistory(object param)
        {
            this.HistoryPanel.SpeedDataCollection.Clear();   
            
            using (SpeedDataContext db = new SpeedDataContext())
            {
                var histories = db.SpeedDatas;

                if (histories != null)
                {
                    db.SpeedDatas?.RemoveRange(histories);
                    db.SaveChanges();
                }
            }
        }

        private void CloseHistory(object param)
        {
            this.IsPopupGridRaise = false;
            this.HistoryPanel.IsHistoryPanelOpen = false;
        }

        private void SingleHistoryDeleting(object param)
        {
            SpeedDataViewModel singleHistoryForDeleting = (SpeedDataViewModel)param;

            this.HistoryPanel.SpeedDataCollection?.Remove(singleHistoryForDeleting);

            using (SpeedDataContext db = new SpeedDataContext())
            {
                SpeedData singlehistory = db.SpeedDatas.FirstOrDefault(x => x.Id == singleHistoryForDeleting.Id);

                if (singlehistory != null)
                {
                    db.SpeedDatas?.Remove(singlehistory);
                    db.SaveChanges();
                }
            }
        }

        private void PhoneSingleHistoryDeleting(object param)
        {
            SpeedDataViewModel singleHistorySelected = (SpeedDataViewModel)param;

            this.HistoryPanel.SpeedDataCollection?.Remove(singleHistorySelected);            
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

        #endregion

        #region Server Actions from Commands

        private void ServerNameTextChanged(object param)
        {
            string inputText = (string)param;

            ObservableCollection<string> serverResults = FindServerInCollection(inputText);            

            if (serverResults != null)
            {
                this.ServerPanel.ServerNamesCollection = serverResults;
            }            
        }

        private void SingleServerSelecting(object param)
        {
            string selectingServer = (string)param;

            if (selectingServer != null && selectingServer != "No results")
            {
                this.UnsetCurrentServer();
                this.SetCurrentServer(selectingServer);
                this.RefreshServerInformationBoard();
            }
        }

        private void CloseServerPanel(object param)
        {
            this.IsPopupGridRaise = false;
            this.ServerPanel.IsServerPanelOpen = false;
        }

        #endregion

        #region Model Methods

        private async void IPerf_ErrorDataUpdated(IPerfLibrary.IPerfApp sender, IPerfLibrary.iPerfErrorReport args)
        {
            var errors = args.ErrorCode;

            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                this.ArcBoard.IsStartButtonPressed = false;
                this.ArcBoard.IsTryConnect = false;
                this.ArcBoard.IsSpeedMeterBackgroundVisible = false;
            });

        }

        private async void IPerf_ConnectingDataUpdated(IPerfLibrary.IPerfApp sender, IPerfLibrary.iPerfConnectingReport args)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                this.ArcBoard.IsTryConnect = true;
                this.ArcBoard.IsSpeedMeterBackgroundVisible = true;
            });
        }

        private async void IPerf_ConnectedDataUpdated(IPerfLibrary.IPerfApp sender, IPerfLibrary.iPerfConnectedReport args)
        {
            string hostName = this.ServerInformationBoard.CurrentServer.IPerf3Server;

            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                this.ArcBoard.IsTryConnect = false;
            });

            await this.Model.IPerf.PingTestAsync(hostName, this.timeOut, this.numberOfPingTests);
        }

        private async void IPerf_StartOfTestDataUpdated(IPerfLibrary.IPerfApp sender, IPerfLibrary.iPerfStartOfTestReport args)
        {

        }

        private async void IPerf_SpeedDataUpdated(IPerfLibrary.IPerfApp sender, IPerfLibrary.iPerfSpeedReport args)
        {    
            if (!testMode)
            {
                if (args.ReportSender.Length == 0)
                {
                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        // Set View Elements

                        this.ArcBoard.IsDownloadSpeedDataRecieved = true;
                        this.ArcBoard.IsSpeedDataNumbersReceiving = true;

                        this.ArcBoard.IsStartButtonPressed = true;
                        this.ArcBoard.IsTryConnect = false;
                        this.ArcBoard.IsSpeedMeterBackgroundVisible = true;

                        // Set Arc Board from Recieving Data

                        string[] downloadValue = args.BitrateBuf.Split(' '); ;
                        string downloadSpeed = downloadValue[0];
                        string measureValue = resources.GetString("MeasureValue");

                        double downloadSpeedParse = 0;

                        if (double.TryParse(downloadSpeed, out downloadSpeedParse))
                        {
                            if (downloadSpeedParse != 0)
                            {
                                this.ArcBoard.DownloadSpeedArcValue = downloadSpeedParse;
                                this.ArcBoard.SpeedDataNumbers = downloadSpeed + " " + measureValue;

                                // Set List of Speed Test Samples

                                SpeedDataViewModel speedDataSample = new SpeedDataViewModel
                                {
                                    Ping = this.HistoryPanel.CurrentPing,
                                    DownloadSpeed = downloadSpeedParse,
                                    UploadSpeed = 0,
                                    Server = this.ServerInformationBoard.CurrentServer.IPerf3Server,
                                    Date = DateTime.Now,
                                    Id = ++this._id
                                };

                                this.HistoryPanel.SpeedDataCollection.Add(speedDataSample);

                                // Set DataBoard

                                if (this.HistoryPanel.SpeedDataCollection != null)
                                {
                                    var testSample = this.HistoryPanel.SpeedDataCollection[this.HistoryPanel.SpeedDataCollection.Count - 1];
                                    this.DataBoard.PingData = testSample.Ping;
                                    this.DataBoard.DownloadSpeedData = ((int)testSample.DownloadSpeed).ToString();
                                    this.DataBoard.IsPingFieldsGridVisible = true;
                                    this.DataBoard.IsDownloadSpeedFieldsGridVisible = true;
                                }
                            }
                        }
                    });
                }

                else 
                {
                    string hostName = this.ServerInformationBoard.CurrentServer.IPerf3Server;
                    int port = this.ServerInformationBoard.CurrentServer.Port;

                    await this.Model.IPerf.TestNTimesAsync(hostName, port, this.numberOfUploadTests, interval, this.uploadMode);

                    this.testMode = true;
                }
            }

            else
            {
                if (args.ReportSender.Length == 0)
                {
                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        // Set View Elements   
                        
                        this.ArcBoard.IsUploadSpeedDataRecieved = true;

                        this.ArcBoard.IsStartButtonPressed = true;
                        this.ArcBoard.IsTryConnect = false;
                        this.ArcBoard.IsSpeedMeterBackgroundVisible = true;

                        // Set Arc Board from Recieving Data

                        string[] uploadValue = args.BitrateBuf.Split(' '); ;
                        string uploadSpeed = uploadValue[0];
                        string measureValue = resources.GetString("MeasureValue");

                        double uploadSpeedParse = 0;

                        if (double.TryParse(uploadSpeed, out uploadSpeedParse))
                        {
                            if (uploadSpeedParse != 0)
                            {
                                this.ArcBoard.UploadSpeedArcValue = uploadSpeedParse;
                                this.ArcBoard.SpeedDataNumbers = uploadSpeed + " " + measureValue;

                                // Set List of Speed Test Samples

                                SpeedDataViewModel speedDataSample = new SpeedDataViewModel
                                {
                                    Ping = this.HistoryPanel.CurrentPing,
                                    UploadSpeed = uploadSpeedParse,
                                    DownloadSpeed = 0,
                                    Server = this.ServerInformationBoard.CurrentServer.IPerf3Server,
                                    Date = DateTime.Now,
                                    Id = ++this._id
                                };

                                this.HistoryPanel.SpeedDataCollection.Add(speedDataSample);

                                // Set DataBoard

                                if (this.HistoryPanel.SpeedDataCollection != null)
                                {
                                    var testSample = this.HistoryPanel.SpeedDataCollection[this.HistoryPanel.SpeedDataCollection.Count - 1];
                                    this.DataBoard.PingData = testSample.Ping;
                                    this.DataBoard.UploadSpeedData = ((int)testSample.UploadSpeed).ToString();
                                    this.DataBoard.IsPingFieldsGridVisible = true;
                                    this.DataBoard.IsUploadSpeedFieldsGridVisible = true;
                                }
                            }
                        }

                    });
                }

                else if (args.ReportSender == "receiver")
                {
                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        this.testMode = false;

                        this.ArcBoard.IsSpeedDataNumbersReceiving = false;
                        this.ArcBoard.IsStartButtonPressed = false;
                    });
                }
            }
        }

        private async void IPerf_PingTestDataUpdated(IPerfLibrary.IPerfApp sender, IPerfLibrary.iPerfPingTestReport args)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var ping = args.Latency;

                // Remove 0 in the end

                while (ping % 10 == 0)
                {
                    ping = ping / 10;
                }

                this.HistoryPanel.CurrentPing = ping.ToString();
            });
        }

        // 

        private void Model_DownloudDataRecieved(object sender, Model.SpeedDataEventArgs e)
        {
            // Set View Elements 

            this.ArcBoard.IsTryConnect = false;
            this.ArcBoard.IsDownloadSpeedDataRecieved = true;
            this.ArcBoard.IsSpeedDataNumbersReceiving = true;

            // Set Arc Board from Recieving Data 

            string measureValue = resources.GetString("MeasureValue");

            this.ArcBoard.SpeedDataNumbers = e.DownloudSpeed.ToString() +" "+ measureValue;
            this.ArcBoard.DownloadSpeedArcValue = e.DownloudSpeed;

            // Set List of Speed Test Samples

            SpeedDataViewModel speedDataSample = new SpeedDataViewModel
            {
                Ping = e.Ping.ToString(),
                DownloadSpeed = (int)e.DownloudSpeed,
                Server = e.Server,
                Date = e.Date,
                Id = ++this._id
            };

            this.HistoryPanel.SpeedDataCollection.Add(speedDataSample);

            // write in Database

            using (SpeedDataContext db = new SpeedDataContext())
            {
                var speedData = new SpeedData
                {
                    Id = speedDataSample.Id,
                    Date = speedDataSample.Date,
                    Server = speedDataSample.Server,
                    DownloadSpeed = speedDataSample.DownloadSpeed,
                    UploadSpeed = 0
                };

                db.SpeedDatas.Add(speedData);
                db.SaveChanges();
            }

            // Set DataBoard Ping and Download Speed

            if (this.HistoryPanel.SpeedDataCollection != null)
            {
                var testSample = this.HistoryPanel.SpeedDataCollection[this.HistoryPanel.SpeedDataCollection.Count - 1];
                this.DataBoard.PingData = testSample.Ping.ToString();
                this.DataBoard.DownloadSpeedData = testSample.DownloadSpeed.ToString();
                this.DataBoard.IsPingFieldsGridVisible = true;
                this.DataBoard.IsDownloadSpeedFieldsGridVisible = true;
            }
        }

        private void Model_UploadDataRecieved(object sender, Model.SpeedDataEventArgs e)
        {
            // Set View Elements     
            this.ArcBoard.IsUploadSpeedDataRecieved = true;

            // Set Arc Board from Recieving Data

            this.ArcBoard.SpeedDataNumbers = e.UploadSpeed.ToString() + " Mbps";
            this.ArcBoard.UploadSpeedArcValue = e.UploadSpeed;

            // Set List of Speed Test Samples

            SpeedDataViewModel speedDataSample = new SpeedDataViewModel
            {
                Ping = e.Ping.ToString(),
                UploadSpeed = (int)e.UploadSpeed,
                Server = e.Server,
                Date = e.Date,
                Id = ++this._id
            };

            this.HistoryPanel.SpeedDataCollection.Add(speedDataSample);

            using (SpeedDataContext db = new SpeedDataContext())
            {
                var speedData = new SpeedData
                {
                    Id = speedDataSample.Id,
                    Date = speedDataSample.Date,
                    Server = speedDataSample.Server,
                    DownloadSpeed = 0,
                    UploadSpeed = speedDataSample.UploadSpeed
                };

                db.SpeedDatas.Add(speedData);
                db.SaveChanges();
            }

            // Set DataBoard Ping and Upload Speed

            if (this.HistoryPanel.SpeedDataCollection != null)
            {
                var testSample = this.HistoryPanel.SpeedDataCollection[this.HistoryPanel.SpeedDataCollection.Count - 1];
                this.DataBoard.PingData = testSample.Ping.ToString();
                this.DataBoard.UploadSpeedData = testSample.UploadSpeed.ToString();
                this.DataBoard.IsPingFieldsGridVisible = true;
                this.DataBoard.IsUploadSpeedFieldsGridVisible = true;
            }

            // Set ViewModel When Test Ending

            if  (e.IsTestEnd)
            {
                this.ArcBoard.IsSpeedMeterBackgroundVisible = false;
                this.ArcBoard.IsSpeedDataNumbersReceiving = false;
                this.ArcBoard.IsStartButtonPressed = false;
                this.ArcBoard.IsDownloadSpeedDataRecieved = false;
                this.ArcBoard.IsUploadSpeedDataRecieved = false;

                TileSpeedTest.CreateTile(this.DataBoard.PingData, this.DataBoard.DownloadSpeedData, this.DataBoard.UploadSpeedData);
            }
        }

        #endregion

        #region Helpful methods

        private ObservableCollection<string> FindServerInCollection(string inputText)
        {
            var serversResults = this.ServerPanel.FullServerNamesCollection.Where(s => s.ToLower().Contains(inputText.ToLower())).ToList();

            if (serversResults.Count == 0)
            {                              
                string outOfResult = resources.GetString("ChangeServerPanelOutOfResult");

                serversResults.Add(outOfResult);
            }

            ObservableCollection<string> castServersResults = new ObservableCollection<string>(serversResults);

            return castServersResults;
        }

        private void UnsetCurrentServer()
        {
            ServerInformation currentServer = this.ServerPanel.ServersCollection.FirstOrDefault(s => s.IsCurrent == true);
            if (currentServer != null)
            {
                currentServer.IsCurrent = false;
            }
        }

        private void SetCurrentServer(string selectingServer)
        {
            ServerInformation newCurrentServer = this.ServerPanel.ServersCollection.FirstOrDefault(s => s.ProviderName == selectingServer);

            newCurrentServer.IsCurrent = true;
        }

        private void RefreshServerInformationBoard()
        {
            this.ServerInformationBoard.CurrentServer = this.ServerPanel.ServersCollection.FirstOrDefault(s => s.IsCurrent == true);
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
