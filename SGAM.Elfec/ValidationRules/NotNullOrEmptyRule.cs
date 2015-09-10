using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace SGAM.Elfec.ValidationRules
{
    public class NotNullOrEmptyRule : ValidationRule
    {
        public String FieldName{get; set;}
        public bool IsMale { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!String.IsNullOrWhiteSpace((String)value))
                return new ValidationResult(true, null);
            return new ValidationResult(false, (IsMale?"El ":"La ")+FieldName+" no puede estar vací"+ (IsMale ? "o" : "a"));
        }
    }
}
