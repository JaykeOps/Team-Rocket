using Domain.Value_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DomainTests.Value_Objects
{
    [TestClass]
    public class MatchDateAndTimeTest
    {
        private MatchDateAndTime dateOne = new MatchDateAndTime(new DateTime(2017, 01, 01, 19, 30, 00));
        private MatchDateAndTime dateTwo = new MatchDateAndTime(new DateTime(2017, 01, 01, 19, 30, 00));
        private MatchDateAndTime dateThree = new MatchDateAndTime(new DateTime(2017, 11, 01, 19, 30, 00));

        [TestMethod]
        public void MatchDurationIsEqualToEntry()
        {
            var matchDateAndTime = new MatchDateAndTime(DateTime.Now + TimeSpan.FromDays(1));
            var matchDatetimeTwo = DateTime.Now + TimeSpan.FromDays(1);
            Assert.IsTrue($"{matchDateAndTime.Value:yyyy-MM-dd HH: mm}" == $"{matchDatetimeTwo:yyyy-MM-dd HH: mm}");
        }

        [TestMethod]
        public void MatchDurationIsNotNull()
        {
            var matchDateAndTime = new MatchDateAndTime(DateTime.Now + TimeSpan.FromDays(1));
            Assert.IsNotNull(matchDateAndTime);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void MatchDateAndTimeThrowsFormatExceptionOnIsPast()
        {
            var matchDateAndTime = new MatchDateAndTime(DateTime.Now - TimeSpan.FromDays(1));
        }

        [TestMethod]
        public void MatchDateAndTimeThrowsFormatExceptionOnTimeToYearsFuture()
        {
            var matchDateAndTime = new MatchDateAndTime(DateTime.Now + TimeSpan.FromDays(366));
        }

        [TestMethod]
        public void MatchDateAndTimeTryParseCanReturnTrue()
        {
            MatchDateAndTime result;
            Assert.IsTrue(MatchDateAndTime.TryParse("2016-12-30 19:30", out result));
        }

        [TestMethod]
        public void MatchDateAndTimeTryParseCanReturnFalse()
        {
            MatchDateAndTime result;
            Assert.IsFalse(MatchDateAndTime.TryParse("2020-12-02 19:30", out result));
        }

        [TestMethod]
        public void MatchDateAndTimeCanOutValidResult()
        {
            MatchDateAndTime result;
            MatchDateAndTime.TryParse("2016-12-30 19:30", out result);
            Assert.IsTrue($"{result.Value:yyyy-MM-dd HH:mm}" == "2016-12-30 19:30");
        }

        [TestMethod]
        public void MatchDateAndTimeTryParseCanOutNull()
        {
            MatchDateAndTime result;
            MatchDateAndTime.TryParse("2014-12-02", out result);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void MatchDateAndTimeIsComparableByValue()
        {
            Assert.AreEqual(this.dateOne, this.dateTwo);
            Assert.AreNotEqual(this.dateOne, this.dateThree);
        }

        [TestMethod]
        public void MatchDateAndTimeOperatorComparisonByValueTest()
        {
            Assert.IsTrue(this.dateOne == this.dateTwo);
            Assert.IsTrue(this.dateOne != this.dateThree);
        }

        [TestMethod]
        public void MatchDateAndTimeWorksWithHashSet()
        {
            var matchDatehHashSet = new HashSet<MatchDateAndTime> { this.dateOne, this.dateTwo };
            Assert.IsTrue(matchDatehHashSet.Count == 1);
        }
    }
}