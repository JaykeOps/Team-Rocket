using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Helper_Classes;
using Domain.Value_Objects;
using DomainTests.Test_Dummies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests.Helper_Classes
{
    [TestClass]
    public class EntityValidationTest
    {
        [TestMethod]
        public void GameIsValidWorksOnGameDate()
        {
            var series = new DummySeries();
            var game = series.DummyGames.GameOne;
            game.MatchDate=new MatchDateAndTime(new DateTime(2016,12,30,19,30,00));
            Assert.IsTrue(game.IsValidGame());
            game.MatchDate = null;
           // Assert.IsFalse(game.IsValidGame());
        }

        [TestMethod]
        public void MatchIsValidTest()
        {
            var series = new DummySeries();
            var matchs = series.SeriesDummy.Schedule[1];
            var match = matchs.First();
            Assert.IsTrue(match.IsMatchValid());
        }

    }
}
