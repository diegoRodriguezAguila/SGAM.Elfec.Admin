using SGAM.Elfec.Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SGAM.Elfec.Converters
{
    public class DeviceStatusToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DeviceStatus status = (DeviceStatus)value;
            switch (status)
            {
                case DeviceStatus.Unauthorized:
                    return "#54BFBFBF";
                case DeviceStatus.AuthPending:
                    return "#2415386F";
                default:
                    return "#00000000";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
