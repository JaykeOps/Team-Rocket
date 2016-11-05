using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Domain.Value_Objects.Tests
{
    public class TestObjectWithStringList : ValueObject<TestObjectWithStringList>
    {
        public string TestStr { get; set; } = "This is a test!";
        public int TestInt { get; set; } = 1337;
        public List<string> TestListOfStrings { get; set; } = new List<string> { "Test", "Test...", "Test......" };

        public override int GetHashCode()
        {
            return 0;
        }
    }

    public class TestObjectWithIntegerList : ValueObject<TestObjectWithIntegerList>
    {
        public List<int> TestListOfIntegers { get; set; } = new List<int> { 1, 2, 3, 4, 5 };

        public override int GetHashCode()
        {
            return 0;
        }
    }

    [TestClass]
    public class ValueObjectTests
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
        public void ValueObjectWithIntegerListIsComparableByValueUsingTheEqualsMethod()
        {
            var testObjectOne = new TestObjectWithIntegerList();
            var testObjectTwo = new TestObjectWithIntegerList();
            Assert.AreEqual(testObjectOne, testObjectTwo);
            testObjectTwo.TestListOfIntegers.Add(55);
            Assert.AreNotEqual(testObjectOne, testObjectTwo);
        }

    
    }
}