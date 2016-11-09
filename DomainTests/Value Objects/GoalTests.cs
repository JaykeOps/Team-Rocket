using System;
using System.Collections.Generic;
using DomainTests.Entities;
using football_series_manager.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Value_Objects.Tests
{
    [TestClass]
    public class GoalTests
    {
        private Goal goalOne;
        private Goal goalTwo;
        private Goal goalThree;

        public GoalTests()
        {

            var playerOne = new Player(new Name("John", "Doe"), new DateOfBirth("1975-04-18"),
                PlayerPosition.Defender, PlayerStatus.Available, new ShirtNumber(88));
            var playerTwo = new Player(new Name("John", "Doe"), new DateOfBirth("1975-04-18"),
                PlayerPosition.Defender, PlayerStatus.Available, new ShirtNumber(88));

            this.goalOne = new Goal(new MatchMinute(25), playerOne);
            this.goalTwo = new Goal(new MatchMinute(25), playerOne);
            this.goalThree = new Goal(new MatchMinute(25), playerTwo);
        }

        [TestMethod]
        public void GoalIsEqualToEntry()
        {
            Assert.IsTrue(goalOne.MatchMinute.Value.Equals(25));
            Assert.IsTrue(goalOne.Player.Name == new Name("John", "Doe"));
            Assert.IsTrue(goalOne.Player.DateOfBirth == new DateOfBirth("1975-04-18"));
            Assert.IsTrue(goalOne.Player.Position == PlayerPosition.Defender);
            Assert.IsTrue(goalOne.Player.Status == PlayerStatus.Available);
            Assert.IsTrue(goalOne.Player.ShirtNumber == new ShirtNumber(88));

        }
        [TestMethod]
        public void GoalIsComparableByValue()
        {
            Assert.IsTrue(this.goalOne == this.goalTwo);
            Assert.IsTrue(this.goalOne != goalThree);
            Assert.AreEqual(this.goalOne, this.goalTwo);
            Assert.AreNotEqual(this.goalOne, goalThree);

        }
        [TestMethod]
        public void GoalWorksWithHashSet()
        {
            var hashSet = new HashSet<Goal> { this.goalOne, this.goalTwo };
            Assert.IsTrue(hashSet.Count == 1);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GoalThrowsNullExeption()
        {
            Player player = null;
            MatchMinute minute = null;

            new Goal(new MatchMinute(25), player);
            new Goal(minute, new Player(new Name("John", "Doe"), new DateOfBirth("1975-04-18"),
                PlayerPosition.Defender, PlayerStatus.Available, new ShirtNumber(88)));
            new Goal(minute,player);

        }

    }
}