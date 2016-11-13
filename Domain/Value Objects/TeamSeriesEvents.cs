using Domain.Entities;
using Domain.Services;
using System;
using System.Collections.Generic;

namespace Domain.Value_Objects
{
    public class TeamSeriesEvents
    {
        private Guid seriesId;
        public Guid teamId;
        private HashSet<Guid> gameEventIds;
        private List<Goal> goalEvents;

        public string TeamName { get { return DomainService.FindTeamById(this.teamId).Name.ToString(); } }

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

        public TeamSeriesEvents(Guid teamId, Guid seriesId)
        {
            this.seriesId = seriesId;
            this.teamId = teamId;
            this.gameEventIds = new HashSet<Guid>();
            this.goalEvents = new List<Goal>();
        }

        internal void AddGameId(Guid gameId)
        {
            this.gameEventIds.Add(gameId);
        }

        internal void RemoveGameId(Guid gameId)
        {
            this.gameEventIds.Remove(gameId);
        }

        internal void AddGoal(Goal goal)
        {
            this.goalEvents.Add(goal);
        }

        internal void RemoveGoal(Goal goal)
        {
            this.goalEvents.Remove(goal);
        }
    }
}