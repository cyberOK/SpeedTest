using IPerfLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using SpeedTestModel.SpeedTestEventArgs;
using Windows.Storage;
using System.Globalization;
using Windows.Foundation;

namespace SpeedTestModel
{
    public enum TestMode
    {
        Download,
        Upload
    }

    public class IperfWrapper
    {
        const int timeOut = 0;
        const int numberOfPingTests = 1000;
        const int numberOfValidPingTests = 10;
        const int numberOfDownloadTests = 10, numberOfUploadTests = 10;
        const bool downloadMode = false, uploadMode = true;
        private int latencySummary, latencyCallbackCount, ping;
        private TimeSpan interval;
        private TestMode TestMode;
        private readonly IPerfApp iPerf;
        private double downloadSpeed, uploadSpeed;
        private readonly List<double> downloadSamplesCollection, uploadSamplesCollection;
        private IAsyncAction currentTask;

        public event EventHandler<ErrorsEventArgs> ErrorRecieved;
        public event EventHandler<PingEventArgs> PingDataRecieved;
        public event EventHandler<ConnectingEventArgs> ConnectingDataRecieved;
        public event EventHandler<ConnectedEventArgs> ConnectedDataRecieved;
        public event EventHandler<DownloadSpeedEventArgs> DownloadDataRecieved;
        public event EventHandler<UploadSpeedEventArgs> UploadDataRecieved;
        public event EventHandler<AverageDownloadDataEventArgs> AverageDowloadDataRecieved;
        public event EventHandler<AverageUploadDataEventArgs> AverageUploadDataRecieved;

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

        public IperfWrapper()
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

        public async void StartSpeedTest(string hostName, int port)
        {
            this.TestMode = TestMode.Download;
            this.HostName = hostName;
            this.Port = port;
            this.downloadSamplesCollection.Clear();
            this.uploadSamplesCollection.Clear();

            await this.iPerf.TestNTimesAsync(this.HostName, this.Port, numberOfDownloadTests, interval, downloadMode);
        }

        # region Events Calling Methods

        protected virtual void OnGetErrorWhileTesting(ErrorsEventArgs e)
        {
            this.ErrorRecieved?.Invoke(this, e);
        }

        protected virtual void OnConnectingDataRecieved(ConnectingEventArgs e)
        {
            this.ConnectingDataRecieved?.Invoke(this, e);
        }

        protected virtual void OnConnectedDataRecieved(ConnectedEventArgs e)
        {
            this.ConnectedDataRecieved?.Invoke(this, e);
        }

        protected virtual void OnPingDataRecieved(PingEventArgs e)
        {
            this.PingDataRecieved?.Invoke(this, e);
        }

        protected virtual void OnDownloadDataRecieved(DownloadSpeedEventArgs e)
        {
            this.DownloadDataRecieved?.Invoke(this, e);
        }

        protected virtual void OnUploadDataRecieved(UploadSpeedEventArgs e)
        {
            this.UploadDataRecieved?.Invoke(this, e);
        }

        protected virtual void OnAverageDownloadDataRecieved(AverageDownloadDataEventArgs e)
        {
            this.AverageDowloadDataRecieved?.Invoke(this, e);
        }

        protected virtual void OnAverageUploadDataRecieved(AverageUploadDataEventArgs e)
        {
            this.AverageUploadDataRecieved?.Invoke(this, e);
        }

        #endregion

        #region IPerf Collbacks Methods

        private async void IPerf_ErrorDataUpdated(IPerfApp sender, iPerfErrorReport args)
        {
            switch (this.TestMode)
            {
                case TestMode.Download:

                    ErrorsEventArgs errorsDownload = new ErrorsEventArgs(args.ErrorCode, this.TestMode);

                    this.OnGetErrorWhileTesting(errorsDownload);

                    break;

                case TestMode.Upload:

                    ErrorsEventArgs errorsUpload = new ErrorsEventArgs(args.ErrorCode, this.TestMode);

                    this.OnGetErrorWhileTesting(errorsUpload);

                    await this.iPerf.TestNTimesAsync(this.HostName, this.Port, numberOfUploadTests, interval, uploadMode);

                    break;
            }
        }

        private async void IPerf_ErrorPingTestDataUpdated(IPerfApp sender, iPerfErrorPingTestReport args)
        {
            switch (this.TestMode)
            {
                case TestMode.Download:

                    this.latencyCallbackCount = 0; //  Initialize variables for new PingTest
                    this.latencySummary = 0;

                    await this.PerformOperation(async () =>
                    {
                        await this.currentTask;
                    });

                    break;

                case TestMode.Upload:

                    break;
            }
        }

        private async void IPerf_ConnectingDataUpdated(IPerfApp sender, iPerfConnectingReport args)
        {
            switch (this.TestMode)
            {
                case TestMode.Download:

                    ConnectingEventArgs connectingDataSample = new ConnectingEventArgs(args.ConnectingHostname, args.ConnectingPort, this.TestMode);

                    this.OnConnectingDataRecieved(connectingDataSample);

                    this.latencyCallbackCount = 0; //  Initialize variables for new PingTest
                    this.latencySummary = 0;

                    await this.PerformOperation(async () =>
                    {
                        this.currentTask = this.iPerf.PingTestAsync(this.HostName, timeOut, numberOfPingTests);
                        await this.currentTask;
                    });

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

                    ConnectedEventArgs connectedDataSample = new ConnectedEventArgs(args.RemoteIP, args.RemotePort, this.TestMode);

                    this.OnConnectedDataRecieved(connectedDataSample);

                    //this.latencyCallbackCount = 0; //  Initialize variables for new PingTest
                    //this.latencySummary = 0;

                    //await this.PerformOperation(async () =>
                    //{
                    //    this.currentTask = this.iPerf.PingTestAsync(this.HostName, timeOut, numberOfPingTests);
                    //    await this.currentTask;
                    //});

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
                this.currentTask.Cancel();

                int currentPing = this.latencySummary / numberOfValidPingTests;

                this.Ping = currentPing;

                PingEventArgs pingSample = new PingEventArgs(currentPing);

                this.OnPingDataRecieved(pingSample);
            }
        }

        private async void IPerf_SpeedDataUpdated(IPerfApp sender, iPerfSpeedReport args)
        {
            switch (this.TestMode)
            {
                case TestMode.Download:

                    if (args.ReportSender.Length == 0) // Check ending of Dowload Test
                    {
                        // Parse receiving data

                        string[] downloadValue = args.BitrateBuf.Split(' '); ;
                        string downloadSpeed = downloadValue[0];

                        if (double.TryParse(downloadSpeed, NumberStyles.Any, CultureInfo.InvariantCulture, out double downloadSpeedParse))
                        {
                            if (downloadSpeedParse != 0)
                            {
                                this.downloadSamplesCollection.Add(downloadSpeedParse);

                                DownloadSpeedEventArgs downloadDataSample = new DownloadSpeedEventArgs(downloadSpeedParse);

                                this.OnDownloadDataRecieved(downloadDataSample);
                            }
                        }
                    }

                    else if (args.ReportSender == "receiver") // If Download Test ended starting Upload Test
                    {
                        AverageDownloadDataEventArgs averageDownloadDataEventArgs = new AverageDownloadDataEventArgs(this.GetAverageSpeedValue(this.downloadSamplesCollection));

                        this.OnAverageDownloadDataRecieved(averageDownloadDataEventArgs);

                        this.TestMode = TestMode.Upload;

                        await this.iPerf.TestNTimesAsync(this.HostName, this.Port, numberOfUploadTests, interval, uploadMode); // Call Upload Test
                    }

                    break;

                case TestMode.Upload:

                    if (args.ReportSender.Length == 0) // Check ending of Upload Test
                    {
                        // Parse receiving data

                        string[] uploadValue = args.BitrateBuf.Split(' '); ;
                        string uploadSpeed = uploadValue[0];

                        if (double.TryParse(uploadSpeed, NumberStyles.Any, CultureInfo.InvariantCulture, out double uploadSpeedParse))
                        {
                            if (uploadSpeedParse != 0)
                            {
                                this.uploadSamplesCollection.Add(uploadSpeedParse);

                                UploadSpeedEventArgs uploadSpeedSample = new UploadSpeedEventArgs(uploadSpeedParse, false);

                                this.OnUploadDataRecieved(uploadSpeedSample);
                            }
                        }
                    }

                    else // if (args.ReportSender == "receiver") Upload Test ended
                    {
                        UploadSpeedEventArgs testEndedSignal = new UploadSpeedEventArgs(0.0, true);
                        AverageUploadDataEventArgs averageUploadDataEventArgs = new AverageUploadDataEventArgs(this.GetAverageSpeedValue(this.uploadSamplesCollection));

                        this.OnAverageUploadDataRecieved(averageUploadDataEventArgs);
                        this.OnUploadDataRecieved(testEndedSignal);                   // Signal that test ended

                        this.TestMode = TestMode.Download;
                    }

                    break;
            }
        }

        #endregion

        #region Helpful methods

        private double GetAverageSpeedValue(IList<double> samplesCollection)
        {
            double accumulator = 0;

            foreach (double sample in samplesCollection)
            {
                accumulator += sample;
            }

            return accumulator / samplesCollection.Count();
        }
        private async Task PerformOperation(Func<Task> asyncOperation)
        {
            try
            {
                await asyncOperation();
            }
            catch (OperationCanceledException)
            {
            }
        }

        #endregion
    }
}
