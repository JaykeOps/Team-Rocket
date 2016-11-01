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
        MatchMinute matchMinute = new MatchMinute(11);
        Player player = new Player(new Name("John", "Doe"), new DateOfBirth("1975-04-18"), 
            new ContactInformation(new PhoneNumber("0734215687"), new EmailAddress("qwerty@gmail.se")), 
            PlayerPosition.Defender, PlayerStatus.Available, new ShirtNumber(8));

        [TestMethod]
        //[ExpectedException(typeof(NullReferenceException))]
        public void GoalCanBeAssignedMatchMinute()
        {
            var goal = new Goal(new MatchMinute(11), player);
            Assert.IsTrue(matchMinute.Value == 11);
        }
    }
}