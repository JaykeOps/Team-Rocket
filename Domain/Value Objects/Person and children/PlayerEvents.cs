using Domain.Entities;
using Domain.Services;
using System;
using System.Collections.Generic;

namespace Domain.Value_Objects
{
    [Serializable]
    public class PlayerEvents
    {
        private Guid playerId;
        private Guid teamId;
        private Guid seriesId;

        public string PlayerName
        {
            get
            {
                return DomainService.FindPlayerById(this.playerId).Name.ToString();
            }
        }

        public string TeamName
        {
            get
            {
                return DomainService.FindTeamById(this.teamId).Name.ToString();
            }
        }

        public IEnumerable<Game> Games
        {
            get
            {
                return DomainService.GetPlayerPlayedGamesInSeries(this.playerId, this.seriesId);
            }
        }

        public IEnumerable<Goal> Goals
        {
            get
            {
                return DomainService.GetPlayersGoalsInSeries(this.playerId, this.seriesId);
            }
        }

        public IEnumerable<Assist> Assists
        {
            get
            {
                return DomainService.GetPlayerAssistInSeries(this.playerId, this.seriesId);
            }
        }

        public IEnumerable<Card> Cards
        {
            get
            {
                return DomainService.GetPlayerCardsInSeries(this.playerId, this.seriesId);
            }
        }

        public IEnumerable<Penalty> Penalties
        {
            get
            {
                return DomainService.GetPlayerPenaltiesInSeries(this.playerId, this.seriesId);
            }
        }

        public PlayerEvents(Guid seriesId, Guid teamId, Guid playerId)
        {
            this.playerId = playerId;
            this.teamId = teamId;
            this.seriesId = seriesId;
        }
    }
}