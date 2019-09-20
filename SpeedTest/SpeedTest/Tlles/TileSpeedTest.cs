using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Microsoft.Toolkit.Uwp.Notifications;
using Windows.ApplicationModel.Resources;

namespace SpeedTestUWP.Tlles
{
    public static class TileSpeedTest
    {
        public static void CreateTile(string ping, string downloadSpeed, string uploadSpeed, ResourceLoader resources)
        {
            TileContent content = new TileContent()
            {
                Visual = new TileVisual()
                {
                    TileSmall = new TileBinding()
                    {
                        Content = new TileBindingContentAdaptive()
                        {
                            Children =
                            {
                                new AdaptiveText()
                                {
                                    Text = downloadSpeed + resources.GetString("MeasureSpeedValue"),
                                    HintStyle = AdaptiveTextStyle.Default
                                },

                                new AdaptiveText()
                                {
                                    Text = uploadSpeed + resources.GetString("MeasureSpeedValue"),
                                    HintStyle = AdaptiveTextStyle.Default
                                }
                            }
                        }
                    },

                    TileMedium = new TileBinding()
                    {
                        Content = new TileBindingContentAdaptive()
                        {
                            Children =
                            {
                                new AdaptiveText()
                                {
                                    Text = resources.GetString("TilePingText") + ping + resources.GetString("MeasurePingValue"),
                                },

                                new AdaptiveText()
                                {
                                    Text = resources.GetString("TileDownloadText") + downloadSpeed + resources.GetString("MeasureSpeedValue"),
                                },
                                // resources.GetString("MeasureSpeedValue");
                                new AdaptiveText()
                                {
                                    Text = resources.GetString("TileUploadText") + uploadSpeed + resources.GetString("MeasureSpeedValue"),
                                }
                            }
                        }
                    },

                    TileWide = new TileBinding()
                    {
                        Content = new TileBindingContentAdaptive()
                        {
                            Children =
                            {
                                new AdaptiveText()
                                {
                                    Text = resources.GetString("TilePingText") + ping + resources.GetString("MeasurePingValue"),
                                },

                                new AdaptiveText()
                                {
                                    Text = resources.GetString("TileDownloadText") + downloadSpeed + resources.GetString("MeasureSpeedValue"),
                                },

                                new AdaptiveText()
                                {
                                    Text = resources.GetString("TileUploadText") + uploadSpeed + resources.GetString("MeasureSpeedValue"),
                                }
                            }
                        }
                    }                   
                }
            };

            var notification = new TileNotification(content.GetXml());

            TileUpdateManager.CreateTileUpdaterForApplication().Update(notification);
        }       
    }
}
