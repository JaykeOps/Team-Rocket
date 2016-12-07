using System;
using System.Globalization;
using System.Windows.Data;

namespace FootballManager.Admin.Converters
{
    public class ShirtNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                int number;
                int.TryParse(value.ToString(), out number);
                if (number == -1)
                {
                    return "n/a";
                }
                else
                {
                    return value;
                }
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value == "n/a" ? "-1" : value;
        }
    }
}