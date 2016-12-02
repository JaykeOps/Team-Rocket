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
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TeamService teamService = new TeamService();
            
            if (value != null)
            {
                var id = (Guid) value;
                var team = teamService.FindTeamById(id);
                return team.Name.Value;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
