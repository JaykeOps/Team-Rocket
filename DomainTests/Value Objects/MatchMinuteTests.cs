using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Value_Objects.Tests
{
    [TestClass]
    public class MatchMinuteTests
    {
        [TestMethod]
        public void MatchMinuteCanBeAssignedValidValue()
        {
            var matchMinute = new MatchMinute(90);
            Assert.IsTrue(matchMinute.Value == 90);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MatchMinuteCantBeLessThanOneMinute()
        {
            var matchMinute = new MatchMinute(0);
            Assert.IsTrue(matchMinute.Value == 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MatchMinuteCantBeMoreThan120()
        {
            var matchMinute = new MatchMinute(121);
            Assert.IsTrue(matchMinute.Value == 121);
        }

        [TestMethod]
        public void MatchMinuteIsComparableByValue()
        {
            var matchMinuteOne = new MatchMinute(4);
            var matchMinuteTwo = new MatchMinute(4);
            Assert.AreEqual(matchMinuteOne, matchMinuteTwo);
        }

        [TestMethod]
        public void MatchMinuteToStringLessThan91()
        {
            var matchMinute = new MatchMinute(90);
            Assert.AreEqual(matchMinute.ToString(), "90");
        }

        [TestMethod]
        public void MatchMinuteToStringMoreThan90()
        {
            var matchMinute = new MatchMinute(91);
            Assert.AreEqual(matchMinute.ToString(), "90+1");
        }
    }
}
