using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using Domain.Entities;
using Domain.Services;


namespace FootballManager.Admin.Converters
{
    public class TeamIdToStringConverter : IValueConverter
    {
        private readonly TeamService teamService = new TeamService();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            else
            {
                var team = teamService.FindTeamById((Guid)value);
                return team == null ? null : team.Name.Value;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var teams = teamService.GetAllTeams();

            var result = teams.First(x => x == (Team)value).Id; // Isn't value supposed to be of type teamName?

            return result;
        }
    }
}
