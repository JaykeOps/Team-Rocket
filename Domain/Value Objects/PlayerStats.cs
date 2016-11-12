using Domain.Interfaces;
using Domain.Services;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class PlayerStats : ValueObject<PlayerStats>, IPresentablePlayerStats, IPresentablePlayerEvents
    {
        private Guid playerId;
        private List<Goal> goalEvents;
        private List<Assist> assistEvents;
        private List<Card> cardEvents;
        private List<Penalty> penaltyEvents;
        private List<Guid> gameEventIds;

        public string PlayerName { get { return DomainService.FindPlayerById(this.playerId).Name.ToString(); } }

        public IEnumerable<Game> Games
        {
            get
            {
                var games = new List<Game>();
                foreach (var gameId in this.gameEventIds)
                {
                    games.Add(DomainService.FindGameById(gameId));
                }
                return games;
            }
        }

        public IEnumerable<Goal> Goals { get { return this.goalEvents; } }
        public IEnumerable<Assist> Assists { get { return this.assistEvents; } }
        public IEnumerable<Card> Cards { get { return this.cardEvents; } }
        public IEnumerable<Penalty> Penalties { get { return this.penaltyEvents; } }

        public int GoalCount { get { return this.goalEvents.Count; } }
        public int AssistCount { get { return this.assistEvents.Count; } }

        public int YellowCardCount
        {
            get { return this.cardEvents.FindAll(x => x.CardType.Equals(CardType.Yellow)).Count; }
        }

        public int RedCardCount
        {
            get { return this.cardEvents.FindAll(x => x.CardType.Equals(CardType.Red)).Count; }
        }

        public int PenaltyCount { get { return this.penaltyEvents.Count; } }
        public int GamesPlayedCount { get { return this.gameEventIds.Count; } }

        public PlayerStats(Guid playerId)
        {
            this.goalEvents = new List<Goal>();
            this.assistEvents = new List<Assist>();
            this.cardEvents = new List<Card>();
            this.penaltyEvents = new List<Penalty>();
            this.gameEventIds = new List<Guid>();
            this.playerId = playerId;
        }

        public void AddGoal(Goal goal)
        {
            this.goalEvents.Add(goal);
        }

        public void AddAssist(Assist assist)
        {
            this.assistEvents.Add(assist);
        }

        public void AddCard(Card card)
        {
            this.cardEvents.Add(card);
        }

        public void AddPenalty(Penalty penalty)
        {
            this.penaltyEvents.Add(penalty);
        }

        public void AddGameId(Guid gameId)
        {
            this.gameEventIds.Add(gameId);
        }
    }
}