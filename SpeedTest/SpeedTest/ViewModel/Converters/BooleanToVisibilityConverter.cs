using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace SpeedTest.ViewModel.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        /// <summary>

        /// Converts true values to Visibility.Visible and false values to

        /// Visibility.Collapsed, or the reverse if the parameter is "Reverse".

        /// </summary>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ^ (parameter as string ?? string.Empty).Equals("Reverse") ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>

        /// Converts Visibility.Visible values to true and Visibility.Collapsed 

        /// values to false, or the reverse if the parameter is "Reverse".

        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (Visibility)value == Visibility.Visible ^ (parameter as string ?? string.Empty).Equals("Reverse");
        }
    }
}
