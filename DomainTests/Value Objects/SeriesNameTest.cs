using Domain.Value_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DomainTests.Value_Objects
{
    [TestClass]
    public class SeriesNameTest
    {
        [TestMethod]
        public void SeriesNameIsEqualToEntry()
        {
            var seriesName = new SeriesName("Allsvenskan");
            Assert.IsTrue(seriesName.Value == "Allsvenskan");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void SeriesNameLengthIsValid()
        {
            var seriesName = new SeriesName("Det långa namnet på en serie som är för långt");
        }

        [TestMethod]
        public void SeriesNameTryParseCanOutValidResult()
        {
            SeriesName result;
            SeriesName.TryParse("Allsvenskan", out result);
            Assert.IsTrue(result.Value == "Allsvenskan");
        }

        [TestMethod]
        public void SeriesNameTryParseCanOutNullValue()
        {
            SeriesName result;
            SeriesName.TryParse("Allsvenska/", out result);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void SeriesNameTryParseCanReturnTrue()
        {
            SeriesName result;
            Assert.IsTrue(SeriesName.TryParse("Allsvenskan", out result));
        }

        [TestMethod]
        public void SeriesNameTryParseCanReturnFalse()
        {
            SeriesName result;
            Assert.IsFalse(SeriesName.TryParse("Allsvens)an", out result));
        }

        [TestMethod]
        public void SeriesNameIsComparableByValue()
        {
            var arenaNameOne = new SeriesName("Allsvenskan");
            var arenaNameTwo = new SeriesName("Allsvenskan");
            Assert.AreEqual(arenaNameOne, arenaNameTwo);
            Assert.IsTrue(arenaNameOne == arenaNameTwo);
        }

        [TestMethod]
        public void SeriesNameWorksWithHashSet()
        {
            var seriesNameOne = new SeriesName("Allsvenskan");
            var seriesNameTwo = new SeriesName("Allsvenskan");
            var nameHashSet = new HashSet<SeriesName> { seriesNameOne, seriesNameTwo };
            Assert.IsTrue(nameHashSet.Count == 1);
        }
    }
}