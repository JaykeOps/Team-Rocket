using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Value_Objects;

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
    }
}