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

            BackgroundTaskDeferral _deferral = taskInstance.GetDeferral();

            await this.DoWork(taskInstance);
                                    
            _deferral.Complete();
        }

        private async Task DoWork(IBackgroundTaskInstance taskInstance)
        {
            SpeedTest speedTest = new SpeedTest();
            string currentHostName = (string)ApplicationData.Current.LocalSettings.Values["HostName"];
            int currentHostPort = (int)ApplicationData.Current.LocalSettings.Values["HostPort"];

            //await speedTest.StartSpeedTest(currentHostName, currentHostPort);
        }
    }
}
