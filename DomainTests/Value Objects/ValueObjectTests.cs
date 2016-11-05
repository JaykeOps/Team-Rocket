using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using static DomainTests.Value_Objects.Tests.ValueObjectTests;

namespace Domain.Value_Objects.Tests
{
    [TestClass]
    public partial class ValueObjectTests
    {
        [TestMethod]
        public void ValueObjectWithStringListIsComparableByValueUsingTheEqualsMethod()
        {
            var testObjectOne = new TestObjectWithStringList();
            var testObjectTwo = new TestObjectWithStringList();
            Assert.AreEqual(testObjectOne, testObjectTwo);
            testObjectTwo.TestListOfStrings.Add("Hello World!");
            Assert.AreNotEqual(testObjectOne, testObjectTwo);
        }

        [TestMethod]
        public void ValueObjectWithStringListIsComparableByValueUsingOperators()
        {
            var testObjectOne = new TestObjectWithStringList();
            var testObjectTwo = new TestObjectWithStringList();
            Assert.IsTrue(testObjectOne == testObjectTwo);
            testObjectTwo.TestListOfStrings.Add("Hello World!");
            Assert.IsTrue(testObjectOne != testObjectTwo);

        }

        [TestMethod]
        public void ValueObjectWithIntegerListIsComparableByValueUsingTheEqualsMethod()
        {
            var testObjectOne = new TestObjectWithIntegerList();
            var testObjectTwo = new TestObjectWithIntegerList();
            Assert.AreEqual(testObjectOne, testObjectTwo);
            testObjectTwo.TestListOfIntegers.Add(55);
            Assert.AreNotEqual(testObjectOne, testObjectTwo);
        }

        [TestMethod]
        public void ValueObjectWithIntegerListIsComparableByValueUsingOperators()
        {
            var testObjectOne = new TestObjectWithIntegerList();
            var testObjectTwo = new TestObjectWithIntegerList();
            Assert.IsTrue(testObjectOne == testObjectTwo);
            testObjectTwo.TestListOfIntegers.Add(55);
            Assert.IsTrue(testObjectOne != testObjectTwo);
        }

        [TestMethod]
        public void ValueObjectWithDictionaryIsComparableByValueUsingOperators()
        {
            var testObjectOne = new TestObjectWithDictionary();
            var testObjectTwo = new TestObjectWithDictionary();
            Assert.AreEqual(testObjectOne, testObjectTwo);
        }
    }
}