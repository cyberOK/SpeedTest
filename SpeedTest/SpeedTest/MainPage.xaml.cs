using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SpeedTest.ViewModel.HelpfullCollections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.ServiceModel;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using SpeedTest.RingSliceControl;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SpeedTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(380, 500));

            for (int i = 0; i < 360; i++)
            {
                double startAngle = i - 180;
                Brush brush = new SolidColorBrush(Colors.DarkCyan.Interpolate(Colors.Black, (double)i / 360));
                this.ArcGrid.Children.Add(new RingSlice() { StartAngle = startAngle, EndAngle = startAngle + 1, Fill = brush, Stroke = brush, Radius = 210, InnerRadius = 190 });
            }
        }
    }
}
