using Domain.Entities;
using Domain.Value_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DomainTests.Value_Objects
{
    [TestClass]
    public class AssistTests
    {
        private Assist assistOne;
        private Assist assistTwo;
        private Assist assistThree;

        public AssistTests()
        {
            var player = new Player(new Name("Arne", "Anka"), new DateOfBirth("1985-05-20"),
                PlayerPosition.MidFielder, PlayerStatus.Available, new ShirtNumber(80));
            var playerTwo = new Player(new Name("Arne", "Anka"), new DateOfBirth("1985-05-20"),
                PlayerPosition.MidFielder, PlayerStatus.Available, new ShirtNumber(45));
            this.assistOne = new Assist(new MatchMinute(30), player.Id);
            this.assistTwo = new Assist(new MatchMinute(30), player.Id);
            this.assistThree = new Assist(new MatchMinute(30), playerTwo.Id);
        }

        [TestMethod]
        public void AssistIsEqualToValidEntry()
        {
            Assert.IsTrue(this.assistOne.MatchMinute.Value == 30
                && this.assistOne.PlayerId != Guid.Empty);
        }

        [TestMethod]
        public void AssistIsComparableByValue()
        {
            Assert.IsTrue(this.assistOne == this.assistTwo);
            Assert.IsTrue(this.assistOne != assistThree);
            Assert.AreEqual(this.assistOne, this.assistTwo);
            Assert.AreNotEqual(this.assistTwo, assistThree);
        }

        [TestMethod]
        public void PenaltyWorksWithHashSet()
        {
            var hashSet = new HashSet<Assist> { this.assistOne, this.assistTwo };
            Assert.IsTrue(hashSet.Count == 1);
        }
    }
}