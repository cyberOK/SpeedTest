using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTestModel.HistoryProvider
{
    public class SpeedData
    {
        public int Id { get; set; }

        public string Server { get; set; }

        public DateTime Date { get; set; }

        public string Ping { get; set; }

        public double DownloadSpeed { get; set; }

        public double UploadSpeed { get; set; }
    }
}
