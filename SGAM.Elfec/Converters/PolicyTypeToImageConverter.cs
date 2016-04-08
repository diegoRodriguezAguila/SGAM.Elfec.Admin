using SGAM.Elfec.Model.Enums;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SGAM.Elfec.Converters
{
    public class PolicyTypeToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PolicyType? type = value as PolicyType?;
            switch (type)
            {
                case PolicyType.ApplicationControl:
                    return "/Resources/Application/AppPolicy.png";
                case PolicyType.DeviceRestriction:
                    return "/Resources/Device/DevicePolicy.png";
                default:
                    return "/Resources/User/PolicyMini.png";
            }
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
