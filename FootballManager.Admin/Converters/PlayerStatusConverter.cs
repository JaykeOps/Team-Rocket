using System;
using System.Globalization;
using System.Windows.Data;

namespace FootballManager.Admin.Converters
{
    public class PlayerStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (PlayerStatus)value;
            return status == PlayerStatus.NotAssigned ? "n/a" : status.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value == "n/a" ? "NotAssigned" : value;
        }
    }
}