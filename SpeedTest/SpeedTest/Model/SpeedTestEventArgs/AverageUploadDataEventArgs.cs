using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTest.Model.SpeedTestEventArgs
{
    public class AverageUploadDataEventArgs : EventArgs
    {
        private double _averageUploadSpeed;

        public double AverageUploadSpeed
        {
            get { return this._averageUploadSpeed; }
            private set { this._averageUploadSpeed = value; }
        }

        public AverageUploadDataEventArgs(double averageUploadSpeed)
        {
            this.AverageUploadSpeed = averageUploadSpeed;
        }
    }
}
