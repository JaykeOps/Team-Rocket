using DomainTests.Entities;
using football_series_manager.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Value_Objects.Tests
{
    [TestClass]
    public class PenaltyTests
    {
        [TestMethod]
        public void PenaltyIsEqualToValidEntry()
        {
            var contactInformation = new ContactInformation(new PhoneNumber("0739-246788"),
                new EmailAddress("johnDoe_48@hotmail.com"));
            var player = new Player(new Name("John", "Doe"), new DateOfBirth("1988-05-22"),
                contactInformation, PlayerPosition.GoalKeeper, PlayerStatus.Available,
                new ShirtNumber(25));
            var penalty = new Penalty(new MatchMinute(36), player);

            Assert.IsTrue(penalty.Player.Name == new Name("John", "Doe"));
            Assert.IsTrue(penalty.Player.DateOfBirth == new DateOfBirth("1988-05-22"));
            Assert.IsTrue(penalty.Player.ContactInformation.Phone == new PhoneNumber("0739-246788"));
            Assert.IsTrue(penalty.Player.ContactInformation.Email == new EmailAddress("johnDoe_48@hotmail.com"));
            Assert.IsTrue(penalty.Player.Position == PlayerPosition.GoalKeeper);
            Assert.IsTrue(penalty.Player.Status == PlayerStatus.Available);
            Assert.IsTrue(penalty.Player.ShirtNumber == new ShirtNumber(25));
            Assert.IsTrue(penalty.MatchMinute.Value == 36);
        }

        //TODO: Test override bool and GetHashCode
    }
}