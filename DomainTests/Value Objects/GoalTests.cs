using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Domain.Value_Objects.Tests
{
    [TestClass]
    public class GoalTests
    {
        private Goal goalOne;
        private Goal goalTwo;
        private Goal goalThree;
        private Guid playerIdOne;
        private Guid playerIdTwo;

        public GoalTests()
        {
            this.playerIdOne = Guid.NewGuid();
            this.playerIdTwo = Guid.NewGuid();
            this.goalOne = new Goal(new MatchMinute(25), this.playerIdOne);
            this.goalTwo = new Goal(new MatchMinute(25), this.playerIdOne);
            this.goalThree = new Goal(new MatchMinute(25), this.playerIdTwo);
        }

        [TestMethod]
        public void GoalIsEqualToEntry()
        {
            Assert.IsTrue(goalOne.MatchMinute.Value.Equals(25));
            Assert.IsTrue(goalOne.PlayerId == playerIdOne);
            Assert.AreNotEqual(playerIdOne, Guid.Empty);
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
            MatchMinute minute = null;
            var playerId = Guid.NewGuid();
            new Goal(minute, playerId);
        }
    }
}