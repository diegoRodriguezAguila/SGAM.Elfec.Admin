using System;
using System.Globalization;
using System.Windows.Data;

namespace SGAM.Elfec.Converters
{
    public class CountToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int valueInt = 0, minCount = 0;
            if (value != null && value is int)
                valueInt = (int)value;
            if (parameter != null)
                minCount = System.Convert.ToInt32(parameter);
            return minCount < valueInt;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
