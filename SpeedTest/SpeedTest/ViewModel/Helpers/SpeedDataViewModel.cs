using SpeedTestIPerf.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTestIPerf.ViewModel.Helpers
{
    public class SpeedDataViewModel : ObservableObject
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

        public string Ping { get; set; }

        public double DownloadSpeed { get; set; }

        public double UploadSpeed { get; set; }
    }
}
