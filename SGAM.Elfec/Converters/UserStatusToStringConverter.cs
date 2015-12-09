using SGAM.Elfec.Model.Enums;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SGAM.Elfec.Converters
{
    public class UserStatusToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            UserStatus status = (UserStatus)value;
            switch (status)
            {
                case UserStatus.Disabled:
                    return Properties.Resources.ApiStatusDisabled;
                case UserStatus.Enabled:
                    return Properties.Resources.ApiStatusEnabled;
                case UserStatus.NonRegistered:
                    return Properties.Resources.UserStatusNonRegistered;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            String status = (String)value;
            if (status == Properties.Resources.ApiStatusDisabled)
                return UserStatus.Disabled;
            if (status == Properties.Resources.ApiStatusEnabled)
                return UserStatus.Enabled;
            if (status == Properties.Resources.UserStatusNonRegistered)
                return UserStatus.NonRegistered;
            return null;
        }
    }
}
