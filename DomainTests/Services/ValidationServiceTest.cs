using Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests.Services
{
    [TestClass]
    public class ValidationServiceTest
    {
        private ValidationService validationService;

        [TestMethod]
        public void ServiceCanExposeValidations()
        {
            validationService = new ValidationService();
            Assert.IsTrue(validationService.IsValidSeriesName("Allsvenskan", true));
            Assert.IsTrue(validationService.IsValidArenaName("Ullevi", true));
            Assert.IsTrue(validationService.IsValidTeamName("Grötpojkarna", true));
        }


    }
}
