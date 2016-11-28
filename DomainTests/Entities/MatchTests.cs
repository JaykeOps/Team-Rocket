using Domain.Entities;
using Domain.Value_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DomainTests.Entities
{
    [TestClass]
    public class MatchTests
    {
        private Match match;
        private Guid awayTeamId;
        private Guid homeTeamId;

        public MatchTests()
        {
            var arena = new ArenaName("Ullevi");
            var date = new MatchDateAndTime(new DateTime(2016, 12, 23, 19, 00, 00));
            var series = new Series(new MatchDuration(new TimeSpan(90 * 6000000000 / 10)), new NumberOfTeams(16), new SeriesName("Allsvenskan"));
            this.awayTeamId = Guid.NewGuid();
            this.homeTeamId = Guid.NewGuid();
            this.match = new Match(arena, this.homeTeamId, this.awayTeamId, series, date);
        }

        [TestMethod]
        public void MatchCanHoldValidEntries()
        {
            Assert.IsTrue(this.match.Id != Guid.Empty);
            Assert.IsTrue($"{this.match.MatchDate.Value:yyyy-MM-dd HH:mm}" == "2016-12-23 19:00");
            Assert.IsTrue(this.match.AwayTeamId == this.awayTeamId);
            Assert.IsTrue(this.match.HomeTeamId == this.homeTeamId);
            Assert.IsTrue(this.match.Location.Value == "Ullevi");
            Assert.IsTrue(this.match.MatchDuration.Equals(new MatchDuration(new TimeSpan(90 * 6000000000 / 10))));
        }

        [TestMethod]
        public void HomeAndAwayTeamIdsCanChange()
        {
            var id = new Guid();
            Assert.IsFalse(this.match.HomeTeamId == id);
            Assert.IsFalse(this.match.AwayTeamId == id);
            this.match.AwayTeamId = id;
            this.match.HomeTeamId = id;
            Assert.IsTrue(this.match.HomeTeamId == id);
            Assert.IsTrue(this.match.HomeTeamId == id);
        }

        [TestMethod]
        public void MatchLocationCanChange()
        {
            var loaction = new ArenaName("Bravida");
            Assert.IsFalse(this.match.Location == loaction);
            this.match.Location = loaction;
            Assert.IsTrue(this.match.Location == loaction);
        }

        [TestMethod]
        public void MatchDateCanChange()
        {
            var matchDate = new MatchDateAndTime(new DateTime(2016, 12, 24, 19, 00, 00));
            Assert.IsFalse(this.match.MatchDate == matchDate);
            this.match.MatchDate = matchDate;
            Assert.IsTrue(this.match.MatchDate == matchDate);
        }
    }
}