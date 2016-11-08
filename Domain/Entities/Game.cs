using Domain.Interfaces;
using Domain.Value_Objects;
using System;

namespace Domain.Entities
{
    public class Game : IGameDuration
    {
        public Guid Id { get; }
        public MatchDuration MatchDuration { get; }
        public Guid HomeTeamId { get; }     // Need to keep track of which team is hometeam and which is awayteam as it matters for
        public Guid AwayTeamId { get; }     // the order in which GameProtocol entries are written - hometeam entries to the left.
        public GameProtocol Protocol { get; }
        // public HashSet<Guid> TeamIds { get; } // Contains the ids for the two teams playing the game. Why Ids?

        //public Dictionary<Team, HashSet<Guid>> TeamSet { get; } // Contains Team as key and the collection of that team's player ids as value.
        //                                                        // Two entries, one for each team in the game.

        public Game(MatchDuration matchDuration, Guid homeTeamId, Guid awayTeamId)
        {
            this.Id = Guid.NewGuid();
            this.MatchDuration = matchDuration;
            this.HomeTeamId = homeTeamId;
            this.AwayTeamId = awayTeamId;
            // this.TeamIds.Add(homeTeam.Id);
            // this.TeamIds.Add(awayTeam.Id);
            this.Protocol = new GameProtocol(homeTeamId, awayTeamId); // More arguments may be needed...
        }
    }
}