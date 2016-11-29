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
            this.validationService = new ValidationService();
            Assert.IsTrue(this.validationService.IsValidSeriesName("Allsvenskan", true));
            Assert.IsTrue(this.validationService.IsValidArenaName("Ullevi", true));
            Assert.IsTrue(this.validationService.IsValidTeamName("Grötpojkarna", true));
        }


    }
}
