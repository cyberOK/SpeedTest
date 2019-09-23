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

        public BackgroundHelper()
        {
            this.applicationTrigger = new ApplicationTrigger();
            this.RegisteringBackgroundSpeedTest();
        }

        public async void StartBackgroundSpeedTest()
        {
            this.IsBackgroundTestEnable = true;

            await this.applicationTrigger.RequestAsync(); // Need delete after testing
        }

        public async void StopBackgroundSpeedTest()
        {
            this.IsBackgroundTestEnable = false;

            await this.applicationTrigger.RequestAsync(); // Need delete after testing
        }

        public void RegisteringBackgroundSpeedTest()
        {
            // if the task is already registered, there is no need to register it again

            if ( !(this.IsTaskRegistered(this.taskName)))
            {
                var taskBuilder = new BackgroundTaskBuilder();
                taskBuilder.Name = this.taskName;
                taskBuilder.TaskEntryPoint = this.taskEntryPoint;
                taskBuilder.SetTrigger(this.applicationTrigger);                                      // Need set systemTrigger after testing
                taskBuilder.AddCondition(new SystemCondition(SystemConditionType.InternetAvailable));
                taskBuilder.Register();

                this.IsBackgroundTestEnable = true;
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
