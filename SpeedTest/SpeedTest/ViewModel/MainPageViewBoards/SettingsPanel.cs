using SpeedTestUWP.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace SpeedTestUWP.ViewModel.ViewBoards
{
    public class SettingsPanel : ObservableObject
    {
        private bool isSettingsPaneOpen;
        private bool isBackgroundTestEnable = true;
        private string theme;
        private int selectedThemeMode;

        public bool IsSettingsPaneOpen
        {
            get { return isSettingsPaneOpen; }
            set { Set(ref isSettingsPaneOpen, value); }
        }

        public bool IsBackgroundTestEnable
        {
            get { return isBackgroundTestEnable; }
            set { Set(ref isBackgroundTestEnable, value); }
        }
        
        public string Theme
        {
            get { return this.theme; }
            set
            {
                ApplicationData.Current.LocalSettings.Values["AppChoosenTheme"] = value;

                if (value == "Default")
                {
                    value = this.SetWindowsTheme();
                }

                Set(ref this.theme, value);
            }
        }

        public int SelectedThemeMode
        {
            get { return this.selectedThemeMode; }
            set { Set(ref selectedThemeMode, value); }
        }

        public SettingsPanel()
        {
            this.GetUserTheme();
        }

        private string SetWindowsTheme()
        {
            Windows.UI.ViewManagement.UISettings DefaultTheme = new Windows.UI.ViewManagement.UISettings();
            string uiTheme = DefaultTheme.GetColorValue(Windows.UI.ViewManagement.UIColorType.Background).ToString();

            if (uiTheme == "#FF000000")
            {
                return "Dark";
            }
            else // (uiTheme == "#FFFFFFFF")
            {
                return "Light";
            }
        }

        private void GetUserTheme()
        {
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("AppChoosenTheme"))
            {
                this.SetUserChoosenTheme();
            }

            else
            {
                this.selectedThemeMode = 2; // Set Windows Default Theme if user dont pick
                this.Theme = this.SetWindowsTheme();
            }            
        }

        private void SetUserChoosenTheme()
        {
            switch ((string)ApplicationData.Current.LocalSettings.Values["AppChoosenTheme"])
            {
                case "Light":

                    this.SelectedThemeMode = 0;
                    this.Theme = "Light";

                    break;

                case "Dark":

                    this.SelectedThemeMode = 1;
                    this.Theme = "Dark";

                    break;

                default:

                    this.SelectedThemeMode = 2;
                    this.Theme = "Default";

                    break;
            }
        }
    }
}
