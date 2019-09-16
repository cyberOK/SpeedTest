using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTestModel.SpeedTestEventArgs
{
    public class PingEventArgs : EventArgs
    {
        private int _ping;

        public int Ping
        {
            get { return this._ping; }
            private set { this._ping = value; }
        }

        public PingEventArgs(int ping)
        {
            this.Ping = ping;
        }
    }
}
