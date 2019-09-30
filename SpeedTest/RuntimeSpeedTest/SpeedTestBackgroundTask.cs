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
using Microsoft.Toolkit.Uwp.Notifications;
using Windows.UI.Notifications;
using SpeedTestModel.Iperf;

namespace RuntimeSpeedTest
{
    public sealed class SpeedTestBackgroundTask : IBackgroundTask
    {
        private BackgroundTaskDeferral deferral;
        private string space = " ";

        public string CurrentHostName
        {
            get
            {
                return (string)ApplicationData.Current.LocalSettings.Values["HostName"];
            }
        }
        public int CurrentHostPort
        {
            get
            {
                return (int)ApplicationData.Current.LocalSettings.Values["HostPort"];
            }
        }
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
        public bool IsBackgroundTestEnable
        {
            get
            {
                return (bool)ApplicationData.Current.LocalSettings.Values["IsBackgroundTestEnable"];
            }
            private set
            {
                ApplicationData.Current.LocalSettings.Values["IsBackgroundTestEnable"] = value;
            }
        }
        public int Ping
        {
            get
            {
                return (int)ApplicationData.Current.LocalSettings.Values["Ping"];
            }
        }
        public double DownloadSpeed
        {
            get
            {
                double truncateNumber = (double)ApplicationData.Current.LocalSettings.Values["DownloadSpeed"];
                return Math.Truncate(truncateNumber * 100) / 100;
            }
        }
        public double UploadSpeed
        {
            get
            {
                double truncateNumber = (double)ApplicationData.Current.LocalSettings.Values["UploadSpeed"];
                return Math.Truncate(truncateNumber * 100) / 100;                
            }
        }

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            BackgroundWorkCostValue cost = BackgroundWorkCost.CurrentBackgroundWorkCost;

            if (cost != BackgroundWorkCostValue.High)
            {
                this.deferral = taskInstance.GetDeferral();

                if (this.IsBackgroundTestEnable)
                {
                    await this.StartBackgroundSpeedTest();

                    this.LoopingUntilTestEndedOrErrorOccur();

                    this.CreateNotificationWhenTestEnded();

                    this.InitializeNextTest();
                }

                deferral.Complete(); // Ending background test
            }

            else
            {
                return;
            }
        }

        private async Task StartBackgroundSpeedTest()
        {
            IperfWrapper backgroundSpeedTest = new IperfWrapper();

            await backgroundSpeedTest.StartSpeedTest(this.CurrentHostName, this.CurrentHostPort);
        }

        private void LoopingUntilTestEndedOrErrorOccur()
        {
            while (!(this.IsErrorOccur == true) && !(this.IsBackgroundSpeedTestEnded == true))
            {
                // Looping until Test Ended or Occur Error
            }

            return;
        }

        private void CreateNotificationWhenTestEnded()
        {
            if (!this.IsErrorOccur)
            {
                this.CreateNotification(this.Ping, this.DownloadSpeed, this.UploadSpeed);
            }
            return;
        }

        private void CreateNotification(int ping, double downloadSpeed, double uploadSpeed)
        {        
            //CreateSpeedTestToast
            var toastNotification = this.CreateToastNotification(ping, downloadSpeed, uploadSpeed);

            // Notify with toast
            ToastNotificationManager.CreateToastNotifier().Show(toastNotification);
        }

        private void InitializeNextTest()
        {
            this.IsBackgroundSpeedTestEnded = false;
            this.IsErrorOccur = false;

            return;
        }

        private ToastNotification CreateToastNotification(int ping, double downloadSpeed, double uploadSpeed)
        {
            ToastVisual toastVisual = this.CreateToastVisual(ping, downloadSpeed, uploadSpeed);

            ToastContent toastContent = this.CreateToastContent(toastVisual);

            ToastNotification toast = new ToastNotification(toastContent.GetXml());

            return toast;
        }

        private ToastContent CreateToastContent(ToastVisual toastVisual)
        {
            ToastContent toastContent = new ToastContent()
            {
                Visual = toastVisual
            };

            return toastContent;
        }

        private ToastVisual CreateToastVisual(int ping, double downloadSpeed, double uploadSpeed)
        {
            ToastVisual visual = new ToastVisual()
            {
                BindingGeneric = new ToastBindingGeneric()
                {
                    Children =
                    {
                        new AdaptiveText()
                        {
                            Text = ApplicationData.Current.LocalSettings.Values["ToastPingText"] + this.space + ping.ToString() + this.space + ApplicationData.Current.LocalSettings.Values["MeasurePingValue"],
                            HintMaxLines = 1
                        },

                        new AdaptiveText()
                        {
                            Text = ApplicationData.Current.LocalSettings.Values["ToastDownloadText"] + this.space + downloadSpeed.ToString() + this.space + ApplicationData.Current.LocalSettings.Values["MeasureSpeedValue"],
                            HintMaxLines = 1
                        },

                        new AdaptiveText()
                        {
                            Text = ApplicationData.Current.LocalSettings.Values["ToastUploadText"] + this.space + uploadSpeed.ToString() + this.space + ApplicationData.Current.LocalSettings.Values["MeasureSpeedValue"],
                            HintMaxLines = 1
                        }
                    }
                }
            };

            return visual;
        }
    }
}
