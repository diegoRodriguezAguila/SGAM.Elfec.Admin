using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SGAM.Elfec.Converters
{
    [ValueConversion(typeof(int), typeof(Visibility))]
    public class CountToVisibilityConverter : IValueConverter
    {
        public Visibility TrueValue { get; set; }
        public Visibility FalseValue { get; set; }

        public CountToVisibilityConverter()
        {
            // set defaults
            TrueValue = Visibility.Visible;
            FalseValue = Visibility.Collapsed;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int valueInt = 0, minCount = 0;
            if (value != null && value is int)
                valueInt = (int)value;
            if (parameter != null)
                minCount = System.Convert.ToInt32(parameter);
            return minCount < valueInt ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
