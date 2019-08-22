using SpeedTest.ViewModel.Helpers;
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
        private bool _downloadTestRunTime, _uploadTestRunTime;
        int lowerBoundD, upperBoundD, lowerBoundu, upperBoundu;
        int dowloadDataCounter = 0, uploadDataCounter = 0;
        Random random;

        public event EventHandler<SpeedDataEventArgs> DownloudDataRecieved;
        public event EventHandler<SpeedDataEventArgs> UploadDataRecieved;

        public SpeedTest()
        {
            this.random = new Random();
        }

        public async void StartTest()
        {
            this._downloadTestRunTime = true;
            this._uploadTestRunTime = true;
            lowerBoundD = 50;
            upperBoundD = 60;
            lowerBoundu = 30;
            upperBoundu = 35;

            await Task.Delay(TimeSpan.FromMilliseconds(1000));

            while (_downloadTestRunTime)
            {
                this.GetDowloadData();
                await Task.Delay(TimeSpan.FromMilliseconds(100));
            }

            while (_uploadTestRunTime)
            {
                this.GetUploadData();
                await Task.Delay(TimeSpan.FromMilliseconds(100));
            }
        }

        private void GetDowloadData()
        {
            SpeedDataEventArgs downloadData = new SpeedDataEventArgs(random.Next(10, 20), downloudSpeed: this.RandomSpeedD());

            dowloadDataCounter++;

            if (dowloadDataCounter == 100)
            {
                dowloadDataCounter = 0;
                _downloadTestRunTime = false;
            }

            this.OnGetDowloadData(downloadData);
        }

        private void GetUploadData()
        {
            SpeedDataEventArgs uploadData = new SpeedDataEventArgs(ping : random.Next(10, 20), uploadSpeed : this.RandomSpeedU());

            uploadDataCounter++;

            if (uploadDataCounter == 100)
            {
                uploadDataCounter = 0;
                _uploadTestRunTime = false;
                uploadData.IsTestEnd = true;
            }

            this.OnGetUploadData(uploadData);
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
            var uS = random.Next(lowerBoundu, upperBoundu);

            lowerBoundu += 1;
            upperBoundu += 1;

            return uS;
        }
    }
}
