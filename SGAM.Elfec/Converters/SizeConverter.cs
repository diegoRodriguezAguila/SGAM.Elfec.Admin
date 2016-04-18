using System;
using System.Globalization;
using System.Windows.Data;

namespace SGAM.Elfec.Converters
{
    [ValueConversion(typeof(double), typeof(double))]
    public class SizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double valueDouble = 0, parameterDouble = 0;
            if (value != null && value is double)
                valueDouble = (double)value;
            if (parameter != null)
                parameterDouble = System.Convert.ToDouble(parameter);
            return valueDouble + parameterDouble;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double valueDouble = 0, parameterDouble = 0;
            if (value != null && value is double)
                valueDouble = (double)value;
            if (parameter != null)
                parameterDouble = System.Convert.ToDouble(parameter);
            return valueDouble - parameterDouble;
        }
    }
}
