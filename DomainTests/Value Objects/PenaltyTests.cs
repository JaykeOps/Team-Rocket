using DomainTests.Entities;
using football_series_manager.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Domain.Value_Objects.Tests
{
    [TestClass]
    public class PenaltyTests
    {
        private Penalty penaltyOne;
        private Penalty penaltyTwo;

        public PenaltyTests()
        {
            var contactInformation = new ContactInformation(new PhoneNumber("0739-246788"),
                new EmailAddress("johnDoe_48@hotmail.com"));
            var player = new Player(new Name("John", "Doe"), new DateOfBirth("1988-05-22"),
                PlayerPosition.GoalKeeper, PlayerStatus.Available,
                new ShirtNumber(25));

            this.penaltyOne = new Penalty(new MatchMinute(36), player);
            this.penaltyTwo = new Penalty(new MatchMinute(36), player);
        }

        [TestMethod]
        public void PenaltyIsEqualToValidEntry()
        {
            Assert.IsTrue(this.penaltyOne.Player.Name == new Name("John", "Doe"));
            Assert.IsTrue(this.penaltyOne.Player.DateOfBirth.Value == new DateOfBirth("1988-05-22").Value);
            Assert.IsTrue(this.penaltyOne.Player.Position == PlayerPosition.GoalKeeper);
            Assert.IsTrue(this.penaltyOne.Player.Status == PlayerStatus.Available);
            Assert.IsTrue(this.penaltyOne.Player.ShirtNumber == new ShirtNumber(25));
            Assert.IsTrue(this.penaltyOne.MatchMinute.Value == 36);
        }

        [TestMethod]
        public void PenaltyIsComparableByValue()
        {
            Assert.AreEqual(this.penaltyOne, this.penaltyTwo);
            Assert.IsTrue(this.penaltyOne == this.penaltyTwo);
        }

        [TestMethod]
        public void PenaltyWorksWithHashSet()
        {
            var hashSet = new HashSet<Penalty>();
            hashSet.Add(this.penaltyOne);
            hashSet.Add(this.penaltyTwo);
            Assert.IsTrue(hashSet.Count == 1);
        }
    }
}