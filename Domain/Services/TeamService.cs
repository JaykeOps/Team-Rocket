using Domain.Entities;
using Domain.Helper_Classes;
using Domain.Interfaces;
using Domain.Repositories;
using Domain.Value_Objects;
using System;
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

        public void Add(Team team)
        {
            if (team.IsTeamValid())
            {
                this.repository.Add(team);
                team.UpdatePlayerIds();
            }
            else
            {
            }
        }

        public void Add(IExposableTeam team)
        {
            if (team.IsTeamValid())
            {
                this.repository.Add((Team)team);
            }
            else
            {
            }
        }

        public void Add(IEnumerable<Team> teams)
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

        public void Add(IEnumerable<IExposableTeam> teams)
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

        public IEnumerable<IExposableTeam> GetAllIExposableTeams()
        {
            return TeamRepository.instance.GetAll();
        }

        public IEnumerable<Team> GetAllTeams()
        {
            return TeamRepository.instance.GetAll();
        }

        public Team FindTeamById(Guid teamId)
        {
            return this.GetAllTeams().ToList().Find(t => t.Id.Equals(teamId));
        }

        public IExposableTeam FindIExposableTeamById(Guid teamId)
        {
            return this.GetAllTeams().ToList().Find(t => t.Id.Equals(teamId));
        }

        public TeamStats GetTeamStatsInSeries(Guid seriesId, Guid teamId)
        {
            return this.GetAllTeams().ToList().Find(t => t.Id == teamId).AggregatedStats[seriesId];
        }

        public IEnumerable<IExposableTeam> Search(string searchText, StringComparison comparison
            = StringComparison.InvariantCultureIgnoreCase)
        {
            return this.GetAllTeams().Where(x =>
            x.Name.ToString().Contains(searchText, comparison)
            || x.ArenaName.ToString().Contains(searchText, comparison)
            || x.Email.Value.Contains(searchText, comparison)
            || x.PlayerIds.Any(y => y != Guid.Empty
            && DomainService.FindPlayerById(y).Name.ToString().Contains(searchText, comparison)));
        }

        public TeamEvents GetTeamEventsInSeries(Guid teamId, Guid seriesId)
        {
            var team = this.FindTeamById(teamId);
            return team.AggregatedEvents[seriesId];
        }

        public TeamStats GetTeamStatsInseries(Guid teamId, Guid seriesId)
        {
            var team = this.FindTeamById(teamId);
            return team.AggregatedStats[seriesId];
        }

        public IEnumerable<Match> GetTeamSchedule(Guid teamId, Guid seriesId)
        {
            var team = this.FindTeamById(teamId);
            return team.MatchSchedules[seriesId];
        }

        public IEnumerable<IExposableTeam> GetTeamsOfSerie(Guid sereisId)
        {
            var series = DomainService.FindSeriesById(sereisId);
            var teamsOfSerie = series.TeamIds;

            return teamsOfSerie.Select(teamId => DomainService.FindTeamById(teamId)).ToList();
        }

        public IEnumerable<TeamStats> TeamStatsSearch(string searchText)
        {
            var teams = (IEnumerable<Team>)this.Search(searchText);
            return teams.SelectMany(x => x.AggregatedStats.AllStats.Values);
        }

        public void RemoveTeam(Guid teamId)
        {
            this.repository.RemoveTeam(teamId);
        }
        internal void Save()
        {
            repository.SaveData();
        }
    }
}