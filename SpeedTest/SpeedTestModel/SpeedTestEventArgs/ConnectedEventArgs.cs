using SpeedTestModel.Iperf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTestModel.SpeedTestEventArgs
{
    public class ConnectedEventArgs : EventArgs
    {
        private string hostIp;
        private int hostPort;
        private TestMode testMode;

        public string HostIp
        {
            get { return this.hostIp; }
            private set { this.hostIp = value; }
        }

        public int HostPort
        {
            get { return this.hostPort; }
            private set { this.hostPort = value; }
        }

        public TestMode TestMode
        {
            get { return this.testMode; }
            private set { this.testMode = value; }
        }

        public ConnectedEventArgs(string hostIp, int hostPort, TestMode testMode)
        {
            this.HostIp = hostIp;
            this.HostPort = hostPort;
            this.TestMode = testMode;
        }
    }
}
