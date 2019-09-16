using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTestModel.SpeedTestEventArgs
{
    public class DownloadSpeedEventArgs : EventArgs
    {
        private double _downloadSpeed;

        public double DownloadSpeed
        {
            get { return this._downloadSpeed; }
            private set { this._downloadSpeed = value; }
        }

        public DownloadSpeedEventArgs(double downloadSpeed)
        {
            this.DownloadSpeed = downloadSpeed;
        }
    }
}
