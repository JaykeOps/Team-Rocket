using Domain.Services;
using System;
using System.Linq;

namespace Domain.Value_Objects
{
    public class TeamStats
    {
        private Guid seriesId;
        private Guid teamId;

        private TeamEvents LeagueEvents
        {
            get
            {
                return DomainService.FindTeamById(this.teamId).SeriesEvents[this.seriesId];
            }
        }

        public string TeamName { get { return DomainService.FindTeamById(this.teamId).Name.ToString(); } }

        public int GamesPlayed { get { return this.LeagueEvents.Goals.Count(); } }

        public int Wins { get { return this.CalculateAllMatchOutComes(MatchOutcome.Win); } }

        public int Losses { get { return this.CalculateAllMatchOutComes(MatchOutcome.Loss); } }
        public int Draws { get { return this.CalculateAllMatchOutComes(MatchOutcome.Draw); } }

        public int GoalsFor
        {
            get
            {
                return this.LeagueEvents.Goals.Where(x => x.TeamId == this.teamId).Count();
            }
        }

        public int GoalsAgainst
        {
            get
            {
                return this.LeagueEvents.Goals.Where(x => x.TeamId != this.teamId).Count();
            }
        }

        public int GoalDifference { get { return this.GoalsFor - this.GoalsAgainst; } }

        public int Points { get { return (this.Wins * 3) + this.Draws; } }

        private int CalculateAllMatchOutComes(MatchOutcome matchOutcomeToTrack)
        {
            int outcome = 0;
            foreach (var game in this.LeagueEvents.Games)
            {
                int gameGoalDifference = 0;
                foreach (var goal in game.Protocol.Goals)
                {
                    gameGoalDifference += (goal.TeamId == this.teamId) ? 1 : -1;
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

        public TeamStats(Guid teamId, Guid seriesId)
        {
            this.seriesId = seriesId;
            this.teamId = teamId;
        }
    }
}