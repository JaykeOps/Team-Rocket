﻿using Domain.Entities;
using Domain.Services;
using Domain.Value_Objects;
using DomainTests.Test_Dummies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainTests.Services
{
    [TestClass]
    public class SeriesServicesTests
    {
        private SeriesService seriesService;
        private Series testSerieOne;

        public SeriesServicesTests()
        {
            this.seriesService = new SeriesService();
            this.testSerieOne = new Series(new MatchDuration(new TimeSpan(45 * 6000000000 / 10)), new NumberOfTeams(4), new SeriesName("Allsvenskan"));
            this.testSerieOne.TeamIds.Add(Guid.NewGuid());
            this.testSerieOne.TeamIds.Add(Guid.NewGuid());
            this.testSerieOne.TeamIds.Add(Guid.NewGuid());
            this.testSerieOne.TeamIds.Add(Guid.NewGuid());
        }

        [TestMethod]
        public void AddSeries()
        {
            Assert.IsFalse(this.seriesService.GetAll().Contains(this.testSerieOne));
            this.seriesService.Add(this.testSerieOne);
            Assert.IsTrue(this.seriesService.GetAll().Contains(this.testSerieOne));
        }

        [TestMethod]
        public void GetAllSeriesesIsReturningIEnumerable()
        {
            Assert.IsInstanceOfType(this.seriesService.GetAll(), typeof(IEnumerable<Series>));
        }

        [TestMethod]
        public void FindSeriesByIdIsWorking()
        {
            Assert.IsFalse(this.seriesService.FindById(this.testSerieOne.Id) == this.testSerieOne);
            this.seriesService.Add(this.testSerieOne);
            Assert.IsTrue(this.seriesService.FindById(this.testSerieOne.Id) == this.testSerieOne);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SeriesCanOnlyBeAddedToDbIfTeamIdsCountIsEqualToNumberOfTeams()
        {
            this.testSerieOne.TeamIds.Add(Guid.NewGuid());
            this.seriesService.Add(this.testSerieOne);
        }

        [TestMethod]
        public void GetLeagueTablePlacementIsWorking()
        {
            var series = new DummySeries();
            var teamIds = series.SeriesDummy.TeamIds;
            var teamsOfSerie = new List<Team>();
            foreach (var teamId in teamIds)
            {
                teamsOfSerie.Add(DomainService.FindTeamById(teamId));
            }
            var orderedTeamList = teamsOfSerie.OrderByDescending(x => x.PresentableSeriesStats[series.SeriesDummy.Id].Points)
                    .ThenByDescending(x => x.PresentableSeriesStats[series.SeriesDummy.Id].GoalDifference)
                    .ThenByDescending(x => x.PresentableSeriesStats[series.SeriesDummy.Id].GoalsFor);

            var leagueTable = seriesService.GetLeagueTablePlacement(series.SeriesDummy.Id);

            for (int i = 0; i < orderedTeamList.Count(); i++)
            {
                Assert.IsTrue(orderedTeamList.ElementAt(i).Name.Value == leagueTable.ElementAt(i).TeamName);
            }
        }

        [TestMethod]
        public void AddTeamToSeriesIsWorking()
        {
            var seriesDummyTeams = new DummySeries().SeriesDummy.TeamIds;

            var series = new Series
              (
              new MatchDuration(new TimeSpan(0, 90, 0)),
              new NumberOfTeams(4),
              new SeriesName("The Dummy Series")
              );
           
            series.TeamIds.Add(seriesDummyTeams.ElementAt(0));
            series.TeamIds.Add(seriesDummyTeams.ElementAt(1));
            series.TeamIds.Add(seriesDummyTeams.ElementAt(2));
            series.TeamIds.Add(seriesDummyTeams.ElementAt(3));

            seriesService.Add(series);
            seriesService.ScheduleGenerator(series.Id);
            
            Assert.IsTrue(series.TeamIds.Contains(seriesDummyTeams.ElementAt(0))
                && series.TeamIds.Contains(seriesDummyTeams.ElementAt(1))
                && series.TeamIds.Contains(seriesDummyTeams.ElementAt(2))
                && series.TeamIds.Contains(seriesDummyTeams.ElementAt(3)));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddTeamToSeriesCanNotAddSameTeamMultipleTimes()
        {
            var seriesDummyTeams = new DummySeries().SeriesDummy.TeamIds;

            var series = new Series
              (
              new MatchDuration(new TimeSpan(0, 90, 0)),
              new NumberOfTeams(4),
              new SeriesName("The Dummy Series")
              );

            series.TeamIds.Add(seriesDummyTeams.ElementAt(0));
            series.TeamIds.Add(seriesDummyTeams.ElementAt(1));
            series.TeamIds.Add(seriesDummyTeams.ElementAt(2));
            series.TeamIds.Add(seriesDummyTeams.ElementAt(3));

            seriesService.Add(series);
            seriesService.ScheduleGenerator(series.Id);

            seriesService.AddTeamToSeries(series.Id, seriesDummyTeams.ElementAt(0));
            seriesService.AddTeamToSeries(series.Id, seriesDummyTeams.ElementAt(1));
            seriesService.AddTeamToSeries(series.Id, seriesDummyTeams.ElementAt(2));
            seriesService.AddTeamToSeries(series.Id, seriesDummyTeams.ElementAt(3));
            seriesService.AddTeamToSeries(series.Id, seriesDummyTeams.ElementAt(0));
            seriesService.AddTeamToSeries(series.Id, seriesDummyTeams.ElementAt(1));
            seriesService.AddTeamToSeries(series.Id, seriesDummyTeams.ElementAt(2));
            seriesService.AddTeamToSeries(series.Id, seriesDummyTeams.ElementAt(3));

            Assert.IsTrue(series.TeamIds.Contains(seriesDummyTeams.ElementAt(0))
                && series.TeamIds.Contains(seriesDummyTeams.ElementAt(1))
                && series.TeamIds.Contains(seriesDummyTeams.ElementAt(2))
                && series.TeamIds.Contains(seriesDummyTeams.ElementAt(3)));
        }

        [TestMethod]
        public void RemoveTeamFromSeriesIsWorking()
        {
            var seriesToEdit = new DummySeries();
            var uneditedSeries = new DummySeries();
            var teamToRemove = seriesToEdit.SeriesDummy.TeamIds.ElementAt(0);
            seriesService.RemoveTeamFromSeries(seriesToEdit.SeriesDummy.Id, teamToRemove);
            Assert.IsTrue(seriesToEdit.SeriesDummy.TeamIds.Count != uneditedSeries.SeriesDummy.TeamIds.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveTeamFromSeriesCanNotRemoveTeamThatIsNotInSeries()
        {
            var series = new Series(new MatchDuration(new TimeSpan(0, 90, 0)), new NumberOfTeams(4), new SeriesName("test"));
            series.TeamIds.Add(DomainService.GetAllTeams().ElementAt(0).Id);
            series.TeamIds.Add(DomainService.GetAllTeams().ElementAt(1).Id);
            series.TeamIds.Add(DomainService.GetAllTeams().ElementAt(2).Id);
            series.TeamIds.Add(DomainService.GetAllTeams().ElementAt(3).Id);
            seriesService.Add(series);
            seriesService.ScheduleGenerator(series.Id);
            Assert.IsTrue(seriesService.GetAll().Contains(series));
            seriesService.DeleteSeries(series.Id);
            Assert.IsFalse(seriesService.GetAll().Contains(series));
            var seriesToEdit = new DummySeries();
            var uneditedSeries = new DummySeries();
            var teamToRemove = seriesToEdit.SeriesDummy.TeamIds.ElementAt(0);
            seriesService.RemoveTeamFromSeries(seriesToEdit.SeriesDummy.Id, teamToRemove);
            seriesService.RemoveTeamFromSeries(seriesToEdit.SeriesDummy.Id, teamToRemove);
        }
    }
}