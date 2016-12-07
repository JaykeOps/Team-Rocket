using Domain.Entities;
using Domain.Repositories;
using Domain.Value_Objects;
using DomainTests.Test_Dummies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainTests.Repositories
{
    [TestClass]
    public class MatchRepositoryTests
    {
        private DummySeries dummySeries;
        private Match dummyMatch;
        private Match dummyMatchDuplicate;

        [TestInitialize]
        public void Initialize()
        {
            this.dummySeries = new DummySeries();
            this.dummyMatch = this.dummySeries.SeriesDummy.Schedule.First();
            this.dummyMatchDuplicate = new Match(this.dummyMatch.Location,
                this.dummyMatch.HomeTeamId, this.dummyMatch.AwayTeamId,
                this.dummySeries.SeriesDummy, this.dummyMatch.MatchDate)
            {
                Id = this.dummyMatch.Id
            };
        }

        [TestMethod]
        public void RepoStateIsTheSame()
        {
            var instance = MatchRepository.instance;
            var instance2 = MatchRepository.instance;

            Assert.AreEqual(instance, instance2);
        }

        [TestMethod]
        public void RepoGetAllReturnsIEnumerable()
        {
            Assert.IsInstanceOfType(MatchRepository.instance.GetAll(), typeof(IEnumerable<Match>));
        }

        [TestMethod]
        public void RepoAddIsWorking()
        {
            var match = new Match(new ArenaName("Ullevi"), Guid.NewGuid(), Guid.NewGuid(), new Series(new MatchDuration(new TimeSpan(0, 90, 0)), new NumberOfTeams(16), new SeriesName("Allsvenskan")), new MatchDateAndTime(new DateTime(2016, 12, 20, 19, 30, 00)));
            var match2 = new Match(new ArenaName("Ullevi"), Guid.NewGuid(), Guid.NewGuid(), new Series(new MatchDuration(new TimeSpan(0, 90, 0)), new NumberOfTeams(16), new SeriesName("Allsvenskan")), new MatchDateAndTime(new DateTime(2016, 12, 20, 19, 30, 00)));
            MatchRepository.instance.AddMatch(match);

            var matchs = MatchRepository.instance.GetAll();
            Assert.IsTrue(matchs.Contains(match));
            Assert.IsFalse(matchs.Contains(match2));
        }

        [TestMethod]
        public void GetAllNotReturningNull()
        {
            var teset = MatchRepository.instance.GetAll();
            Assert.IsNotNull(MatchRepository.instance.GetAll());
        }

        [TestMethod]
        public void TryGetMatchWillReplaceRepositoryMatchWithNewTeamIfIdsAreEqual()
        {
            var matchInRepository = MatchRepository.instance.GetAll()
                .First(x => x.Id == this.dummyMatch.Id);
            Assert.AreEqual(this.dummyMatch, matchInRepository);
            Assert.AreNotEqual(this.dummyMatch, this.dummyMatchDuplicate);
            Assert.AreEqual(this.dummyMatch.Id, this.dummyMatchDuplicate.Id);
            this.dummyMatchDuplicate.Location = new ArenaName("Majvallen 1");
            Assert.AreNotEqual(this.dummyMatch.Location, this.dummyMatchDuplicate.Location);
            MatchRepository.instance.AddMatch(this.dummyMatchDuplicate);
            matchInRepository = MatchRepository.instance.GetAll()
                .First(x => x.Id == this.dummyMatch.Id);
            Assert.AreEqual(this.dummyMatchDuplicate.Location, matchInRepository.Location);
            Assert.AreEqual(this.dummyMatchDuplicate, matchInRepository);
        }

        //TODO: Write tests for MatchRepository duplicate validation.
    }
}