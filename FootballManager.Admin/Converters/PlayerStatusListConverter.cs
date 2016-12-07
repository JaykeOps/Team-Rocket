using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace FootballManager.Admin.Converters
{
    public class PlayerStatusListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var statuses = (IEnumerable<PlayerStatus>)value;
            return statuses.Select(playerStatus =>
                    playerStatus == PlayerStatus.NotAssigned ? "n/a" : playerStatus.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}