using System;
using Domain.Entities;
using Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services
{
    public class TeamService
    {
        private readonly TeamRepository repository;

        public TeamService()
        {
            this.repository = TeamRepository.instance;
        }

        public void AddTeam(Team team)
        {
            this.repository.Add(team);
        }

        public IEnumerable<Team> GetAll()
        {
            return this.repository.GetAll();
        }

        public Team FindById(Guid teamId)
        {
            return GetAll().ToList().Find(t => t.Id.Equals(teamId));
        }

    }
}