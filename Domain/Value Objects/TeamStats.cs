using Domain.Services;
using System;
using System.Collections.Generic;

namespace Domain.Value_Objects
{
    public class TeamStats
    {
        private HashSet<Guid> gameEvents;
        private List<Goal> goalEvents;

        public Guid TeamId { get; }

        public HashSet<Guid> GameEvents { get { return this.gameEvents; } }
        public int GamesPlayed { get { return this.gameEvents.Count; } }

        public int Wins { get { return this.CalculateAllMatchOutComes(MatchOutcome.Win); } }

        public int Losses { get { return this.CalculateAllMatchOutComes(MatchOutcome.Loss); } }
        public int Draws { get { return this.CalculateAllMatchOutComes(MatchOutcome.Draw); } }

        public List<Goal> Goals { get { return this.goalEvents; } }

        public int GoalsFor { get { return this.goalEvents.FindAll(x => x.TeamId == this.TeamId).Count; } }

        public int GoalsAgainst { get { return this.goalEvents.FindAll(x => x.TeamId != this.TeamId).Count; } }

        public int GoalDifference { get { return this.GoalsFor - this.GoalsAgainst; } }

        public int Points { get { return (this.Wins * 3) + this.Draws; } }

        private int CalculateAllMatchOutComes(MatchOutcome matchOutcomeToTrack)
        {
            int outcome = 0;
            foreach (var gameId in this.gameEvents)
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

        public TeamStats(Guid teamId)
        {
            
            this.TeamId = teamId;
            this.gameEvents = new HashSet<Guid>();
            this.goalEvents = new List<Goal>();
        }
    }
}