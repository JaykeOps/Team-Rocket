﻿using Domain.Entities;
using Domain.Value_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DomainTests.Entities
{
    [TestClass]
    public class SeriesTest
    {
        private Series series;
        private TimeSpan duartion = new TimeSpan(90 * 6000000000 / 10);
        private NumberOfTeams numberOfTeams= new NumberOfTeams(16);
        public SeriesTest()
        {
            this.series = new Series(new MatchDuration(duartion), numberOfTeams,"Allsvenskan");
        }

        [TestMethod]
        public void SeriesCanHoldValidEntries()
        {
            Assert.IsTrue(this.series.Id != Guid.Empty);
            Assert.IsTrue(this.series.MatchDuration == new MatchDuration(duartion));
            Assert.IsTrue(this.series.NumberOfTeams == numberOfTeams);
            Assert.IsTrue(this.series.Schedule != null);
        }

        [TestMethod]
        public void SeriesScheduleCanAddMatch()
        {
            this.series.Schedule.Add(Guid.NewGuid());
            Assert.IsTrue(series.Schedule.Count == 1);
        }
    }
}