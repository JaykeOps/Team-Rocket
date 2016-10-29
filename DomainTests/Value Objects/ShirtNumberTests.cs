using Microsoft.VisualStudio.TestTools.UnitTesting;
using DomainTests.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}