using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Value_Objects;

namespace Domain.Helper_Classes
{
    public class ValidationService : IValidator
    {
        public bool IsValidName(string value, bool ignoreCase)
        {
            return ValueObjectValidation.IsValidName(value, ignoreCase);
        }

        public bool IsValidCellPhoneNumber(string value, bool ignoreCase)
        {
            return ValueObjectValidation.IsValidCellPhoneNumber(value, ignoreCase);
        }

        public bool IsValidEmailAddress(string value, bool ignoreCase)
        {
            return ValueObjectValidation.IsValidEmailAddress(value, ignoreCase);
        }

        public bool IsValidTeamName(string value, bool ignoreCase)
        {
            return ValueObjectValidation.IsValidTeamName(value, ignoreCase);
        }

        public bool IsValidArenaName(string value, bool ignoreCase)
        {
            return ValueObjectValidation.IsValidArenaName(value, ignoreCase);
        }

        public bool IsValidSeriesName(string value, bool ignoreCase)
        {
            return ValueObjectValidation.IsValidSeriesName(value, ignoreCase);
        }

        public bool IsValidMatchDuration(TimeSpan value)
        {
            return ValueObjectValidation.IsValidMatchDuration(value);
        }

        public bool IsValidNumberOfTeams(int value)
        {
            return ValueObjectValidation.IsValidNumberOfTeams(value);
        }

        public bool IsValidMatchDateAndTime(DateTime value)
        {
            return ValueObjectValidation.IsValidMatchDateAndTime(value);
        }

        public bool IsValidBirthOfDate(string value)
        {
            return ValueObjectValidation.IsValidBirthOfDate(value);
        }

        public bool IsMatchMinute(int value)
        {
            return ValueObjectValidation.IsMatchMinute(value);
        }

        public bool IsScoreValid(int score)
        {
            return ValueObjectValidation.IsScoreValid(score);
        }

        public bool IsValidGame(Game game)
        {
            return EntityValidation.IsValidGame(game);
        }

        public bool IsMatchValid(Match match)
        {
            return EntityValidation.IsMatchValid(match);
        }

        public bool IsTeamValid(Team team)
        {
            return EntityValidation.IsTeamValid(team);
        }

        public bool IsTeamValid(IExposableTeam exposableTeam)
        {
            return EntityValidation.IsTeamValid(exposableTeam);
        }

        public bool IsValidPlayer(Player player)
        {
            return EntityValidation.IsValidPlayer(player);
        }

        public bool IsValidPlayer(IExposablePlayer exposablePlayer)
        {
            return EntityValidation.IsValidPlayer(exposablePlayer);
        }

        public bool IsSeriesValid(Series series)
        {
            return EntityValidation.IsSeriesValid(series);
        }
    }
}
