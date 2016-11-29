using System;
using System.ServiceModel;
using Domain.Entities;
using Domain.Interfaces;

namespace Domain.Services
{
    [ServiceContract]
    public interface IValidationService
    {
        [OperationContract]
        bool IsValidName(string value, bool ignoreCase);

        [OperationContract]
        bool IsValidCellPhoneNumber(string value, bool ignoreCase);

        [OperationContract]
        bool IsValidEmailAddress(string value, bool ignoreCase);

        [OperationContract]
        bool IsValidTeamName(string value, bool ignoreCase);

        [OperationContract]
        bool IsValidArenaName(string value, bool ignoreCase);

        [OperationContract]
        bool IsValidSeriesName(string value, bool ignoreCase);

        [OperationContract]
        bool IsValidMatchDuration(TimeSpan value);

        [OperationContract]
        bool IsValidNumberOfTeams(int value);

        [OperationContract]
        bool IsValidMatchDateAndTime(DateTime value);

        [OperationContract]
        bool IsValidBirthOfDate(string value);

        [OperationContract]
        bool IsMatchMinute(int value);

        [OperationContract]
        bool IsScoreValid(int score);

        [OperationContract]
        bool IsValidGame(Game game);

        [OperationContract]
        bool IsMatchValid(Match match);

        [OperationContract]
        bool IsTeamValid(Team team);

        [OperationContract]
        bool IsTeamValid(IExposableTeam exposableTeam);

        [OperationContract]
        bool IsValidPlayer(Player player);

        [OperationContract]
        bool IsValidPlayer(IExposablePlayer exposablePlayer);

        [OperationContract]
        bool IsSeriesValid(Series series);
    }
}