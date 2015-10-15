using System;
using System.Globalization;
using System.Windows.Data;

namespace SGAM.Elfec.Converters
{
    /// <summary>
    /// Converter para el modelo de los devices a un url de imagen
    /// </summary>
    public class DeviceModelToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new Uri(String.Format(Properties.Settings.Default.AssetsURL, value));
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
