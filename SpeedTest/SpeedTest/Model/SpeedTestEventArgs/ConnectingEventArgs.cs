using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTest.Model.SpeedTestEventArgs
{
    public class ConnectingEventArgs : EventArgs
    {
        private int _hostPort;
        private string _hostName;
        private TestMode _testMode;

        public string Hostname
        {
            get { return this._hostName; }
            private set { this._hostName = value; }
        }

        public int HostPort
        {
            get { return this._hostPort; }
            private set { this._hostPort = value; }
        }

        public TestMode TestMode
        {
            get { return this._testMode; }
            private set { this._testMode = value; }
        }

        public ConnectingEventArgs(string hostName, int hostPort, TestMode testMode)
        {
            this.Hostname = hostName;
            this.HostPort = hostPort;
            this.TestMode = testMode;
        }
    }
}
