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

namespace RuntimeSpeedTest
{
    public sealed class SpeedTestBackgroundTask : IBackgroundTask
    {
        private BackgroundTaskDeferral deferral;

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
            BackgroundSpeedTest backgroundSpeedTest = new BackgroundSpeedTest();
            string currentHostName = (string)ApplicationData.Current.LocalSettings.Values["HostName"];
            int currentHostPort = (int)ApplicationData.Current.LocalSettings.Values["HostPort"];

            await backgroundSpeedTest.StartSpeedTest(currentHostName, currentHostPort);           
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
                int ping = (int)ApplicationData.Current.LocalSettings.Values["Ping"];
                double downloadSpeed = (double)ApplicationData.Current.LocalSettings.Values["DownloadSpeed"];
                double uploadSpeed = (double)ApplicationData.Current.LocalSettings.Values["UploadSpeed"];

                this.CreateNotification(ping, downloadSpeed, uploadSpeed);
            }

            return;
        }

        private void CreateNotification(int ping, double downloadSpeed, double uploadSpeed)
        {        
            // Truncate Speed Numbers
            double downloadSpeedTruncate = this.TruncateSpeeedNumber(downloadSpeed);
            double uploadSpeedTruncate = this.TruncateSpeeedNumber(uploadSpeed);

            //CreateSpeedTestToast
            var toastNotification = this.CreateToastNotification(ping, downloadSpeedTruncate, uploadSpeedTruncate);

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
                            Text = ping.ToString(),
                            HintMaxLines = 1
                        },

                        new AdaptiveText()
                        {
                            Text = downloadSpeed.ToString(),
                            HintMaxLines = 1
                        },

                        new AdaptiveText()
                        {
                            Text = uploadSpeed.ToString(),
                            HintMaxLines = 1
                        }
                    }
                }
            };

            return visual;
        }

        private double TruncateSpeeedNumber(double truncateNumber)
        {
            return Math.Truncate(truncateNumber * 100) / 100;
        }
    }
}
