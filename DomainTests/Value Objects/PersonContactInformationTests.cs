using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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