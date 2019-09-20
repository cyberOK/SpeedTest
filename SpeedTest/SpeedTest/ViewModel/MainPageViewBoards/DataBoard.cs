using SpeedTestUWP.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTestUWP.ViewModel.ViewBoards
{
    public class DataBoard : ObservableObject
    {
        private bool isPingFieldsGridVisible;
        private bool isDownloadSpeedFieldsGridVisible;
        private bool isUploadSpeedFieldsGridVisible;
        private string pingDataPing = "000";
        private string downloadSpeedData = "000";
        private string uploadData = "000";

        public bool IsPingFieldsGridVisible
        {
            get { return isPingFieldsGridVisible; }
            set { Set(ref isPingFieldsGridVisible, value); }
        }

        public bool IsDownloadSpeedFieldsGridVisible
        {
            get { return isDownloadSpeedFieldsGridVisible; }
            set { Set(ref isDownloadSpeedFieldsGridVisible, value); }
        }

        public bool IsUploadSpeedFieldsGridVisible
        {
            get { return isUploadSpeedFieldsGridVisible; }
            set { Set(ref isUploadSpeedFieldsGridVisible, value); }
        }

        public string PingData
        {
            get { return pingDataPing; }
            set { Set(ref pingDataPing, value); }
        }

        public string DownloadSpeedData
        {
            get { return downloadSpeedData; }
            set { Set(ref downloadSpeedData, value); }
        }

        public string UploadSpeedData
        {
            get { return uploadData; }
            set { Set(ref uploadData, value); }
        }
    }
}
