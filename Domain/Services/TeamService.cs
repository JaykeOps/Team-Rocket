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
            repository = TeamRepository.instance;
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
            return GetAll().ToList().Find(t => t.Id.Equals(teamId));
        }

    }
}