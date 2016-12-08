using Domain.Entities;
using Domain.Helper_Classes;
using Domain.Repositories;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services
{
    public class SeriesService
    {
        private readonly SeriesRepository repository = SeriesRepository.instance;

        public void Add(Series series)
        {
            if (series.NumberOfTeams.Value == series.TeamIds.Count && series.IsSeriesValid())
            {
                this.repository.AddSeries(series);
            }
            else
            {
                throw new ArgumentException("Series cannot be added. Invalid seriesdata");
            }
        }

        public void ScheduleGenerator(Guid seriesId)
        {
            var schedule = new Schedule();
            var series = DomainService.FindSeriesById(seriesId);
            schedule.GenerateSchedule(series);
            DomainService.AddMatches(series.Schedule);
            repository.SaveData();
        }

        public IEnumerable<Series> GetAll()
        {
            return this.repository.GetAll();
        }

        public Series FindById(Guid seriesId)
        {
            var allSeries = this.GetAll();
            return allSeries.ToList().Find(s => s.Id.Equals(seriesId));
        }

        public IEnumerable<TeamStats> GetLeagueTablePlacement(Guid seriesId)
        {
            var series = this.FindById(seriesId);
            var teamIdsOfSerie = series.TeamIds;
            var teamsOfSerie = teamIdsOfSerie.Select(teamId => DomainService.FindTeamById(teamId)).ToList();
            var teamStats = new List<TeamStats>();
            foreach (var team in teamsOfSerie)
            {
                try
                {
                    teamStats.Add(team.AggregatedStats[seriesId]);
                }
                catch (SeriesMissingException)
                {
                }
            }

            var orderTeamStats = teamStats.OrderByDescending(x => x.Points)
                .ThenByDescending(x => x.GoalDifference)
               .ThenByDescending(x => x.GoalsFor).ToList();

            for (int i = 0; i < orderTeamStats.Count; i++)
            {
                var teamStat = orderTeamStats.ElementAt(i);
                teamStat.Ranking = i + 1;
            }
            return orderTeamStats;
        }

        public void DeleteSeries(Guid seriesId)
        {
            DomainService.RemoveGameAndMatchesFromSeries(seriesId);
            this.repository.DeleteSeries(seriesId);
            DomainService.SaveAll();
        }

        public void AddTeamToSeries(Guid seriesId, Guid teamId)
        {
            var series = this.FindById(seriesId);
            if (!(series.TeamIds.Contains(teamId)))
            {
                series.TeamIds.Add(teamId);
                var team = DomainService.FindTeamById(teamId);
                team.AddSeries(series);
                DomainService.AddSeriesToPlayers(series, team);
            }
            else
            {
                throw new ArgumentException($"Series already contains team {DomainService.FindTeamById(teamId)}");
            }
        }
        

        public void AddTeamToSeries(Series seriesToAdd, Guid teamId)
        {
            var series = seriesToAdd;
            if (!(series.TeamIds.Contains(teamId)))
            {
                series.TeamIds.Add(teamId);
                var team = DomainService.FindTeamById(teamId);
                team.AddSeries(series);
                team.UpdatePlayerIds();
                DomainService.AddSeriesToPlayers(series, team);
            }
            else
            {
                throw new ArgumentException($"Series already contains team {DomainService.FindTeamById(teamId)}");
            }
        }

        public IEnumerable<Series> Search(string searchText, StringComparison comparison
            = StringComparison.InvariantCultureIgnoreCase)
        {
            return this.GetAll().Where(x => x.TeamIds.Any
            (y => y != Guid.Empty && DomainService.FindTeamById(y).Name.ToString().Contains(searchText, comparison)
            || x.SeriesName.ToString().Contains(searchText, comparison)));
        }

        public void RemoveTeamFromSeries(Guid seriesId, Guid teamId)
        {
            var series = DomainService.FindSeriesById(seriesId);
            if (series.TeamIds.Contains(teamId))
            {
                series.TeamIds.Remove(teamId);
            }
            else
            {
                throw new ArgumentException($"Can not remove team \"{DomainService.FindTeamById(teamId)}\"."
                + $" Team doesn't exist in series \"{DomainService.FindSeriesById(seriesId)}\".");
            }
        }
        internal void Save()
        {
            repository.SaveData();
        }
    }
}