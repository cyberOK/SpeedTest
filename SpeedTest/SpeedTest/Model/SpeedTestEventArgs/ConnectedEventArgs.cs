using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTest.Model.SpeedTestEventArgs
{
    public class ConnectedEventArgs : EventArgs
    {
        private string _hostIp;
        private int _hostPort;
        private TestMode _testMode;

        public string HostIp
        {
            get { return this._hostIp; }
            private set { this._hostIp = value; }
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

        public ConnectedEventArgs(string hostIp, int hostPort, TestMode testMode)
        {
            this.HostIp = hostIp;
            this.HostPort = hostPort;
            this.TestMode = testMode;
        }
    }
}
