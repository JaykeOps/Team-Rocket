using Domain.Entities;
using Domain.Repositories;
using Domain.Value_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainTests.Repositories
{
    [TestClass]
    public class MatchRepositoryTests
    {
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
            var match = new Match(new ArenaName("Ullevi"), Guid.NewGuid(), Guid.NewGuid(), new Series(new MatchDuration(new TimeSpan(0, 90, 0)), new NumberOfTeams(16), "Allsvenskan"), new MatchDateAndTime(new DateTime(2016, 12, 20, 19, 30, 00)));
            var match2 = new Match(new ArenaName("Ullevi"), Guid.NewGuid(), Guid.NewGuid(), new Series(new MatchDuration(new TimeSpan(0, 90, 0)), new NumberOfTeams(16), "Allsvenskan"), new MatchDateAndTime(new DateTime(2016, 12, 20, 19, 30, 00)));
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

        //TODO: Write tests for MatchRepository duplicate validation.
    }
}