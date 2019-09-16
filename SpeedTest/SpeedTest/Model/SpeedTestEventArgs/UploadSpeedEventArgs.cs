using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTest.Model.SpeedTestEventArgs
{
    public class UploadSpeedEventArgs : EventArgs
    {
        private double _uploadSpeed;
        private bool _isTestEnd;

        public double UploadSpeed
        {
            get { return this._uploadSpeed; }
            private set { this._uploadSpeed = value; }
        }

        public bool IsTestEnd
        {
            get { return this._isTestEnd; }
            set { this._isTestEnd = value; }
        }

        public UploadSpeedEventArgs(double uploadSpeed, bool isTestEnd)
        {
            this.UploadSpeed = uploadSpeed;
            this.IsTestEnd = isTestEnd;
        }
    }
}
