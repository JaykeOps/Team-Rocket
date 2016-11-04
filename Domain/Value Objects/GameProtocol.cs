using Domain.Entities;
using System.Collections.Generic;
using System;

namespace Domain.Value_Objects
{
    class GameProtocol : ValueObject<GameProtocol>
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

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
