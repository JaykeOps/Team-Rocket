using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Helper_Classes;
using Domain.Entities;
using Domain.Value_Objects;
using Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests.Helper_Classes
{
    [TestClass]
    public class ScheduleTest
    {
        private Series series = new Series(new MatchDuration(new TimeSpan(0, 90, 0)), new NumberOfTeams(8));
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
            var teams = teamService.GetAll();
            foreach (var team in teams)
            {
                series.TeamIds.Add(team.Id);
            }
            var generatedMatchSchedule = schedule.GenerateSchedule(series);
            Assert.IsNotNull(generatedMatchSchedule);
        }

        [TestMethod]
        public void ScheduleCanGenerateMatchesForSixteenTeams()
        {
            this.teamService.AddTeam(testTeamOne);
            this.teamService.AddTeam(testTeamTwo);
            this.teamService.AddTeam(testTeamThree);
            this.teamService.AddTeam(testTeamFour);
            this.teamService.AddTeam(testTeamFive);
            this.teamService.AddTeam(testTeamSix);
            this.teamService.AddTeam(testTeamSeven);
            this.teamService.AddTeam(testTeamEight);

            var teams = teamService.GetAll();
            foreach (var team in teams)
            {
                series.TeamIds.Add(team.Id);
            }
            var generatedMatchSchedule = schedule.GenerateSchedule(series);
            Assert.IsNotNull(generatedMatchSchedule);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ScheduleCanHandleOddNumberOfTeams()
        {
            this.teamService.AddTeam(testTeamOne);

            var teams = teamService.GetAll();
            foreach (var team in teams)
            {
                series.TeamIds.Add(team.Id);
            }

            schedule.GenerateSchedule(series);
        }
    }
}