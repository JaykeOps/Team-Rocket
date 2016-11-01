using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Value_Objects
{
    internal class GameProtocol : ValueObject
    {
        private Team homeTeam; // Or should it be a property?
        private Team awayTeam; // Or should it be a property?

        private OverTime OverTime { get; }
        private GameResult GameResult { get; }
        private List<Goal> Goals { get; }
        private List<Assist> Assists { get; }
        private List<Penalty> Penalties { get; }
        private List<Card> Cards { get; }

        public GameProtocol(Team homeTeam, Team awayTeam) // More arguments may be needed.
        {
            this.homeTeam = homeTeam;
            this.awayTeam = awayTeam;
            this.Goals = new List<Goal>();
            this.Assists = new List<Assist>();
            this.Penalties = new List<Penalty>();
            this.Cards = new List<Card>();
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
            //if (obj.GetType() != typeof(GameProtocol))
            //{
            //    return false;
            //}
            //else
            //{
            //    GameProtocol gameProtocolObject = (GameProtocol)obj;
            //    return (...) ? true : false;   // This is a tough one...
            //}
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public static bool operator !=(GameProtocol gameProtocolOne, GameProtocol gameProtocolTwo)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(GameProtocol gameProtocolOne, GameProtocol gameProtocolTwo)
        {
            throw new NotImplementedException();
        }
    }
}