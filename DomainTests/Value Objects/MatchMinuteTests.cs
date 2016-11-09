using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

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
        public void MatchMinuteIsComparableEqualValue()
        {
            var matchMinuteOne = new MatchMinute(4);
            var matchMinuteTwo = new MatchMinute(4);
            var test = matchMinuteOne == matchMinuteTwo;
            Assert.AreEqual(matchMinuteOne, matchMinuteTwo);
        }

        [TestMethod]
        public void MatchMinuteIsComparableNotEqualValue()
        {
            var matchMinuteOne = new MatchMinute(4);
            var matchMinuteTwo = new MatchMinute(5);
            Assert.IsTrue(matchMinuteOne != matchMinuteTwo);
        }

        [TestMethod]
        public void MatchMinuteIsComparableEqualValueOperator()
        {
            var matchMinuteOne = new MatchMinute(4);
            var matchMinuteTwo = new MatchMinute(4);
            Assert.IsTrue(matchMinuteOne == matchMinuteTwo);
        }

        [TestMethod]
        public void MatchMinuteIsComparableEqualValueEquals()
        {
            var matchMinuteOne = new MatchMinute(4);
            var matchMinuteTwo = new MatchMinute(4);
            Assert.IsTrue(matchMinuteOne.Equals(matchMinuteTwo));
        }

        [TestMethod]
        public void MatchMinuteValueIsComparableNotEqualValue()
        {
            var matchMinuteOne = new MatchMinute(4);
            var matchMinuteTwo = new MatchMinute(5);
            Assert.IsTrue(matchMinuteOne.Value != matchMinuteTwo.Value);
        }

        [TestMethod]
        public void MatchMinuteValueIsComparableEqualValueOperator()
        {
            var matchMinuteOne = new MatchMinute(4);
            var matchMinuteTwo = new MatchMinute(4);
            Assert.IsTrue(matchMinuteOne.Value == matchMinuteTwo.Value);
        }

        [TestMethod]
        public void MatchMinuteValueIsComparableEqualValueEquals()
        {
            var matchMinuteOne = new MatchMinute(4);
            var matchMinuteTwo = new MatchMinute(4);
            Assert.IsTrue(matchMinuteOne.Value.Equals(matchMinuteTwo.Value));
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

        [TestMethod]
        public void MatchMinuteWorksWithHashSet()
        {
            var matchMinuteOne = new MatchMinute(58);
            var matchMinuteTwo = new MatchMinute(58);
            var hashSet = new HashSet<MatchMinute> { matchMinuteOne, matchMinuteTwo };
            Assert.IsTrue(hashSet.Count == 1);
        }
    }
}