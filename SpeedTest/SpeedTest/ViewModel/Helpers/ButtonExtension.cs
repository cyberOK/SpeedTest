using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace SpeedTest.ViewModel.Helpers
{
    public static class ButtonExtension
    {
        public static object FindGridViewItemParent(this Button button, DependencyObject dependencyObject)
        {            
            var parent = VisualTreeHelper.GetParent(dependencyObject);

            if (parent == null)
            {
                return null;
            } 

            var parentT = parent as GridViewItem;

            return parentT ?? FindGridViewItemParent(button, parent);
        }
    }
}
