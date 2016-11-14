using Domain.Services;
using Domain.Value_Objects;
using System;
using System.Linq;

namespace Domain.Entities
{
    public class PlayerSeriesStats : ValueObject<PlayerSeriesStats>
    {
        private Guid seriesId;
        private Guid playerId;
        private Guid teamId;

        private PlayerSeriesEvents SeriesSpecificEvents
        {
            get
            {
                var player = DomainService.FindPlayerById(this.playerId);
                return player.PlayerSeriesEvents[this.seriesId];
            }
        }

        public string PlayerName
        {
            get
            {
                return DomainService.FindPlayerById(this.playerId).Name.ToString();
            }
        }

        public int GoalCount
        {
            get
            {
                return this.SeriesSpecificEvents.Goals.Count();
            }
        }

        public int AssistCount
        {
            get
            {
                return this.SeriesSpecificEvents.Assists.Count();
            }
        }

        public int YellowCardCount
        {
            get
            {
                var cards = this.SeriesSpecificEvents.Cards;
                return cards.Where(x => x.CardType.Equals(CardType.Yellow)).Count();
            }
        }

        public int RedCardCount
        {
            get
            {
                var cards = this.SeriesSpecificEvents.Cards;
                return cards.Where(x => x.CardType.Equals(CardType.Red)).Count();
            }
        }

        public int PenaltyCount
        {
            get
            {
                return SeriesSpecificEvents.Penalties.Count();
            }
        }

        public int GamesPlayedCount
        {
            get
            {
                return SeriesSpecificEvents.Games.Count();
            }
        }

        public PlayerSeriesStats(Guid playerId, Guid teamId, Guid seriesId)
        {
            this.seriesId = seriesId;
            this.playerId = playerId;
            this.teamId = teamId;
        }
    }
}