using System;
using System.Globalization;
using System.Windows.Data;

namespace FootballManager.Admin.Converters
{
    public class PlayerPositionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var position = (PlayerPosition)value;
            return position.ToString() == "NotAssigned" ? "n/a" : position.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value == "n/a" ? PlayerPosition.NotAssigned : value;
        }
    }
}