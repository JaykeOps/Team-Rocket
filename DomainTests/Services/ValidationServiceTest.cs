using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Helper_Classes;
using Domain.Value_Objects;
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
