using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Storage;

namespace RuntimeSpeedTest
{
    public sealed class SpeedTestBackgroundTask : IBackgroundTask
    {
        volatile bool _cancelRequested = false;
        public void Run(IBackgroundTaskInstance taskInstance)
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

            //await DoWork(taskInstance);

            _deferral.Complete();
        }

        private async Task DoWork(IBackgroundTaskInstance taskInstance)
        {
            bool canGetSpeedTestInstance = ApplicationData.Current.LocalSettings.Values.ContainsKey("SpeedTest");

            if (canGetSpeedTestInstance)
            {
                var speedTest = ApplicationData.Current.LocalSettings.Values["SpeedTest"];
            }
            await Task.Delay(1500);
            if (_cancelRequested)
            {
                //break;
            }

            
        }
    }
}
