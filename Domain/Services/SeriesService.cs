using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services
{
    public class SeriesService
    {
        private readonly SeriesRepository repository = SeriesRepository.instance;

        public void AddSeries(Series series)
        {
            if (series.NumberOfTeams.Value == series.TeamIds.Count)
            {
                this.repository.AddSeries(series);
            }
            else
            {
                throw new ArgumentException($"Invalid numbers of teams. Number of teamIds in HashSet TeamIds must be {series.NumberOfTeams}");
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

        public IOrderedEnumerable<Team> GetLeagueTablePlacement(Guid seriesId)
        {
            Series series = this.FindById(seriesId);
            HashSet<Guid> teamIdsOfSerie = series.TeamIds;
            HashSet<Team> teamsOfSerie = new HashSet<Team>();

            foreach (var teamId in teamIdsOfSerie)
            {
                teamsOfSerie.Add(DomainService.FindTeamById(teamId));
            }
            return teamsOfSerie.OrderByDescending(x => x.PresentableSeriesStats[series.Id].Points)
                    .ThenByDescending(x => x.PresentableSeriesStats[series.Id].GoalDifference)
                    .ThenByDescending(x => x.PresentableSeriesStats[series.Id].GoalsFor);
        }
    }
}