using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;

namespace SpeedTest.Toasts
{
    public class SpeedTestToast
    {
        private readonly StringBuilder template;
        private readonly XmlDocument xml;

        public SpeedTestToast()
        {
            this.template = new StringBuilder();
            this.xml = new XmlDocument();
        }

        public void CreateSpeedTestToast(int ping = 0, double downloadSpeed = 0, double uploadSpeed = 0)
        {
            this.template.Append("<toast><visual><binding>");            
            this.template.AppendFormat("<text id='1'>Ping: {0}</text>",ping);
            this.template.AppendFormat("<text id='1'>Download speed: {0}</text>", downloadSpeed);
            this.template.AppendFormat("<text id='1'>Upload speed: {0}</text>", uploadSpeed);
            this.template.Append("</binding></visual></toast>");

            this.xml.LoadXml(this.template.ToString());

            Windows.UI.Notifications.ToastNotificationManager.CreateToastNotifier().Show(new Windows.UI.Notifications.ToastNotification(this.xml));
        }
    }
}
