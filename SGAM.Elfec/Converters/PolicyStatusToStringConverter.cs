using SGAM.Elfec.Model.Enums;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SGAM.Elfec.Converters
{
    [ValueConversion(typeof(ApiStatus), typeof(string))]
    public class PolicyStatusToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ApiStatus? status = value as ApiStatus?;
            switch (status)
            {
                case ApiStatus.Disabled:
                    return Properties.Resources.PolicyStatusDisabled;
                case ApiStatus.Enabled:
                    return Properties.Resources.PolicyStatusEnabled;
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string status = value as string;
            if (status == Properties.Resources.PolicyStatusDisabled)
                return ApiStatus.Disabled;
            if (status == Properties.Resources.PolicyStatusEnabled)
                return ApiStatus.Enabled;
            return null;
        }
    }
}
