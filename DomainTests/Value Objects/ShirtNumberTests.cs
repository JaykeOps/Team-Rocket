using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DomainTests.Entities.Tests
{
    [TestClass()]
    public class ShirtNumberTests
    {
        [TestMethod()]
        public void ShirtNumberIsEqualToValidEntry()
        {
            var shirtNumber = new ShirtNumber(1);
            Assert.IsTrue(shirtNumber.Value == 1);
        }

        [TestMethod()]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ShirtNumberLessThanZeroThrowsIndexOutOfRangeException()
        {
            var shirtNumber = new ShirtNumber(-1);
        }

        [TestMethod()]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ShirtNumberGreaterThanNinteyNineThrowsIndexOutOfRangeException()
        {
            var shirtNumber = new ShirtNumber(100);
        }

        [TestMethod()]
        [ExpectedException(typeof(ShirtNumberAlreadyInUseException))]
        public void ShirtNumberUnAvailibeUseThrowsShirtNumberAlreadyInUseException()
        {
            var shirtNumber = new ShirtNumber(8);
        }

        [TestMethod()]
        public void ShirtNumberTryParseCanOutValidResult()
        {
            ShirtNumber result;
            ShirtNumber.TryParse(17, out result);
            Assert.IsTrue(result.Value == 17);
        }

        [TestMethod()]
        public void ShirtNumberTryParseCanOutNullResult()
        {
            ShirtNumber result;
            ShirtNumber.TryParse(9, out result);
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void ShirtNumberTryParseCanReturnTrue()
        {
            ShirtNumber result;
            Assert.IsTrue(ShirtNumber.TryParse(17, out result));
        }

        [TestMethod()]
        public void ShirtNumberTryParseCanReturnFalse()
        {
            ShirtNumber result;
            Assert.IsFalse(ShirtNumber.TryParse(2, out result));
        }

        [TestMethod()]
        public void ShirtNumberIsComparableByValued()
        {
            var shirtNumberOne = new ShirtNumber(25);
            var shirtNumberTwo = new ShirtNumber(25);
            Assert.AreEqual(shirtNumberOne, shirtNumberTwo);
            Assert.IsTrue(shirtNumberOne == shirtNumberTwo);
        }

        [TestMethod]
        public void ShirtNumberWorksWithHashSet()
        {
            var shirtNumberOne = new ShirtNumber(25);
            var shirtNumberTwo = new ShirtNumber(25);
            var shirtNumberHashSet = new HashSet<ShirtNumber>();
            shirtNumberHashSet.Add(shirtNumberOne);
            shirtNumberHashSet.Add(shirtNumberTwo);
            Assert.IsTrue(shirtNumberHashSet.Count == 1);
        }
    }
}