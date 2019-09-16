using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTest.Model.SpeedTestEventArgs
{
    public class ErrorsEventArgs : EventArgs
    {
        private int _error;
        private TestMode _testMode;

        public int Error
        {
            get { return this._error; }
            private set { this._error = value; }
        }

        public TestMode TestMode
        {
            get { return this._testMode; }
            private set { this._testMode = value; }
        }

        public ErrorsEventArgs(int error, TestMode testMode)
        {
            this.Error = error;
            this.TestMode = testMode;
        }
    }
}
