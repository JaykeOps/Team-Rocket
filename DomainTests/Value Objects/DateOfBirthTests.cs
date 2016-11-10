using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Domain.Value_Objects.Tests
{
    [TestClass]
    public class DateOfBirthTests
    {
        [TestMethod]
        public void DateOfBirthInRangeIsEqualToEntry()
        {
            var dateOfBirth = new DateOfBirth("1989-12-14");
            Assert.IsTrue($"{dateOfBirth.Value:yyyy-MM-dd}" == "1989-12-14");
        }

        [ExpectedException(typeof(FormatException))]
        [TestMethod]
        public void DateOfBirthUnderTheAgeOfThreeThrowsFormatException()
        {
            var dateOfBirth = new DateOfBirth("2015-10-30");
        }

        [ExpectedException(typeof(FormatException))]
        [TestMethod]
        public void DateOfBirthOverTheAgeOfEightyThrowsNewFormatException()
        {
            var dateOfBirth = new DateOfBirth("1933-06-25");
        }

        [TestMethod]
        public void DateOfBirthTryParseCanOutValidResult()
        {
            DateOfBirth result;
            DateOfBirth.TryParse("2012-03-29", out result);
            Assert.IsTrue($"{result:yyyy-MM-dd}" == "2012-03-29");
        }

        [TestMethod]
        public void DateOfBirthTryParseCanOutNullValue()
        {
            DateOfBirth result;
            DateOfBirth.TryParse("1920-02-09", out result);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void DateOfBirthTryParseCanReturnTrue()
        {
            DateOfBirth result;
            Assert.IsTrue(DateOfBirth.TryParse("1995-05-01", out result));
        }

        [TestMethod]
        public void DateOfBirthTryParseCanReturnFalse()
        {
            DateOfBirth result;
            Assert.IsFalse(DateOfBirth.TryParse("1776-07-04", out result));
        }

        [TestMethod]
        public void DateOfBirthIsComparableByValue()
        {
            var dateOfBirthOne = new DateOfBirth("1998-11-01");
            var dateOfBirthTwo = new DateOfBirth("1998-11-01");
            Assert.AreEqual(dateOfBirthOne, dateOfBirthTwo);
            Assert.IsTrue(dateOfBirthOne.Value == dateOfBirthTwo.Value);
        }

        [TestMethod]
        public void DateOfBirthWorksWithHashSet()
        {
            var dateOfBirthOne = new DateOfBirth("1998-11-01");
            var dateOfBirthTwo = new DateOfBirth("1998-11-01");
            var daetOfBirthHashSet = new HashSet<DateOfBirth> { dateOfBirthOne, dateOfBirthTwo };
            Assert.IsTrue(daetOfBirthHashSet.Count == 1);
        }
    }
}