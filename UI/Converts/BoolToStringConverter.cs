using System;
using System.Globalization;
using System.Windows.Data;

namespace UI.Converts
{
    public class BoolToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool.TryParse(value?.ToString(), out bool bRet);
            string[] par = parameter?.ToString().Split(',');
            string trueText = par.Length > 0 ? par[0] : string.Empty;
            string falseText = par.Length > 1 ? par[1] : string.Empty;
            return bRet ? trueText : falseText;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
