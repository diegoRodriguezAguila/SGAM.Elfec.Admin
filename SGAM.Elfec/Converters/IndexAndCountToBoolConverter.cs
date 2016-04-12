using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace SGAM.Elfec.Converters
{
    public class IndexAndCountToBoolConverter : IMultiValueConverter
    {
        public bool Invert { get; set; }
        public object Convert(
            object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.All(v => (v is int)))
            {
                int index = System.Convert.ToInt32(values[0]);
                int count = System.Convert.ToInt32(values[1]);
                return index == count - 1 ^ Invert;
            }

            return !Invert;
        }

        public object[] ConvertBack(
            object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
