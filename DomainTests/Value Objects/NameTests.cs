using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace football_series_manager.Domain.Entities.Tests
{
    [TestClass()]
    public class NameTests
    {
        [TestMethod]
        public void NameIsEqualToEntry()
        {
            var name = new Name("John", "Doe");
            Assert.IsTrue(name.FirstName == "John" && name.LastName == "Doe");
        }

        [TestMethod()]
        public void NameIsNotNull()
        {
            var name = new Name("John", "Doe");
            Assert.IsNotNull(name);
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void NameThatContainsNoneNumeralInvalidCharactersThrowsFormatException()
        {
            var name = new Name(".%&#/", ")(##¤%");
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void NameThatContainsNumbersThrowsFormatException()
        {
            var name = new Name("Jay554", "Adams443456");
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void NameThatContainsLessThanFourCharactersThrowsFormatException()
        {
            var name = new Name("h", "p");
        }

        [TestMethod()]
        [ExpectedException(typeof(FormatException))]
        public void NameThatContainsMoreThanFourtyCharactersThrowsFormatException()
        {
            var name = new Name("Helloeveryoneletsseeifthisispassed", "Wonderifthistestwillbepassedornot");
        }

        [TestMethod]
        public void NameTryParseCanOutValidResult()
        {
            Name result;
            Name.TryParse("John", "Doe", out result);
            Assert.IsTrue(result.FirstName == "John" && result.LastName == "Doe");
        }

        [TestMethod()]
        public void NameTryParseCanOutNullValue()
        {
            Name result;
            Name.TryParse("J%hn", "D03", out result);
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void NameTryParseCanReturnTrue()
        {
            Name result;
            Assert.IsTrue(Name.TryParse("John", "Doe", out result));
        }

        [TestMethod()]
        public void NameTryParseCanReturnFalse()
        {
            Name result;
            Assert.IsFalse(Name.TryParse("J0hn", "#oe", out result));
        }
    }
}