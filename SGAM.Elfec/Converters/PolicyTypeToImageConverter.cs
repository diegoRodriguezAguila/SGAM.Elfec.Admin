using SGAM.Elfec.Model.Enums;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SGAM.Elfec.Converters
{
    public class PolicyTypeToImageConverter : IValueConverter
    {
        public string Size { get; set; } = "";
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PolicyType? type = value as PolicyType?;
            string imagePath;
            switch (type)
            {
                case PolicyType.ApplicationControl:
                    imagePath = "/Resources/Application/AppPolicy{0}.png";
                    break;
                case PolicyType.DeviceRestriction:
                    imagePath = "/Resources/Device/DevicePolicy{0}.png";
                    break;
                default:
                    imagePath = "/Resources/User/Policy{0}.png";
                    break;
            }
            var te = string.Format(imagePath, Size);
            return string.Format(imagePath, Size);
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
