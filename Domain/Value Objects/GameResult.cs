using Domain.Entities;
using System;
using Domain.Services;

namespace Domain.Value_Objects
{
    [Serializable]
    public class GameResult : ValueObject<GameResult>
    {
        public Guid HomeTeamId { get; } // Maybe a field instead?
        public Guid AwayTeamId { get; } // Maybe a field instead?
        public int HomeTeam_Score { get; }     // Maybe a field instead?
        public int AwayTeam_Score { get; }     // Maybe a field instead?

        public GameResult(Guid homeTeamId, Guid awayTeamId, int homeTeam_Score, int awayTeam_Score)
        {
            this.HomeTeamId = homeTeamId;
            this.AwayTeamId = awayTeamId;

            if (this.IsScoreValid(homeTeam_Score))
            {
                this.HomeTeam_Score = homeTeam_Score;
            }
            else
            {
                throw new ArgumentException("Score can only be between 0-50.");
            }

            if (this.IsScoreValid(awayTeam_Score))
            {
                this.AwayTeam_Score = awayTeam_Score;
            }
            else
            {
                throw new ArgumentException("Score can only be between 0-50.");
            }
        }

        private bool IsScoreValid(int score)
        {
            if (score >= 0 && score <= 50)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return $"{DomainService.FindTeamById(this.HomeTeamId)}  {this.HomeTeam_Score} : {this.AwayTeam_Score}  {DomainService.FindTeamById(this.AwayTeamId)}"; // E.g. "Hammarby  3 : 0  Malmö"
        }                                                                                                          // Format style can be discussed...
    }
}