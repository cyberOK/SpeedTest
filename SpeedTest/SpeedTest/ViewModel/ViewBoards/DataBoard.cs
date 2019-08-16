using SpeedTest.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTest.ViewModel.ViewBoards
{
    public class DataBoard : ObservableObject
    {
        private bool _isPingFieldsGridVisible = true;
        private bool _isDownloadSpeedFieldsGridVisible = true;
        private bool _isUploadSpeedFieldsGridVisible = true;
        private string _pingDataPing = "100";
        private string _downloadSpeedData = "200";
        private string _uploadData = "300";

        public bool IsPingFieldsGridVisible
        {
            get { return _isPingFieldsGridVisible; }
            set { Set(ref _isPingFieldsGridVisible, value); }
        }

        public bool IsDownloadSpeedFieldsGridVisible
        {
            get { return _isDownloadSpeedFieldsGridVisible; }
            set { Set(ref _isDownloadSpeedFieldsGridVisible, value); }
        }

        public bool IsUploadSpeedFieldsGridVisible
        {
            get { return _isUploadSpeedFieldsGridVisible; }
            set { Set(ref _isUploadSpeedFieldsGridVisible, value); }
        }

        public string PingData
        {
            get { return _pingDataPing; }
            set { Set(ref _pingDataPing, value); }
        }

        public string DownloadSpeedData
        {
            get { return _downloadSpeedData; }
            set { Set(ref _downloadSpeedData, value); }
        }

        public string UploadSpeedData
        {
            get { return _uploadData; }
            set { Set(ref _uploadData, value); }
        }
    }
}
