using Domain.Entities;
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
        private NumberOfTeams numberOfTeams = new NumberOfTeams(16);

        public SeriesTest()
        {
            this.series = new Series(new MatchDuration(this.duartion), this.numberOfTeams, new SeriesName("Allsvenskan"));
        }

        [TestMethod]
        public void SeriesCanHoldValidEntries()
        {
            Assert.IsTrue(this.series.Id != Guid.Empty);
            Assert.IsTrue(this.series.MatchDuration == new MatchDuration(this.duartion));
            Assert.IsTrue(this.series.NumberOfTeams == this.numberOfTeams);
            Assert.IsTrue(this.series.Schedule != null);
        }

        [TestMethod]
        public void SeriesScheduleCanAddMatch()
        {
            var match = new Match(new ArenaName("Ullevi"), Guid.NewGuid(), Guid.NewGuid(), this.series);
            this.series.Schedule.Add(match);
            Assert.IsTrue(this.series.Schedule.Count == 1);
        }
    }
}