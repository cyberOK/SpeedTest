using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTest.ViewModel.HelpfullCollections
{
    public class SpeedTestDataSample
    {
        public int Ping { get; set; }
        public int DownloadSpeed { get; set; }
        public int UploadSpeed { get; set; }
        public string Server { get; set; }
        public DateTime Date { get; set; }
    }
}
