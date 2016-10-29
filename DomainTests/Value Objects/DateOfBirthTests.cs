using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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

        [TestMethod()]
        public void TryParseCanOutValidResult()
        {
            DateOfBirth result;
            DateOfBirth.TryParse("2012-03-29", out result);
            Assert.IsTrue($"{result:yyyy-MM-dd}" == "2012-03-29");
        }

        [TestMethod()]
        public void TryParseCanOutNullValue()
        {
            DateOfBirth result;
            DateOfBirth.TryParse("1920-02-09", out result);
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void TryParseCanReturnTrue()
        {
            DateOfBirth result;
            Assert.IsTrue(DateOfBirth.TryParse("1995-05-01", out result));
        }

        [TestMethod()]
        public void TryParseCanReturnFalse()
        {
            DateOfBirth result;
            Assert.IsFalse(DateOfBirth.TryParse("1776-07-04", out result));
        }
    }
}