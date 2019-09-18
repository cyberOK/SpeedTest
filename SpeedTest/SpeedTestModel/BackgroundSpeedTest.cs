using IPerfLibrary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI.Core;

namespace SpeedTestModel
{
    public class BackgroundSpeedTest
    {
        const int timeOut = 0;
        const int numberOfPingTests = 50;
        const int numberOfValidPingTests = 10;
        const int numberOfDownloadTests = 10, numberOfUploadTests = 10;
        const bool downloadMode = false, uploadMode = true;
        private double downloadSum = 0, uploadSum = 0;
        private int latencySummary, latencyCallbackCount;
        private TimeSpan interval;
        private TestMode TestMode;
        private readonly IPerfApp iPerf;
        private int ping;
        private double downloadSpeed;
        private double uploadSpeed;
        private List<double> downloadSamplesCollection;
        private List<double> uploadSamplesCollection;

        public string HostName { get; private set; }
        public int Port { get; private set; }
        public int Ping
        {
            get
            {
                return ping;
            }
            private set
            {
                ApplicationData.Current.LocalSettings.Values["Ping"] = value;
                ping = value;
            }
        }
        public double DownloadSpeed
        {
            get
            {
                return downloadSpeed;
            }
            private set
            {
                ApplicationData.Current.LocalSettings.Values["DownloadSpeed"] = value;
                downloadSpeed = value;
            }
        }
        public double UploadSpeed
        {
            get
            {
                return uploadSpeed;
            }
            private set
            {
                ApplicationData.Current.LocalSettings.Values["UploadSpeed"] = value;
                uploadSpeed = value;
            }
        }

        public BackgroundSpeedTest()
        {
            this.interval = TimeSpan.FromMilliseconds(250);
            this.iPerf = new IPerfApp();
            this.downloadSamplesCollection = new List<double>();
            this.uploadSamplesCollection = new List<double>();

            this.iPerf.ErrorDataUpdated += IPerf_ErrorDataUpdated;
            this.iPerf.ConnectingDataUpdated += IPerf_ConnectingDataUpdated;
            this.iPerf.ConnectedDataUpdated += IPerf_ConnectedDataUpdated;
            this.iPerf.SpeedDataUpdated += IPerf_SpeedDataUpdated;
            this.iPerf.ErrorPingTestDataUpdated += IPerf_ErrorPingTestDataUpdated;
            this.iPerf.PingTestDataUpdated += IPerf_PingTestDataUpdated;
        }

        public async Task StartSpeedTest(string hostName, int port)
        {
            this.TestMode = TestMode.Download;
            this.HostName = hostName;
            this.Port = port;
            this.downloadSamplesCollection.Clear();
            this.uploadSamplesCollection.Clear();

            await this.iPerf.TestNTimesAsync(this.HostName, this.Port, numberOfDownloadTests, interval, downloadMode);
        }

        #region IPerf Collbacks Methods

        private async void IPerf_ErrorDataUpdated(IPerfApp sender, iPerfErrorReport args)
        {
            switch (this.TestMode)
            {
                case TestMode.Download:

                    ApplicationData.Current.LocalSettings.Values["IsTestEnded"] = true;

                    break;

                case TestMode.Upload:

                    await this.iPerf.TestNTimesAsync(this.HostName, this.Port, numberOfUploadTests, interval, uploadMode);

                    break;
            }
        }

        private async void IPerf_ErrorPingTestDataUpdated(IPerfApp sender, iPerfErrorPingTestReport args)
        {
            switch (this.TestMode)
            {
                case TestMode.Download:

                    await this.iPerf.PingTestAsync(this.HostName, timeOut, numberOfPingTests);

                    break;

                case TestMode.Upload:

                    break;
            }
        }

        private void IPerf_ConnectingDataUpdated(IPerfApp sender, iPerfConnectingReport args)
        {
            switch (this.TestMode)
            {
                case TestMode.Download:

                    break;

                case TestMode.Upload:

                    break;
            }
        }

        private async void IPerf_ConnectedDataUpdated(IPerfApp sender, iPerfConnectedReport args)
        {
            switch (this.TestMode)
            {
                case TestMode.Download:

                    this.latencyCallbackCount = 0;
                    this.latencySummary = 0;

                    await this.iPerf.PingTestAsync(this.HostName, timeOut, numberOfPingTests);

                    break;

                case TestMode.Upload:

                    break;
            }
        }

        private void IPerf_PingTestDataUpdated(IPerfApp sender, iPerfPingTestReport args)
        {
            var ping = new TimeSpan(args.Latency).Milliseconds;

            if (ping != 0)
            {
                this.latencySummary += ping;
                this.latencyCallbackCount++;
            }

            if (this.latencyCallbackCount == numberOfValidPingTests)
            {
                int currentPing = this.latencySummary / numberOfValidPingTests;

                this.Ping = currentPing;
            }
        }

        private async void IPerf_SpeedDataUpdated(IPerfApp sender, iPerfSpeedReport args)
        {
            switch (this.TestMode)
            {
                case TestMode.Download:

                    if (args.ReportSender.Length == 0) // Check ending of Test
                    {
                        string[] downloadValue = args.BitrateBuf.Split(' '); ;
                        string downloadSpeed = downloadValue[0];

                        if (double.TryParse(downloadSpeed, NumberStyles.Any, CultureInfo.InvariantCulture, out double downloadSpeedParse))
                        {
                            if (downloadSpeedParse != 0)
                            {
                                this.downloadSamplesCollection.Add(downloadSpeedParse);
                            }
                        }

                    }

                    else if (args.ReportSender == "receiver")// If Download Test ended starting Upload Test
                    {
                        this.TestMode = TestMode.Upload;
                        this.downloadSum = 0;

                        foreach (double downloadSample in downloadSamplesCollection)
                        {
                            downloadSum += downloadSample;
                        }

                        this.DownloadSpeed = downloadSum / downloadSamplesCollection.Count(); 

                        await this.iPerf.TestNTimesAsync(this.HostName, this.Port, numberOfUploadTests, interval, uploadMode);
                    }

                    break;

                case TestMode.Upload:

                    if (args.ReportSender.Length == 0) // Check ending of Test
                    {
                        string[] uploadValue = args.BitrateBuf.Split(' '); ;
                        string uploadSpeed = uploadValue[0];

                        if (double.TryParse(uploadSpeed, NumberStyles.Any, CultureInfo.InvariantCulture, out double uploadSpeedParse))
                        {
                            if (uploadSpeedParse != 0)
                            {
                                this.uploadSamplesCollection.Add(uploadSpeedParse);
                            }
                        }

                    }

                    else // if (args.ReportSender == "receiver") Upload Test ended reset TestMode
                    {
                        this.TestMode = TestMode.Download;
                        this.uploadSum = 0;
                        this.latencyCallbackCount = 0;

                        foreach (double uploadSample in downloadSamplesCollection)
                        {
                            uploadSum += uploadSample;
                        }

                        this.UploadSpeed = uploadSum / uploadSamplesCollection.Count();

                        ApplicationData.Current.LocalSettings.Values["IsTestEnded"] = true;
                    }

                    break;
            }
        }

        #endregion
    }
}
