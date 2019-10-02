using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.ApplicationModel.Resources.Core;
using Windows.Storage;

namespace SpeedTestUWP.ViewModel.Helpers
{
    public class LocalizationManager : INotifyPropertyChanged
    {
        private ResourceContext resoureContext;
        private readonly string basicLanguage = "en-US";

        public string AppLanguage
        {
            get
            {
                if (ApplicationData.Current.LocalSettings.Values.ContainsKey("AppLanguage"))
                {
                    return (string)ApplicationData.Current.LocalSettings.Values["AppLanguage"];
                }
                else
                {
                    return null;
                }
            }
            private set
            {
                ApplicationData.Current.LocalSettings.Values["AppLanguage"] = value;
            }
        }
        public ObservableCollection<Language> Languages { get; private set; }
        public string this[string key] => this.GetResource(key);

        public event PropertyChangedEventHandler PropertyChanged;

        public LocalizationManager()
        {
            this.LanguagesCollectionInitialization();
            this.LanguageInitialization();
        }

        public void ChangeLanguage(string langCode)
        {
            this.UpdateCulture(langCode);
        }

        public void UpdateCulture(string language)
        {
            this.resoureContext = ResourceContext.GetForCurrentView();
            this.resoureContext.Languages = new List<string> { language };
            this.AppLanguage = language;
            this.LocalizeToastText();
            this.OnPropertyChanged("Item[]");
        }

        private string GetResource(string stringResource)
        {
            try
            {
                var resourceStringMap = ResourceManager.Current.MainResourceMap.GetSubtree("Resources");
                return resourceStringMap.GetValue(stringResource, this.resoureContext).ValueAsString;
            }
            catch
            {
                return $"?{stringResource}?";
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LanguageInitialization()
        {
            if (this.AppLanguage == null)
            {
                this.UpdateCulture(this.basicLanguage);
            }
            else
            {
                this.UpdateCulture(this.AppLanguage);
            }
        }

        private void LanguagesCollectionInitialization()
        {
            this.Languages = new ObservableCollection<Language>
            {
                new Language { DisplayName = "English", LanguageCode = "en-US" },
                new Language { DisplayName = "Русский", LanguageCode = "ru" }
            };
        }

        private void LocalizeToastText()
        {
            ApplicationData.Current.LocalSettings.Values["ToastPingText"] = this.GetResource("ToastPingText");
            ApplicationData.Current.LocalSettings.Values["ToastDownloadText"] = this.GetResource("ToastDownloadText");
            ApplicationData.Current.LocalSettings.Values["ToastUploadText"] = this.GetResource("ToastUploadText");
            ApplicationData.Current.LocalSettings.Values["MeasurePingValue"] = this.GetResource("MeasurePingValue");
            ApplicationData.Current.LocalSettings.Values["MeasureSpeedValue"] = this.GetResource("MeasureSpeedValue");
        }
    }

    public class Language
    {
        public string DisplayName { get; set; }
        public string LanguageCode { get; set; }
    }
}
