using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Interfaces;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SGAM.Elfec.Converters
{
    [ValueConversion(typeof(IEntity), typeof(string))]
    public class IEntityToImageConverter : IValueConverter
    {
        public string Size { get; set; } = "";
        public object Convert(object entity, Type targetType, object parameter, CultureInfo culture)
        {
            string imagePath = "/Resources/User/Entity{0}.png";
            if (entity is User)
                imagePath = "/Resources/User/User{0}.png";
            if (entity is UserGroup)
                imagePath = "/Resources/User/UserGroup{0}.png";
            return string.Format(imagePath, Size);
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
