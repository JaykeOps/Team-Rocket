using Domain.Entities;
using DomainTests.Value_Objects.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
        public void ValueObjectWithDictionaryIsComparableByValueUsingTheEqualsMethod()
        {
            var testObjectOne = new TestObjectWithDictionary();
            var testObjectTwo = new TestObjectWithDictionary();
            Assert.AreEqual(testObjectOne, testObjectTwo);
            testObjectTwo.TestDictionary.Add("Okay", 555);
            Assert.AreNotEqual(testObjectOne, testObjectTwo);
        }

        [TestMethod]
        public void ValueObjectWithDictionaryIsComparableByValueUsingOperators()
        {
            var testObjectOne = new TestObjectWithDictionary();
            var testObjectTwo = new TestObjectWithDictionary();
            Assert.IsTrue(testObjectOne == testObjectTwo);
            testObjectTwo.TestDictionary.Add("Okay", 555);
            Assert.IsTrue(testObjectOne != testObjectTwo);
        }

        [TestMethod]
        public void ValueObjectWorksWithHashSet()
        {
            var hashSet = new HashSet<TestObject>();
            var testObjectOne = new TestObject();
            var testObjectTwo = new TestObject();
            hashSet.Add(testObjectOne);
            hashSet.Add(testObjectTwo);
            Assert.IsTrue(hashSet.Count == 1);
        }

        [TestMethod]
        public void ValueObjectWithStringListPropertyWorksWithHashSet()
        {
            var hashSet = new HashSet<TestObjectWithStringList>();
            var testObjectOne = new TestObjectWithStringList();
            var testObjectTwo = new TestObjectWithStringList();
            hashSet.Add(testObjectOne);
            hashSet.Add(testObjectTwo);
            Assert.IsTrue(hashSet.Count == 1);
        }

        [TestMethod]
        public void ValueObjectWithIntegerListPropertyWorksWithHashSet()
        {
            var hashSet = new HashSet<TestObjectWithIntegerList>();
            var testObjectOne = new TestObjectWithIntegerList();
            var testObjectTwo = new TestObjectWithIntegerList();
            hashSet.Add(testObjectOne);
            hashSet.Add(testObjectTwo);
            Assert.IsTrue(hashSet.Count == 1);
        }

        [TestMethod]
        public void ValueObjectWithDictionaryPropertyWorksWithHashSet()
        {
            var hashSet = new HashSet<TestObjectWithDictionary>();
            var testObjectOne = new TestObjectWithDictionary();
            var testObjectTwo = new TestObjectWithDictionary();
            hashSet.Add(testObjectOne);
            hashSet.Add(testObjectTwo);
            Assert.IsTrue(hashSet.Count == 1);
            testObjectTwo.TestDictionary.Add("Opss!", 333);
            hashSet.Add(testObjectTwo);
            Assert.IsTrue(hashSet.Count == 2);
        }

        [TestMethod]
        public void EqualsWorksWithIdenticalNames()

        {
            var name = new Name("John", "Doe");
            var name2 = new Name("John", "Doe");

            Assert.IsTrue(name.Equals(name2));
        }

        [TestMethod]
        public void EqualsWorksWithNonIdenticalNames()

        {
            var name = new Name("John", "Doe");
            var name2 = new Name("Name", "Doe");

            Assert.IsFalse(name.Equals(name2));
        }

        [TestMethod]
        public void EqualsIsReflexive()

        {
            var name = new Name("John", "Doe");

            Assert.IsTrue(name.Equals(name));
        }

        [TestMethod]
        public void EqualsIsSymmetric()

        {
            var name = new Name("John", "Doe");
            var name2 = new Name("Name", "Doe");

            Assert.IsFalse(name.Equals(name2));
            Assert.IsFalse(name2.Equals(name));
        }

        [TestMethod]
        public void EqualsIsTransitive()

        {
            var name = new Name("John", "Doe");
            var name2 = new Name("John", "Doe");
            var name3 = new Name("John", "Doe");

            Assert.IsTrue(name.Equals(name2));
            Assert.IsTrue(name2.Equals(name3));
            Assert.IsTrue(name.Equals(name3));
        }

        [TestMethod]
        public void OperatorsWorks()

        {
            var name = new Name("John", "Doe");
            var name2 = new Name("John", "Doe");
            var name3 = new Name("Name", "Doe");

            Assert.IsTrue(name == name2);
            Assert.IsTrue(name2 != name3);
        }

        [TestMethod]
        public void EqualsWorkWithOneNull()
        {
            Name name = null;
            var name2 = new Name("John", "Doe");
            Assert.IsFalse(name2.Equals(name));
        }

        [TestMethod]
        public void OperatorWorkWhitTwoNulls()
        {
            Name name = null;
            Name name2 = null;
            Assert.IsTrue(name == name2);
        }

        [TestMethod]
        public void EqualsWorkWithValueAndReferenceTypes()
        {
            var name = new Name("John", "Doe");
            var name2 = new Name("John", "Doe");
            var time = new MatchMinute(30);
            var time2 = new MatchMinute(30);
            Assert.IsTrue(name.Equals(name2) && time.Equals(time2));
        }

        [TestMethod]
        public void EqualsWorksWithDeepthTrueTest()
        {
            var contact = new ContactInformation(new PhoneNumber("0738-040807"), new EmailAddress("kallekulling@gamil.com"));
            var contact2 = new ContactInformation(new PhoneNumber("0738-040807"), new EmailAddress("kallekulling@gamil.com"));
            Assert.IsTrue(contact.Equals(contact2));
        }

        [TestMethod]
        public void EqualsWorksWithDeepthFalseTest()
        {
            var contact = new ContactInformation(new PhoneNumber("0738-040807"), new EmailAddress("ballefuling@gamil.com"));
            var contact2 = new ContactInformation(new PhoneNumber("0738-040807"), new EmailAddress("kallekulling@gamil.com"));
            Assert.IsFalse(contact.Equals(contact2));
        }
    }
}