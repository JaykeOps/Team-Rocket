using Domain.Services;
using Domain.Value_Objects;
using System;
using System.Linq;

namespace Domain.Entities
{
    [Serializable]
    public class PlayerStats : ValueObject<PlayerStats>, ICloneable
    {
        private readonly Guid seriesId;
        private readonly Guid playerId;
        private readonly Guid teamId;
        private string playerName;
        private string teamName;

        private PlayerEvents seriesEvents;

        public int Ranking { get; set; }
        public string PlayerName => this.playerName;
        public string TeamName => this.teamName;
        public int GoalCount => this.seriesEvents.Goals.Count();
        public int AssistCount => this.seriesEvents.Assists.Count();

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

        public int PenaltyCount => this.seriesEvents.Penalties.Count();
        public int GamesPlayedCount => this.seriesEvents.Games.Count();

        public void UpdateSeriesEvents()
        {
            var player = DomainService.FindPlayerById(this.playerId);
            this.seriesEvents = player.AggregatedEvents[this.seriesId];
        }

        private void UpdatePlayerName()
        {
            this.playerName = DomainService.FindPlayerById(this.playerId).Name.ToString();
        }

        private void UpdateTeamName()
        {
            this.teamName = DomainService.FindTeamById(this.teamId).Name.ToString();
        }

        public void UpdateAllStats()
        {
            this.UpdatePlayerName();
            this.UpdateTeamName();
            this.UpdateSeriesEvents();
        }

        public PlayerStats(Guid seriesId, Guid teamId, Player player)
        {
            this.seriesId = seriesId;
            this.playerId = player.Id;
            this.teamId = teamId;
            this.seriesEvents = player.AggregatedEvents[this.seriesId];
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}