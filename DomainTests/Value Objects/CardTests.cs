using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Domain.Value_Objects.Tests
{
    [TestClass]
    public class CardTests
    {
        private Card cardOne;
        private Card cardTwo;
        private Card cardThree;
        private Guid playerIdOne;
        private Guid playerIdTwo;

        public CardTests()
        {
            this.playerIdOne = Guid.NewGuid();
            this.playerIdTwo = Guid.NewGuid();
            this.cardOne = new Card(new MatchMinute(15), new Guid(), this.playerIdOne, CardType.Yellow);
            this.cardTwo = new Card(new MatchMinute(15), new Guid(), this.playerIdOne, CardType.Yellow);
            this.cardThree = new Card(new MatchMinute(15), new Guid(), this.playerIdTwo, CardType.Red);
        }

        [TestMethod]
        public void CardIsEqualToEntry()
        {
            Assert.IsTrue(this.cardOne.CardType.Equals(CardType.Yellow));
            Assert.IsTrue(this.cardOne.MatchMinute.Value.Equals(15));
            Assert.IsTrue(this.cardOne.PlayerId == this.playerIdOne);
        }

        [TestMethod]
        public void CardIsComparableByValue()
        {
            Assert.IsTrue(this.cardOne == this.cardTwo);
            Assert.IsTrue(this.cardOne != this.cardThree);
            Assert.AreEqual(this.cardOne, this.cardTwo);
            Assert.AreNotEqual(this.cardOne, this.cardThree);
        }

        [TestMethod]
        public void CardtWorksWithHashSet()
        {
            var hashSet = new HashSet<Card> { this.cardOne, this.cardTwo };
            Assert.IsTrue(hashSet.Count == 1);
        }
    }
}