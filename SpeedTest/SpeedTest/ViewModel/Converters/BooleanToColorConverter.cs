using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;

namespace SpeedTest.ViewModel.Converters
{
    public class BooleanToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((bool)value)
            {              
                return "#77777777";
            }
            return "{ThemeResource BackgroundPanelContent}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if ((bool)value)
            {
                return "WhiteSmoke";
            }
            return "LightGray";
        }
    }
}
