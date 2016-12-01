using Domain.Entities;
using Domain.Services;
using Domain.Value_Objects;
using DomainTests.Test_Dummies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace DomainTests.Entities.Tests
{
    [TestClass]
    public class PlayerTests
    {
        private Player testPlayer;
        private DummySeries dummySeries;
        private Team dummyTeam;
        private Player dummyPlayer;

        [TestInitialize]
        public void Init()
        {
            var name = new Name("John", "Doe");
            var dateOfBirth = new DateOfBirth("1974-08-24");
            var contactInformation = new ContactInformation(new PhoneNumber("0735-688231"),
                new EmailAddress("johnDoe_84@hotmail.com"));
            this.testPlayer = new Player(name, dateOfBirth, PlayerPosition.Forward,
                PlayerStatus.Available);

            this.dummySeries = new DummySeries();
            this.dummyTeam = this.dummySeries.DummyTeams.DummyTeamOne;
            this.dummyPlayer = DomainService.FindPlayerById(this.dummyTeam.PlayerIds.ElementAt(1));
        }

        [TestMethod]
        public void PlayerCanHoldValidEntries()
        {
            var service = new TeamService();
            var team = service.GetAllTeams().First();
            this.testPlayer.TeamId = team.Id;
            this.testPlayer.ShirtNumber = new ShirtNumber(this.testPlayer.TeamId, 25);

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
            var newName = new Name("Stefan", "Schwartz");
            Assert.IsNotNull(newName);
            Assert.IsNotNull(this.dummyPlayer.Name);
            Assert.AreNotEqual(this.dummyPlayer.Name, newName);
            this.dummyPlayer.Name = newName;
            Assert.AreEqual(this.dummyPlayer.Name, newName);
            Assert.AreEqual(DomainService.FindPlayerById(this.dummyTeam.PlayerIds.ElementAt(1)).Name, this.dummyPlayer.Name);
        }

        [TestMethod]
        public void PlayerDateOfBirthCanChange()
        {
            var newDateOfBirth = new DateOfBirth("1979-12-24");
            Assert.IsNotNull(newDateOfBirth);
            Assert.IsNotNull(this.dummyPlayer.DateOfBirth);
            Assert.AreNotEqual(this.dummyPlayer.DateOfBirth, newDateOfBirth);
            this.dummyPlayer.DateOfBirth = newDateOfBirth;
            Assert.AreEqual(this.dummyPlayer.DateOfBirth, newDateOfBirth);
            Assert.AreEqual(DomainService.FindPlayerById(this.dummyTeam.PlayerIds.ElementAt(1)).DateOfBirth, newDateOfBirth);
        }

        [TestMethod]
        public void PlayerPositionCanChange()
        {
            var newPosition = PlayerPosition.Defender;
            Assert.IsNotNull(newPosition);
            Assert.IsNotNull(this.dummyPlayer.Position);
            Assert.AreNotEqual(this.dummyPlayer.Position, newPosition);
            this.dummyPlayer.Position = newPosition;
            Assert.AreEqual(this.dummyPlayer.Position, newPosition);
            Assert.AreEqual(DomainService.FindPlayerById(this.dummyTeam.PlayerIds.ElementAt(1)).Position, newPosition);
        }

        [TestMethod]
        public void PlayerStatusCanChange()
        {
            var newStatus = PlayerStatus.Suspended;
            Assert.IsNotNull(newStatus);
            Assert.IsNotNull(this.dummyPlayer.Status);
            Assert.AreNotEqual(this.dummyPlayer.Status, newStatus);
            this.dummyPlayer.Status = newStatus;
            Assert.AreEqual(this.dummyPlayer.Status, newStatus);
            Assert.AreEqual(DomainService.FindPlayerById(this.dummyTeam.PlayerIds.ElementAt(1)).Status, newStatus);
        }

        [TestMethod]
        public void PlayerTeamIdCanChange()
        {
            var newTeamId = this.dummySeries.DummyTeams.DummyTeamTwo.Id;
            Assert.IsNotNull(newTeamId);
            Assert.AreNotEqual(newTeamId, Guid.Empty);
            Assert.IsNotNull(this.dummyPlayer.TeamId);
            Assert.AreNotEqual(this.dummyPlayer.TeamId, Guid.Empty);
            Assert.AreNotEqual(this.dummyPlayer.TeamId, newTeamId);
            this.dummyPlayer.TeamId = newTeamId;
            Assert.AreEqual(this.dummyPlayer.TeamId, newTeamId);
        }
    }
}