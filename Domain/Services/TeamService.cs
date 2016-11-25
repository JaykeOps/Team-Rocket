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

        public void Add(Team team)
        {
            TeamRepository.instance.Add(team);
        }

        public void AddTeam(IEnumerable<Team> teams)
        {
            if (teams != null)
            {
                foreach (var team in teams)
                {
                    Add(team);
                }
            }
            else
            {
                throw new NullReferenceException("List of teams is null");
            }
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
    }
}