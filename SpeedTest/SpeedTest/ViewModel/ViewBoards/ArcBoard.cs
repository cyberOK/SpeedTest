using SpeedTest.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTest.ViewModel.ViewBoards
{
    public class ArcBoard : ObservableObject
    {
        private bool _isStartButtonPressed;
        private bool _isTryConnect;
        private bool _isDownloadSpeedDataRecieved;
        private bool _isUploadSpeedDataRecieved;
        private bool _isSpeedDataNumbersReceiving;
        private double _speedDataNumbers;

        public bool IsStartButtonPressed
        {
            get { return _isStartButtonPressed; }
            set { Set(ref _isStartButtonPressed, value); }
        }

        public bool IsTryConnect
        {
            get { return _isTryConnect; }
            set { Set(ref _isTryConnect, value); }
        }

        public bool IsDownloadSpeedDataRecieved
        {
            get { return _isDownloadSpeedDataRecieved; }
            set { Set(ref _isDownloadSpeedDataRecieved, value); }
        }

        public bool IsUploadSpeedDataRecieved
        {
            get { return _isUploadSpeedDataRecieved; }
            set { Set(ref _isUploadSpeedDataRecieved, value); }
        }

        public bool IsSpeedDataNumbersReceiving
        {
            get { return _isSpeedDataNumbersReceiving; }
            set { Set(ref _isSpeedDataNumbersReceiving, value); }
        }

        public double SpeedDataNumbers
        {
            get { return _speedDataNumbers; }
            set { Set(ref _speedDataNumbers, value); }
        }
    }
}
