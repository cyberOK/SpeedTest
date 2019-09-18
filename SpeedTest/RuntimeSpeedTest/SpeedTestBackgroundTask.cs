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

namespace RuntimeSpeedTest
{
    public sealed class SpeedTestBackgroundTask : IBackgroundTask
    {
        volatile bool _cancelRequested = false;
        BackgroundTaskDeferral _deferral;

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
                this._cancelRequested = true;
            };

            this._deferral = taskInstance.GetDeferral();

            await this.StartBackgroundSpeedTest(taskInstance);

            bool isTestEnded = (bool)ApplicationData.Current.LocalSettings.Values["IsTestEnded"];

            while (isTestEnded)
            {
                _deferral.Complete();
            }
        }

        private async Task StartBackgroundSpeedTest(IBackgroundTaskInstance taskInstance)
        {
            BackgroundSpeedTest backgroundSpeedTest = new BackgroundSpeedTest();
            string currentHostName = (string)ApplicationData.Current.LocalSettings.Values["HostName"];
            int currentHostPort = (int)ApplicationData.Current.LocalSettings.Values["HostPort"];

            await backgroundSpeedTest.StartSpeedTest(currentHostName, currentHostPort);
        }
    }
}
