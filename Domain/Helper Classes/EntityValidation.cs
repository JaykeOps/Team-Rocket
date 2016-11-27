using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Value_Objects;

namespace Domain.Helper_Classes
{
    public static class EntityValidation
    {
        public static bool IsValidGame(this Game game)
        {


            try
            {
                return game.Id != Guid.Empty &&
                       game.AwayTeamId != Guid.Empty &&
                       game.HomeTeamId != Guid.Empty &&
                       game.SeriesId != Guid.Empty &&
                       game.MatchDuration.Value.IsValidMatchDuration() &&
                       //game.MatchDate.Value.IsValidMatchDateAndTime() &&
                       game.Location.Value.IsValidArenaName(true) &&
                       IsGameProtocolValid(game.Protocol);
            }
            catch (NullReferenceException)
            {
                return false;

            }

        }

        public static bool IsMatchValid(this Match match)
        {
            try
            {
                return match.Id != Guid.Empty &&
                       match.AwayTeamId != Guid.Empty &&
                       match.HomeTeamId != Guid.Empty &&
                       match.SeriesId != Guid.Empty &&
                       match.Location.Value.IsValidArenaName(true) &&
                       //match.MatchDate.Value.IsValidMatchDateAndTime() &&
                       match.MatchDuration.Value.IsValidMatchDuration();

            }
            catch (NullReferenceException)
            {
                return false;
            }
        }

        public static bool IsTeamValid(this Team team)
        {
            try
            {
                return team.Id != Guid.Empty &&
                       team.Events != null &&
                       team.Stats != null &&
                       team.Name.Value.IsValidName(true) &&
                       team.playerIds != null &&
                       team.ArenaName.Value.IsValidArenaName(true) &&
                       team.Email.Value.IsValidEmailAddress(true) &&
                       team.MatchSchedules != null &&
                       team.ShirtNumbers != null;
            }
            catch (NullReferenceException)
            {
                return false;

            }
        }

        public static bool IsValidPlayer(this Player player)
        {
            try
            {
                return player.Id != null &&
                       player.AggregatedEvents != null &&
                       player.AggregatedStats != null &&
                       player.Name.FirstName.IsValidName(true) &&
                       player.Name.LastName.IsValidName(true);
                       //player.DateOfBirth.Value.ToString().IsValidBirthOfDate();
            }
            catch (NullReferenceException)
            {
                return false;

            }
        }

        public static bool IsValidPlayer(this IExposablePlayer player)
        {
            try
            {
                return player.Id != null &&
                       player.AggregatedEvents != null &&
                       player.AggregatedStats != null &&
                       player.Name.FirstName.IsValidName(true) &&
                       player.Name.LastName.IsValidName(true);
                //player.DateOfBirth.Value.ToString().IsValidBirthOfDate();
            }
            catch (NullReferenceException)
            {
                return false;

            }
        }

        public static bool IsSeriesValid(this Series series)
        {
            try
            {
                return series.Id != Guid.Empty &&
                       series.SeriesName != null &&
                       series.NumberOfTeams.Value.IsValidNumberOfTeams() &&
                       series.MatchDuration.Value.IsValidMatchDuration() &&
                       series.TeamIds != null &&
                       series.Schedule != null;
            }
            catch (NullReferenceException)
            {

                return false;
            }
        }
        private static bool IsGameProtocolValid(GameProtocol protocol)
        {
            return protocol.Goals != null &&
                   protocol.Assists != null &&
                   protocol.Penalties != null &&
                   protocol.Cards != null &&
                   protocol.AwayTeamActivePlayers != null &&
                   protocol.HomeTeamActivePlayers != null &&
                   protocol.AwayTeamId != Guid.Empty &&
                   protocol.HomeTeamId != Guid.Empty;

        }
    }
}
