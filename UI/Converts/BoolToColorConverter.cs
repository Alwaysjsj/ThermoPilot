using System.Windows;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace UI.Converts
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool.TryParse(value?.ToString(), out bool bRet);
            if (bRet)
            {
                return (Brush)Application.Current.Resources["ModuleReadyColor"];
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
