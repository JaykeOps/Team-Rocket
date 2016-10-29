using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Value_Objects.Tests
{
    [TestClass()]
    public class DateOfBirthTests
    {
        [TestMethod()]
        public void DateOfBirthInRangeIsEqualToInput()
        {
            var dateOfBirth = new DateOfBirth("1989-12-14");
            Assert.IsTrue($"{dateOfBirth.Value:yyyy-MM-dd}" == "1989-12-14");
        }

        [ExpectedException(typeof(FormatException))]
        [TestMethod()]
        public void DateOfBirthUnderTheAgeOfThreeThrowsFormatException()
        {
            var dateOfBirth = new DateOfBirth("2015-10-30");
        }

        [ExpectedException(typeof(FormatException))]
        [TestMethod()]
        public void DateOfBirthOverTheAgeOfEightyThrowsNewFormatException()
        {
            var dateOfBirth = new DateOfBirth("1933-06-25");
        }

        
    }
}