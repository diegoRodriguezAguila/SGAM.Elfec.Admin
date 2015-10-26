using System;
using System.Globalization;
using System.Windows.Data;

namespace SGAM.Elfec.Converters
{
    /// <summary>
    /// Converter para url relativa a url completa
    /// </summary>
    public class RelativeToAbsoluteUrlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Uri)
                return new Uri(new Uri(Settings.Properties.SGAM.Default.BaseURL), (value as Uri));
            return new Uri(Settings.Properties.SGAM.Default.BaseURL);
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
