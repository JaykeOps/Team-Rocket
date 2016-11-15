using Domain.Entities;
using Domain.Value_Objects;
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
            var gameService= new GameService();
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
            var matches = new List<Match>();
            foreach (var matchId in matchIds)
            {
                matches.Add(FindMatchById(matchId));
            }
            
            foreach (var team in teams)
            {
                var matchIdsRelevantForTeam = matches.Where(x => x.HomeTeamId.Equals(team.Id)
                    || x.AwayTeamId.Equals(team.Id)).Select(x => x.Id);

                team.AddSeries(seriesId, matchIdsRelevantForTeam);
                AddSeriesIdToPlayers(seriesId, team.Players);
            }
        }
        public static void AddSeriesIdToPlayers(Guid seriesId, IEnumerable<Player> players)
        {
            foreach (var player in players)
            {
                player.AddSeriesEvents(seriesId);
                player.AddSeriesStats(seriesId);
            }
        }

        public static void AddGoal(Goal goal, Guid seriesId, Guid teamId, Guid playerId)
        {
            var team = FindTeamById(teamId);
            var player = FindPlayerById(playerId);
            team.AddGoalEvent(seriesId, goal);
            player.AddGoalToSeriesEvents(seriesId, goal);
        }

        public static void AddAssist(Assist assist, Guid seriesId, Guid teamId, Guid playerId)
        {
            var team = FindTeamById(teamId);
            var player = FindPlayerById(playerId);
            player.AddAssistToSeriesEvents(seriesId, assist);
        }

        public static void AddCard(Card card, Guid seriesId, Guid teamId, Guid playerId)
        {
            var team = FindTeamById(teamId);
            var player = FindPlayerById(playerId);
            player.AddCardToSeriesEvents(seriesId, card);
        }

        public static void MethodName()
        {

        }
    }
}