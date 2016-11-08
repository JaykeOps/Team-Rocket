using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Domain.Value_Objects.Tests
{
    [TestClass]
    public class EmailAddressTests
    {
        [TestMethod]
        public void EmailIsEqualToValidEntry()
        {
            var email = new EmailAddress("johnDoe84@outlook.com");
            Assert.IsTrue(email.Value == "johnDoe84@outlook.com");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void EmailMissingCommercialAtThrowsFormatException()
        {
            var email = new EmailAddress("johnDoe84outlook.com");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void EmailMissingDomainAddressThrowsFormatException()
        {
            var email = new EmailAddress("johnDoe_84@outlook.");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void EmailExceedsThirtyCharactersThrowsFormatException()
        {
            var email = new EmailAddress("johnDoe_84He@hahahahahahahaha.com");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void EmailContainingLessThanFiveCharactersThrowsFormatException()
        {
            var email = new EmailAddress("g@o.");
        }

        [TestMethod]
        public void EmailTryParseCanOutValidEmailAddress()
        {
            EmailAddress result;
            EmailAddress.TryParse("johnDoe_84@outlook.com", out result);
            Assert.IsTrue(result.Value == "johnDoe_84@outlook.com");
        }

        [TestMethod]
        public void EmailTryParseCanOutNull()
        {
            EmailAddress result;
            EmailAddress.TryParse("johnDoe_84outlook.com", out result);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void EmailTryParseCanReturnTrue()
        {
            EmailAddress result;
            Assert.IsTrue(EmailAddress.TryParse("johnDoe_84@outlook.com", out result));
        }

        [TestMethod]
        public void EmailTryParseCanReturnFalse()
        {
            EmailAddress result;
            Assert.IsFalse(EmailAddress.TryParse("johnDoe_84@outlookcom", out result));
        }

        [TestMethod]
        public void EmailIsComparableByValue()
        {
            var emailOne = new EmailAddress("donaldTrump@hillary.com");
            var emailTwo = new EmailAddress("donaldTrump@hillary.com");
            Assert.AreEqual(emailOne, emailTwo);
            Assert.IsTrue(emailOne == emailTwo);
        }

        [TestMethod]
        public void EmailWorksWithHashSet()
        {
            var emailOne = new EmailAddress("donaldTrump@hillary.com");
            var emailTwo = new EmailAddress("donaldTrump@hillary.com");
            var emailHashTable = new HashSet<EmailAddress>();
            emailHashTable.Add(emailOne);
            emailHashTable.Add(emailTwo);
            Assert.IsTrue(emailHashTable.Count == 1);
        }
    }
}