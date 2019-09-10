using System;
using Windows.UI.Xaml.Data;

namespace SpeedTestIPerf.ViewModel.Converters
{
    public class RadioModeSelectedIntToBooltConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var mode = (int)value;
            return mode == int.Parse(parameter?.ToString()) ? true : false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return parameter;
        }
    }
}
