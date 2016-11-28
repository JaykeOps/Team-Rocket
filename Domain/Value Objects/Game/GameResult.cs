using Domain.Helper_Classes;
using Domain.Services;
using System;

namespace Domain.Value_Objects
{
    [Serializable]
    public class GameResult : ValueObject<GameResult>
    {
        public Guid HomeTeamId { get; }
        public Guid AwayTeamId { get; }
        public int HomeTeamScore { get; }
        public int AwayTeamScore { get; }

        public GameResult(Guid homeTeamId, Guid awayTeamId, int homeTeamScore, int awayTeamScore)
        {
            this.HomeTeamId = homeTeamId;
            this.AwayTeamId = awayTeamId;

            if (homeTeamScore.IsScoreValid())
            {
                this.HomeTeamScore = homeTeamScore;
            }
            else
            {
                throw new ArgumentException("Score can only be between 0-50.");
            }

            if (awayTeamScore.IsScoreValid())
            {
                this.AwayTeamScore = awayTeamScore;
            }
            else
            {
                throw new ArgumentException("Score can only be between 0-50.");
            }
        }

        public override string ToString()
        {
            return $"{DomainService.FindTeamById(this.HomeTeamId)}  {this.HomeTeamScore} : {this.AwayTeamScore}  {DomainService.FindTeamById(this.AwayTeamId)}";
        }
    }
}