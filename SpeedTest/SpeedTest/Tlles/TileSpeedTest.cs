using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Microsoft.Toolkit.Uwp.Notifications;

namespace SpeedTestUWP.Tlles
{
    public static class TileSpeedTest
    {
        public static void CreateTile(string ping, string downloadSpeed, string uploadSpeed)
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
                                    Text = downloadSpeed,
                                    HintStyle = AdaptiveTextStyle.Default
                                },

                                new AdaptiveText()
                                {
                                    Text = uploadSpeed,
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
                                    Text = "Ping: " + ping,
                                },

                                new AdaptiveText()
                                {
                                    Text = "Download: " + downloadSpeed,
                                },

                                new AdaptiveText()
                                {
                                    Text = "Upload: " + uploadSpeed,
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
                                    Text = "Ping: " + ping,
                                },

                                new AdaptiveText()
                                {
                                    Text = "Download: " + downloadSpeed,
                                },

                                new AdaptiveText()
                                {
                                    Text = "Upload: " + uploadSpeed,
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
