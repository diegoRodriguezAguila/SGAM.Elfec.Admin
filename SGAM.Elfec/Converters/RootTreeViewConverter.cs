using System;
using System.Linq;
using System.Windows.Data;

namespace SGAM.Elfec.Converters
{
    public class RootTreeviewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;

            return new object[] { value };
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null; ;
            return (value as object[]).FirstOrDefault();
        }
    }
}
