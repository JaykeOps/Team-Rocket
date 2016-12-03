using Domain.Entities;
using DomainTests.Test_Dummies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Domain.Value_Objects.Tests
{
    [TestClass]
    public class PenaltyTests
    {
        private Penalty penaltyOne;
        private Penalty penaltyTwo;

        public PenaltyTests()
        {
            var contactInformation = new ContactInformation(new PhoneNumber("0739-246788"),
                new EmailAddress("johnDoe_48@hotmail.com"));
            var player = new Player(new Name("John", "Doe"), new DateOfBirth("1988-05-22"),
                PlayerPosition.Goalkeeper, PlayerStatus.Available);
            var series = new DummySeries();
            this.penaltyOne = new Penalty(new MatchMinute(36), player.Id, true, series.DummyGames.GameTwo, series.DummyGames.GameTwo.AwayTeamId);
            this.penaltyTwo = new Penalty(new MatchMinute(36), player.Id, true, series.DummyGames.GameTwo, series.DummyGames.GameTwo.AwayTeamId);
        }

        [TestMethod]
        public void PenaltyIsEqualToValidEntry()
        {
            Assert.IsTrue(this.penaltyOne.MatchMinute.Value == 36
                && this.penaltyOne.PlayerId != Guid.Empty);
        }

        [TestMethod]
        public void PenaltyIsComparableByValue()
        {
            Assert.AreEqual(this.penaltyOne, this.penaltyTwo);
            Assert.IsTrue(this.penaltyOne == this.penaltyTwo);
        }

        [TestMethod]
        public void PenaltyWorksWithHashSet()
        {
            var hashSet = new HashSet<Penalty>();
            hashSet.Add(this.penaltyOne);
            hashSet.Add(this.penaltyTwo);
            Assert.IsTrue(hashSet.Count == 1);
        }
    }
}