using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Value_Objects.Tests
{
    [TestClass()]
    public class PersonContactInformationTests
    {
        [TestMethod()]
        public void PersonContactInformationCanBeInitiatedWithValidArguments()
        {
            var phone = new PhoneNumber("0734-668899");
            var email = new EmailAddress("johnDoe@outlook.com");
            var contactInformation = new PersonContactInformation(phone, email);

            Assert.IsTrue(contactInformation.Phone.Value == "0734-668899"
                && contactInformation.Email.Value == "johnDoe@outlook.com");
        }
    }
}