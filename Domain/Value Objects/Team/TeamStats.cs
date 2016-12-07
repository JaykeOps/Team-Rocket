using Domain.Services;
using System;
using System.Linq;

namespace Domain.Value_Objects
{
    [Serializable]
    public class TeamStats
    {
        private Guid seriesId;
        private Guid teamId;
        private TeamEvents teamEvents;
        private string teamName;
        private int wins;
        private int losses;
        private int draws;

        public string TeamName => this.teamName;
        public int GamesPlayed => this.teamEvents.Games.Count();
        public int Wins => this.wins;
        public int Losses => this.losses;
        public int Draws => this.draws;
        public int GoalDifference => this.GoalsFor - this.GoalsAgainst;
        public int Points => this.Wins * 3 + this.Draws;
        public int Ranking { get; set; }

        public int GoalsFor
        {
            get
            {
                return this.teamEvents.Goals.Count(x => x.TeamId == this.teamId);
            }
        }

        public int GoalsAgainst
        {
            get
            {
                return this.teamEvents.Goals.Count(x => x.TeamId != this.teamId);
            }
        }

        private int CalculateAllMatchOutComes(MatchOutcome matchOutcomeToTrack)
        {
            int outcome = 0;
            foreach (var game in this.teamEvents.Games)
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

        private void UpdateTeamName()
        {
            this.teamName = DomainService.FindTeamById(this.teamId).Name.ToString();
        }

        private void UpdateSeriesEvents()
        {
            var thisTeam = DomainService.FindTeamById(this.teamId);
            this.teamEvents = thisTeam.AggregatedEvents[this.seriesId];
        }

        private void UpdateWins()
        {
            this.wins = this.CalculateAllMatchOutComes(MatchOutcome.Win);
        }

        private void UpdateLosses()
        {
            this.losses = this.CalculateAllMatchOutComes(MatchOutcome.Loss);
        }

        private void UpdateDraws()
        {
            this.draws = this.CalculateAllMatchOutComes(MatchOutcome.Draw);
        }

        public void UpdateAllStats()
        {
            this.UpdateSeriesEvents();
            this.UpdateTeamName();
            this.UpdateWins();
            this.UpdateLosses();
            this.UpdateDraws();
        }

        public TeamStats(Guid seriesId, Guid teamId)
        {
            this.seriesId = seriesId;
            this.teamId = teamId;
        }
    }
}