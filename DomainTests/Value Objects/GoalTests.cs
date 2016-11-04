using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainTests.Entities;
using football_series_manager.Domain.Entities;

namespace Domain.Value_Objects.Tests
{
    [TestClass]
    public class GoalTests
    {
        MatchMinute matchMinuteOne = new MatchMinute(11);
        MatchMinute matchMinuteTwo = new MatchMinute(11);
        Player player = new Player(new Name("John", "Doe"), new DateOfBirth("1975-04-18"), 
            new ContactInformation(new PhoneNumber("073-4215687"), new EmailAddress("qwerty@gmail.se")), 
            PlayerPosition.Defender, PlayerStatus.Available, new ShirtNumber(25));

        [TestMethod]
        public void GoalCanBeAssignedMatchMinuteEqual()
        {
            var goal = new Goal(matchMinuteOne, player);
            Assert.IsTrue(goal.MatchMinute == matchMinuteTwo);
        }

        [TestMethod]
        public void GoalCanBeAssignedMatchMinuteNotEqual()
        {
            var goal = new Goal(matchMinuteOne, player);
            Assert.IsFalse(goal.MatchMinute != matchMinuteTwo);
        }
    }
}