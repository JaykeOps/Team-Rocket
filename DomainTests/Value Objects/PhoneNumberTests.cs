using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Domain.Value_Objects.Tests
{
    [TestClass]
    public class PhoneNumberTests
    {
        [TestMethod]
        public void PhoneNumberIsEqualToValidEntry()
        {
            var phone = new PhoneNumber("0734-556688");
            Assert.IsTrue(phone.Value == "0734-556688");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void PhoneNumberContaningInvalidCharactersThrowsFormatException()
        {
            var phone = new PhoneNumber("%¤#&-556^88");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void PhoneFirstSequenceExceedingSixCharactersThrowsFormatException()
        {
            var phone = new PhoneNumber("0734562-894455");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void PhoneLastSequenceExceedingNineCharactersThrowsFormatException()
        {
            var phone = new PhoneNumber("0734-5566778800");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void PhoneFirstSequenceLessThanThreeCharactersThrowsFormatException()
        {
            var phone = new PhoneNumber("07-44556677");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void PhoneLastSequenceLessThanSixCharactersThrowsFormatException()
        {
            var phone = new PhoneNumber("0734-55660");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void PhoneNumberWithoutHyphenThrowsFormatException()
        {
            var phone = new PhoneNumber("0734556688");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void PhoneNumberContainingLettersThrowsFormatException()
        {
            var phone = new PhoneNumber("O73E-55O8O9");
        }

        [TestMethod]
        public void PhoneNumberTryParseCanReturnTrue()
        {
            PhoneNumber result;
            Assert.IsTrue(PhoneNumber.TryParse("0736-296438", out result));
        }

        [TestMethod]
        public void PhoneNumberTryParseCanReturnFalse()
        {
            PhoneNumber result;
            Assert.IsFalse(PhoneNumber.TryParse("073%-558833", out result));
        }

        [TestMethod]
        public void PhoneNumberTryParseCanOutValidResult()
        {
            PhoneNumber result;
            PhoneNumber.TryParse("0739-326412", out result);
            Assert.IsTrue(result.Value == "0739-326412");
        }

        [TestMethod]
        public void PhoneNumberTryParseCanOutNullResult()
        {
            PhoneNumber result;
            PhoneNumber.TryParse("O739-E26412", out result);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void PhoneNumberIsComparableByValue()
        {
            var phoneOne = new PhoneNumber("0734-556677");
            var phoneTwo = new PhoneNumber("0734-556677");
            Assert.AreEqual(phoneOne, phoneTwo);
            Assert.IsTrue(phoneOne == phoneTwo);
        }

        [TestMethod]
        public void PhoneNumberWorksWithHashSet()
        {
            var phoneOne = new PhoneNumber("0734-556677");
            var phoneTwo = new PhoneNumber("0734-556677");
            var phoneHashSet = new HashSet<PhoneNumber>();
            phoneHashSet.Add(phoneOne);
            phoneHashSet.Add(phoneTwo);
            Assert.IsTrue(phoneHashSet.Count == 1);
        }
    }
}