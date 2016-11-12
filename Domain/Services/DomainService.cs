using Domain.Entities;
using System;

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

        //internal static Match FindMatchById(Guid id)
        //{
        //    var matchService = new MatchService();

        //}

        public static Series FindSeriesById(Guid id)
        {
            var seriesService = new SeriesService();
            return seriesService.FindById(id);
        }
    }
}