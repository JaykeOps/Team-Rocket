using System;
using Domain.Entities;
using Domain.Helper_Classes;
using Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Domain.Value_Objects;

namespace DomainTests.Value_Objects
{
    [TestClass]
    public class PlayerSeriesEventsTests
    {
        private Player playerOne;
        private Team teamOne;
        private Series seriesOne;
        private IEnumerable<Match> teamOneSeriesOneMatchSchedule;
        private GameService gameService;
        private Game gameOne;
        private Game gameTwo;
        private Game gameThree;
        private Game gameFour;

        [TestInitialize]
        public void Init()
        {
            this.seriesOne = DomainService.GetAllSeries().First();
            var schedule = new Schedule();
            schedule.GenerateSchedule(this.seriesOne);
            this.teamOne = DomainService.FindTeamById(this.seriesOne.TeamIds.First());
            this.teamOneSeriesOneMatchSchedule = this.teamOne.MatchSchedules[this.seriesOne.Id];
            this.playerOne = this.teamOne.Players.First();
            this.gameService = new GameService();
            this.gameOne = new Game(this.teamOneSeriesOneMatchSchedule.First());
            this.gameTwo = new Game(this.teamOneSeriesOneMatchSchedule.ElementAt(1));
            this.gameThree = new Game(this.teamOneSeriesOneMatchSchedule.ElementAt(2));
            this.gameFour = new Game(this.teamOneSeriesOneMatchSchedule.ElementAt(3));
            this.gameService.Add(this.gameOne);
            this.gameService.Add(this.gameTwo);
            this.gameService.Add(this.gameThree);
            this.gameService.Add(this.gameFour);
            DomainService.AddSeriesToPlayers(this.seriesOne, this.teamOne.Players);
        }

        [TestMethod]
        public void PlayerSeriesEventsIsUpdateWhenNewGoalIsAdded()
        {
            this.teamOneSeriesOneMatchSchedule.First();
            Assert.IsTrue(this.playerOne.SeriesEvents[this.seriesOne.Id] != null);
            var playerOneGoalsPreAdd = this.playerOne.PresentableSeriesEvents[this.seriesOne.Id].Goals.Count();
            this.gameOne.Protocol.Goals.Add(new Goal(new MatchMinute(14), this.playerOne.TeamId, playerOne.Id));
            var playerOneGoalsPostAdd = this.playerOne.SeriesEvents[this.seriesOne.Id].Goals.Count();
            Assert.IsTrue(playerOneGoalsPostAdd - playerOneGoalsPreAdd == 1);
        }
    }
}