using Domain.Entities;
using Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using DomainTests.Test_Dummies;

namespace DomainTests.Entities.Tests
{
    [TestClass]
    public class ShirtNumberTests
    {
        private PlayerService playerService;
        private TeamService teamService;
        private DummySeries dummySeries;
        private Team dummyTeamOne;
        private Team dummyTeamTwo;
        private Player dummyPlayerOne;
        private Player dummyPlayerTwo;

        [TestInitialize]
        public void Init()
        {
            this.playerService = new PlayerService();
            this.teamService = new TeamService();
            this.dummySeries = new DummySeries();
            this.dummyTeamOne = this.dummySeries.DummyTeams.DummyTeamOne;
            this.dummyTeamTwo = this.dummySeries.DummyTeams.DummyTeamTwo;
            this.dummyPlayerOne = this.dummyTeamOne.Players.ElementAt(0);
            this.dummyPlayerTwo = this.dummyTeamOne.Players.ElementAt(1);
        }

        [TestMethod]
        public void ShirtNumberIsEqualToValidEntry()
        {
            this.dummyPlayerOne.ShirtNumber = new ShirtNumber(this.dummyPlayerOne.TeamId, 9);
            this.dummyPlayerTwo.ShirtNumber = new ShirtNumber(this.dummyPlayerTwo.TeamId, 20);
            Assert.IsTrue(this.dummyPlayerOne.ShirtNumber.Value == 9);
            Assert.IsTrue(this.dummyPlayerTwo.ShirtNumber.Value == 20);
        }

        [TestMethod]
        [ExpectedException(typeof(ShirtNumberAlreadyInUseException))]
        public void ShirtNumberCanThrowAlreadyInUseExceptionIfAlreadyUsed()
        {
            this.dummyPlayerOne.ShirtNumber = new ShirtNumber(this.dummyPlayerOne.TeamId, 9);
            this.dummyPlayerTwo.ShirtNumber = new ShirtNumber(this.dummyPlayerTwo.TeamId, 9);
        }

        [TestMethod]
        public void ShirtNumberCanBeAssignedAfterBeingUnAssigned()
        {
            this.dummyPlayerOne.ShirtNumber = new ShirtNumber(this.dummyPlayerOne.TeamId, 55);
            Assert.IsTrue(this.dummyPlayerOne.ShirtNumber.Value == 55);
            this.dummyTeamOne.RemovePlayerId(this.dummyPlayerOne.Id);
            this.dummyPlayerOne.TeamId = Guid.Empty;
            this.dummyPlayerTwo.ShirtNumber = new ShirtNumber(this.dummyPlayerTwo.TeamId, 55);
            Assert.IsTrue(this.dummyPlayerTwo.ShirtNumber.Value == 55);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ShirtNumberThrowsIndexOutOfRangeExceptionIfNumberIsGreaterThanNinteyNine()
        {
            this.dummyPlayerOne.ShirtNumber = new ShirtNumber(this.dummyPlayerOne.TeamId, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ShirtNumberThrowsIndexOutOfRangeExceptionIfNumberIsLessThanZero()
        {
            this.dummyPlayerOne.ShirtNumber = new ShirtNumber(this.dummyPlayerOne.TeamId, -1);
        }

        [TestMethod]
        public void ShirtNumberGetPropertyCanReturnNull()
        {
            this.dummyPlayerOne.ShirtNumber = new ShirtNumber(this.dummyTeamOne.Id, null);
            Assert.IsNull(this.dummyPlayerOne.ShirtNumber.Value);
        }

        [TestMethod]
        public void ShirtNumberTeamIdCanChangeWhenPlayerTeamIdChange()
        {
            this.dummyPlayerTwo.ShirtNumber = new ShirtNumber(this.dummyTeamOne.Id, 7);
            Assert.IsTrue(this.dummyPlayerTwo.ShirtNumber.Value == 7);
            this.dummyPlayerTwo.TeamId = this.dummyTeamTwo.Id;
            Assert.IsTrue(this.dummyPlayerTwo.ShirtNumber.Value == null);
            this.dummyPlayerTwo.ShirtNumber = new ShirtNumber(9);
            Assert.IsTrue(this.dummyPlayerTwo.ShirtNumber.Value == 9);
        }
    }
}