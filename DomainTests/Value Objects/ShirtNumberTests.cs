﻿using Domain.Entities;
using Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainTests.Entities.Tests
{
    [TestClass]
    public class ShirtNumberTests
    {
        private PlayerService playerService;
        private TeamService teamService;
        private IEnumerable<Player> players;
        private IEnumerable<Team> teams;
        private Player playerOne;
        private Player playerTwo;
        private Team teamOne;

        [TestInitialize]
        public void Init()
        {
            this.playerService = new PlayerService();
            this.teamService = new TeamService();
            this.players = this.playerService.GetAll();
            this.teams = this.teamService.GetAll();
            this.playerOne = this.players.FirstOrDefault();
            this.playerTwo = this.players.ElementAt(1);
            this.teamOne = this.teams.FirstOrDefault();
            this.playerOne.TeamId = this.teamOne.Id;
            this.teamOne.PlayerIds.Add(this.playerOne.Id);
            this.playerTwo.TeamId = this.teamOne.Id;
            this.teamOne.PlayerIds.Add(this.playerTwo.Id);
        }

        [TestMethod]
        public void ShirtNumberIsEqualToValidEntry()
        {
            this.playerOne.ShirtNumber = new ShirtNumber(playerOne.TeamId, 9);
            this.playerTwo.ShirtNumber = new ShirtNumber(playerTwo.TeamId, 20);
            Assert.IsTrue(playerOne.ShirtNumber.Value == 9);
            Assert.IsTrue(playerTwo.ShirtNumber.Value == 20);
        }

        [TestMethod]
        [ExpectedException(typeof(ShirtNumberAlreadyInUseException))]
        public void ShirtNumberCanThrowAlreadyInUseExceptionIfAlreadyUsed()
        {
            this.playerOne.ShirtNumber = new ShirtNumber(playerOne.TeamId, 9);
            this.playerTwo.ShirtNumber = new ShirtNumber(playerTwo.TeamId, 9);
        }

        [TestMethod]
        public void ShirtNumberCanBeAssignedAfterBeingUnAssigned()
        {
            playerOne.ShirtNumber = new ShirtNumber(playerOne.TeamId, 55);
            Assert.IsTrue(playerOne.ShirtNumber.Value == 55);
            this.teamOne.PlayerIds.Remove(playerOne.Id);
            this.playerOne.TeamId = Guid.Empty;
            playerTwo.ShirtNumber = new ShirtNumber(playerTwo.TeamId, 55);
            Assert.IsTrue(playerTwo.ShirtNumber.Value == 55);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ShirtNumberLessThanZeroThrowsIndexOutOfRangeException()
        {
            //var shirtNumber = new ShirtNumber(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ShirtNumberGreaterThanNinteyNineThrowsIndexOutOfRangeException()
        {
            //var shirtNumber = new ShirtNumber(100);
        }

        [TestMethod]
        [ExpectedException(typeof(ShirtNumberAlreadyInUseException))]
        public void ShirtNumberUnAvailibeUseThrowsShirtNumberAlreadyInUseException()
        {
            //var shirtNumber = new ShirtNumber(8);
        }

        [TestMethod]
        public void ShirtNumberTryParseCanOutValidResult()
        {
            //ShirtNumber result;
            //ShirtNumber.TryParse(17, out result);
            //Assert.IsTrue(result.Value == 17);
        }

        [TestMethod]
        public void ShirtNumberTryParseCanReturnTrue()
        {
            //ShirtNumber result;
            //Assert.IsTrue(ShirtNumber.TryParse(17, out result));
        }

        [TestMethod]
        public void ShirtNumberIsComparableByValue()
        {
            //var shirtNumberOne = new ShirtNumber(25);
            //var shirtNumberTwo = new ShirtNumber(25);
            //Assert.AreEqual(shirtNumberOne, shirtNumberTwo);
            //Assert.IsTrue(shirtNumberOne == shirtNumberTwo);
        }

        [TestMethod]
        public void ShirtNumberWorksWithHashSet()
        {
            //var shirtNumberOne = new ShirtNumber(25);
            //var shirtNumberTwo = new ShirtNumber(25);
            var shirtNumberHashSet = new HashSet<ShirtNumber>();
            //shirtNumberHashSet.Add(shirtNumberOne);
            //shirtNumberHashSet.Add(shirtNumberTwo);
            Assert.IsTrue(shirtNumberHashSet.Count == 1);
        }
    }
}