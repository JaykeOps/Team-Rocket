using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Helper_Classes;
using Domain.Value_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests.Helper_Classes
{
    [TestClass]
    public class ExposeableValidationTest
    {
        private ExposeableValidations exposeableValidations;

        [TestMethod]
        public void ClassCanExposeValidations()
        {
            exposeableValidations = new ExposeableValidations();

            var seriesName = new SeriesName("Allsvenskan");
            Assert.IsTrue(seriesName.Value == "Allsvenskan");
            exposeableValidations.IsValidSeriesName("Allsvenskan", true);
        }
    }
}
