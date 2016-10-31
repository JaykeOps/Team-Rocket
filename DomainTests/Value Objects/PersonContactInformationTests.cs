using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Value_Objects.Tests
{
    [TestClass()]
    public class PersonContactInformationTests
    {
        private ContactInformation contactInformation;

        public PersonContactInformationTests()
        {
            var phone = new PhoneNumber("0734-668899");
            var email = new EmailAddress("johnDoe@outlook.com");
            this.contactInformation = new ContactInformation(phone, email);
        }

        [TestMethod()]
        public void PersonContactInformationCanBeInitiatedWithValidArguments()
        {
            Assert.IsTrue(contactInformation.Phone.Value == "0734-668899"
                && contactInformation.Email.Value == "johnDoe@outlook.com");
        }

        [TestMethod()]
        public void PersonContactInformationPhoneNumberCanChange()
        {
            this.contactInformation.Phone = new PhoneNumber("0739-884477");
            Assert.IsTrue(this.contactInformation.Phone.Value == "0739-884477");
        }

        [TestMethod()]
        public void PersonContactInformationEmailAddressCanChange()
        {
            this.contactInformation.Email = new EmailAddress("marco_polo@gm.com");
            Assert.IsTrue(this.contactInformation.Email.Value == "marco_polo@gm.com");
        }
    }
}