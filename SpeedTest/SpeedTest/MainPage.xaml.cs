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
            ApplicationView.PreferredLaunchViewSize = new Size(1920, 1080);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(380, 500));
        }

        //private async void BackButton_Click(object sender, RoutedEventArgs e)
        //{
        //    //this.IsEnabled = false;
        //    CoreApplicationView newView = CoreApplication.CreateNewView();
        //    int newViewId = 0;
        //    await newView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
        //    {

        //        Frame frame = new Frame();
        //        //frame.Height = 300;
        //        //frame.Width = 300;
        //        frame.Navigate(typeof(BlankPage1), null);
        //        Window.Current.Content = frame;

        //        Window.Current.Activate();

        //        newViewId = ApplicationView.GetForCurrentView().Id;
        //    });

        //    bool viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newViewId,ViewSizePreference.UseMinimum);
        //}
    }
}
