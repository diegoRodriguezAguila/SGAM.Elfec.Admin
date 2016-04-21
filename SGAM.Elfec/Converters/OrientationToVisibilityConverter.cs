using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SGAM.Elfec.Converters
{
    [ValueConversion(typeof(Orientation), typeof(Visibility))]
    public class OrientationToVisibilityConverter : IValueConverter
    {
        public Visibility HorizontalValue { get; set; }
        public Visibility VerticalValue { get; set; }

        public OrientationToVisibilityConverter()
        {
            // set defaults
            HorizontalValue = Visibility.Visible;
            VerticalValue = Visibility.Collapsed;
        }

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (!(value is Orientation))
                return Visibility.Collapsed;
            return ((Orientation)value) == Orientation.Horizontal ? HorizontalValue : VerticalValue;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (Equals(value, HorizontalValue))
                return Orientation.Horizontal;
            if (Equals(value, VerticalValue))
                return Orientation.Vertical;
            return null;
        }
    }
}
