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
        private Guid playerTeamIdOne;
        private Guid playerTeamIdTwo;

        public GoalTests()
        {
            this.playerIdOne = Guid.NewGuid();
            this.playerIdTwo = Guid.NewGuid();
            this.playerTeamIdOne = Guid.NewGuid();
            this.playerTeamIdTwo = Guid.NewGuid();
            this.goalOne = new Goal(new MatchMinute(25), this.playerTeamIdOne, this.playerIdOne);
            this.goalTwo = new Goal(new MatchMinute(25), this.playerTeamIdOne, this.playerIdOne);
            this.goalThree = new Goal(new MatchMinute(25), this.playerTeamIdTwo, this.playerIdTwo);
        }

        [TestMethod]
        public void GoalIsEqualToEntry()
        {
            Assert.IsTrue(this.goalOne.MatchMinute.Value.Equals(25));
            Assert.IsTrue(this.goalOne.PlayerId == this.playerIdOne);
            Assert.AreNotEqual(this.playerIdOne, Guid.Empty);
        }

        [TestMethod]
        public void GoalIsComparableByValue()
        {
            Assert.IsTrue(this.goalOne == this.goalTwo);
            Assert.IsTrue(this.goalOne != this.goalThree);
            Assert.AreEqual(this.goalOne, this.goalTwo);
            Assert.AreNotEqual(this.goalOne, this.goalThree);
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
            new Goal(minute, this.playerTeamIdOne, playerId);
        }
    }
}