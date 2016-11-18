using Domain.Entities;
using Domain.Value_Objects;
using Domain.Helper_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Domain.Services
{
    public static class DomainService
    {
        public static Player FindPlayerById(Guid id)
        {
            var playerService = new PlayerService();
            return playerService.FindById(id);
        }

        public static Team FindTeamById(Guid id)
        {
            var teamService = new TeamService();
            return teamService.FindById(id);
        }

        public static Game FindGameById(Guid id)
        {
            var gameService = new GameService();
            return gameService.FindById(id);
        }

        public static Match FindMatchById(Guid id)
        {
            var matchService = new MatchService();
            return matchService.FindById(id);
        }

        public static Series FindSeriesById(Guid id)
        {
            var seriesService = new SeriesService();
            return seriesService.FindById(id);
        }
        public static IEnumerable<Game> GetAllGames()
        {
            var gameService = new GameService();
            return gameService.GetAll();
        }

        public static void AddSeriesToTeam(Guid seriesId, Guid teamId)
        {
            var team = FindTeamById(teamId);
            team.AddSeries(FindSeriesById(seriesId));
        }

        public static void AddSeriesToPlayers(Series series,
            IEnumerable<Player> players)
        {
            foreach (var player in players)
            {
                player.AddSeries(series);
            }
        }


        public static IEnumerable<Game> GetTeamsGamesInSeries(Guid teamId,
            Guid seriesId)
        {
            return
                from game in GetAllGames()
                where game.SeriesId == seriesId
                && (game.Protocol.HomeTeamId == teamId
                || game.Protocol.AwayTeamId == teamId)
                select game;
        }

        public static IEnumerable<Goal> GetAllTeamsGoalsInSeries(Guid teamId, Guid seriesId)
        {
            var allGames = DomainService.GetAllGames();
            var allMatchinGames = allGames.Where(game => game.SeriesId == seriesId).ToList();
            return (from game in allMatchinGames from goal in game.Protocol.Goals where goal.TeamId == teamId select goal).ToList();
        }



        public static IEnumerable<Goal> GetPlayersGoalsInSeries(Guid playerId,
            Guid seriesId)
        {
            var playerGoals = new List<Goal>();
            var playerGoalsInGames = GetAllGames().Where(x => x.SeriesId == seriesId)
                .Select(x => x.Protocol.Goals.Where(y => y.PlayerId == playerId))
                .ToList();
            playerGoalsInGames.ForEach(x => playerGoals.AddRange(x));
            return playerGoals;
        }

        public static IEnumerable<Assist> GetPlayerAssistInSeries(Guid playerId, Guid seriesId)
        {
            var playerAssists = new List<Assist>();
            var playerAssistInGames = GetAllGames().Where(x => x.SeriesId == seriesId)
                .Select(x => x.Protocol.Assists.Where(y => y.PlayerId == playerId))
                .ToList();
            playerAssistInGames.ForEach(x => playerAssists.AddRange(x));
            return playerAssists;
        }

        public static IEnumerable<Card> GetPlayerCardsInSeries(Guid playerId, Guid seriesId)
        {
            var playerCards = new List<Card>();
            var playerCardsInGames = GetAllGames().Where(x => x.SeriesId == seriesId)
                .Select(x => x.Protocol.Cards.Where(y => y.PlayerId == playerId))
                .ToList();
            playerCardsInGames.ForEach(x => playerCards.AddRange(x));
            return playerCards;
        }
        public static IEnumerable<Penalty> GetPlayerPenaltiesInSeries(Guid playerId, Guid seriesId)
        {
            var playerPenalties = new List<Penalty>();
            var playerPenaltiesInGames = GetAllGames().Where(x => x.SeriesId == seriesId)
                .Select(x => x.Protocol.Penalties.Where(y => y.PlayerId == playerId))
                .ToList();
            playerPenaltiesInGames.ForEach(x => playerPenalties.AddRange(x));
            return playerPenalties;
        }

        public static IEnumerable<Game> GetPlayerPlayedGamesInSeries(Guid playerId, Guid seriesId)
        {
            var allGames = GetAllGames();
            var gamesMatchingSeries = allGames.Where(game => game.SeriesId == seriesId).ToList();
            return gamesMatchingSeries.Where(game => game.Protocol.AwayTeamStartingPlayers.Contains(playerId) ||
                                                                 game.Protocol.AwayTeamSub.Contains(playerId) ||
                                                     game.Protocol.HomeTeamStartingPlayers.Contains(playerId) ||
                                                                 game.Protocol.HomeTeamSub.Contains(playerId));
        }

        public static Dictionary<int, List<Match>> ScheduleGenerator(Guid seriesId)
        {
            var schedule = new Schedule();
            return schedule.GenerateSchedule(FindSeriesById(seriesId));
        }

        public static IEnumerable<Team> GetAllTeams()
        {
            var teamService = new TeamService();
            return teamService.GetAll();
        }

        public static IEnumerable<Match> GetAllMatches()
        {
            var matchService = new MatchService();
            return matchService.GetAll();
        }

        public static IEnumerable<Series> GetAllSeries()
        {
            var seriesService = new SeriesService();
            return seriesService.GetAll();
        }

        public static IEnumerable<Player> GetAllPlayers()
        {
            var playerService = new PlayerService();
            return playerService.GetAll();
        }

        public static IEnumerable<Guid> GetTeamSeriesSchedule(Guid teamId)
        {

            return from match in GetAllMatches() where match.HomeTeamId == teamId || match.AwayTeamId == teamId select match.Id;
        }

        public static GameResult GetGameResult(GameProtocol protocol)
        {
            var homeTeamScore = 0;
            var awayTeamScore = 0;
            if (protocol.Goals != null)
            {
                foreach (var goal in protocol.Goals)
                {
                    if (goal.TeamId == protocol.HomeTeamId)
                    {
                        homeTeamScore++;
                    }
                    else if (goal.TeamId == protocol.AwayTeamId)
                    {
                        awayTeamScore++;
                    }

                }
                return new GameResult(protocol.HomeTeamId, protocol.AwayTeamId, homeTeamScore, awayTeamScore);
            }
            else
            {
                throw new NullReferenceException("GameProtocol goal list is null");
            }
        }
    }
}