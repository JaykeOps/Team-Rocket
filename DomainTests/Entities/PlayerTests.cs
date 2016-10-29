using Domain.Value_Objects;
using football_series_manager.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DomainTests.Entities.Tests
{
    [TestClass()]
    public class PlayerTests
    {
        [TestMethod()]
        public void PlayerCanHoldValidEntries()
        {
            var name = new Name("John", "Doe");
            var dateOfBirth = new DateOfBirth("1974-08-24");
            var contactInformation = new ContactInformation(new PhoneNumber("0735-688231"),
                new EmailAddress("johnDoe_84@hotmail.com"));

            var player = new Player(name, dateOfBirth, contactInformation, PlayerPosition.Forward,
                PlayerStatus.Available, new ShirtNumber(25));

            Assert.IsTrue(player.Id != Guid.Empty
               && player.Name.FirstName == "John" && player.Name.LastName == "Doe"
               && $"{player.DateOfBirth.Value:yyyy-MM-dd}" == "1974-08-24"
               && player.ContactInformation.Phone.Value == "0735-688231"
               && player.ContactInformation.Email.Value == "johnDoe_84@hotmail.com"
               && player.Position == PlayerPosition.Forward
               && player.Status == PlayerStatus.Available
               && player.ShirtNumber.Value == 25);
        }
    }
}