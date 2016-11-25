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
            //TODO: Add validation when merged to master!
            //TODO: Validate Name
            //TODO: Validate DateOfBirth
            //TODO: Validate Contactinformation - Email, Phone
             
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

        public IEnumerable<IPresentablePlayer> FreeTextSearchForPlayers(string searchText, StringComparison comp)
        {
            var result = this.repository.GetAll().Where(x =>
                x.Name.ToString().Contains(searchText, comp) ||
                x.DateOfBirth.Value.ToString().Contains(searchText, comp));

            return result;
        }

        public void RenamePlayer(IPresentablePlayer presentablePlayer, Name newName)
        {
            //TODO: Implement validaiton when merged!   
            var player = (Player) presentablePlayer;
            player.Name = newName;
            this.Add(player);
        }

        public void RenamePlayer(Guid playerId, Name newName)
        {
            //TODO: Implement validaiton when merged!
            var player = this.FindById(playerId);
            player.Name = newName;
        }

        public void SetShirtNumber(IPresentablePlayer presentablePlayer, ShirtNumber newShirtNumber)
        {
            var player = (Player) presentablePlayer;
            try
            {
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
            this.Add(player);

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
            catch (NullReferenceException ex)
            {
                throw new Exception($"Search failed! Datasource does not contain any data related to the player id '{playerId}'");
            }
        }

        public void SetEmailAddress(Guid playerId, EmailAddress newEmailAddress)
        {
            //TODO: Implement validaiton when merged!
            var player = this.FindById(playerId);
            player.ContactInformation.Email = newEmailAddress;
        }

        public void SetPhoneNumber(Guid playerId, PhoneNumber newPhoneNumber)
        {
            //TODO: Implement validaiton when merged!
            var player = this.FindById(playerId);
            player.ContactInformation.Phone = newPhoneNumber;
        }

        public void SetPlayerPosition(Guid playerId, PlayerPosition newPosition)
        {
            var player = this.FindById(playerId);
            player.Position = newPosition;
        }

        public void SetPlayerStatus(Guid playerId, PlayerStatus newStatus)
        {
            var player = this.FindById(playerId);
            player.Status = newStatus;
        }
    }
}