using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services
{
    public class TeamService
    {
        private readonly TeamRepository repository = TeamRepository.instance;
        private readonly IEnumerable<Team> allTeams;

        public TeamService()
        {
            this.allTeams = repository.GetAll();
        }

        public void AddTeam(Team team)
        {
            TeamRepository.instance.Add(team);
        }

        public IEnumerable<Team> GetAll()
        {
            return TeamRepository.instance.GetAll();
        }

        public Team FindById(Guid teamId)
        {
            return this.GetAll().ToList().Find(t => t.Id.Equals(teamId));
        }
    }
}