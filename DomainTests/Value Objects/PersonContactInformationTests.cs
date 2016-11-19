using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Domain.Value_Objects.Tests
{
    [TestClass]
    public class PersonContactInformationTests
    {
        private string phoneString;
        private string emailString;
        private ContactInformation contactInformation;

        public PersonContactInformationTests()
        {
            this.phoneString = "0734-668899";
            this.emailString = "johnDoe@outlook.com";
            var phone = new PhoneNumber(this.phoneString);
            var email = new EmailAddress(this.emailString);
            this.contactInformation = new ContactInformation(phone, email);
        }

        [TestMethod]
        public void PersonContactInformationCanBeInitiatedWithValidArguments()
        {
            Assert.IsTrue(this.contactInformation.Phone.Value == "0734-668899"
                && this.contactInformation.Email.Value == "johnDoe@outlook.com");
        }

        [TestMethod]
        public void PersonContactInformationPhoneNumberCanChange()
        {
            this.contactInformation.Phone = new PhoneNumber("0739-884477");
            Assert.IsTrue(this.contactInformation.Phone.Value == "0739-884477");
        }

        [TestMethod]
        public void PersonContactInformationEmailAddressCanChange()
        {
            this.contactInformation.Email = new EmailAddress("marco_polo@gm.com");
            Assert.IsTrue(this.contactInformation.Email.Value == "marco_polo@gm.com");
        }

        [TestMethod]
        public void PersonContactInformationWorksWithHashSet()
        {
            var hashSet = new HashSet<ContactInformation>();
            var phoneOne = new PhoneNumber(this.phoneString);
            var emailOne = new EmailAddress(this.emailString);
            var phoneTwo = new PhoneNumber(this.phoneString);
            var emailTwo = new EmailAddress(this.emailString);
            var contatctOne = new ContactInformation(phoneOne, emailOne);
            var contactTwo = new ContactInformation(phoneTwo, emailTwo);
            hashSet.Add(contatctOne);
            hashSet.Add(contactTwo);
            Assert.IsTrue(hashSet.Count == 1);
            contactTwo = new ContactInformation(phoneOne,
                new EmailAddress("carlton@hoolywood.com"));
            hashSet.Add(contactTwo);
            Assert.IsTrue(hashSet.Count == 2);
        }
    }
}