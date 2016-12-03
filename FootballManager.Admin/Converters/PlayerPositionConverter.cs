using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FootballManager.Admin.Converters
{
    public class PlayerPositionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var position = (PlayerPosition) value;
            switch (position)
            {
                case PlayerPosition.Forward:
                    return "Forward";
                case PlayerPosition.MidFielder:
                    return "Midfielder";
                case PlayerPosition.Defender:
                    return "Defender";
                case PlayerPosition.GoalKeeper:
                    return "Goalkeeper";
                default:
                    return "Unknown";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return PlayerPosition.Forward;
            }
            else
            {
                return (PlayerPosition) value;
            }
        }
    }
}
