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

        private string _providerName = "ProviderName";
        private string _ipAdress = "IpAdress";
        private string _serverName = "ServerName";
        private string _serverLocation = "ServerLocation";
        private bool _isServerLoaded;
        private bool _isSettingsPaneOpen;
        private SettingViewModel _settings;
        private int _selectedMode;
        private bool _isHistoryPanelOpen;

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

        public ObservableCollection<SpeedDataViewModel> SpeedData { get; private set; }

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

            this.DeleteHistoryButtonPressed = new SpeedTestCommand(new Action<object>(DeleteHistory));
            this.CloseHistoryButtonPressed = new SpeedTestCommand(new Action<object>(CloseHistory));
           

            this.Settings = SettingViewModel.GetInstance();
            this.SelectedMode = (int)Mode.Light;
            this.IsHistoryPanelOpen = false;

            ///////////////
            ///
            SpeedDataCollectionViewModel sd = new SpeedDataCollectionViewModel();
            this.SpeedData = sd.DataColection;
            ///////////////
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

        public void HistoryCalling(object param)
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

        public void SettingsCalling(object param)
        {
            this.IsSettingsPaneOpen = true;
        }

        public async void ChangeServerCalling(object param)
        {
            await new Windows.UI.Popups.MessageDialog("ChangeServerCalling()").ShowAsync();
        }

        #endregion

        #region Setting Split View Commands

        public void SettingSplitViewDontClosing(object sender)
        {           
            this.IsSettingsPaneOpen = false;            
        }

        public async void LanguageChange(object param)
        {
            await new Windows.UI.Popups.MessageDialog(param.ToString()).ShowAsync();
        }

        public async void ModeChanged(object param)
        {
            await new Windows.UI.Popups.MessageDialog(param.ToString()).ShowAsync();
        }

        #endregion

        #region History view commands

        private async void DeleteHistory(object param)
        {
            Style primaryButtonStyle = new Style();
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

            primaryButtonStyle.Setters.Add(new Setter { Property = Control.BackgroundProperty, Value = gradientBrush });

            var bst = new Style(typeof(Button));
            bst.Setters.Add(new Setter(Button.BackgroundProperty, gradientBrush));
            bst.Setters.Add(new Setter(Button.ForegroundProperty, Colors.White));

            ContentDialog deleteFileDialog = new ContentDialog
            {
                Title = "Clear Application history?",
                Content = "Do you really want to delete the history?" + "\n" + "To Continue press 'Clear history', this may take a while.",
                PrimaryButtonText = "Clear history",
                CloseButtonText = "Cancel",
                PrimaryButtonStyle = bst
            };

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

        #endregion
    }
}
