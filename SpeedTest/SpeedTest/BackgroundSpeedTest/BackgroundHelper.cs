using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using SpeedTestModel;
using Windows.Storage;
using SpeedTest.Toasts;
using SpeedTestUWP.ViewModel.ViewBoards;

namespace SpeedTestUWP.BackgroundSpeedTest
{
    public class BackgroundHelper
    {
        private string _taskName = "BackgroundSpeedTest";
        private SpeedTestToast toast;

        public BackgroundHelper()
        {
            this.toast = new SpeedTestToast();
        }

        public async void StartBackgroundSpeedTest()
         {
            ApplicationTrigger applicationTrigger = new ApplicationTrigger();

            var taskList = BackgroundTaskRegistration.AllTasks.Values;
            var task = taskList.FirstOrDefault(t => t.Name == _taskName);

            if (task == null)
            {
                var taskBuilder = new BackgroundTaskBuilder();

                taskBuilder.Name = _taskName;
                taskBuilder.TaskEntryPoint = typeof(RuntimeSpeedTest.SpeedTestBackgroundTask).FullName;
                //SystemTrigger trigger = new SystemTrigger(SystemTriggerType.NetworkStateChange, false);
                taskBuilder.SetTrigger(applicationTrigger);
                task = taskBuilder.Register();
                task.Completed += Task_Completed;                

                await applicationTrigger.RequestAsync();
            }
            else
            {
                await applicationTrigger.RequestAsync();
            }
        }

        public void StopBackgroundSpeedTest()
        {
            var taskList = BackgroundTaskRegistration.AllTasks.Values;
            var task = taskList.FirstOrDefault(t => t.Name == _taskName);

            if (task != null)
            {
                task.Unregister(true);
            }
        }

        private void Task_Completed(BackgroundTaskRegistration sender, BackgroundTaskCompletedEventArgs args)
        {
            ApplicationData.Current.LocalSettings.Values["IsTestEnded"] = false;

            int ping = (int)ApplicationData.Current.LocalSettings.Values["Ping"];
            double downloadSpeed = (double)ApplicationData.Current.LocalSettings.Values["DownloadSpeed"];
            double uploadSpeed = (double)ApplicationData.Current.LocalSettings.Values["UploadSpeed"];

            this.toast.CreateSpeedTestToast(ping, downloadSpeed, uploadSpeed);
        }
    }
}
