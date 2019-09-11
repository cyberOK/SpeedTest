using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace SpeedTestIPerf.ViewModel.Helpers
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
