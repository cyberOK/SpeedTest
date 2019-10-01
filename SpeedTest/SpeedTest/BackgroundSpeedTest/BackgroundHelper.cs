using System.Linq;
using Windows.ApplicationModel.Background;
using Windows.Storage;
using System;
using Windows.ApplicationModel.Resources;

namespace SpeedTestUWP.BackgroundSpeedTest
{
    public class BackgroundHelper
    {
        private string taskName = "IperfWrapper";
        private string taskEntryPoint = "RuntimeSpeedTest.SpeedTestBackgroundTask";
        private SystemTrigger networkStateChangeTrigger;
        //private ApplicationTrigger applicationTrigger;        // TODO: Delete after testing


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
            //this.applicationTrigger = new ApplicationTrigger();   // TODO: Delete after testing
            this.networkStateChangeTrigger = new SystemTrigger(SystemTriggerType.NetworkStateChange, false);
            this.RegisteringBackgroundSpeedTest();

        }

        public async void StartBackgroundSpeedTest()
        {
            this.IsBackgroundTestEnable = true;
            //await this.applicationTrigger.RequestAsync();   // TODO: Delete after testing
        }

        public async void StopBackgroundSpeedTest()
        {
            this.IsBackgroundTestEnable = false;
        }

        public void RegisteringBackgroundSpeedTest()
        {
            if ( !(this.IsTaskRegistered(this.taskName)))
            {
                var taskBuilder = new BackgroundTaskBuilder();
                taskBuilder.Name = this.taskName;
                taskBuilder.TaskEntryPoint = this.taskEntryPoint; 
                taskBuilder.SetTrigger(this.networkStateChangeTrigger);
                //taskBuilder.SetTrigger(this.applicationTrigger);   // TODO: Delete after testing
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
