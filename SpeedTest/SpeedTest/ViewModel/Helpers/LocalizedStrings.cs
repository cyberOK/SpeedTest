using System;
using System.Collections.Generic;
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
    public class LocalizedStrings : INotifyPropertyChanged
    {
        private ResourceContext resoureContext;
        private string basicLanguage = "en-US";

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
        public string this[string key] => this.GetResource(key);

        public event PropertyChangedEventHandler PropertyChanged;

        public LocalizedStrings()
        {
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
            this.OnPropertyChanged("Item[]");
        }

        public string GetResource(string stringResource)
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
    }
}
