using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SpeedTest.ViewModel.Helpers;
using SpeedTest.ViewModel.HelpfullCollections;
using SpeedTest.ViewModel.ViewBoards;
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

        private ObservableCollection<Server> _serversCollection;
        private bool _isPopupGridRaise = false;
        private bool _isPhoneMainPanelOpen = false;
        private DataBoard _dataBoard;
        private ServerInformationBoard _serverInformationBoard;
        private ArcBoard _arcBoard;
        private SettingsPanel _settingsPanel;
        private HistoryPanel _historyPanel;
        private ServerPanel _serverPanel;

        #endregion

        #region Property binding

        public ObservableCollection<Server> ServersCollection
        {
            get { return this._serversCollection; }
            set { Set(ref _serversCollection, value); }
        }

        public bool IsPopupGridRaise
        {
            get { return this._isPopupGridRaise; }
            private set { Set(ref _isPopupGridRaise, value); }
        }

        public bool IsPhoneMainPanelOpen
        {
            get { return this._isPhoneMainPanelOpen; }
            private set { Set(ref this._isPhoneMainPanelOpen, value); }
        }

        public ArcBoard ArcBoard
        {
            get { return _arcBoard; }
            set { Set(ref _arcBoard, value); }
        }

        public DataBoard DataBoard
        {
            get { return _dataBoard; }
            set { Set(ref _dataBoard, value); }
        }

        public ServerInformationBoard ServerInformationBoard
        {
            get { return _serverInformationBoard; }
            set { Set(ref _serverInformationBoard, value); }
        }

        public SettingsPanel SettingsPanel
        {
            get { return _settingsPanel; }
            set { Set(ref _settingsPanel, value); }
        }

        public HistoryPanel HistoryPanel
        {
            get { return _historyPanel; }
            set { Set(ref _historyPanel, value); }
        }

        public ServerPanel ServerPanel
        {
            get { return _serverPanel; }
            set { Set(ref _serverPanel, value); }
        }

        #endregion

        #region Property commands

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
        
        // Server panel properties commands

        public SpeedTestCommand ServerSuggestBoxTextChanged { get; private set; }
        public SpeedTestCommand SingleServerSelected { get; private set; }
        public SpeedTestCommand CloseServerPanelButtonPressed { get; private set; }
     
        #endregion

        #region Constructors

        public MainPageViewModel()
        {
            // Initialization Helpers

            SpeedDataManager speedDataManager = new SpeedDataManager();
            ServerManager serverManager = new ServerManager();

            // Initialization MainPageViewModel

            this.ServersCollection = serverManager.ServerDataCollection;
            this.DataBoard = new DataBoard();
            this.ArcBoard = new ArcBoard();

            this.ServerInformationBoard = new ServerInformationBoard
            {
                ServerName = this.ServersCollection.FirstOrDefault(s => s.IsCurrent == true)?.ProviderName,
                ServerLocation = this.ServersCollection.FirstOrDefault(s => s.IsCurrent == true)?.Location
            };

            this.SettingsPanel = new SettingsPanel
            {
                Settings = AppSetting.GetInstance()
            };

            this.HistoryPanel = new HistoryPanel
            {
                SpeedDataCollection = speedDataManager.SpeedDataCollection
            };

            this.ServerPanel = new ServerPanel
            {
                ServersCollection = serverManager.ServerDataCollection,
                ServerNamesCollection = serverManager.ServerNamesCollection,
                FullServerNamesCollection = serverManager.ServerNamesCollection
            };

            // Main panel commands assigning

            this.StartButtonPressed = new SpeedTestCommand(new Action<object>(StartSpeedTest));
            this.BackButtonPressed = new SpeedTestCommand(new Action<object>(BackCalling));
            this.HistoryButtonPressed = new SpeedTestCommand(new Action<object>(HistoryCalling));
            this.SettingsButtonPressed = new SpeedTestCommand(new Action<object>(SettingsCalling));
            this.ChangeServerButtonPressed = new SpeedTestCommand(new Action<object>(ChangeServerCalling));
            this.GamburgerButtonPressed = new SpeedTestCommand(new Action<object>(PhoneMainPanelCalling));

            // Settings panel commands assigning

            this.SettingSplitViewClosing = new SpeedTestCommand(new Action<object>(SettingsClosing));
            this.LanguageComboBoxChanged = new SpeedTestCommand(new Action<object>(LanguageChange));
            this.SelectedItemRadioButtonChanged = new SpeedTestCommand(new Action<object>(ModeChanged));

            // History panel commands assigning

            this.DeleteHistoryButtonPressed = new SpeedTestCommand(new Action<object>(DeleteHistory));
            this.CloseHistoryButtonPressed = new SpeedTestCommand(new Action<object>(CloseHistory));
            this.SingleHistoryDeletedButtonPressed = new SpeedTestCommand(new Action<object>(SingleHistoryDeleting));
            this.SingleHistorySelected = new SpeedTestCommand(new Action<object>(SingleHistorySelecting));
            this.PhoneSingleHistoryDeletedButtonPressed = new SpeedTestCommand(new Action<object>(PhoneSingleHistoryDeleting));

            // Server panel commands assigning

            this.ServerSuggestBoxTextChanged = new SpeedTestCommand(new Action<object>(ServerNameTextChanged));
            this.SingleServerSelected = new SpeedTestCommand(new Action<object>(SingleServerSelecting));
            this.CloseServerPanelButtonPressed = new SpeedTestCommand(new Action<object>(CloseServerPanel));
        }

        #endregion

        #region Mainboard Actions for Delegates

        private void StartSpeedTest(object param) 
        {
            this.ArcBoard.IsStartButtonPressed = true;
            this.ArcBoard.IsTryConnect = true;
        }

        private async void BackCalling(object param)
        {
            this.ClosePhoneGrid();
            this.IsPopupGridRaise = false;

            await new Windows.UI.Popups.MessageDialog("BackCalling()").ShowAsync();
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

        private async void LanguageChange(object param)
        {
            await new Windows.UI.Popups.MessageDialog(param.ToString()).ShowAsync();
        }

        private  void ModeChanged(object param)
        {
            string selectedMode = (string)param;
            
            if (selectedMode == "Dark")
            {
                this.SettingsPanel.Settings.Theme = "Dark";
            }

            else if (selectedMode == "Light")
            {
                this.SettingsPanel.Settings.Theme = "Light";
            }
        }

        private void SettingsClosing(object param)
        {
            this.SettingsPanel.IsSettingsPaneOpen = false;
            this.IsPopupGridRaise = false;
        }

        #endregion

        #region History Actions for Delegates

        private async void DeleteHistory(object param)
        {       
            if (this.HistoryPanel.SpeedDataCollection.Count != 0)
            {
                Style buttonStyle = CreateContentDialogButtonStyle();
                ContentDialog deleteFileDialog = CreateContentDialog(buttonStyle);
                ContentDialogResult result = await deleteFileDialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    this.HistoryPanel.SpeedDataCollection.Clear();
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
            SpeedData singleHistoryForDeleting = (SpeedData)param;

            this.HistoryPanel.SpeedDataCollection?.Remove(singleHistoryForDeleting);
        }

        private void PhoneSingleHistoryDeleting(object param)
        {
            var args = (RoutedEventArgs)param;
            Button but = (Button)args.OriginalSource;

            GridViewItem gvi = this.FindParent<GridViewItem>(but);

            if (gvi != null)
            {
                SpeedData singleHistoryForDeleting = (SpeedData)gvi.Content;
                this.HistoryPanel.SpeedDataCollection?.Remove(singleHistoryForDeleting);
            }
        }

        private void SingleHistorySelecting(object param)
        {
            SpeedData newSingleHistorySelected = (SpeedData)param;
                        
            SpeedData filteredHistory = this.HistoryPanel.SpeedDataCollection.FirstOrDefault(h => h.Id == newSingleHistorySelected?.Id);
            
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
                this.NewServerNameLocationAssign();
            }
        }

        private void CloseServerPanel(object param)
        {
            this.IsPopupGridRaise = false;
            this.ServerPanel.IsServerPanelOpen = false;
        }

        #endregion

        #region Helpful methods

        private T FindParent<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);

            if (parent == null) return null;

            var parentT = parent as T;
            return parentT ?? FindParent<T>(parent);
        }

        private ObservableCollection<string> FindServerInCollection(string inputText)
        {
            var serversResults = this.ServerPanel.FullServerNamesCollection.Where(s => s.ToLower().Contains(inputText.ToLower())).ToList();

            if (serversResults.Count == 0)
            {
                serversResults.Add("No results");
            }

            ObservableCollection<string> castServersResults = new ObservableCollection<string>(serversResults);

            return castServersResults;
        }

        private void UnsetCurrentServer()
        {
            Server currentServer = this.ServerPanel.ServersCollection.FirstOrDefault(s => s.IsCurrent == true);
            if (currentServer != null)
            {
                currentServer.IsCurrent = false;
            }
        }

        private void SetCurrentServer(string selectingServer)
        {
            Server newCurrentServer = this.ServerPanel.ServersCollection.FirstOrDefault(s => s.ProviderName == selectingServer);
            if (newCurrentServer != null)
            {
                newCurrentServer.IsCurrent = true;
            }
        }

        private void SetCurrentServer(Server selectingServer)
        {
            Server newCurrentServer = this.ServerPanel.ServersCollection.FirstOrDefault(s => s == selectingServer);
            if (newCurrentServer != null)
            {
                newCurrentServer.IsCurrent = true;
            }
        }

        private void NewServerNameLocationAssign()
        {
            this.ServerInformationBoard.ServerName = this.ServersCollection.FirstOrDefault(s => s.IsCurrent == true)?.ProviderName;
            this.ServerInformationBoard.ServerLocation = this.ServersCollection.FirstOrDefault(s => s.IsCurrent == true)?.Location;
        }

        private Style CreateContentDialogButtonStyle()
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
                PrimaryButtonStyle = buttonStyle,
                RequestedTheme = ThemeNow(this.SettingsPanel.Settings.Theme)
            };

            return FileDialog;
        }
         
        private ElementTheme ThemeNow(string theme)
        {
            if (theme == "Dark")
            {
                return ElementTheme.Dark;
            }
            else
            {
                return ElementTheme.Light;
            }
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
