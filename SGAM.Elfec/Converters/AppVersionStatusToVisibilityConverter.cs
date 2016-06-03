using SGAM.Elfec.Model.Enums;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SGAM.Elfec.Converters
{
    public class AppVersionStatusToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ApiStatus status = (ApiStatus)value;
            string menuItem = parameter == null ? string.Empty : parameter.ToString();
            if (status == ApiStatus.Disabled &&
                menuItem == Properties.Resources.LblDisableAppVersion)
                return Visibility.Collapsed;
            if (status == ApiStatus.Enabled &&
                menuItem == Properties.Resources.LblEnableAppVersion)
                return Visibility.Collapsed;
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
