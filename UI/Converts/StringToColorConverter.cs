using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace UI.Converts
{
    public class StringToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string colorString = value?.ToString() ?? string.Empty;
            if (colorString == "Ready")
            {
                return (Brush)Application.Current.Resources["ReadyColor"];
            }
            else
            {
                return (Brush)Application.Current.Resources["DisabledColor"];
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
