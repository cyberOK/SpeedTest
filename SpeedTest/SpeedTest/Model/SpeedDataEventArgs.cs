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
        private string _server;
        private DateTime _date;
        private bool _isTestEnd;

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

        public string Server
        {
            get { return this._server; }
            private set { this._server = value; }
        }

        public DateTime Date
        {
            get { return this._date; }
            private set { this._date = value; }
        }

        public bool IsTestEnd
        {
            get { return this._isTestEnd; }
            set { this._isTestEnd = value; }
        }

        public SpeedDataEventArgs(double ping, double downloudSpeed = 0, double uploadSpeed = 0, bool isTestEnd = false, string server = "TestServer")
        {
            this.Ping = ping;
            this.IsTestEnd = IsTestEnd;
            this.DownloudSpeed = downloudSpeed;
            this.UploadSpeed = uploadSpeed;
            this.Server = server;
            this.Date = DateTime.Now;
        }
    }
}
