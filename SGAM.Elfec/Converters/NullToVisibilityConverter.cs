using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SGAM.Elfec.Converters
{
    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                if (parameter != null && parameter is Visibility)
                    return (Visibility)parameter;
                return Visibility.Collapsed;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
