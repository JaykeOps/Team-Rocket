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

        public void Add(Player player)
        {
            if (player.IsValidPlayer())
            {
                this.repository.Add(player);
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
        public void Add(IEnumerable<IExposablePlayer> players)
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
            var allPlayers = this.GetAllPlayers();
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
            var allPlayers = this.GetAllPlayers();
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
            var allPlayers = this.GetAllPlayers();
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
            var allPlayers = this.GetAllPlayers();
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

        public IEnumerable<IExposablePlayer> GetAllExposablePlayers()
        {
            return this.repository.GetAll();
        }

        public Player FindById(Guid playerId)
        {
            return this.repository.GetAll().ToList().Find(p => p.Id.Equals(playerId));
        }

        public IEnumerable<IExposablePlayer> Search(string searchText, StringComparison comparison
            = StringComparison.InvariantCultureIgnoreCase)
        {
            return this.repository.GetAll().Where(x =>
                x.Name.ToString().Contains(searchText, comparison)
                || x.DateOfBirth.ToString().Contains(searchText, comparison)
                || DomainService.FindTeamById(x.TeamId).Name.ToString().Contains(searchText, comparison) 
                || x.AggregatedStats.AllStats.Keys.Any
                (y => DomainService.FindSeriesById(y).SeriesName.ToString().Contains(searchText)));
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
                throw new NullReferenceException(
                    "Search failed! Datasource does not contain any " 
                    + $"data related to the player id '{playerId}'");
            }
        }

        public PlayerEvents GetPlayerEventsInSeries(Guid playerId, Guid seriesId)
        {
            var player = this.FindById(playerId);
            return player.AggregatedEvents[seriesId];
        }

        public PlayerStats GetPlayerStatsInSeries(Guid playerId, Guid seriesId)
        {
            var player = this.FindById(playerId);
            return player.AggregatedStats[seriesId];
        }

        public void RemovePlayer(Guid playerId)
        {
            this.repository.RemovePlayer(playerId);
        }

        public void AssignPlayerToTeam(IExposablePlayer exposablePlayer, Guid teamId)
        {
            var player = (Player) exposablePlayer;
            var team = DomainService.FindTeamById(teamId);
            player.UpdateTeamAffiliation(team);
            team.UpdatePlayerIds();
        }

        
    }
}