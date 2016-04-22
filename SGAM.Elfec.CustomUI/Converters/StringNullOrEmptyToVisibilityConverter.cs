using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SGAM.Elfec.CustomUI.Converters
{
    public enum NullToVisibility
    {
        Default,
        Invert
    }
    public class StringNullOrEmptyToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            NullToVisibility type = NullToVisibility.Default;
            if (parameter != null && parameter is NullToVisibility)
                type = (NullToVisibility)parameter;

            if ((value == null || !(value is string) || string.IsNullOrEmpty(value as string)) ^ (type == NullToVisibility.Default))
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
