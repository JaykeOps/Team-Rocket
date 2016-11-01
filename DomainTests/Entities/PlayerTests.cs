using Domain.Value_Objects;
using football_series_manager.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DomainTests.Entities.Tests
{
    [TestClass()]
    public class PlayerTests
    {
        private Player testPlayer;

        public PlayerTests()
        {
            var name = new Name("John", "Doe");
            var dateOfBirth = new DateOfBirth("1974-08-24");
            var contactInformation = new ContactInformation(new PhoneNumber("0735-688231"),
                new EmailAddress("johnDoe_84@hotmail.com"));
            this.testPlayer = new Player(name, dateOfBirth, contactInformation, PlayerPosition.Forward,
                PlayerStatus.Available, new ShirtNumber(25));
        }

        [TestMethod()]
        public void PlayerCanHoldValidEntries()
        {
            Assert.IsTrue(this.testPlayer.Id != Guid.Empty
               && this.testPlayer.Name.FirstName == "John"
               && this.testPlayer.Name.LastName == "Doe"
               && $"{this.testPlayer.DateOfBirth.Value:yyyy-MM-dd}" == "1974-08-24"
               && this.testPlayer.ContactInformation.Phone.Value == "0735-688231"
               && this.testPlayer.ContactInformation.Email.Value == "johnDoe_84@hotmail.com"
               && this.testPlayer.Position == PlayerPosition.Forward
               && this.testPlayer.Status == PlayerStatus.Available
               && this.testPlayer.ShirtNumber.Value == 25
               && this.testPlayer.TeamId == Guid.Empty);
        }

        [TestMethod]
        public void PlayerNameCanChange()
        {
            Assert.IsFalse(testPlayer.Name.FirstName == "Marco"
                && testPlayer.Name.LastName == "Polo");
            this.testPlayer.Name = new Name("Marco", "Polo");
            Assert.IsTrue(testPlayer.Name.FirstName == "Marco"
                && testPlayer.Name.LastName == "Polo");
        }

        [TestMethod()]
        public void PlayerDateOfBirthCanChange()
        {
            Assert.IsFalse($"{this.testPlayer.DateOfBirth.Value:yyyy-MM-dd}" == "1994-07-24");
            this.testPlayer.DateOfBirth = new DateOfBirth("1994-07-24");
            Assert.IsTrue($"{this.testPlayer.DateOfBirth.Value:yyyy-MM-dd}" == "1994-07-24");
        }

        [TestMethod()]
        public void PlayerContactInformationPhoneNumberCanChange()
        {
            Assert.IsFalse(this.testPlayer.ContactInformation.Phone.Value == "0739-886677");
            this.testPlayer.ContactInformation.Phone = new PhoneNumber("0739-886677");
            Assert.IsTrue(this.testPlayer.ContactInformation.Phone.Value == "0739-886677");
        }

        [TestMethod()]
        public void PlayerContactInformationEmailAddressCanChange()
        {
            Assert.IsFalse(this.testPlayer.ContactInformation.Email.Value == "ibn_battuta@explorer.com");
            this.testPlayer.ContactInformation.Email = new EmailAddress("ibn_battuta@explorer.com");
            Assert.IsTrue(this.testPlayer.ContactInformation.Email.Value == "ibn_battuta@explorer.com");
        }

        [TestMethod()]
        public void PlayerPositionCanChange()
        {
            Assert.IsFalse(testPlayer.Position == PlayerPosition.MidFielder);
            this.testPlayer.Position = PlayerPosition.MidFielder;
            Assert.IsTrue(this.testPlayer.Position == PlayerPosition.MidFielder);
        }

        [TestMethod()]
        public void PlayerStatusCanChange()
        {
            Assert.IsFalse(this.testPlayer.Status == PlayerStatus.Injured);
            this.testPlayer.Status = PlayerStatus.Injured;
            Assert.IsTrue(this.testPlayer.Status == PlayerStatus.Injured);
            
        }

        [TestMethod()]
        public void PlayerShirtNumberCanChange()
        {
            Assert.IsFalse(this.testPlayer.ShirtNumber.Value == 10);
            this.testPlayer.ShirtNumber = new ShirtNumber(10);
            Assert.IsTrue(this.testPlayer.ShirtNumber.Value == 10);
        }

        [TestMethod]
        [ExpectedException(typeof(ShirtNumberAlreadyInUseException))]
        public void PlayerShirtNumberCanThrowExceptionIfShirtNumberAlreadyInUse()
        {
            this.testPlayer.ShirtNumber = new ShirtNumber(0);
        }

        [TestMethod]
        public void PlayerTeamIdCanChange()
        {
            var newTeamId = Guid.NewGuid();
            Assert.IsFalse(this.testPlayer.TeamId == newTeamId);
            this.testPlayer.TeamId = newTeamId;
            Assert.IsTrue(this.testPlayer.TeamId == newTeamId);
        }

    }
}