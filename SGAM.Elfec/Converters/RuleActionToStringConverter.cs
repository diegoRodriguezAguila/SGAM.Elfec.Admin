using SGAM.Elfec.Model.Enums;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SGAM.Elfec.Converters
{
    [ValueConversion(typeof(RuleAction), typeof(string))]
    public class RuleActionToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            RuleAction? status = value as RuleAction?;
            switch (status)
            {
                case RuleAction.Permit:
                    return Properties.Resources.RuleActionPermit;
                case RuleAction.Deny:
                    return Properties.Resources.RuleActionDeny;
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string status = value as string;
            if (string.Equals(status, Properties.Resources.RuleActionPermit, 
                StringComparison.OrdinalIgnoreCase))
                return RuleAction.Permit;
            if (string.Equals(status, Properties.Resources.RuleActionDeny, 
                StringComparison.OrdinalIgnoreCase))
                return RuleAction.Deny;
            return null;
        }
    }
}
