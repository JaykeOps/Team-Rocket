using Domain.Value_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DomainTests.Value_Objects
{
    [TestClass]
    public class TeamNameTests
    {
        [TestMethod]
        public void TeamNameIsEqualToEntry()
        {
            var teamName = new TeamName("Ifk Göteborg");
            Assert.IsTrue(teamName.Value == "Ifk Göteborg");
        }

        [TestMethod]
        public void TeamNameIsNotNull()
        {
            var teamName = new TeamName("Ifk Göteborg");
            Assert.IsNotNull(teamName);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TeamNameThatContainsNoneNumeralInvalidCharactersThrowsFormatException()
        {
            var name = new TeamName(".%&#/");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TeamNameThatContainsLessThanTwoCharactersThrowsFormatException()
        {
            var teamName = new TeamName("h");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TeamNameThatContainsMoreThanFourtyCharactersThrowsFormatException()
        {
            var teamName = new TeamName("HelloeveryoneletsseeifthisispassedWonderifthistestwillbepassedornot");
        }

        [TestMethod]
        public void TeamNameTryParseCanOutValidResult()
        {
            TeamName result;
            TeamName.TryParse("Ifk Göteborg", out result);
            Assert.IsTrue(result.Value == "Ifk Göteborg");
        }

        [TestMethod]
        public void TeamNameTryParseCanOutNullValue()
        {
            TeamName result;
            TeamName.TryParse("Ifk Götebor/", out result);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TeamNameTryParseCanReturnTrue()
        {
            TeamName result;
            Assert.IsTrue(TeamName.TryParse("Ifk Göteborg", out result));
        }

        [TestMethod]
        public void TeamNameTryParseCanReturnFalse()
        {
            TeamName result;
            Assert.IsFalse(TeamName.TryParse("Ifk G?teborg", out result));
        }

        [TestMethod]
        public void TeamNameIsComparableByValue()
        {
            var nameOne = new TeamName("Ifk Göteborg");
            var nameTwo = new TeamName("Ifk Göteborg");
            Assert.AreEqual(nameOne, nameTwo);
            Assert.IsTrue(nameOne == nameTwo);
        }

        [TestMethod]
        public void TeamNameWorksWithHashSet()
        {
            var nameOne = new TeamName("Ifk Göteborg");
            var nameTwo = new TeamName("Ifk Göteborg");
            var nameHashSet = new HashSet<TeamName> { nameOne, nameTwo };
            Assert.IsTrue(nameHashSet.Count == 1);
        }

        [TestMethod]
        public void TeamNameCanHoldGermanLetters()
        {
            var teamName = new TeamName("äöü ßÄÖÜẞ");
        }

        [TestMethod]
        public void TeamNameCanHoldFrenchLetters()
        {
            var teamName = new TeamName("àâäôéèëêïîçùûüÿ æœÀÂÄÔÉÈËÊÏÎŸÇÙÛÜÆŒ");
        }

        [TestMethod]
        public void TeamNameCanHoldPolishLetters()
        {
            var teamName = new TeamName("ąćęłńóśźż ĄĆĘŁŃÓŚŹŻ");
        }

        [TestMethod]
        public void TeamNameCanHoldItalianLetters()
        {
            var teamName = new TeamName("àèéìíîòóùú ÀÈÉÌÍÎÒÓÙÚ");
        }

        [TestMethod]
        public void TeamNameCanHoldSpanishLetters()
        {
            var teamName = new TeamName("áéíñóúü ÁÉÍÑÓÚÜ");
        }
    }
}