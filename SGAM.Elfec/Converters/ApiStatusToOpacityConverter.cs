using SGAM.Elfec.Model.Enums;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SGAM.Elfec.Converters
{
    [ValueConversion(typeof(ApiStatus), typeof(double))]
    public class ApiStatusToOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ApiStatus status = (ApiStatus)value;
            if (status == ApiStatus.Disabled)
                return 0.65;
            return 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
