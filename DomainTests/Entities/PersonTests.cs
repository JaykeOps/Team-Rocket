using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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