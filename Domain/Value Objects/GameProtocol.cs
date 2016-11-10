using System;
using System.Collections.Generic;

namespace Domain.Value_Objects
{
    public class GameProtocol : ValueObject<GameProtocol>
    {
        public Guid HomeTeamId { get; } 
        public Guid AwayTeamId { get; } 
        private OverTime OverTime { get; set; }
        private GameResult GameResult { get; set; }
        private List<Goal> Goals { get; }
        private List<Assist> Assists { get; }
        private List<Penalty> Penalties { get; }
        private List<Card> Cards { get; }

        public GameProtocol(Guid homeTeamId, Guid awayTeamId) // More arguments may be needed.
        {
            this.HomeTeamId = homeTeamId;
            this.AwayTeamId = awayTeamId;
            this.Goals = new List<Goal>();
            this.Assists = new List<Assist>();
            this.Penalties = new List<Penalty>();
            this.Cards = new List<Card>();
        }


       

        
    }
}