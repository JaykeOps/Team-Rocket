using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Value_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests.Value_Objects
{
    [TestClass()]
    public class MatchDurationTest
    {
        [TestMethod]
        public void MatchDurationIsEqualToEntry()
        {
            var duration = new TimeSpan(60 * 6000000000 / 10);
            var macthDuration = new MatchDuration(duration);
            Assert.IsTrue(macthDuration.Value.TotalMinutes == 60.0);
        }
        [TestMethod]
        public void MatchDurationIsNotNull()
        {
            var duration = new TimeSpan(60 * 6000000000 / 10);
            var macthDuration = new MatchDuration(duration);
            Assert.IsNotNull(macthDuration);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MatchDurationInputBelowMinimumValueThrowsArgumentExeption()
        {
            var duration = new TimeSpan(5 * 6000000000 / 10);
            var matchDuartion=new MatchDuration(duration);
            
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MatchDurationInputAboveMaximummValueThrowsArgumentExeption()
        {
            var duration = new TimeSpan(95 * 6000000000 / 10);
            var matchDuartion = new MatchDuration(duration);

        }

        [TestMethod]
        public void MatchDurationTryParseCanReturnTrue()
        {
            MatchDuration result;
            Assert.IsTrue(MatchDuration.TryParse("60", out result));
        }
        [TestMethod]
        public void MatchDurationTryParseCanReturnFalse()
        {
            MatchDuration result;
            Assert.IsFalse(MatchDuration.TryParse("5", out result));
        }
        [TestMethod]
        public void MatchDurationTryParseCanOutNull()
        {
            MatchDuration result;
            MatchDuration.TryParse("5", out result);
            Assert.IsNull(result);
        }
        [TestMethod]
        public void MatchDurationTryParseCanOutValidResult()
        {
            MatchDuration result;
            MatchDuration.TryParse("60", out result);
            Assert.IsTrue(result.Value.TotalMinutes==60.0);
        }
    }
}
