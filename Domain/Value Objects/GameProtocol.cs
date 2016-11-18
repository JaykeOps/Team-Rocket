using System;
using System.Collections.Generic;

namespace Domain.Value_Objects
{
    [Serializable]
    public class GameProtocol : ValueObject<GameProtocol>
    {
        public Guid HomeTeamId { get; }
        public Guid AwayTeamId { get; }
        private OverTime OverTime { get; set; }
        private GameResult GameResult { get; set; }
        public HashSet<Guid> HomeTeamStartingPlayers { get;}
        public HashSet<Guid> HomeTeamSub { get;}
        public HashSet<Guid> AwayTeamStartingPlayers { get;}
        public HashSet<Guid> AwayTeamSub { get; }
        public List<Goal> Goals { get; }
        public List<Assist> Assists { get; }
        public List<Penalty> Penalties { get; }
        public List<Card> Cards { get; }

        public GameProtocol(Guid homeTeamId, Guid awayTeamId)
        {
            this.HomeTeamId = homeTeamId;
            this.AwayTeamId = awayTeamId;
            this.Goals = new List<Goal>();
            this.Assists = new List<Assist>();
            this.Penalties = new List<Penalty>();
            this.Cards = new List<Card>();
            this.AwayTeamStartingPlayers=new HashSet<Guid>();
            this.HomeTeamStartingPlayers=new HashSet<Guid>();
            this.AwayTeamSub= new HashSet<Guid>();
            this.HomeTeamSub=new HashSet<Guid>();
        }
    }
}