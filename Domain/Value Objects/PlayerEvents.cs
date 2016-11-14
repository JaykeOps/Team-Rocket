using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using System;
using System.Collections.Generic;

namespace Domain.Value_Objects
{
    public class PlayerEvents : IPresentablePlayerEvents
    {
        private Guid playerId;
        private Guid teamId;
        private Guid seriesId;
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

        public PlayerEvents(Guid playerId, Guid teamId, Guid seriesId)
        {
            this.goalEvents = new List<Goal>();
            this.assistEvents = new List<Assist>();
            this.cardEvents = new List<Card>();
            this.penaltyEvents = new List<Penalty>();
            this.gameEventIds = new List<Guid>();
            this.playerId = playerId;
            this.teamId = teamId;
            this.seriesId = seriesId;
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

        public void RemoveGoal(Goal goal)
        {
            this.goalEvents.Remove(goal);
        }

        public void RemoveAssist(Assist assist)
        {
            this.assistEvents.Remove(assist);
        }

        public void RemoveCard(Card card)
        {
            this.cardEvents.Remove(card);
        }

        public void RemovePenalty(Penalty penalty)
        {
            this.penaltyEvents.Remove(penalty);
        }
    }
}