using Domain.CustomExceptions;
using Domain.Interfaces;
using Domain.Services;
using Domain.Value_Objects;
using System;

namespace Domain.Entities
{
    [Serializable]
    public class Game : IGameDuration
    {
        public Guid Id { get; set; } //TODO: set is for test only!
        public Guid SeriesId { get; }
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
                this.HomeTeamId = match.HomeTeamId;
                this.AwayTeamId = match.AwayTeamId;
                this.SeriesId = match.SeriesId;
                this.Location = match.Location;
                MatchDateAndTime dateAndTime;
                this.MatchDate = MatchDateAndTime.TryParse("2017-08-22 10:10", out dateAndTime) //TODO: For tests!
                    ? dateAndTime : match.MatchDate;
                this.Protocol = new GameProtocol(this.HomeTeamId, this.AwayTeamId);
            }
            else
            {
                throw new GameContainsSameTeamTwiceException();
            }
        }

        
    }
}