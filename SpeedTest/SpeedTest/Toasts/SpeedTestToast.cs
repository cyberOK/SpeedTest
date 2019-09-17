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

        public void CreateSpeedTestToast(int ping, double downloadSpeed, double uploadSpeed)
        {
            this.template.Append("<toast><visual version='2'><binding template='ToastText02'>");
            this.template.Append("<text id='2'>TimeZoneChanged</text>");
            this.template.AppendFormat("<text id='1'>{0}</text>",ping);
            this.template.AppendFormat("<text id='1'>{0}</text>", downloadSpeed);
            this.template.AppendFormat("<text id='1'>{0}</text>", uploadSpeed);
            this.template.Append("</binding></visual></toast>");

            this.xml.LoadXml(this.template.ToString());

            Windows.UI.Notifications.ToastNotificationManager.CreateToastNotifier().Show(new Windows.UI.Notifications.ToastNotification(this.xml));
        }
    }
}
