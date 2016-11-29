using System;
using System.ServiceModel;
using Domain.Entities;
using Domain.Interfaces;

namespace Domain.Helper_Classes
{
    [ServiceContract]
    public interface IValidator
    {
        bool IsValidName(string value, bool ignoreCase);
        bool IsValidCellPhoneNumber(string value, bool ignoreCase);
        bool IsValidEmailAddress(string value, bool ignoreCase);
        bool IsValidTeamName(string value, bool ignoreCase);
        bool IsValidArenaName(string value, bool ignoreCase);
        bool IsValidSeriesName(string value, bool ignoreCase);
        bool IsValidMatchDuration(TimeSpan value);
        bool IsValidNumberOfTeams(int value);
        bool IsValidMatchDateAndTime(DateTime value);
        bool IsValidBirthOfDate(string value);
        bool IsMatchMinute(int value);
        bool IsScoreValid(int score);
        bool IsValidGame(Game game);
        bool IsMatchValid(Match match);
        bool IsTeamValid(Team team);
        bool IsTeamValid(IExposableTeam exposableTeam);
        bool IsValidPlayer(Player player);
        bool IsValidPlayer(IExposablePlayer exposablePlayer);
        bool IsSeriesValid(Series series);
    }
}