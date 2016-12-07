using Domain.Entities;
using DomainTests.Test_Dummies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Domain.Services.Tests
{
    [TestClass]
    public class DomainServiceTests
    {
        private DummySeries dummySeries;
        private Team dummyTeam;
        private Player dummyPlayer;

        [TestInitialize]
        public void Init()
        {
            this.dummySeries = new DummySeries();
            this.dummyTeam = this.dummySeries.DummyTeams.DummyTeamFour;
            this.dummyPlayer = DomainService.FindPlayerById(this.dummyTeam.PlayerIds.ElementAt(0));
        }

        [TestMethod]
        public void FindPlayerByIdCanReturnPlayer()
        {
            var playerService = new PlayerService();
            var player = DomainService.FindPlayerById(this.dummyPlayer.Id);
            Assert.AreEqual(player.Id, this.dummyPlayer.Id);
        }

        //[TestMethod]
        //public void FindTeamByIdCanReturnTeam()
        //{
        //    Guid teamId = this.teamService.FindById(this.teamId).Id;
        //    Assert.AreEqual(teamId, this.teamOne.Id);
        //}

        //[TestMethod]
        //public void FindGameByIdTest()
        //{
        //    Guid gameId = this.gameService.FindById(this.gameId).Id;
        //    Assert.AreEqual(gameId, this.gameOne.Id);
        //}

        //[TestMethod]
        //public void FindSeriesByIdTest()
        //{
        //    Guid seriesId = this.seriesService.FindById(this.seriesId).Id;
        //    Assert.AreEqual(seriesId, this.seriesOne.Id);
        //}
    }
}