using Domain.Services;
using System;
using System.Globalization;
using System.Windows.Data;

namespace FootballManager.App.Converters
{
    public class TeamIdToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TeamService teamService = new TeamService();
            if (value == null)
            {
                return null;
            }
            else
            {
                var team = teamService.FindTeamById((Guid)value);
                return team?.Name.Value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}