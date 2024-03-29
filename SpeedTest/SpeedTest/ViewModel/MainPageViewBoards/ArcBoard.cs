﻿using SpeedTestUWP.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTestUWP.ViewModel.ViewBoards
{
    public class ArcBoard : ObservableObject
    {
        private bool _isStartArcVisible = true;
        private bool _isStartButtonPressed;
        private bool _isTryConnect;
        private bool _isSpeedMeterBackgroundVisible;
        private bool _isDownloadSpeedDataRecieved;
        private bool _isUploadSpeedDataRecieved;
        private bool _isSpeedDataNumbersReceiving;
        private string _speedDataNumbers;
        private double _downloadSpeedArcValue;
        private double _uploadSpeedArcValu;

        
        public bool IsStartArcVisible
        {
            get { return _isStartArcVisible; }
            set { Set(ref _isStartArcVisible, value); }
        }

        public bool IsStartButtonPressed
        {
            get { return _isStartButtonPressed; }
            set { Set(ref _isStartButtonPressed, value); }
        }

        public bool IsSpeedMeterBackgroundVisible
        {
            get { return _isSpeedMeterBackgroundVisible; }
            set { Set(ref _isSpeedMeterBackgroundVisible, value); }
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

        public string SpeedDataNumbers
        {
            get { return _speedDataNumbers; }
            set { Set(ref _speedDataNumbers, value); }
        }

        public double DownloadSpeedArcValue
        {
            get { return _downloadSpeedArcValue; }
            set { Set(ref _downloadSpeedArcValue, value); }
        }

        public double UploadSpeedArcValue
        {
            get { return _uploadSpeedArcValu; }
            set { Set(ref _uploadSpeedArcValu, value); }
        }
        
    }
}
