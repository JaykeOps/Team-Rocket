using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Value_Objects
{
    public class TeamEvents : IPresentableTeamEvents
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
                return DomainService.GetAllGames().Where(game => game.HomeTeamId == this.teamId || game.AwayTeamId == this.teamId).ToList();
            }
        }

        public IEnumerable<Goal> Goals
        {
            get
            {
                var allGames = DomainService.GetAllGames();
                return
                (from game in allGames
                 from goal in game.Protocol.Goals
                 where goal.TeamId == this.teamId
                 select goal);

            }
        }

        public TeamEvents(Guid seriesId, Guid teamId)
        {
            
            this.teamId = teamId;
            this.seriesId = seriesId;

        }
    }
}