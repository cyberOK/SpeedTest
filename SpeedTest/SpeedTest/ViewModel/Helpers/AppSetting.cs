using SpeedTestUWP.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTestUWP.ViewModel.Helpers
{
    public class Language
    {
        public string DisplayName { get; set; }
        public string LanguageCode { get; set; }
    }

    public class AppSetting : ObservableObject
    {
        private string _theme;
        private ObservableCollection<Language> _languages;
        
        public string ProgramName { get; private set; } 
        public string Version { get; private set; }          
        public string Theme
        {
            get { return this._theme; }
            set { Set(ref this._theme, value); }
        }
        public ObservableCollection<Language> Languages
        {
            get { return this._languages; }
            private set { Set(ref this._languages, value); }
        }

        public AppSetting()
        {
            this.ProgramName = typeof(App).GetTypeInfo().Assembly.GetName().Name;
            this.Version = "Version: " + typeof(App).GetTypeInfo().Assembly.GetName().Version.ToString();
            this.Theme = this.GetUserTheme();
            this.Languages = new ObservableCollection<Language>
            {
                new Language { DisplayName = "English", LanguageCode = "en-US" },
                new Language { DisplayName = "Русский", LanguageCode = "ru" }
            };
        }    

        public string GetUserTheme()
        {
            var DefaultTheme = new Windows.UI.ViewManagement.UISettings();
            var uiTheme = DefaultTheme.GetColorValue(Windows.UI.ViewManagement.UIColorType.Background).ToString();

            if (uiTheme == "#FF000000")
            {
                this.Theme = "Dark";
            }
            else // (uiTheme == "#FFFFFFFF")
            {
                this.Theme = "Light";
            }

            return this.Theme;
        }
    }
}
