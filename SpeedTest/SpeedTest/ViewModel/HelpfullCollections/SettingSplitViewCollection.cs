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
        Russian
    }

     public enum Mode
    {
        Light,
        Dark
    }

    public class SettingSplitViewCollection
    {        
        public SettingSplitViewCollection Settings { get; private set; }
        public List<Language> Languages { get; } = new List<Language> { Language.English, Language.Russian };
        public List<Mode> Modes { get; set; } = new List<Mode> { Mode.Light, Mode.Dark };
        public string ProgramName { get;  set; } = typeof(App).GetTypeInfo().Assembly.GetName().Name;
        public string Version { get;  set; } = typeof(App).GetTypeInfo().Assembly.GetName().Version.ToString();
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

        static SettingSplitViewCollection()
        {
            
        }

        public SettingSplitViewCollection()
        {
            this.Settings = new SettingSplitViewCollection();
        }
    }
}
