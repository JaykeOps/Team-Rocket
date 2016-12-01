using Domain.Entities;
using Domain.Services;
using DomainTests.Test_Dummies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

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
            this.dummyPlayerOne = DomainService.FindPlayerById(this.dummyTeamOne.PlayerIds.ElementAt(0));
            this.dummyPlayerTwo = DomainService.FindPlayerById(this.dummyTeamOne.PlayerIds.ElementAt(1));
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
        public void ShirtNumberTeamIdCanChangeWhenPlayerTeamIdChange()
        {
            this.dummyPlayerTwo.ShirtNumber = new ShirtNumber(this.dummyTeamOne.Id, 7);
            Assert.IsTrue(this.dummyPlayerTwo.ShirtNumber.Value == 7);
            this.dummyPlayerTwo.TeamId = this.dummyTeamTwo.Id;
            Assert.IsTrue(this.dummyPlayerTwo.ShirtNumber.Value == -1);
            this.dummyPlayerTwo.ShirtNumber = new ShirtNumber(this.dummyPlayerTwo.TeamId, 9);
            Assert.IsTrue(this.dummyPlayerTwo.ShirtNumber.Value == 9);
        }

        [TestMethod]
        public void ShirtNumberRemainsUnchangedWhenShirtNumberAlreadyInUseExceptionIsThrown()
        {
            try
            {
                this.dummyPlayerOne.ShirtNumber = new ShirtNumber(this.dummyPlayerOne.TeamId, 3);
                this.dummyPlayerTwo.ShirtNumber = new ShirtNumber(this.dummyPlayerTwo.TeamId, 7);
                Assert.AreEqual(this.dummyPlayerOne.ShirtNumber.Value, 3);
                Assert.AreEqual(this.dummyPlayerTwo.ShirtNumber.Value, 7);
                this.dummyPlayerOne.ShirtNumber = new ShirtNumber(this.dummyPlayerOne.TeamId, 7);
            }
            catch (ShirtNumberAlreadyInUseException)
            {
            }
            Assert.AreEqual(this.dummyPlayerOne.ShirtNumber.Value, 3);
        }

        [TestMethod]
        public void ShirtNumberRemainsUnchangedWhenIndexOutOfRangeExceptionIsThrown()
        {
            try
            {
                this.dummyPlayerOne.ShirtNumber = new ShirtNumber(this.dummyPlayerOne.TeamId, 5);
                Assert.AreEqual(this.dummyPlayerOne.ShirtNumber.Value, 5);
                this.dummyPlayerOne.ShirtNumber = new ShirtNumber(this.dummyPlayerOne.TeamId, 100);
            }
            catch (IndexOutOfRangeException)
            {
            }
            Assert.AreEqual(this.dummyPlayerOne.ShirtNumber.Value, 5);
        }
    }
}