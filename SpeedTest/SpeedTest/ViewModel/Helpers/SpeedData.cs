using SpeedTestIPerf.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTestIPerf.ViewModel.Helpers
{
    public class SpeedData : ObservableObject
    {
        private bool _isSelected = false;

        public int Id { get; set; }

        public bool IsSelected
        {
            get { return _isSelected; }
            set { Set(ref _isSelected, value); }
        }

        public string Server { get; set; }

        public DateTime Date { get; set; }

        public int Ping { get; set; }

        public int DownloadSpeed { get; set; }

        public int UploadSpeed { get; set; }
    }
}
