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
            string menuItem = parameter.ToString();
            if (status != DeviceStatus.AuthPending && 
                menuItem == Properties.Resources.MenuItemAuthorizeDevice)
                return Visibility.Collapsed;
            if (status == DeviceStatus.Authorized &&
                menuItem == Properties.Resources.MenuItemEnableDevice)
                return Visibility.Collapsed;
            if (status == DeviceStatus.Unauthorized &&
                menuItem == Properties.Resources.MenuItemDisableDevice)
                return Visibility.Collapsed;
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
