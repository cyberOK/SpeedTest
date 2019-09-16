using System;
using Windows.UI.Xaml.Data;

namespace SpeedTestUWP.ViewModel.Converters
{
    public class BooleanToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((bool)value) 
            {              
                return "#77777777"; // if item selected background will be grey in all themes
            }
            return "{ThemeResource BackgroundPanelContent}"; // if item doesn't select background will be set by default theme
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return new NotImplementedException();
        }
    }
}
