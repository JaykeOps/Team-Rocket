using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Domain.Helper_Classes;
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
            if (team.IsTeamValid())
            {
                this.repository.Add(team);
            }
            else
            {
                throw new FormatException("Match cannot be added. Invalid matchdata");
            }
        }

        public void AddTeam(IEnumerable<Team> teams)
        {
            if (teams != null)
            {
                foreach (var team in teams)
                {
                    this.Add(team);
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
            return this.GetAll().ToList().Find(t => t.Id == teamId).PresentableSeriesStats[seriesId];
        }

        public IEnumerable<Team> Search(string searchText, StringComparison comparison 
            = StringComparison.InvariantCultureIgnoreCase)
        {
            return this.GetAll().Where(x => x.Name.ToString().Contains(searchText, comparison)
            || x.ArenaName.ToString().Contains(searchText, comparison)
            || x.Email.ToString().Contains(searchText, comparison)
            || x.Players.Any(y => y.Name.ToString().Contains(searchText, comparison)));
        }

        public TeamEvents GetTeamEventsInSeries(Guid teamId, Guid seriesId)
        {
            var team = this.FindById(teamId);
            return team.PresentableSeriesEvents[seriesId];
        }

        public TeamStats GetTeamStatsInseries(Guid teamId, Guid seriesId)
        {
            var team = this.FindById(teamId);
            return team.PresentableSeriesStats[seriesId];
        }

        public IEnumerable<Match> GetTeamSchedule(Guid teamId, Guid seriesId)
        {
            var team = this.FindById(teamId);
            return team.MatchSchedules[seriesId];
        } 
    }
}