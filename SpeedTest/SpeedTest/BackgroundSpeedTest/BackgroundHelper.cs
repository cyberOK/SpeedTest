using System.Linq;
using Windows.ApplicationModel.Background;
using Windows.Storage;
using System;
using Windows.ApplicationModel.Resources;

namespace SpeedTestUWP.BackgroundSpeedTest
{
    public class BackgroundHelper
    {
        private readonly string taskName = "IperfWrapper";
        private readonly string taskEntryPoint = "RuntimeSpeedTest.SpeedTestBackgroundTask";
        private readonly SystemTrigger networkStateChangeTrigger;
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

        public void StartBackgroundSpeedTest()
        {
            this.IsBackgroundTestEnable = true;
            //await this.applicationTrigger.RequestAsync();   // TODO: Delete after testing
        }

        public void StopBackgroundSpeedTest()
        {
            this.IsBackgroundTestEnable = false;
        }

        public void RegisteringBackgroundSpeedTest()
        {
            if ( !(IsTaskRegistered(this.taskName)))
            {
                var taskBuilder = new BackgroundTaskBuilder
                {
                    Name = this.taskName,
                    TaskEntryPoint = this.taskEntryPoint
                };
                taskBuilder.SetTrigger(this.networkStateChangeTrigger);
                //taskBuilder.SetTrigger(this.applicationTrigger);   // TODO: Delete after testing
                taskBuilder.AddCondition(new SystemCondition(SystemConditionType.InternetAvailable));
                taskBuilder.Register();

                this.IsBackgroundTestEnable = true;
            }            
        }

        private static bool IsTaskRegistered(string taskName)
        {
            return BackgroundTaskRegistration.AllTasks.Any(x => x.Value.Name.Equals(taskName, StringComparison.Ordinal));
        }

        //private void UnregisterTask(string taskName, bool cancelTask) =>
        //    BackgroundTaskRegistration.AllTasks.First(x => x.Value.Name.Equals(taskName)).Value?.Unregister(cancelTask);

        //private BackgroundTaskRegistration GetTaskByName(string taskName) =>
        //    BackgroundTaskRegistration.AllTasks.FirstOrDefault(x => x.Value.Name.Equals(taskName)).Value as BackgroundTaskRegistration;
    }
}
