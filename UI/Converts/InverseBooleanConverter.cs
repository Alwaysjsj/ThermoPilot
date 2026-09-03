using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace UI.Converts
{
    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool.TryParse(value.ToString(), out bool bRet);

            return !bRet;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
