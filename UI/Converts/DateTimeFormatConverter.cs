using System;
using System.Globalization;
using System.Windows.Data;

namespace UI.Converts
{
    public class DateTimeFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
            {
                return "";
            }

            if (value is DateTime dateTime)
            {
                int.TryParse(parameter?.ToString(), out int decimalPlace);
                switch (decimalPlace)
                {
                    case 1:
                        return dateTime.ToString("yyyy-MM-dd HH:mm:ss.f");
                    case 2:
                        return dateTime.ToString("yyyy-MM-dd HH:mm:ss.ff");
                    case 3:
                        return dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    case 4:
                        return dateTime.ToString("yyyy-MM-dd HH:mm:ss.ffff");
                    case 5:
                        return dateTime.ToString("yyyy-MM-dd HH:mm:ss.fffff");
                }
                return dateTime.ToString("yyyy-MM-dd HH:mm:ss.fffff");
            }
            else
            {
                return value.ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
