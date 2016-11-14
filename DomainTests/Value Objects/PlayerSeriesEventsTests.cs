using Domain.Entities;
using Domain.Services;
using Domain.Value_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace DomainTests.Value_Objects
{
    [TestClass]
    public class PlayerSeriesEventsTests
    {
        private Player playerOne;
        private Team teamOne;
        private Series seriesOne;

        [TestInitialize]
        public void Init()
        {
            var playerService = new PlayerService();
            var teamService = new TeamService();
            var seriesService = new SeriesService();
            this.playerOne = playerService.GetAll().ElementAt(0);
            this.teamOne = teamService.GetAll().ElementAt(0);
            this.seriesOne = seriesService.GetAll().ElementAt(0);
        }

        [TestMethod]
        public void PlayerSeriesEventsCanReturnValidEntries()
        {
            this.teamOne.AddSeries(this.seriesOne.Id, this.seriesOne.Schedule);
            this.playerOne.AddSeriesEvents(this.seriesOne.Id);
            this.playerOne.PlayerSeriesEvents
        }
    }
}