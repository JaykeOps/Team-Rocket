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
        private PlayerRepository repository => PlayerRepository.instance;

        public void Add(Player player)
        {
            this.repository.Add(player);
        }

        public IEnumerable<PlayerStats> GetTopScorersForSeries(Guid seriesId)
        {
            var allPlayers = this.GetAllPresentablePlayers();
            var playerStats = new List<PlayerStats>();
            foreach (var player in allPlayers)
            {
                try
                {
                    var p = player.AggregatedStats[seriesId];
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
            var allPlayers = this.GetAllPresentablePlayers();
            var playerStats = new List<PlayerStats>();
            foreach (var player in allPlayers)
            {
                try
                {
                    var p = player.AggregatedStats[seriesId];
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
            var allPlayers = this.GetAllPresentablePlayers();
            var playerStats = new List<PlayerStats>();
            foreach (var player in allPlayers)
            {
                try
                {
                    var p = player.AggregatedStats[seriesId];
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
            var allPlayers = this.GetAllPresentablePlayers();
            var playerStats = new List<PlayerStats>();
            foreach (var player in allPlayers)
            {
                try
                {
                    var p = player.AggregatedStats[seriesId];
                    playerStats.Add(p);
                }
                catch (SeriesMissingException)
                {
                }
            }
            return playerStats.OrderByDescending(ps => ps.RedCardCount).Take(5);
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return this.repository.GetAll();
        }

        public IEnumerable<Player> GetAllPresentablePlayers()
        {
            return this.repository.GetAll();
        }

        public Player FindById(Guid playerId)
        {
            return this.repository.GetAll().ToList().Find(p => p.Id.Equals(playerId));
        }

        public IEnumerable<IPresentablePlayer> FindPlayer(string searchText, StringComparison comp)
        {
            var result = this.repository.GetAll().Where(x =>
                x.Name.ToString().Contains(searchText, comp) ||
                x.DateOfBirth.Value.ToString().Contains(searchText, comp));

            return result;
        }

        public IPresentablePlayer RenamePlayer(IPresentablePlayer presentablePlayer, Name newName)
        {
            var player = (Player) presentablePlayer;
            player.Name = newName;
            this.Add(player);
            return player;
        }

        public void RenamePlayer(Guid playerId, Name newName)
        {
            var player = this.FindById(playerId);
            player.Name = newName;
        }
    }
}