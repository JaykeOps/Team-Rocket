using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Value_Objects
{
    public class TeamEvents
    {
        public Guid teamId;
        private Guid seriesId;

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
                return DomainService.GetTeamsGamesInSeries(this.teamId, this.seriesId);

            }
        }

        public IEnumerable<Goal> Goals
        {
            get
            {
                return DomainService.GetAllTeamsGoalsInSeries(this.teamId,this.seriesId); 
                
            }
        }

        public TeamEvents(Guid seriesId, Guid teamId)
        {
            this.teamId = teamId;
            this.seriesId = seriesId;
        }
    }
}