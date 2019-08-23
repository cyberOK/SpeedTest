﻿using SpeedTest.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTest.ViewModel.HelpfullCollections
{
    public enum Language
    {
        English,
        Russian,
        Polish,
        Ukrainian
    }

     public enum Mode
    {
        Light,
        Dark
    }

    public class AppSetting : ObservableObject
    {
        private static AppSetting _instance;
        private string _theme;

        public List<Language> Languages { get; private set; }
        public List<Mode> Modes { get; private set; }
        public string ProgramName { get; private set; } 
        public string Version { get; private set; }  
        public string AboutArticle { get;  set; } = $"Also, since we explicitly separated out the style that tragets the ListBoxItem rather than putting it inline," +
                                                    $" again as the other examples have shown, you can now create a new style off of it to customize things on a per-item basis such as spacing." +
                                                    $" (This will not work if you simply try to target ListBoxItem as the keyed style overrides generic control targets.)Also," +
                                                    $"since we explicitly separated out the style that tragets the ListBoxItem rather than putting it inline, gain as the other examples have shown," +
                                                    $"you can now create a new style off of it to customize things on a per - item basis such as spacing. " +
                                                    $"(This will not work if you simply try to target ListBoxItem as the keyed style overrides generic control targets.)" +
                                                    $" again as the other examples have shown, you can now create a new style off of it to customize things on a per-item basis such as spacing." +
                                                    $" (This will not work if you simply try to target ListBoxItem as the keyed style overrides generic control targets.)Also," +
                                                    $"since we explicitly separated out the style that tragets the ListBoxItem rather than putting it inline, gain as the other examples have shown," +
                                                    $"you can now create a new style off of it to customize things on a per - item basis such as spacing. " +
                                                    $"(This will not work if you simply try to target ListBoxItem as the keyed style overrides generic control targets.)" +
                                                    $" again as the other examples have shown, you can now create a new style off of it to customize things on a per-item basis such as spacing." +
                                                    $" (This will not work if you simply try to target ListBoxItem as the keyed style overrides generic control targets.)Also," +
                                                    $"since we explicitly separated out the style that tragets the ListBoxItem rather than putting it inline, gain as the other examples have shown," +
                                                    $"you can now create a new style off of it to customize things on a per - item basis such as spacing. " +
                                                    $"(This will not work if you simply try to target ListBoxItem as the keyed style overrides generic control targets.)";
        public string FeedBackLink { get;  set; } = $"https://docs.microsoft.com";
        public string RateLink { get;  set; } = $"https://docs.microsoft.com";

        public string Theme
        {
            get { return _theme; }
            set { Set(ref _theme, value); }
        }

        private AppSetting()
        {
            this.ProgramName = typeof(App).GetTypeInfo().Assembly.GetName().Name;
            this.Version = "Version: " + typeof(App).GetTypeInfo().Assembly.GetName().Version.ToString();
            this.Languages = Enum.GetValues(typeof(Language)).Cast<Language>().ToList();
            this.Modes = Enum.GetValues(typeof(Mode)).Cast<Mode>().ToList();
            this.Theme = this.GetWindowsTheme();            
        }
       
        public static AppSetting GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AppSetting();
            }

            return _instance;
        }

        private string GetWindowsTheme()
        {
            var DefaultTheme = new Windows.UI.ViewManagement.UISettings();
            var uiTheme = DefaultTheme.GetColorValue(Windows.UI.ViewManagement.UIColorType.Background).ToString();

            if (uiTheme == "#FF000000")
            {
                return this._theme = "Dark";
            }
            else // (uiTheme == "#FFFFFFFF")
            {
              return  this._theme = "Light";
            }
        }
    }
}
