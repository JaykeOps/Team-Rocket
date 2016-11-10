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

        public SeriesTest()
        {
            this.series = new Series(new MatchDuration(duartion), 16);
        }

        [TestMethod]
        public void SeriesCanHoldValidEntries()
        {
            Assert.IsTrue(this.series.Id != Guid.Empty);
            Assert.IsTrue(this.series.MatchDuration == new MatchDuration(duartion));
            Assert.IsTrue(this.series.NumberOfTeams == 16);
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