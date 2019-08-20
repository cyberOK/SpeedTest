using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpeedTest.Model
{
    public class SpeedTest
    {
        private bool _testRunTime, _downloadTestRunTime = true, _uploadTestRunTime = true;
        int lowerBoundD = 50, upperBoundD = 60, lowerBoundu = 30, upperBoundu = 35;
        int dowloadDataCounter = 0, uploadDataCounter = 0;

        Random random = new Random();

        public event EventHandler<SpeedDataEventArgs> DownloudDataRecieved;
        public event EventHandler<SpeedDataEventArgs> UploadDataRecieved;

        public SpeedTest()
        {

        }

        public  void StartTest()
        {
            while (_downloadTestRunTime)
            {
                this.GetDowloadData();
            }

            while (_uploadTestRunTime)
            {
                this.GetUploadData();
            }
        }

        private void GetDowloadData()
        {
            SpeedDataEventArgs downloadData = new SpeedDataEventArgs(random.Next(10, 20), this.RandomSpeedD());

            this.OnGetDowloadData(downloadData);

            dowloadDataCounter++;

            if (dowloadDataCounter == 100)
            {
                dowloadDataCounter = 0;
                _downloadTestRunTime = false;
            }
        }

        private void GetUploadData()
        {
            SpeedDataEventArgs uploadData = new SpeedDataEventArgs(ping : random.Next(10, 20), uploadSpeed : this.RandomSpeedU());

            this.OnGetUploadData(uploadData);

            uploadDataCounter++;

            if (uploadDataCounter == 100)
            {
                uploadDataCounter = 0;
                _uploadTestRunTime = false;
            }
        }

        protected virtual void OnGetDowloadData(SpeedDataEventArgs e)
        {
            EventHandler<SpeedDataEventArgs> temp = Volatile.Read(ref DownloudDataRecieved);

            if (temp != null)
            {
                temp(this, e);
            }
        }

        protected virtual void OnGetUploadData(SpeedDataEventArgs e)
        {
            EventHandler<SpeedDataEventArgs> temp = Volatile.Read(ref UploadDataRecieved);

            if (temp != null)
            {
                temp(this, e);
            }
        }

        private int RandomSpeedD()
        {
            var dS = random.Next(lowerBoundD, upperBoundD);

            lowerBoundD += 2;
            upperBoundD += 2;

            return dS;
        }

        private int RandomSpeedU()
        {
            var dS = random.Next(lowerBoundu, upperBoundu);

            lowerBoundu += 1;
            upperBoundu += 1;

            return dS;
        }
    }
}
