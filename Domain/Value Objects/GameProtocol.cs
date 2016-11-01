using Domain.Entities;
using System.Collections.Generic;
using System;

namespace Domain.Value_Objects
{
    class GameProtocol : ValueObject
    {
        Team homeTeam; // Or should it be a property?
        Team awayTeam; // Or should it be a property?

        OverTime OverTime { get; }
        GameResult GameResult { get; }
        List<Goal> Goals { get; }
        List<Assist> Assists { get; }
        List<Penalty> Penalties { get; }
        List<Card> Cards { get; }


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
            throw new 
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
