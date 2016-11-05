using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Value_Objects.Tests
{
    public class TestObjectWithList : ValueObject<TestObjectWithList>
    {
        public string TestStr { get; set; } = "This is a test!";
        public int TestInt { get; set; } = 1337;
        public List<string> TestListOfStrings { get; set; } = new List<string> { "Test", "Test...", "Test......" };
        public List<int> TestOfIntegers { get; set; } = new List<int> { 1, 2, 3, 4, 5 };

        public override int GetHashCode()
        {
            return 0;
        }
    }
    [TestClass]
    public class ValueObjectTests
    {
        [TestMethod]
        public void ValueObjectWithListIsComparableByValueUsingTheEqualsMethod()
        {
            var testObjectOne = new TestObjectWithList();
            var testObjectTwo = new TestObjectWithList();
            Assert.AreEqual(testObjectOne, testObjectTwo);
            testObjectTwo.TestListOfStrings.Add("Hello World!");
            Assert.AreNotEqual(testObjectOne, testObjectTwo);
        }

        [TestMethod]
        public void ValueObjectWithListIsComparableByValueUsingOperators()
        {

        }
    }
}