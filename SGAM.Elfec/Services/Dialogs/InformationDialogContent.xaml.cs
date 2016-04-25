using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SGAM.Elfec.Services.Dialogs
{
    public enum IconType { None, Information, Warning, Error, Custom }
    /// <summary>
    /// Interaction logic for InformationDialogContent.xaml
    /// </summary>
    public partial class InformationDialogContent : UserControl
    {
        internal InformationDialogContent()
        {
            InitializeComponent();
            DataContext = this;
        }

        #region Properties       
        #region Message

        public static DependencyProperty MessageProperty = DependencyProperty.RegisterAttached(
                "Message",
                typeof(string),
                typeof(InformationDialogContent));
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }
        #endregion
        #region IconType
        public static DependencyProperty IconTypeProperty = DependencyProperty.RegisterAttached(
                "IconType",
                typeof(IconType),
                typeof(InformationDialogContent),
                new PropertyMetadata(IconType.Information));
        public IconType IconType
        {
            get { return (IconType)GetValue(IconTypeProperty); }
            set { SetValue(IconTypeProperty, value); }
        }
        #endregion
        #endregion       
    }

    [ValueConversion(typeof(IconType), typeof(string))]
    public class IconTypeToSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IconType? type = value as IconType?;
            switch (type)
            {
                case IconType.None:
                    return null;
                case IconType.Information:
                    return "/Resources/Tools/Information.png";
                case IconType.Warning:
                    return "/Resources/Tools/Warning.png";
                case IconType.Error:
                    return "/Resources/Tools/Error.png";
                case IconType.Custom:
                    throw new NotImplementedException("Custom icon is not implemented, please implement, lol");
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    [ValueConversion(typeof(IconType), typeof(Visibility))]
    public class IconTypeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IconType? type = value as IconType?;
            if (type == IconType.None)
                return Visibility.Collapsed;
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
