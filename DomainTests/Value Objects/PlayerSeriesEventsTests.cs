using System;
using Domain.Entities;
using Domain.Helper_Classes;
using Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Domain.Value_Objects;
using DomainTests.Test_Dummies;

namespace DomainTests.Value_Objects
{
    [TestClass]
    public class PlayerSeriesEventsTests
    {
        private DummySeries dummySeries;
        private Team teamOne;
        private Player playerOne;

        [TestInitialize]
        public void Init()
        {
            this.dummySeries = new DummySeries();
            this.teamOne = this.dummySeries.DummyTeams.DummyTeamOne;
            this.playerOne = this.dummySeries.DummyTeams.DummyTeamOne.Players.ElementAt(0);
        }

        [TestMethod]
        public void PlayerSeriesEventsIsUpdateWhenNewGoalIsAdded()
        {
            
            Assert.IsNotNull(this.playerOne.SeriesEvents[this.dummySeries.SeriesDummy.Id]);
            var playerOneGoalsPreAdd = this.playerOne.PresentableSeriesEvents[this.dummySeries.SeriesDummy.Id].Goals.Count();
            this.dummySeries.DummyGames.GameTwo.Protocol.Goals.Add(new Goal(new MatchMinute(14), this.playerOne.TeamId, this.playerOne.Id));
            var playerOneGoalsPostAdd = this.playerOne.SeriesEvents[this.dummySeries.SeriesDummy.Id].Goals.Count();
            Assert.IsTrue(playerOneGoalsPostAdd - playerOneGoalsPreAdd == 1);
        }
    }
}