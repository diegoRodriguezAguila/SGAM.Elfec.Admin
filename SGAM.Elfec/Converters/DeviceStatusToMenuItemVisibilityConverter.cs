using SGAM.Elfec.Model.Enums;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SGAM.Elfec.Converters
{
    public class DeviceStatusToMenuItemVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DeviceStatus status = (DeviceStatus)value;
            String menuItem = parameter.ToString();
            if (status == DeviceStatus.AuthPending)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
