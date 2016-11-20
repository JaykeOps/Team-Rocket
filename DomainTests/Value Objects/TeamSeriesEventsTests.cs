using Domain.Entities;
using DomainTests.Test_Dummies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Domain.Value_Objects.Tests
{
    [TestClass]
    public class TeamSeriesEventsTests
    {
        private DummySeries dummySeries;
        private Team teamTwo;

        [TestInitialize]
        public void Init()
        {
            this.dummySeries = new DummySeries();
            this.teamTwo = this.dummySeries.DummyTeams.DummyTeamTwo;
        }

        [TestMethod]
        public void TeamSeriesEventGamesIsNotNull()
        {
            Assert.IsNotNull(this.teamTwo.SeriesEvents[this.dummySeries.SeriesDummy.Id].Games);
        }

        [TestMethod]
        public void TeamSeriesStatsCanShowCorrectTeamWinCountBasedOnEvents()
        {
            var games = this.teamTwo.SeriesEvents[this.dummySeries.SeriesDummy.Id].Games;
            Assert.IsNotNull(games);
            var gamesWon = 0;
            foreach (var game in games)
            {
                if (game.Protocol.HomeTeamId == this.teamTwo.Id)
                {
                    if (game.Protocol.GameResult.HomeTeam_Score > game.Protocol.GameResult.AwayTeam_Score)
                    {
                        gamesWon++;
                    }
                }
                else if (game.Protocol.AwayTeamId == this.teamTwo.Id)
                {
                    if (game.Protocol.GameResult.HomeTeam_Score < game.Protocol.GameResult.AwayTeam_Score)
                    {
                        gamesWon++;
                    }
                }
            }
            var teamWins = this.teamTwo.PresentableSeriesStats[this.dummySeries.SeriesDummy.Id].Wins;
            Assert.IsTrue(teamWins != 0);
            Assert.IsTrue(gamesWon == teamWins);
        }

        [TestMethod]
        public void TeamSeriesStatsCanShowCorrectTeamDrawCountBasedOnEvents()
        {
            var games = this.teamTwo.SeriesEvents[this.dummySeries.SeriesDummy.Id].Games;
            Assert.IsNotNull(games);
            var gamesDraw = 0;
            foreach (var game in games)
            {
                if (game.Protocol.HomeTeamId == this.teamTwo.Id)
                {
                    if (game.Protocol.GameResult.HomeTeam_Score == game.Protocol.GameResult.AwayTeam_Score)
                    {
                        gamesDraw++;
                    }
                }
                else if (game.Protocol.AwayTeamId == this.teamTwo.Id)
                {
                    if (game.Protocol.GameResult.HomeTeam_Score == game.Protocol.GameResult.AwayTeam_Score)
                    {
                        gamesDraw++;
                    }
                }
            }
            var teamDraws = this.teamTwo.PresentableSeriesStats[this.dummySeries.SeriesDummy.Id].Draws;
            Assert.IsTrue(teamDraws != 0);
            Assert.IsTrue(gamesDraw == teamDraws);
        }

        [TestMethod]
        public void TeamSeriesStatsCanShowCorrectTeamLossCountBasedOnEvents()
        {
            var games = this.teamTwo.SeriesEvents[this.dummySeries.SeriesDummy.Id].Games;
            Assert.IsNotNull(games);
            var gamesLost = 0;
            foreach (var game in games)
            {
                if (game.Protocol.HomeTeamId == this.teamTwo.Id)
                {
                    if (game.Protocol.GameResult.HomeTeam_Score < game.Protocol.GameResult.AwayTeam_Score)
                    {
                        gamesLost++;
                    }
                }
                else if (game.Protocol.AwayTeamId == this.teamTwo.Id)
                {
                    if (game.Protocol.GameResult.HomeTeam_Score > game.Protocol.GameResult.AwayTeam_Score)
                    {
                        gamesLost++;
                    }
                }
            }
            var teamLosses = this.teamTwo.PresentableSeriesStats[this.dummySeries.SeriesDummy.Id].Losses;
            Assert.IsTrue(teamLosses != 0);
            Assert.IsTrue(gamesLost == teamLosses);
        }

        [TestMethod]
        public void TeamStatsCanShowCorrectTeamScoreBasedOnEvents()
        {
            var teamWins = this.teamTwo.PresentableSeriesStats[this.dummySeries.SeriesDummy.Id].Wins;
            var teamDraws = this.teamTwo.PresentableSeriesStats[this.dummySeries.SeriesDummy.Id].Draws;
            var teamLosses = this.teamTwo.PresentableSeriesStats[this.dummySeries.SeriesDummy.Id].Losses;
            var teamScore = this.teamTwo.PresentableSeriesStats[this.dummySeries.SeriesDummy.Id].Points;
            Assert.IsTrue(teamScore == (3 * teamWins) + (1 * teamDraws) + (0 * teamLosses));
        }
    }
}