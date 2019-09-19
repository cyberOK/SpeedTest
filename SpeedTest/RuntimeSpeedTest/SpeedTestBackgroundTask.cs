using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Storage;
using SpeedTestModel;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.Data.Xml.Dom;

namespace RuntimeSpeedTest
{
    public sealed class SpeedTestBackgroundTask : IBackgroundTask
    {
        volatile bool cancelRequested = false;
        bool isTestEnded;
        BackgroundTaskDeferral deferral;
        private readonly StringBuilder template = new StringBuilder();
        private readonly XmlDocument xml = new XmlDocument();

        public bool IsBackgroundSpeedTestEnded
        {
            get
            {
                return (bool)ApplicationData.Current.LocalSettings.Values["IsTestEnded"];
            }
            private set
            {
                ApplicationData.Current.LocalSettings.Values["IsTestEnded"] = value;
            }
        }

        public bool IsErrorOccur
        {
            get
            {
                return (bool)ApplicationData.Current.LocalSettings.Values["ErrorOccur"];
            }
            private set
            {
                ApplicationData.Current.LocalSettings.Values["ErrorOccur"] = value;
            }
        }

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var cost = BackgroundWorkCost.CurrentBackgroundWorkCost;
            if (cost == BackgroundWorkCostValue.High)
                return;

            var cancel = new CancellationTokenSource();
            taskInstance.Canceled += (s, e) =>
            {
                cancel.Cancel();
                cancel.Dispose();
                this.cancelRequested = true;
            };

            this.deferral = taskInstance.GetDeferral();

            this.IsBackgroundSpeedTestEnded = false;
            this.IsErrorOccur = false;

            if ((bool)ApplicationData.Current.LocalSettings.Values["IsBackgroundTestEnable"])
            {
                await this.StartBackgroundSpeedTest();

                while (!(this.IsErrorOccur == true) && !(this.IsBackgroundSpeedTestEnded == true))
                {
                    // Looping until Test ended or Occur Error
                }

                if (!this.IsErrorOccur)
                {
                    int ping = (int)ApplicationData.Current.LocalSettings.Values["Ping"];
                    double downloadSpeed = (double)ApplicationData.Current.LocalSettings.Values["DownloadSpeed"];
                    double uploadSpeed = (double)ApplicationData.Current.LocalSettings.Values["UploadSpeed"];

                    this.CreateNotification(ping, downloadSpeed, uploadSpeed);
                }

                ApplicationData.Current.LocalSettings.Values["IsTestEnded"] = false;
            }            

            deferral.Complete();
        }

        private async Task StartBackgroundSpeedTest()
        {
            BackgroundSpeedTest backgroundSpeedTest = new BackgroundSpeedTest();
            string currentHostName = (string)ApplicationData.Current.LocalSettings.Values["HostName"];
            int currentHostPort = (int)ApplicationData.Current.LocalSettings.Values["HostPort"];

            await backgroundSpeedTest.StartSpeedTest(currentHostName, currentHostPort);           
        }

        private void CreateNotification(int ping, double downloadSpeed, double uploadSpeed)
        {        
            // Truncate Speed Numbers
            double downloadSpeedTruncate = this.TruncateSpeeedNumber(downloadSpeed);
            double uploadSpeedTruncate = this.TruncateSpeeedNumber(uploadSpeed);

            //CreateSpeedTestToast
            this.template.Append("<toast><visual version='2'><binding template='ToastText02'>");
            this.template.AppendFormat("<text id='1'>Ping: {0}</text>", ping);
            this.template.AppendFormat("<text id='1'>Download speed: {0:N2}</text>", downloadSpeedTruncate);
            this.template.AppendFormat("<text id='1'>Upload speed: {0:N2}</text>", uploadSpeedTruncate);
            this.template.Append("</binding></visual></toast>");

            this.xml.LoadXml(this.template.ToString());

            // Notify
            Windows.UI.Notifications.ToastNotificationManager.CreateToastNotifier().Show(new Windows.UI.Notifications.ToastNotification(this.xml));
        }


        private void CreateToastXmlDocument(int ping, double downloadSpeed, double uploadSpeed)
        {

        }

        private double TruncateSpeeedNumber(double truncateNumber)
        {
            return Math.Truncate(truncateNumber * 100) / 100;
        }
    }
}
