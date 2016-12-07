using Domain.Services;
using System;
using System.Collections.Generic;

namespace Domain.Value_Objects
{
    [Serializable]
    public class GameProtocol : ValueObject<GameProtocol>
    {
        private GameResult gameResult;

        public Guid HomeTeamId { get; }
        public Guid AwayTeamId { get; }
        public OverTime OverTime { get; set; }
        public HashSet<Guid> HomeTeamActivePlayers { get; }
        public HashSet<Guid> AwayTeamActivePlayers { get; }
        public List<Goal> Goals { get; }
        public List<Assist> Assists { get; }
        public List<Penalty> Penalties { get; }
        public List<Card> Cards { get; }
        public GameResult GameResult => this.gameResult;

        public GameProtocol(Guid homeTeamId, Guid awayTeamId)
        {
            this.HomeTeamId = homeTeamId;
            this.AwayTeamId = awayTeamId;
            this.Goals = new List<Goal>();
            this.Assists = new List<Assist>();
            this.Penalties = new List<Penalty>();
            this.Cards = new List<Card>();
            this.AwayTeamActivePlayers = new HashSet<Guid>();
            this.HomeTeamActivePlayers = new HashSet<Guid>();
        }

        public void UpdateGameResult()
        {
            this.gameResult = DomainService.GetGameResult(this);
        }
    }
}