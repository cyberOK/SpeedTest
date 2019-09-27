using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace SpeedTestUWP.ViewModel.Helpers
{
    public class LocalizedStrings
    {
        public string this[string key]
        {
            get
            {
                return ResourceLoader.GetForViewIndependentUse().GetString(key);
            }
        }
    }
}
