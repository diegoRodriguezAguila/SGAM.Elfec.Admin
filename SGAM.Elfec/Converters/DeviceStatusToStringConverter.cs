using SGAM.Elfec.Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SGAM.Elfec.Converters
{
    public class DeviceStatusToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DeviceStatus status = (DeviceStatus)value;
            switch (status)
            {
                case DeviceStatus.Unauthorized:
                    return Properties.Resources.DeviceStatusUnauthorized;
                case DeviceStatus.Authorized:
                    return Properties.Resources.DeviceStatusAuthorized;
                case DeviceStatus.AuthPending:
                    return Properties.Resources.DeviceStatusAuthPending;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
