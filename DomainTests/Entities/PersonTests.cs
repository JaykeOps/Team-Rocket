using Microsoft.VisualStudio.TestTools.UnitTesting;
using football_series_manager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace football_series_manager.Domain.Entities.Tests
{
    [TestClass()]
    public class PersonTests
    {
        [TestMethod()]
        public void PersonIdIsNotEmptyGuid()
        {
            Assert.IsTrue(Guid.Empty != Guid.Empty);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Assert.Fail();
        }
    }
}