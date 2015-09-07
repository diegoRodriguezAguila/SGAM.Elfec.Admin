using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SGAM.Elfec.CustomUI
{
    public class IconToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var bitmapFrame = (BitmapFrame)value;
            string path = bitmapFrame.ToString();
            int size = 32;
            if(parameter!=null)
                size = System.Convert.ToInt32(parameter);
            Uri uri = new Uri(path);
            Icon icon = new Icon(Application.GetResourceStream(uri).Stream, size, size);
            return Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value as ImageSource).ToString();
        }
    }
}
