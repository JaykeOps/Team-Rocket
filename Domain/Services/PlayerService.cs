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

        public IEnumerable<PlayerStats> GetTopScorersForSeries(Guid seriesId)
        {
            var allPlayers = this.GetAllPlayers();
            var playerStats = new List<PlayerStats>();

            foreach (var player in allPlayers)
            {
                try
                {
                    var p = player.AggregatedStats[seriesId];
                    playerStats.Add((PlayerStats)p.Clone());
                }
                catch (SeriesMissingException)
                {
                }
            }
            var topStat = playerStats.OrderByDescending(ps => ps.GoalCount).Take(15);
            if (topStat.Count() == 0)
            {
                return topStat;
            }
            var bufferPlayer = topStat.First();
            bufferPlayer.Ranking = 1;
            for (var i = 0; i < topStat.Count(); i++)
            {
                var player = topStat.ElementAt(i);
                if (bufferPlayer.GoalCount != player.GoalCount)
                {
                    player.Ranking = bufferPlayer.Ranking + 1;
                }
                else
                {
                    player.Ranking = bufferPlayer.Ranking;
                }
                bufferPlayer = player;
            }
            return topStat;
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
                    playerStats.Add((PlayerStats)p.Clone());
                }
                catch (SeriesMissingException)
                {
                }
            }
            var topStat = playerStats.OrderByDescending(ps => ps.AssistCount).Take(15);
            if (topStat.Count() == 0)
            {
                return topStat;
            }
            var bufferPlayer = topStat.First();
            bufferPlayer.Ranking = 1;
            for (var i = 0; i < topStat.Count(); i++)
            {
                var player = topStat.ElementAt(i);
                if (bufferPlayer.AssistCount != player.AssistCount)
                {
                    player.Ranking = bufferPlayer.Ranking + 1;
                }
                else
                {
                    player.Ranking = bufferPlayer.Ranking;
                }
                bufferPlayer = player;
            }
            return topStat;
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
                    playerStats.Add((PlayerStats)p.Clone());
                }
                catch (SeriesMissingException)
                {
                }
            }
            var topStat = playerStats.OrderByDescending(ps => ps.YellowCardCount).Take(5);
            if (topStat.Count() == 0)
            {
                return topStat;
            }
            var bufferPlayer = topStat.First();
            bufferPlayer.Ranking = 1;
            for (var i = 0; i < topStat.Count(); i++)
            {
                var player = topStat.ElementAt(i);
                if (bufferPlayer.YellowCardCount != player.YellowCardCount)
                {
                    player.Ranking = bufferPlayer.Ranking + 1;
                }
                else
                {
                    player.Ranking = bufferPlayer.Ranking;
                }
                bufferPlayer = player;
            }
            return topStat;
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
                    playerStats.Add((PlayerStats)p.Clone());
                }
                catch (SeriesMissingException)
                {
                }
            }
            var topStat = playerStats.OrderByDescending(ps => ps.RedCardCount).Take(5);
            if (topStat.Count() == 0)
            {
                return topStat;
            }
            var bufferPlayer = topStat.First();
            bufferPlayer.Ranking = 1;
            for (var i = 0; i < topStat.Count(); i++)
            {
                var player = topStat.ElementAt(i);
                if (bufferPlayer.RedCardCount != player.RedCardCount)
                {
                    player.Ranking = bufferPlayer.Ranking + 1;
                }
                else
                {
                    player.Ranking = bufferPlayer.Ranking;
                }
                bufferPlayer = player;
            }
            return topStat;
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
            return this.repository.GetAll().Where(x => x.Id != Guid.Empty &&
                (x.Name.ToString().Contains(searchText, comparison)
                || x.DateOfBirth.ToString().Contains(searchText, comparison)
                || x.AffiliatedTeamName.ToString().Contains(searchText, comparison)));
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
            var player = (Player)exposablePlayer;
            var team = DomainService.FindTeamById(teamId);
            player.UpdateTeamAffiliation(team);
            team.UpdatePlayerIds();
            this.repository.SaveData();
        }

        public void DismissPlayerFromTeam(IExposablePlayer exposablePlayer)
        {
            var player = (Player)exposablePlayer;
            var oldTeam = exposablePlayer.TeamId != Guid.Empty ?
                DomainService.FindTeamById(exposablePlayer.TeamId) : null;
            player.UpdateTeamAffiliation(null);
            oldTeam?.UpdatePlayerIds();
            this.repository.SaveData();
        }

        public IEnumerable<PlayerStats> GetPlayerStatsFreeTextSearch(string searchText)
        {
            var expoPlayers = this.Search(searchText);
            return expoPlayers.Cast<Player>().SelectMany(player => player.AggregatedStats.AllStats.Values);
        }

        public IEnumerable<IExposablePlayer> GetAllExposablePlayersInTeam(Guid teamId)
        {
            var players = this.GetAllPlayers();
            return players.Where(player => player.TeamId == teamId).ToList();
        }

        public IEnumerable<Player> GetAllPlayersInTeam(Guid teamId)
        {
            var players = this.GetAllPlayers();
            return players.Where(player => player.TeamId == teamId).ToList();
        }

        public IEnumerable<IExposablePlayer> SearchForTeamlessPlayers(string searchText)
        {
            var players = this.GetAllPlayers().ToList();
            return searchText == string.Empty
                ? new List<Player>()
                : players.Where(x => x.TeamId == Guid.Empty &&
                (x.Name.ToString().Contains(searchText)
                || x.Position.ToString().Contains(searchText)
                || x.Status.ToString().Contains(searchText)
                || x.DateOfBirth.ToString().Contains(searchText)));
        }
        internal void Save()
        {
            repository.SaveData();
        }

    }
}