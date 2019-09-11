using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTest.Model
{
    public class SpeedData
    {
        public int Id { get; set; }

        public string Server { get; set; }

        public DateTime Date { get; set; }

        public int Ping { get; set; }

        public int DownloadSpeed { get; set; }

        public int UploadSpeed { get; set; }
    }
}
