﻿using SGAM.Elfec.Model.Enums;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SGAM.Elfec.Converters
{
    public class ApiStatusToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ApiStatus status = (ApiStatus)value;
            switch (status)
            {
                case ApiStatus.Disabled:
                    return Properties.Resources.ApiStatusDisabled;
                case ApiStatus.Enabled:
                    return Properties.Resources.ApiStatusEnabled;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string status = (String)value;
            if (status == Properties.Resources.ApiStatusDisabled)
                return ApiStatus.Disabled;
            if (status == Properties.Resources.ApiStatusEnabled)
                return ApiStatus.Enabled;
            return null;
        }
    }
}
