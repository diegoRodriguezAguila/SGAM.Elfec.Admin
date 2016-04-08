using SGAM.Elfec.Model.Enums;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SGAM.Elfec.Converters
{
    public class RuleActionToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            RuleAction? action = value as RuleAction?;
            switch (action)
            {
                case RuleAction.Permit:
                    return "/Resources/Rule/Permit.png";
                case RuleAction.Deny:
                    return "/Resources/Rule/Deny.png";
                default:
                    return "/Resources/Rule/Rule.png";
            }
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
