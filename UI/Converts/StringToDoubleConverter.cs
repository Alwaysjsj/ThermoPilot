using System;
using System.Globalization;
using System.Windows.Data;

namespace UI.Converts
{
   public class StringToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double.TryParse(value.ToString(), out double dRet);
            return dRet;
        }
    }
}
