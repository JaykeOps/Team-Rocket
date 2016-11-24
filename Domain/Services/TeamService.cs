using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Value_Objects;

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
        public TeamStats GetTeamStatsInSeries(Guid seriesId, Guid teamId)
        {
            return GetAll().ToList().Find(t => t.Id == teamId).PresentableSeriesStats[seriesId];
        }

        public IEnumerable<Team> GetTeamsOfSerie(Guid sereisId)
        {
            return repository.GetTeamsOfSerie(sereisId);
        }
    }
}