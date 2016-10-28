using Microsoft.VisualStudio.TestTools.UnitTesting;
using football_series_manager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace football_series_manager.Domain.Entities.Tests
{
    [TestClass()]
    public class NameTests
    {
        [TestMethod]
        public void NameIsEqualToEntry()
        {
            var name = new Name("Donald", "Trump");
            Assert.IsTrue(name.FirstName == "Donald" && name.LastName == "Trump");
        }

        [TestMethod()]
        public void NameIsNotNull()
        {
            var name = new Name("Hillary", "Clinton");
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
            var name = new Name(".%&#/", ")(##¤%");
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

        [TestMethod()]
        public void FirstNameIsValid()
        {
            Name.IsValid("Donald");
        }

        [TestMethod()]
        public void LastNameIsValid()
        {
            Name.IsValid("Trump");
        }

    }
}