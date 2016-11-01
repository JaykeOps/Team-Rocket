using Domain.Interfaces;
using DomainTests.Entities;
using System;

namespace Domain.Value_Objects
{
    public class Card : ValueObject, IGameEvent
    {
        public MatchMinute MatchMinute { get; }
        public Player Player { get; } // The player who got the card.
        public CardType CardType { get; } // Enum: Yellow or Red.

        public Card(MatchMinute matchMinute, Player player, CardType cardType)
        {
            this.MatchMinute = matchMinute;
            this.Player = player;
            this.CardType = cardType;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Card))
            {
                return false;
            }
            else
            {
                Card cardObject = (Card)obj;
                return (this.MatchMinute.Equals(cardObject.MatchMinute) && this.Player.Id == cardObject.Player.Id && ((int)this.CardType) == ((int)cardObject.CardType)) ? true : false; // Necessary to override MatchMinute.Equals()!
            }                                                                                                          // This comparison works as for ints I hope!?
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public static bool operator !=(Card cardOne, Card cardTwo)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(Card cardOne, Card cardTwo)
        {
            throw new NotImplementedException();
        }
    }
}