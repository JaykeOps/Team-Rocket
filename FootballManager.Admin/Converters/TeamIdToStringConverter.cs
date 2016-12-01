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
        TeamService teamService = new TeamService();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var teams = this.teamService.GetAllTeams();

            var teamName = teams.Where(x => x.Id == (Guid) value).Select(x => x.Name).FirstOrDefault();
            return teamName;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var teams = this.teamService.GetAllTeams();

            var result = teams.First(x => x == (Team)value).Id;

            return result;
        }
    }
}
