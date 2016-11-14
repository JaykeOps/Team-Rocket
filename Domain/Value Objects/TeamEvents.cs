using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using System;
using System.Collections.Generic;

namespace Domain.Value_Objects
{
    public class TeamEvents : IPresentableTeamEvents
    {
        private Guid seriesId;
        public Guid teamId;
        private HashSet<Guid> gameEventIds;
        private List<Goal> goalEvents;

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
                var games = new List<Game>();
                foreach (var gameId in this.gameEventIds)
                {
                    games.Add(DomainService.FindGameById(gameId));
                }
                return games;
            }
        }

        public IEnumerable<Goal> Goals { get { return this.goalEvents; } }

        public TeamEvents(Guid seriesId, Guid teamId)
        {
            this.seriesId = seriesId;
            this.teamId = teamId;
            this.gameEventIds = new HashSet<Guid>();
            this.goalEvents = new List<Goal>();
        }

        public void AddGameId(Game game)
        {
            this.gameEventIds.Add(game.Id);
        }

        public void RemoveGameId(Game game)
        {
            this.gameEventIds.Remove(game.Id);
        }

        public void AddGoal(Goal goal)
        {
            this.goalEvents.Add(goal);
        }

        public void RemoveGoal(Goal goal)
        {
            this.goalEvents.Remove(goal);
        }
    }
}