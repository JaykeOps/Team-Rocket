using Domain.Entities;
using Domain.Services;
using Domain.Value_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace DomainTests.Entities.Tests
{
    [TestClass]
    public class PlayerTests
    {
        private Player testPlayer;

        public PlayerTests()
        {
            var name = new Name("John", "Doe");
            var dateOfBirth = new DateOfBirth("1974-08-24");
            var contactInformation = new ContactInformation(new PhoneNumber("0735-688231"),
                new EmailAddress("johnDoe_84@hotmail.com"));
            this.testPlayer = new Player(name, dateOfBirth, PlayerPosition.Forward,
                PlayerStatus.Available);
        }

        [TestMethod]
        public void PlayerCanHoldValidEntries()
        {
            var service = new TeamService();
            var team = service.GetAll().First();
            this.testPlayer.TeamId = team.Id;
            this.testPlayer.ShirtNumber = new ShirtNumber(25);

            Assert.IsTrue(this.testPlayer.Id != Guid.Empty);
            Assert.IsTrue(this.testPlayer.Name.FirstName == "John");
            Assert.IsTrue(this.testPlayer.Name.LastName == "Doe");
            Assert.IsTrue($"{this.testPlayer.DateOfBirth.Value:yyyy-MM-dd}" == "1974-08-24");
            Assert.IsTrue(this.testPlayer.Position == PlayerPosition.Forward);
            Assert.IsTrue(this.testPlayer.Status == PlayerStatus.Available);
            Assert.IsTrue(this.testPlayer.ShirtNumber.Value == 25);
            Assert.IsTrue(this.testPlayer.TeamId == team.Id);
        }

        [TestMethod]
        public void PlayerNameCanChange()
        {
            Assert.IsFalse(this.testPlayer.Name.FirstName == "Marco"
                && this.testPlayer.Name.LastName == "Polo");
            this.testPlayer.Name = new Name("Marco", "Polo");
            Assert.IsTrue(this.testPlayer.Name.FirstName == "Marco"
                && this.testPlayer.Name.LastName == "Polo");
        }

        [TestMethod]
        public void PlayerDateOfBirthCanChange()
        {
            Assert.IsFalse($"{this.testPlayer.DateOfBirth.Value:yyyy-MM-dd}" == "1994-07-24");
            this.testPlayer.DateOfBirth = new DateOfBirth("1994-07-24");
            Assert.IsTrue($"{this.testPlayer.DateOfBirth.Value:yyyy-MM-dd}" == "1994-07-24");
        }

        [TestMethod]
        public void PlayerPositionCanChange()
        {
            Assert.IsFalse(this.testPlayer.Position == PlayerPosition.MidFielder);
            this.testPlayer.Position = PlayerPosition.MidFielder;
            Assert.IsTrue(this.testPlayer.Position == PlayerPosition.MidFielder);
        }

        [TestMethod]
        public void PlayerStatusCanChange()
        {
            Assert.IsFalse(this.testPlayer.Status == PlayerStatus.Injured);
            this.testPlayer.Status = PlayerStatus.Injured;
            Assert.IsTrue(this.testPlayer.Status == PlayerStatus.Injured);
        }

        [TestMethod()]
        public void PlayerTeamIdCanChange()
        {
            var newTeamId = Guid.NewGuid();
            Assert.IsFalse(this.testPlayer.TeamId == newTeamId);
            this.testPlayer.TeamId = newTeamId;
            Assert.IsTrue(this.testPlayer.TeamId == newTeamId);
        }
    }
}