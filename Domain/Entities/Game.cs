using Domain.CustomExceptions;
using Domain.Interfaces;
using Domain.Value_Objects;
using System;

namespace Domain.Entities
{
    [Serializable]
    public class Game : IGameDuration
    {
        public Guid Id { get; set; }
        public Guid SeriesId { get; }
        public Guid MatchId { get; }
        public MatchDuration MatchDuration { get; }
        public Guid HomeTeamId { get; }
        public Guid AwayTeamId { get; }
        public ArenaName Location { get; set; }
        public MatchDateAndTime MatchDate { get; set; }
        public GameProtocol Protocol { get; }

        public Game(Match match)
        {
            if (match.HomeTeamId != match.AwayTeamId)
            {
                this.Id = Guid.NewGuid();
                this.MatchDuration = match.MatchDuration;
                this.MatchId = match.Id;
                this.HomeTeamId = match.HomeTeamId;
                this.AwayTeamId = match.AwayTeamId;
                this.SeriesId = match.SeriesId;
                this.Location = match.Location;
                this.MatchDate = match.MatchDate;
                this.Protocol = new GameProtocol(this.HomeTeamId, this.AwayTeamId);
            }
            else
            {
                throw new GameContainsSameTeamTwiceException();
            }
        }
    }
}