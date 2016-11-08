using Microsoft.VisualStudio.TestTools.UnitTesting;
using DomainTests.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Value_Objects;

namespace DomainTests.Entities.Tests
{
    [TestClass]
    public class PlayerStatsTests
    {
        PlayerStats playerStatsOne;
        PlayerStats playerStatsTwo;
        public PlayerStatsTests()
        {
            Player
            playerStatsOne = new PlayerStats();
            playerStatsTwo = new PlayerStats();
        }
        [TestMethod]
        public void PlayerStatsTest()
        {
            Assert.Fail();
        }
    }
}