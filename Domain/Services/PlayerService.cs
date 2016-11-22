using Domain.Entities;
using Domain.Helper_Classes;
using Domain.Interfaces;
using Domain.Repositories;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services
{
    public class PlayerService
    {
        private readonly PlayerRepository repository = PlayerRepository.instance;
        private readonly IEnumerable<Player> allPlayers;

        public PlayerService()
        {
            this.allPlayers = this.repository.GetAll();
        }

        public void Add(Player player)
        {
            this.repository.Add(player);
        }

        public IEnumerable<PlayerStats> GetTopScorersForSeries(Guid seriesId)
        {
            var allPlayers = GetAll();
            var playerStats = new List<PlayerStats>();
            foreach (var player in allPlayers)
            {
                try
                {
                    var p = player.PresentableSeriesStats[seriesId];
                    playerStats.Add(p);
                }
                catch (SeriesMissingException)
                {
                }
            }
            return playerStats.OrderByDescending(ps => ps.GoalCount).Take(15);
        }

        public IEnumerable<PlayerStats> GetTopAssistsForSeries(Guid seriesId)
        {
            var allPlayers = GetAll();
            var playerStats = new List<PlayerStats>();
            foreach (var player in allPlayers)
            {
                try
                {
                    var p = player.PresentableSeriesStats[seriesId];
                    playerStats.Add(p);
                }
                catch (SeriesMissingException)
                {
                }
            }
            return playerStats.OrderByDescending(ps => ps.AssistCount).Take(15);
        }

        public IEnumerable<PlayerStats> GetTopYellowCardsForSeries(Guid seriesId)
        {
            var allPlayers = GetAll();
            var playerStats = new List<PlayerStats>();
            foreach (var player in allPlayers)
            {
                try
                {
                    var p = player.PresentableSeriesStats[seriesId];
                    playerStats.Add(p);
                }
                catch (SeriesMissingException)
                {
                }
            }
            return playerStats.OrderByDescending(ps => ps.YellowCardCount).Take(5);
        }

        public IEnumerable<PlayerStats> GetTopRedCardsForSeries(Guid seriesId)
        {
            var allPlayers = GetAll();
            var playerStats = new List<PlayerStats>();
            foreach (var player in allPlayers)
            {
                try
                {
                    var p = player.PresentableSeriesStats[seriesId];
                    playerStats.Add(p);
                }
                catch (SeriesMissingException)
                {
                }
            }
            return playerStats.OrderByDescending(ps => ps.RedCardCount).Take(5);
        }

        public IEnumerable<Player> GetAll()
        {
            return this.repository.GetAll();
        }

        public Player FindById(Guid playerId)
        {
            return this.allPlayers.ToList().Find(p => p.Id.Equals(playerId));
        }

        public IEnumerable<IPresentablePlayer> FindPlayer(string searchText, StringComparison comp)
        {
            var result = this.allPlayers.Where(x =>
                x.Name.ToString().Contains(searchText, comp) ||
                x.DateOfBirth.Value.ToString().Contains(searchText, comp));

            return result;
        }

        public string GetPlayerName(Guid playerId)
        {
            var result = this.allPlayers.Where(x => x.Id == playerId).Select(x => x.Name.ToString()).First();
            return result;
        }

        public Guid GetPlayerTeamId(Guid playerId)
        {
            var result = this.allPlayers.Where(x => x.Id == playerId).Select(x => x.TeamId).First();
            return result;
        }

        public IEnumerable<PlayerPosition> PlayerPositions()
        {
            var result = Enum.GetValues(typeof(PlayerPosition)).Cast<PlayerPosition>();
            return result;
        }
        public IEnumerable<PlayerStatus> PlayerStatuses()
        {
            var result = Enum.GetValues(typeof(PlayerStatus)).Cast<PlayerStatus>();
            return result;
        }

    }
}