using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Domain.Value_Objects;

namespace FootballManager.Admin.Converters
{
    public class TeamConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (targetType.Name)
            {
                case "TeamName":
                    return new TeamName(value.ToString());
                case "ArenaName":
                    return new ArenaName(value.ToString());
                case "EmailAddress":
                    return new EmailAddress(value.ToString());
                default:
                    return value.ToString();
            }
        }
    }
}
