using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Value_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests.Value_Objects
{
    [TestClass]
    public class MatchDateAndTimeTest
    {
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
        public void MatchDurationThrowsFormatExceptionOnTimeInPast()
        {
            var matchDateAndTime = new MatchDateAndTime(DateTime.Now - TimeSpan.FromDays(1));
        }
        [TestMethod]
        public void MatchDurationThrowsFormatExceptionOnTimeToYearsFuture()
        {
            var matchDateAndTime = new MatchDateAndTime(DateTime.Now + TimeSpan.FromDays(366));
        }
        [TestMethod]
        public void MatchDateAndTimeTryParseCanReturnTrue()
        {
            MatchDateAndTime result;
            Assert.IsTrue(MatchDateAndTime.TryParse("2016-12-02 19:30", out result));
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
            MatchDateAndTime.TryParse("2016-12-02 19:30", out result);
            Assert.IsTrue($"{result.Value:yyyy-MM-dd HH:mm}" == "2016-12-02 19:30");
        }
        [TestMethod]
        public void MatchDurationTryParseCanOutNull()
        {
            MatchDateAndTime result;
            MatchDateAndTime.TryParse("2014-12-02", out result);
            Assert.IsNull(result);
        }


    }
}
