using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Domain.Services.Tests
{
    [TestClass]
    public class DomainServiceTests
    {
        private PlayerService playerService;
        private SeriesService seriesService;
        private TeamService teamService;
        private MatchService matchService;
        private GameService gameService;
        private Player playerOne;
        private Team teamOne;
        private Game gameOne;
        private Match matchOne;
        private Series seriesOne;
        private Guid playerId;
        private Guid teamId;
        private Guid gameId;
        private Guid matchId;
        private Guid seriesId;

        [TestInitialize]
        public void Init()
        {
            this.playerService = new PlayerService();
            this.seriesService = new SeriesService();
            this.teamService = new TeamService();
            this.gameService = new GameService();
            this.matchService = new MatchService();
            this.playerOne = this.playerService.GetAll().ToList().First();
            this.teamOne = this.teamService.GetAll().ToList().First();
            this.gameOne = this.gameService.GetAll().ToList().First();
            //this.matchOne = this.matchService.GetAll().ToList().First();
            this.seriesOne = this.seriesService.GetAll().ToList().First();
            this.playerId = this.playerOne.Id;
            this.teamId = this.teamOne.Id;
            this.gameId = this.gameOne.Id;
            //this.matchId = this.matchOne.Id;
            this.seriesId = this.seriesOne.Id;
        }

        [TestMethod]
        public void FindPlayerByIdCanReturnPlayer()
        {
            Guid playerId = this.playerService.FindById(this.playerId).Id;
            Assert.AreEqual(playerId, this.playerOne.Id);
        }

        [TestMethod]
        public void FindTeamByIdCanReturnTeam()
        {
            Guid teamId = this.teamService.FindById(this.teamId).Id;
            Assert.AreEqual(teamId, this.teamOne.Id);
        }

        [TestMethod]
        public void FindGameByIdTest()
        {
            Guid gameId = this.gameService.FindById(this.gameId).Id;
            Assert.AreEqual(gameId, this.gameOne.Id);
        }

        [TestMethod]
        public void FindSeriesByIdTest()
        {
            Guid seriesId = this.seriesService.FindById(this.seriesId).Id;
            Assert.AreEqual(seriesId, this.seriesOne.Id);
        }
    }
}