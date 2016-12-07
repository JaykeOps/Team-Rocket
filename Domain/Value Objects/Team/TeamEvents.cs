using Domain.Entities;
using Domain.Services;
using System;
using System.Collections.Generic;

namespace Domain.Value_Objects
{
    [Serializable]
    public class TeamEvents
    {
        public Guid TeamId { get; }
        private Guid seriesId;

        private string teamName;
        private IEnumerable<Game> games;
        private IEnumerable<Goal> goals;

        public string TeamName => this.teamName;
        public IEnumerable<Game> Games => this.games;
        public IEnumerable<Goal> Goals => this.goals;

        private void UpdateTeamName()
        {
            this.teamName = DomainService.FindTeamById(this.TeamId).Name.ToString();
        }

        private void UpdateGames()
        {
            this.games = DomainService.GetTeamsGamesInSeries(this.TeamId, this.seriesId);
        }

        private void UpdateGoals()
        {
            this.goals = DomainService.GetAllTeamsGoalsForAndAgainstInSeries(this.TeamId, this.seriesId);
        }

        public void UpdateAllEvents()
        {
            this.UpdateTeamName();
            this.UpdateGames();
            this.UpdateGoals();
        }

        public TeamEvents(Guid seriesId, Guid teamId)
        {
            this.TeamId = teamId;
            this.seriesId = seriesId;
        }
    }
}