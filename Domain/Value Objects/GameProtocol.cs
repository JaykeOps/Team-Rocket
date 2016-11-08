using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Value_Objects
{
    internal class GameProtocol : ValueObject<GameProtocol>
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


       

        
    }
}