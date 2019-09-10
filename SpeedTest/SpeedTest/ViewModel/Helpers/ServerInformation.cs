using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTestIPerf.ViewModel.Helpers
{
    public class ServerInformation
    {
        public bool IsCurrent { get; set; }
        public string IPerf3Server { get; set; }
        public string Location { get; set; }
        public string ProviderName { get; set; }
        public string Port { get; set; }
    }
}
