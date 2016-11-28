using Domain.Entities;
using Domain.Helper_Classes;
using Domain.Repositories;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            return this.GetAll().ToList().Find(t => t.Id == teamId).AggregatedTeamStats[seriesId];
        }

        public IEnumerable<Team> Search(string searchText, StringComparison comparison 
            = StringComparison.InvariantCultureIgnoreCase)
        {
            return this.GetAll().Where(x => x.Name.ToString().Contains(searchText, comparison)
            || x.ArenaName.ToString().Contains(searchText, comparison)
            || x.Email.ToString().Contains(searchText, comparison)
            || x.playerIds.Any(y => DomainService.FindPlayerById(y).Name.ToString().Contains(searchText, comparison)));
        }

        public TeamEvents GetTeamEventsInSeries(Guid teamId, Guid seriesId)
        {
            var team = this.FindById(teamId);
            return team.PresentableSeriesEvents[seriesId];
        }

        public TeamStats GetTeamStatsInseries(Guid teamId, Guid seriesId)
        {
            var team = this.FindById(teamId);
            return team.AggregatedTeamStats[seriesId];
        }

        public IEnumerable<Match> GetTeamSchedule(Guid teamId, Guid seriesId)
        {
            var team = this.FindById(teamId);
            return team.MatchSchedules[seriesId];
        } 
            

        public IEnumerable<Team> GetTeamsOfSerie(Guid sereisId)
        {
            var series = DomainService.FindSeriesById(sereisId);
            var teamsOfSerie = series.TeamIds;

            return teamsOfSerie.Select(teamId => DomainService.FindTeamById(teamId)).ToList();
        }
    }
}