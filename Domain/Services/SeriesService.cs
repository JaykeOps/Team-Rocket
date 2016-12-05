﻿using Domain.Entities;
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
            var teamStats = teamsOfSerie.Select(team => team.AggregatedStats[series.Id]).ToList();

             teamStats.OrderByDescending(x => x.Points)
                .ThenByDescending(x => x.GoalDifference)
                .ThenByDescending(x => x.GoalsFor);

            for (int i = 0; i < teamStats.Count; i++)
            {
                var teamStat = teamStats.ElementAt(i);
                teamStat.Ranking = i + 1;
            }
            return teamStats;
        }

        public void DeleteSeries(Guid seriesId)
        {
            this.repository.DeleteSeries(seriesId);
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

        public IEnumerable<Series> Search(string searchText, StringComparison comparison
            = StringComparison.InvariantCultureIgnoreCase)
        {
            return this.GetAll().Where(x => x.TeamIds.Any
            (y => DomainService.FindTeamById(y).Name.ToString().Contains(searchText, comparison)
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
    }
}