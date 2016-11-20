using Domain.Entities;
using DomainTests.Test_Dummies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Value_Objects.Tests
{
    [TestClass]
    public class TeamSeriesEventsTests
    {
        private DummySeries dummySeries;
        private Team teamFour;

        [TestInitialize]
        public void Init()
        {
            this.dummySeries = new DummySeries();
            this.teamFour = this.dummySeries.DummyTeams.DummyTeamFour;
        }

        [TestMethod]
        public void TeamSeriesEventGamesIsNotNull()
        {
            Assert.IsNotNull(this.teamFour.SeriesEvents[this.dummySeries.SeriesDummy.Id].Games.Count());
        }

        [TestMethod]
        public IEnumerable<Game> TeamSeriesEventGamesCanBeAccessed()
        {
            var games = this.teamFour.SeriesEvents[this.dummySeries.SeriesDummy.Id].Games;
            Assert.IsNotNull(games);
            return games;
        }

        [TestMethod()]
        public void TeamSeriesStatsCanShowCorrectTeamWinCountBasedOnEvents()
        {
            var gamesWon = 0;
            var games = this.TeamSeriesEventGamesCanBeAccessed();
            foreach (var game in games)
            {
                if (game.Protocol.HomeTeamId == this.teamFour.Id)
                {
                    if (game.Protocol.GameResult.HomeTeam_Score > game.Protocol.GameResult.AwayTeam_Score)
                    {
                        gamesWon++;
                    }
                }
                else if (game.Protocol.AwayTeamId == this.teamFour.Id)
                {
                    if (game.Protocol.GameResult.HomeTeam_Score < game.Protocol.GameResult.AwayTeam_Score)
                    {
                        gamesWon++;
                    }
                }
            }
            var teamWins = this.teamFour.PresentableSeriesStats[this.dummySeries.SeriesDummy.Id].Wins;
            Assert.IsTrue(teamWins != 0);
            Assert.IsTrue(gamesWon == teamWins);
        }
    }
}