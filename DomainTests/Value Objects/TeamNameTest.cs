using Domain.Value_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DomainTests.Value_Objects
{
    [TestClass()]
    public class TeamNameTests
    {
        [TestMethod]
        public void TeamNameIsEqualToEntry()
        {
            var teamName = new TeamName("Ifk Göteborg");
            Assert.IsTrue(teamName.Value == "Ifk Göteborg");
        }

        [TestMethod()]
        public void TeamNameIsNotNull()
        {
            var teamName = new TeamName("Ifk Göteborg");
            Assert.IsNotNull(teamName);
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void NameThatContainsLessThanTwoCharactersThrowsFormatException()
        {
            var teamName = new TeamName("h");
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void NameThatContainsMoreThanFourtyCharactersThrowsFormatException()
        {
            var teamName = new ArenaName("HelloeveryoneletsseeifthisispassedWonderifthistestwillbepassedornot");
        }

        [TestMethod]
        public void NameTryParseCanOutValidResult()
        {
            TeamName result;
            TeamName.TryParse("Ifk Göteborg", out result);
            Assert.IsTrue(result.Value == "Ifk Göteborg");
        }

        [TestMethod()]
        public void NameTryParseCanOutNullValue()
        {
            TeamName result;
            TeamName.TryParse("Ullev/", out result);
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void NameTryParseCanReturnTrue()
        {
            TeamName result;
            Assert.IsTrue(TeamName.TryParse("Ifk Göteborg", out result));
        }

        [TestMethod()]
        public void NameTryParseCanReturnFalse()
        {
            TeamName result;
            Assert.IsFalse(TeamName.TryParse("Ulle?i", out result));
        }

        [TestMethod()]
        public void TeamNameIsComparableByValue()
        {
            var nameOne = new TeamName("Ifk Göteborg");
            var nameTwo = new TeamName("Ifk Göteborg");
            Assert.AreEqual(nameOne, nameTwo);
            Assert.IsTrue(nameOne == nameTwo);
        }

        [TestMethod]
        public void TeamNameWorksWithHashTable()
        {
            var nameOne = new TeamName("Ifk Göteborg");
            var nameTwo = new TeamName("Ifk Göteborg");
            var nameHashSet = new HashSet<TeamName> { nameOne, nameTwo };
            Assert.IsTrue(nameHashSet.Count == 1);
        }
    }
}