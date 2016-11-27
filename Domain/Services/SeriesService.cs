using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Domain.Helper_Classes;
using Domain.Value_Objects;

namespace Domain.Services
{
    public class SeriesService
    {
        private readonly SeriesRepository repository = SeriesRepository.instance;

        public void Add(Series series)
        {
            if (series.NumberOfTeams.Value == series.TeamIds.Count&&series.IsSeriesValid())
            {
                this.repository.AddSeries(series);
            }
            else
            {
                throw new ArgumentException("Series cannot be added. Invalid seriesdata");
            }
        }

        public void Add(IEnumerable<Series> series)
        {
            if (series != null)
            {
                foreach (var serie in series)
                {
                    this.Add(serie);
                }
            }
            else
            {
                throw new NullReferenceException("List of series is null");
            }
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

        public IOrderedEnumerable<TeamStats> GetLeagueTablePlacement(Guid seriesId)
        {
            var series = FindById(seriesId);
            var teamIdsOfSerie = series.TeamIds;

            var teamsOfSerie = teamIdsOfSerie.Select(teamId => DomainService.FindTeamById(teamId)).ToList();
            var teamStats = teamsOfSerie.Select(team => team.PresentableSeriesStats[series.Id]).ToList();

            return teamStats.OrderByDescending(x => x.Points)
                .ThenByDescending(x => x.GoalDifference)
                .ThenByDescending(x => x.GoalsFor);

        }

        public IEnumerable<Series> Search(string searchText, StringComparison comparison)
        {
            return this.GetAll().Where(x => x.TeamIds.Any
            (y => DomainService.FindTeamById(y).ToString().Contains(searchText, comparison)
            || x.MatchDuration.ToString().Contains(searchText, comparison)
            || x.NumberOfTeams.ToString().Contains(searchText, comparison)
            || x.SeriesName.Contains(searchText, comparison)));
        }

    }
}