using System.Collections.Generic;
using Domain.Entities;
using Domain.Value_Objects;
using DomainTests.Entities;
using football_series_manager.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests.Value_Objects
{
    [TestClass]
    public class AssistTests
    {
        private Assist assistOne;
        private Assist assistTwo;
        private Assist assistThree;

        public AssistTests()
        {

            var player = new Player(new Name("Arne", "Anka"), new DateOfBirth("1985-05-20"), PlayerPosition.MidFielder,
                PlayerStatus.Available, new ShirtNumber(80));
            this.assistOne = new Assist(new MatchMinute(30), player);
            this.assistTwo = new Assist(new MatchMinute(30), player);
            this.assistThree = new Assist(new MatchMinute(30),
                new Player(new Name("Arne", "Anka"), new DateOfBirth("1985-05-20"), PlayerPosition.MidFielder, PlayerStatus.Available, new ShirtNumber(45)));

        }

        [TestMethod]
        public void AssistIsEqualToValidEntry()
        {
            Assert.IsTrue(this.assistOne.MatchMinute.Value == 30);
            Assert.IsTrue(this.assistOne.Player.Name == new Name("Arne", "Anka"));
            Assert.IsTrue(this.assistOne.Player.DateOfBirth.Value == new DateOfBirth("1985-05-20").Value);
            Assert.IsTrue(this.assistOne.Player.Position == PlayerPosition.MidFielder);
            Assert.IsTrue(this.assistOne.Player.Status == PlayerStatus.Available);
            Assert.IsTrue(this.assistOne.Player.ShirtNumber.Value == new ShirtNumber(80).Value);

        }

        [TestMethod]
        public void AssistIsComparableByValue()
        {
            Assert.IsTrue(this.assistOne == this.assistTwo);
            Assert.IsTrue(this.assistOne != assistThree);
            Assert.AreEqual(this.assistOne, this.assistTwo);
            Assert.AreNotEqual(this.assistTwo, assistThree);

        }
        [TestMethod]
        public void PenaltyWorksWithHashSet()
        {
            var hashSet = new HashSet<Assist> { this.assistOne, this.assistTwo };
            Assert.IsTrue(hashSet.Count == 1);
        }
    }
}