using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using Domain.Entities;
using Domain.Services;

namespace FootballManager.App.Converters
{
    public class TeamIdToStringConverter : IValueConverter 
    {
        TeamService teamService = new TeamService();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var teams = teamService.GetAll();

            var teamName = teams.Where(x => x.Id == (Guid) value).Select(x => x.Name).FirstOrDefault();
            return teamName; // This is NOT a  string, it has the type "TeamName".
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var teams = teamService.GetAll();

            var result = teams.First(x => x == (Team)value).Id; // Isn't value supposed to be of type teamName?

            return result;
        }
    }
}
