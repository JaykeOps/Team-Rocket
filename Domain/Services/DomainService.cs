using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public static IEnumerable<Game> GetAllGames()
        {
            var gameService = new GameService();
            return gameService.GetAll();
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

        public static void AddSeriesToTeam(Guid seriesId, IEnumerable<Guid> matchIds,
            IEnumerable<Team> teams)
        {
            var series = FindSeriesById(seriesId);
            var matches = new List<Match>();
            foreach (var matchId in matchIds)
            {
                matches.Add(FindMatchById(matchId));
            }

            foreach (var team in teams)
            {
                var matchIdsRelevantForTeam = matches.Where(x => x.HomeTeamId.Equals(team.Id)
                    || x.AwayTeamId.Equals(team.Id)).Select(x => x.Id);

                team.AddSeries(series, matchIdsRelevantForTeam);
                AddSeriesToPlayers(series, team.Players);
            }
        }

        public static void AddSeriesToPlayers(Series series, IEnumerable<Player> players)
        {
            foreach (var player in players)
            {
                player.AddSeries(series);
            }
        }
    }
}