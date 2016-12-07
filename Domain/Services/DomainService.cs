using Domain.Entities;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services
{
    internal static class DomainService
    {
        internal static Player FindPlayerById(Guid id)
        {
            var playerService = new PlayerService();
            return playerService.FindById(id);
        }

        internal static Team FindTeamById(Guid id)
        {
            var teamService = new TeamService();
            return teamService.FindTeamById(id);
        }

        internal static Game FindGameById(Guid id)
        {
            var gameService = new GameService();
            return gameService.FindById(id);
        }

        internal static Match FindMatchById(Guid id)
        {
            var matchService = new MatchService();
            return matchService.FindById(id);
        }

        internal static Series FindSeriesById(Guid id)
        {
            var seriesService = new SeriesService();
            return seriesService.FindById(id);
        }

        internal static IEnumerable<Game> GetAllGames()
        {
            var gameService = new GameService();
            return gameService.GetAll();
        }

        internal static void AddSeriesToTeam(Series series)
        {
            foreach (var teamId in series.TeamIds)
            {
                var team = FindTeamById(teamId);
                team.AddSeries(series);
                AddSeriesToPlayers(series, team);
            }
        }

        internal static void AddSeriesToPlayers(Series series,
            Team team)
        {
            foreach (var playerId in team.PlayerIds)
            {
                var player = FindPlayerById(playerId);
                player.AddSeries(series);
            }
        }

        internal static void AddTeamToPlayer(Team team, Guid playerId)
        {
            var player = FindPlayerById(playerId);
            player.UpdateTeamAffiliation(team); 
        }

        internal static void AddMatches(IEnumerable<Match> matches)
        {
            var matchService = new MatchService();
            matchService.Add(matches);
        }

        internal static void RemoveGameAndMatchesFromSeries(Guid seriesId)
        {
            var series = FindSeriesById(seriesId);
            var matchService = new MatchService();
            var gameService = new GameService();
            var allGames = gameService.GetAll().ToList();
            foreach (var match in series.Schedule)
            {
                matchService.RemoveMatch(match.Id);

                if (allGames.Find(x => x.MatchId == match.Id) != null)
                {
                    gameService.RemoveGame(allGames.Find(x => x.MatchId == match.Id).Id);
                }
            }
        }

        internal static IEnumerable<Game> GetTeamsGamesInSeries(Guid teamId,
            Guid seriesId)
        {
            var result =
                from game in GetAllGames()
                where game.SeriesId == seriesId
                && (game.Protocol.HomeTeamId == teamId
                || game.Protocol.AwayTeamId == teamId)
                select game;
            return result.ToList();
        }

        internal static IEnumerable<Goal> GetAllTeamsGoalsForAndAgainstInSeries(Guid teamId, Guid seriesId)
        {
            return GetAllGames().Where(x => x.SeriesId == seriesId && x.HomeTeamId == teamId
                                     || x.AwayTeamId == teamId).SelectMany(y => y.Protocol.Goals).ToList();
        }

        internal static IEnumerable<Goal> GetPlayersGoalsInSeries(Guid playerId,
            Guid seriesId)
        {
            var playerGoals = new List<Goal>();
            var playerGoalsInGames = GetAllGames().Where(x => x.SeriesId == seriesId)
                .Select(x => x.Protocol.Goals.Where(y => y.PlayerId == playerId))
                .ToList();
            playerGoalsInGames.ForEach(x => playerGoals.AddRange(x));
            return playerGoals;
        }

        internal static IEnumerable<Assist> GetPlayerAssistInSeries(Guid playerId, Guid seriesId)
        {
            var playerAssists = new List<Assist>();
            var playerAssistInGames = GetAllGames().Where(x => x.SeriesId == seriesId)
                .Select(x => x.Protocol.Assists.Where(y => y.PlayerId == playerId))
                .ToList();
            playerAssistInGames.ForEach(x => playerAssists.AddRange(x));
            return playerAssists;
        }

        internal static IEnumerable<Card> GetPlayerCardsInSeries(Guid playerId, Guid seriesId)
        {
            var playerCards = new List<Card>();
            var playerCardsInGames = GetAllGames().Where(x => x.SeriesId == seriesId)
                .Select(x => x.Protocol.Cards.Where(y => y.PlayerId == playerId))
                .ToList();
            playerCardsInGames.ForEach(x => playerCards.AddRange(x));
            return playerCards;
        }

        internal static IEnumerable<Penalty> GetPlayerPenaltiesInSeries(Guid playerId, Guid seriesId)
        {
            var playerPenalties = new List<Penalty>();
            var playerPenaltiesInGames = GetAllGames().Where(x => x.SeriesId == seriesId)
                .Select(x => x.Protocol.Penalties.Where(y => y.PlayerId == playerId))
                .ToList();
            playerPenaltiesInGames.ForEach(x => playerPenalties.AddRange(x));
            return playerPenalties;
        }

        internal static IEnumerable<Game> GetPlayerPlayedGamesInSeries(Guid playerId, Guid seriesId)
        {
            var allGames = GetAllGames();
            var gamesMatchingSeries = allGames.Where(game => game.SeriesId == seriesId).ToList();
            return gamesMatchingSeries.Where(game => game.Protocol.AwayTeamActivePlayers.Contains(playerId) ||
                                                     game.Protocol.HomeTeamActivePlayers.Contains(playerId)).ToList();
        }

        internal static void ScheduleGenerator(Guid seriesId)
        {
            var seriesService = new SeriesService();
            seriesService.ScheduleGenerator(seriesId);
        }

        internal static IEnumerable<Team> GetAllTeams()
        {
            var teamService = new TeamService();
            return teamService.GetAllTeams().ToList();
        }

        internal static IEnumerable<Match> GetAllMatches()
        {
            var matchService = new MatchService();
            return matchService.GetAll().ToList();
        }

        internal static IEnumerable<Series> GetAllSeries()
        {
            var seriesService = new SeriesService();
            return seriesService.GetAll().ToList();
        }

        internal static IEnumerable<Player> GetAllPlayers()
        {
            var playerService = new PlayerService();
            return playerService.GetAllPlayers().ToList();
        }

        internal static IEnumerable<Guid> GetTeamSchedules(Guid teamId)
        {
            var result = from match in GetAllMatches()
                         where match.HomeTeamId == teamId || match.AwayTeamId == teamId
                         select match.Id;
            return result.ToList();
        }

        internal static IEnumerable<Guid> GetTeamScheduleForSeries(Guid seriesId, Guid teamId)
        {
            var result = from match in GetAllMatches()
                         where (match.HomeTeamId == teamId || match.AwayTeamId == teamId)
                         && match.SeriesId == seriesId
                         select match.Id;
            return result.ToList();
        }

        internal static void SaveAll()
        {
            var teamService = new TeamService();
            var playerService = new PlayerService();
            var seriesService = new SeriesService();
            var matchService = new MatchService();
            var gameService = new GameService();
            teamService.Save();
            playerService.Save();
            seriesService.Save();
            matchService.Save();
            gameService.Save();
        }

        internal static GameResult GetGameResult(GameProtocol protocol)
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