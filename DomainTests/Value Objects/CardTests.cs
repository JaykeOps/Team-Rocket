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
    public class CardTests
    {
        private Card cardOne;
        private Card cardTwo;
        private Card cardThree;

        private Player playerOne;
        private Player playerTwo;
        

        public CardTests()
        {
            this.playerOne = new Player(new Name("John", "Doe"),
                new DateOfBirth("1993-02-04"),
                PlayerPosition.Forward, PlayerStatus.Absent,
                new ShirtNumber(25));

            this.playerTwo = new Player(new Name("John", "Doe"),
                new DateOfBirth("1993-02-04"),
                PlayerPosition.Forward, PlayerStatus.Absent,
                new ShirtNumber(25));

            

            this.cardOne = new Card(new MatchMinute(15), playerOne, CardType.Yellow);
            this.cardTwo = new Card(new MatchMinute(15), playerOne, CardType.Yellow);
            this.cardThree = new Card(new MatchMinute(15), playerTwo, CardType.Red);
        }

        [TestMethod]
        public void CardIsEqualToEntry()
        {
            Assert.IsTrue(cardOne.CardType.Equals(CardType.Yellow));
            Assert.IsTrue(cardOne.MatchMinute.Value.Equals(15));
            Assert.IsTrue(cardOne.Player.Name == new Name("John", "Doe"));
            Assert.IsTrue(cardOne.Player.DateOfBirth == new DateOfBirth("1993-02-04"));
            Assert.IsTrue(cardOne.Player.Position == PlayerPosition.Forward);
            Assert.IsTrue(cardOne.Player.Status == PlayerStatus.Absent);
            Assert.IsTrue(cardOne.Player.ShirtNumber == new ShirtNumber(25));

        }
        [TestMethod]
        public void AssistIsComparableByValue()
        {
            Assert.IsTrue(this.cardOne == this.cardTwo);
            Assert.IsTrue(this.cardOne != cardThree);
            Assert.AreEqual(this.cardOne, this.cardTwo);
            Assert.AreNotEqual(this.cardOne, cardThree);

        }
        [TestMethod]
        public void PenaltyWorksWithHashSet()
        {
            var hashSet = new HashSet<Card> { this.cardOne, this.cardTwo };
            Assert.IsTrue(hashSet.Count == 1);
        }



    }
}