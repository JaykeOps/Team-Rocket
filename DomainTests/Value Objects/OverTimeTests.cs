using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Domain.Value_Objects.Tests
{
    [TestClass]
    public class OverTimeTests
    {
        [TestMethod]
        public void OverTimeIsEqualToValidEntry()
        {
            var overTime = new OverTime(30);
            Assert.IsTrue(overTime.Value == 30);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OverTimeThrowsArgumentExceptionIfLessThanZero()
        {
            var overTime = new OverTime(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OverTimeThrowsArgumentExceptionIfGreaterThan30()
        {
            var overTime = new OverTime(31);
        }

        [TestMethod]
        public void OverTimeIsComparableByValue()
        {
            var overTimeOne = new OverTime(3);
            var overtimeTwo = new OverTime(3);

            Assert.AreEqual(overTimeOne, overtimeTwo);
            Assert.IsTrue(overTimeOne == overtimeTwo);
        }

        [TestMethod]
        public void OverTimeWorksWithHashSet()
        {
            var overTimeOne = new OverTime(3);
            var overtimeTwo = new OverTime(3);
            var overTimeHashSet = new HashSet<OverTime>();
            overTimeHashSet.Add(overTimeOne);
            overTimeHashSet.Add(overtimeTwo);
            Assert.IsTrue(overTimeHashSet.Count == 1);
        }
    }
}