using System;
using System.Globalization;
using System.Windows.Data;

namespace FootballManager.App.Converters
{
    public class ShirtNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var number = (int)value;
            if (number == -1)
            {
                return "n/a";
            }
            else
            {
                return number;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}