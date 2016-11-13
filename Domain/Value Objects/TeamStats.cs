using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using System;
using System.Collections.Generic;

namespace Domain.Value_Objects
{
    public class TeamStats : IPresentableTeamStats, IPresentableTeamEvents
    {
        public Guid TeamId { get; }
        private HashSet<Guid> gameEventIds;
        private List<Goal> goalEvents;

        public string TeamName { get { return DomainService.FindTeamById(this.TeamId).Name.ToString(); } }
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
        public int GamesPlayed { get { return this.gameEventIds.Count; } }

        public int Wins { get { return this.CalculateAllMatchOutComes(MatchOutcome.Win); } }

        public int Losses { get { return this.CalculateAllMatchOutComes(MatchOutcome.Loss); } }
        public int Draws { get { return this.CalculateAllMatchOutComes(MatchOutcome.Draw); } }

        public int GoalsFor { get { return this.goalEvents.FindAll(x => x.TeamId == this.TeamId).Count; } }

        public int GoalsAgainst { get { return this.goalEvents.FindAll(x => x.TeamId != this.TeamId).Count; } }

        public int GoalDifference { get { return this.GoalsFor - this.GoalsAgainst; } }

        public int Points { get { return (this.Wins * 3) + this.Draws; } }

        public TeamStats(Guid teamId)
        {
            this.TeamId = teamId;
            this.gameEventIds = new HashSet<Guid>();
            this.goalEvents = new List<Goal>();
        }

        private int CalculateAllMatchOutComes(MatchOutcome matchOutcomeToTrack)
        {
            int outcome = 0;
            foreach (var gameId in this.gameEventIds)
            {
                int gameGoalDifference = 0;
                var game = DomainService.FindGameById(gameId);
                foreach (var goal in game.Protocol.Goals)
                {
                    gameGoalDifference += (goal.TeamId == this.TeamId) ? 1 : -1;
                }
                if (gameGoalDifference > 0 && matchOutcomeToTrack.Equals(MatchOutcome.Win))
                {
                    outcome++;
                }
                else if (gameGoalDifference < 0 && matchOutcomeToTrack.Equals(MatchOutcome.Loss))
                {
                    outcome++;
                }
                else if (gameGoalDifference == 0 && matchOutcomeToTrack.Equals(MatchOutcome.Draw))
                {
                    outcome++;
                }
            }
            return outcome;
        }

        internal void AddGameId(Guid gameId)
        {
            this.gameEventIds.Add(gameId);
        }

        internal void AddGoal(Goal goal)
        {
            this.goalEvents.Add(goal);
        }
    }
}