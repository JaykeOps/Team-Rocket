using Domain.Interfaces;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public class PlayerStats : ValueObject<PlayerStats>, ICountablePlayerStats, IFullPlayerStats
    {
        private List<Goal> goalStats;
        private List<Assist> assistStats;
        private List<Card> cardStats;
        private List<Penalty> penaltyStats;
        private List<Guid> gamesPlayedIds;
        public List<Goal> GoalStats { get { return this.goalStats; } }
        public List<Assist> AssistStats { get { return this.assistStats; } }
        public List<Card> CardStats { get { return this.cardStats; } }
        public List<Penalty> PenaltyStats { get { return this.penaltyStats; } }
        public List<Guid> GamesPlayedIds { get { return this.gamesPlayedIds; } }

        public int GoalCount { get { return this.goalStats.Count; } }
        public int AssistCount { get { return this.assistStats.Count; } }
        public int YellowCardCount
        {
            get { return this.cardStats.FindAll(x => x.CardType.Equals(CardType.Yellow)).Count; }
        }
        public int RedCardCount
        {
            get { return this.cardStats.FindAll(x => x.CardType.Equals(CardType.Red)).Count; }
        }
        public int PenaltyCount { get { return this.penaltyStats.Count; } }
        public int GamesPlayedCount { get { return this.gamesPlayedIds.Count; } }

        public PlayerStats()
        {
            this.goalStats = new List<Goal>();
            this.assistStats = new List<Assist>();
            this.cardStats = new List<Card>();
            this.penaltyStats = new List<Penalty>();
            this.gamesPlayedIds = new List<Guid>();
        }
    }
}