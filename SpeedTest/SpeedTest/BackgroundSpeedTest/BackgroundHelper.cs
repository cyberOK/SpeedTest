using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using SpeedTestModel;
using Windows.Storage;
using SpeedTestUWP.ViewModel.ViewBoards;

namespace SpeedTestUWP.BackgroundSpeedTest
{
    public class BackgroundHelper
    {
        private string taskName = "BackgroundSpeedTest";
        private string taskEntryPoint = "RuntimeSpeedTest.SpeedTestBackgroundTask";
        private ApplicationTrigger applicationTrigger;

        public BackgroundHelper()
        {
            this.applicationTrigger = new ApplicationTrigger();
        }

        public async void StartBackgroundSpeedTest()
        {
            ApplicationData.Current.LocalSettings.Values["IsBackgroundTestEnable"] = true;

            await this.applicationTrigger.RequestAsync();
        }

        public async void StopBackgroundSpeedTest()
        {
            ApplicationData.Current.LocalSettings.Values["IsBackgroundTestEnable"] = false;

            await this.applicationTrigger.RequestAsync();
        }

        public void Register()
        {
            // if the task is already registered, there is no need to register it again

            if ( !(this.IsTaskRegistered(this.taskName)))
            {
                var taskBuilder = new BackgroundTaskBuilder();
                taskBuilder.Name = this.taskName;
                taskBuilder.TaskEntryPoint = this.taskEntryPoint;
                taskBuilder.SetTrigger(this.applicationTrigger);
                taskBuilder.AddCondition(new SystemCondition(SystemConditionType.InternetAvailable));
                taskBuilder.Register();

                ApplicationData.Current.LocalSettings.Values["IsBackgroundTestEnable"] = true;
            }            
        }

        private bool IsTaskRegistered(string taskName) =>
            BackgroundTaskRegistration.AllTasks.Any(x => x.Value.Name.Equals(taskName));

        //private void UnregisterTask(string taskName, bool cancelTask) =>
        //    BackgroundTaskRegistration.AllTasks.First(x => x.Value.Name.Equals(taskName)).Value?.Unregister(cancelTask);

        //private BackgroundTaskRegistration GetTaskByName(string taskName) =>
        //    BackgroundTaskRegistration.AllTasks.FirstOrDefault(x => x.Value.Name.Equals(taskName)).Value as BackgroundTaskRegistration;
    }
}
