using Domain.Entities;
using Domain.Helper_Classes;
using Domain.Services;
using Domain.Value_Objects;
using DomainTests.Test_Dummies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DomainTests.Helper_Classes
{
    [TestClass]
    public class ScheduleTest
    {
        private Series series = new Series(new MatchDuration(new TimeSpan(0, 90, 0)), new NumberOfTeams(8), new SeriesName("Allsvenskan"));
        private Schedule schedule;
        private TeamService teamService;

        private Team testTeamOne = new Team(new TeamName("Test One"), new ArenaName("Test One Arena"), new EmailAddress("test1@test.com"));
        private Team testTeamTwo = new Team(new TeamName("Test Two"), new ArenaName("Test Two Arena"), new EmailAddress("test2@test.com"));
        private Team testTeamThree = new Team(new TeamName("Test Three"), new ArenaName("Test Three Arena"), new EmailAddress("test3@test.com"));
        private Team testTeamFour = new Team(new TeamName("Test Four"), new ArenaName("Test Four Arena"), new EmailAddress("test4@test.com"));
        private Team testTeamFive = new Team(new TeamName("Test Five"), new ArenaName("Test Five Arena"), new EmailAddress("test5@test.com"));
        private Team testTeamSix = new Team(new TeamName("Test Six"), new ArenaName("Test Six Arena"), new EmailAddress("test6@test.com"));
        private Team testTeamSeven = new Team(new TeamName("Test Seven"), new ArenaName("Test Seven Arena"), new EmailAddress("test7@test.com"));
        private Team testTeamEight = new Team(new TeamName("Test Eight"), new ArenaName("Test Eight Arena"), new EmailAddress("test8@test.com"));

        public ScheduleTest()
        {
            this.schedule = new Schedule();
            this.teamService = new TeamService();
        }

        [TestMethod]
        public void ScheduleCanGenerateMatchesWithRounds()
        {
            var series = new DummySeries();
            var teams = this.teamService.GetAllTeams();
            foreach (var team in teams)
            {
                this.series.TeamIds.Add(team.Id);
            }
            this.schedule.GenerateSchedule(this.series);
            Assert.IsNotNull(this.series.Schedule);
        }

        [TestMethod]
        public void ScheduleCanGenerateMatchesForSixteenTeams()
        {
            this.teamService.Add(this.testTeamOne);
            this.teamService.Add(this.testTeamTwo);
            this.teamService.Add(this.testTeamThree);
            this.teamService.Add(this.testTeamFour);
            this.teamService.Add(this.testTeamFive);
            this.teamService.Add(this.testTeamSix);
            this.teamService.Add(this.testTeamSeven);
            this.teamService.Add(this.testTeamEight);

            var teams = this.teamService.GetAllTeams();
            foreach (var team in teams)
            {
                this.series.TeamIds.Add(team.Id);
            }
            this.schedule.GenerateSchedule(this.series);
            Assert.IsNotNull(this.series.Schedule);
            Assert.IsFalse(this.series.Schedule.Count == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ScheduleCanHandleOddNumberOfTeams()
        {
            this.teamService.Add(this.testTeamOne);

            var teams = this.teamService.GetAllTeams();
            foreach (var team in teams)
            {
                this.series.TeamIds.Add(team.Id);
            }

            this.schedule.GenerateSchedule(this.series);
        }
    }
}