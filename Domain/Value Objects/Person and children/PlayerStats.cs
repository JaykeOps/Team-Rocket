using Domain.Services;
using Domain.Value_Objects;
using System;
using System.Linq;

namespace Domain.Entities
{
    [Serializable]
    public class PlayerStats : ValueObject<PlayerStats>
    {
        private readonly Guid seriesId;
        private readonly Guid playerId;
        private readonly Guid teamId;
        private PlayerEvents seriesEvents;

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

        public int GoalCount
        {
            get
            {
                return this.seriesEvents.Goals.Count();
            }
        }

        public int AssistCount
        {
            get
            {
                return this.seriesEvents.Assists.Count();
            }
        }

        public int YellowCardCount
        {
            get
            {
                var cards = this.seriesEvents.Cards;
                return cards.Count(x => x.CardType.Equals(CardType.Yellow));
            }
        }

        public int RedCardCount
        {
            get
            {
                var cards = this.seriesEvents.Cards;
                return cards.Count(x => x.CardType.Equals(CardType.Red));
            }
        }

        public int PenaltyCount
        {
            get
            {
                return this.seriesEvents.Penalties.Count();
            }
        }

        public int GamesPlayedCount
        {
            get
            {
                return this.seriesEvents.Games.Count();
            }
        }

        public void UpdateSeriesEvents()
        {
            var player = DomainService.FindPlayerById(this.playerId);
            this.seriesEvents = player.AggregatedEvents[this.seriesId];
        }

        public PlayerStats(Guid seriesId, Guid teamId, Player player)
        {
            this.seriesId = seriesId;
            this.playerId = player.Id;
            this.teamId = teamId;
            this.seriesEvents = player.AggregatedEvents[this.seriesId];
        }
    }
}