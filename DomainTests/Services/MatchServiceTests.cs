using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Services;
using Domain.Value_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests.Services
{
    [TestClass]
    public class MatchServiceTests
    {
        private MatchService service = new MatchService();
        private Match match = new Match(new ArenaName("Ullevi"), Guid.NewGuid(), Guid.NewGuid(), new Series(new MatchDuration(new TimeSpan(0, 90, 0)), new NumberOfTeams(16)), new MatchDateAndTime(new DateTime(2016, 12, 20, 19, 30, 00)));
        private Match match2 = new Match(new ArenaName("Ullevi"), Guid.NewGuid(), Guid.NewGuid(), new Series(new MatchDuration(new TimeSpan(0, 90, 0)), new NumberOfTeams(16)), new MatchDateAndTime(new DateTime(2016, 12, 20, 19, 30, 00)));

        [TestMethod]
        public void GetAllIsReturningIEnumerable()
        {
            Assert.IsInstanceOfType(service.GetAll(), typeof(IEnumerable<Match>));
        }

        [TestMethod]
        public void GetAllMatchsNotReturningNull()
        {
            Assert.IsNotNull(service.GetAll());
        }

        [TestMethod]
        public void AddMatchIsWorking()
        {
            service.AddMatch(match);
            var matchs = service.GetAll();
            Assert.IsTrue(matchs.Contains(match));
            Assert.IsFalse(matchs.Contains(match2));
        }

        [TestMethod]
        public void ServiceRepoInstanceIsTheSame()
        {
            var service1 = new MatchService();
            var service2 = new MatchService();
            service1.AddMatch(match);
            var matchs = service2.GetAll();
            Assert.IsTrue(matchs.Contains(match));
        }

        [TestMethod]
        public void FindMatchByIdIsWorking()
        {
            Assert.IsFalse(service.FindById(match.Id) == match);
            service.AddMatch(match);
            Assert.IsTrue(service.FindById(match.Id) == match);
        }
    }
}
