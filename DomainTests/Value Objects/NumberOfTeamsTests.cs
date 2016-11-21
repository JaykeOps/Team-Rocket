using Domain.Value_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DomainTests.Value_Objects
{
    [TestClass]
    public class NumberOfTeamsTests
    {
        private NumberOfTeams NumberOfTeamsOne = new NumberOfTeams(12);
        private NumberOfTeams NumberOfTeamsTwo = new NumberOfTeams(12);
        private NumberOfTeams NumberOfTeamsThree = new NumberOfTeams(36);

        [TestMethod]
        public void NumberOfTeamsIsEqualToEntry()
        {
            var numberOfTeams = new NumberOfTeams(24);
            Assert.IsTrue(numberOfTeams.Value == 24);
        }

        [TestMethod]
        public void NumberOfTeamsIsNotNull()
        {
            var numberOfTeams = new NumberOfTeams(4);
            Assert.IsNotNull(numberOfTeams);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NumberOfTeamsInputBelowMinimumValueThrowsArgumentExeption()
        {
            var matchDuartion = new NumberOfTeams(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NumberOfTeamsInputIsUnevenThrowsArgumentExeption()
        {
            var matchDuartion = new NumberOfTeams(7);
        }

        [TestMethod]
        public void NumberOfTeamsTryParseCanReturnTrue()
        {
            NumberOfTeams result;
            Assert.IsTrue(NumberOfTeams.TryParse("16", out result));
        }

        [TestMethod]
        public void NumberOfTeamsTryParseCanReturnFalse()
        {
            NumberOfTeams result;
            Assert.IsFalse(NumberOfTeams.TryParse("1", out result));
        }

        [TestMethod]
        public void NumberOfTeamsTryParseCanOutNull()
        {
            NumberOfTeams result;
            NumberOfTeams.TryParse("7", out result);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void NumberOfTeamsTryParseCanOutValidResult()
        {
            NumberOfTeams result;
            NumberOfTeams.TryParse("24", out result);
            Assert.IsTrue(result.Value == 24);
        }

        [TestMethod]
        public void NumberOfTeamsIsComparableByValue()
        {
            Assert.AreEqual(this.NumberOfTeamsOne, this.NumberOfTeamsTwo);
            Assert.AreNotEqual(this.NumberOfTeamsOne, this.NumberOfTeamsThree);
        }

        [TestMethod]
        public void NumberOfTeamsOperatorComparisonByValueTest()
        {
            Assert.IsTrue(this.NumberOfTeamsOne == this.NumberOfTeamsTwo);
            Assert.IsTrue(this.NumberOfTeamsOne != this.NumberOfTeamsThree);
        }

        [TestMethod]
        public void NumberOfTeamsWorksWithHashSet()
        {
            var matchHashSet = new HashSet<NumberOfTeams> { this.NumberOfTeamsOne, this.NumberOfTeamsTwo };
            Assert.IsTrue(matchHashSet.Count == 1);
        }
    }
}