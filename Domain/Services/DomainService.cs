using Domain.Entities;
using Domain.Helper_Classes;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Interfaces;

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

        public static void AddSeriesToTeam(Series series)
        {
            foreach (var teamId in series.TeamIds)
            {
                var team = FindTeamById(teamId);
                team.AddSeries(series);
                AddSeriesToPlayers(series, team);
            }
        }

        public static void AddSeriesToPlayers(Series series,
            Team team)
        {
            foreach (var player in team.Players)
            {
                player.AddSeries(series);
            }
        }

        public static void AddTeamToPlayer(Team team, Guid playerId)
        {
            var player = FindPlayerById(playerId);
            player.TeamId = team.Id;
        }

        public static void AddMatches(IEnumerable<Match> matches)
        {
            var matchService = new MatchService();
            matchService.Add(matches);
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

        public static IEnumerable<Goal> GetAllTeamsGoalsForAndAgainstInSeries(Guid teamId, Guid seriesId)
        {
            return GetAllGames().Where(x => x.SeriesId == seriesId && x.HomeTeamId == teamId
                                     || x.AwayTeamId == teamId).SelectMany(y => y.Protocol.Goals);
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
            return gamesMatchingSeries.Where(game => game.Protocol.AwayTeamActivePlayers.Contains(playerId) ||
                                                     game.Protocol.HomeTeamActivePlayers.Contains(playerId));

        }

        public static void ScheduleGenerator(Guid seriesId)
        {
            var schedule = new Schedule();
            var series = FindSeriesById(seriesId);
            schedule.GenerateSchedule(series);
            AddMatches(series.Schedule[0]);
            AddMatches(series.Schedule[1]);
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

        public static IEnumerable<IExposablePlayer> GetAllPlayers()
        {
            var playerService = new PlayerService();
            return playerService.GetAllExposablePlayers();
        }

        public static IEnumerable<Guid> GetTeamSchedules(Guid teamId)
        {
            return from match in GetAllMatches()
                   where match.HomeTeamId == teamId || match.AwayTeamId == teamId
                   select match.Id;
        }

        public static IEnumerable<Guid> GetTeamScheduleForSeries(Guid seriesId, Guid teamId)
        {
            return from match in GetAllMatches()
                   where (match.HomeTeamId == teamId || match.AwayTeamId == teamId)
                   && match.SeriesId == seriesId
                   select match.Id;
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