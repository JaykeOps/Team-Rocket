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
        Card cardOne;
        Card cardTwo;
        Player playerOne;
        Player playerTwo;

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
            this.cardTwo = new Card(new MatchMinute(15), playerTwo, CardType.Yellow);
        }

        [TestMethod]
        public void CardIsEqualToEntry()
        {
            Assert.IsTrue(cardOne.CardType.Equals(CardType.Yellow));
            Assert.IsTrue(cardOne.MatchMinute.Value.Equals(15));
        }


        
    }
}