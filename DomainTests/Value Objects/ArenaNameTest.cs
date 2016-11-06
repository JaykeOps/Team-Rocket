using Domain.Value_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DomainTests.Value_Objects
{
    [TestClass()]
    public class ArenaNameTests
    {
        [TestMethod]
        public void ArenaNameIsEqualToEntry()
        {
            var arenaName = new ArenaName("Ullevi");
            Assert.IsTrue(arenaName.Value == "Ullevi");
        }

        [TestMethod]
        public void ArenaNameIsNotNull()
        {
            var arenaName = new ArenaName("Ullevi");
            Assert.IsNotNull(arenaName);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void AreanNameThatContainsNoneNumeralInvalidCharactersThrowsFormatException()
        {
            var name = new ArenaName(".%&#/");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ArenaNameThatContainsLessThanTwoCharactersThrowsFormatException()
        {
            var name = new ArenaName("h");
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void ArenaNameThatContainsMoreThanFourtyCharactersThrowsFormatException()
        {
            var name = new ArenaName("HelloeveryoneletsseeifthisispassedWonderifthistestwillbepassedornot");
        }

        [TestMethod]
        public void ArenaNameTryParseCanOutValidResult()
        {
            ArenaName result;
            ArenaName.TryParse("Ullevi", out result);
            Assert.IsTrue(result.Value == "Ullevi");
        }

        [TestMethod()]
        public void ArenaNameTryParseCanOutNullValue()
        {
            ArenaName result;
            ArenaName.TryParse("Ullev/", out result);
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void ArenaNameTryParseCanReturnTrue()
        {
            ArenaName result;
            Assert.IsTrue(ArenaName.TryParse("Ullevi", out result));
        }

        [TestMethod()]
        public void ArenaNameTryParseCanReturnFalse()
        {
            ArenaName result;
            Assert.IsFalse(ArenaName.TryParse("Ulle)i", out result));
        }

        [TestMethod()]
        public void ArenaNameIsComparableByValue()
        {
            var arenaNameOne = new ArenaName("Ullevi");
            var arenaNameTwo = new ArenaName("Ullevi");
            Assert.AreEqual(arenaNameOne, arenaNameTwo);
            Assert.IsTrue(arenaNameOne == arenaNameTwo);
        }

        [TestMethod]
        public void ArenaNameWorksWithHashSet()
        {
            var nameOne = new ArenaName("Ullevi");
            var arenaNameTwo = new ArenaName("Ullevi");
            var nameHashSet = new HashSet<ArenaName> { nameOne, arenaNameTwo };
            Assert.IsTrue(nameHashSet.Count == 1);
        }
    }
}