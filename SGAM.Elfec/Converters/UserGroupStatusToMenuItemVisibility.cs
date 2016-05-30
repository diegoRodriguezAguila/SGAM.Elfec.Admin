using SGAM.Elfec.Model.Enums;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SGAM.Elfec.Converters
{
    public class UserGroupStatusToMenuItemVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            UserGroupStatus status = (UserGroupStatus)value;
            string menuItem = parameter == null ? string.Empty : parameter.ToString();
            if (status == UserGroupStatus.Sealed &&
                (menuItem == Properties.Resources.LblDismissUserGroup ||
                menuItem == Properties.Resources.LblEnableUserGroup))
                return Visibility.Collapsed;
            if (status == UserGroupStatus.Disabled &&
                menuItem == Properties.Resources.LblDismissUserGroup)
                return Visibility.Collapsed;
            if (status == UserGroupStatus.Enabled &&
                menuItem == Properties.Resources.LblEnableUserGroup)
                return Visibility.Collapsed;
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
