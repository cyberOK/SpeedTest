using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTestModel.SpeedTestEventArgs
{
    public class AverageDownloadDataEventArgs : EventArgs
    {
        private double _averageDownloadSpeed;

        public double AverageDownloadSpeed
        {
            get { return this._averageDownloadSpeed; }
            private set { this._averageDownloadSpeed = value; }
        }

        public AverageDownloadDataEventArgs(double averageDownloadSpeed)
        {
            this.AverageDownloadSpeed = averageDownloadSpeed;
        }
    }
}
