using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTest.Model
{
    public class SpeedDataEventArgs : EventArgs
    {
        private double _ping;
        private double _downloudSpeed;
        private double _uploadSpeed;

        public double Ping
        {
            get { return this._ping; }
            private set { this._ping = value; }
        }

        public double DownloudSpeed
        {
            get { return this._downloudSpeed; }
            private set { this._downloudSpeed = value; }
        }

        public double UploadSpeed
        {
            get { return this._uploadSpeed; }
            private set { this._uploadSpeed = value; }
        }

        public SpeedDataEventArgs(double ping, double downloudSpeed = 0, double uploadSpeed = 0)
        {
            this.Ping = ping;
            this.DownloudSpeed = downloudSpeed;
            this.UploadSpeed = uploadSpeed;
        }

    }
}
