using Core.Entities;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace UI.Converts
{
    public class MonitorVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null)
            {
                return Visibility.Collapsed;
            }

            WaferRouteEntity entity = (WaferRouteEntity)value;

            if (!string.IsNullOrEmpty(entity.RecipeName) && entity.RecipeEndTime > entity.RecipeStartTime && entity.RecipeStartTime > DateTime.MinValue)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
