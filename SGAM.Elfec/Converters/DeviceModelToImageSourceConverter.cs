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
            return new Uri("http://192.168.50.56:3000/assets/" + value + ".png");
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
