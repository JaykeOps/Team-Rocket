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
    public class PlayerService : IPlayerService
    {
        private PlayerRepository repository => PlayerRepository.instance;

        public void Add(IExposablePlayer player)
        {
            if (player.IsValidPlayer())
            {
                this.repository.Add((Player)player);
            }
            else
            {
                throw new FormatException("Player cannot be added. Player carries invalid values!");
            }
        }

        public void Add(IEnumerable<Player> players)
        {
            if (players != null)
            {
                foreach (var player in players)
                {
                    this.Add(player);
                }
            }
            else
            {
                throw new NullReferenceException("List of player is null");
            }
        }

        public IEnumerable<PlayerStats> GetTopScorersForSeries(Guid seriesId)
        {
            var allPlayers = this.GetAllExposablePlayers();
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
            var allPlayers = this.GetAllExposablePlayers();
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
            var allPlayers = this.GetAllExposablePlayers();
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
            var allPlayers = this.GetAllExposablePlayers();
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

        public IEnumerable<Player> GetAllExposablePlayers()
        {
            return this.repository.GetAll();
        }

        public Player FindById(Guid playerId)
        {
            return this.repository.GetAll().ToList().Find(p => p.Id.Equals(playerId));
        }

        public IEnumerable<IExposablePlayer> FreeTextSearchForPlayers(string searchText, StringComparison comp)
        {
            var result = this.repository.GetAll().Where(x =>
                x.Name.ToString().Contains(searchText, comp) ||
                x.DateOfBirth.Value.ToString().Contains(searchText, comp));

            return result;
        }

        public void SetShirtNumber(Guid playerId, ShirtNumber newShirtNumber)
        {
            try
            {
                var player = this.FindById(playerId);
                player.ShirtNumber = newShirtNumber;
            }
            catch (ShirtNumberAlreadyInUseException ex)
            {
                throw ex;
            }
            catch (IndexOutOfRangeException ex)
            {
                throw ex;
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException($"Search failed! Datasource does not contain any data related to the player id '{playerId}'");
            }
        }
    }
}