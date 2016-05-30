using SGAM.Elfec.Model.Enums;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SGAM.Elfec.Converters
{
    [ValueConversion(typeof(UserGroupStatus), typeof(string))]
    public class UserGroupStatusToStringConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            UserGroupStatus status = (UserGroupStatus)value;
            switch (status)
            {
                case UserGroupStatus.Disabled:
                    return Properties.Resources.ApiStatusDisabled;
                case UserGroupStatus.Enabled:
                    return Properties.Resources.ApiStatusEnabled;
                case UserGroupStatus.Sealed:
                    return Properties.Resources.UserGroupStatusSealed;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            String status = (String)value;
            if (status == Properties.Resources.ApiStatusDisabled)
                return UserGroupStatus.Disabled;
            if (status == Properties.Resources.ApiStatusEnabled)
                return UserGroupStatus.Enabled;
            if (status == Properties.Resources.UserGroupStatusSealed)
                return UserGroupStatus.Sealed;
            return null;
        }
    }
}
