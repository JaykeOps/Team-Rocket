using System;

namespace Domain.Value_Objects
{
    public class GameResult : ValueObject
    {
        public TeamName HomeTeam_Name { get; } // Maybe a field instead?
        public TeamName AwayTeam_Name { get; } // Maybe a field instead?
        public int HomeTeam_Score { get; }     // Maybe a field instead?
        public int AwayTeam_Score { get; }     // Maybe a field instead?

        public GameResult(TeamName homeTeam_Name, TeamName awayTeam_Name, int homeTeam_Score, int awayTeam_Score)
        {
            this.HomeTeam_Name = homeTeam_Name;
            this.AwayTeam_Name = awayTeam_Name;

            if (IsScoreValid(homeTeam_Score))
            {
                this.HomeTeam_Score = homeTeam_Score;
            }
            else
            {
                throw new ArgumentException("Score can only be between 0-50.");
            }

            if (IsScoreValid(awayTeam_Score))
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
            return $"{HomeTeam_Name.ToString()}  {HomeTeam_Score} : {AwayTeam_Score}  {AwayTeam_Name.ToString()}"; // E.g. "Hammarby  3 : 0  Malmö"
        }                                                                                                          // Format style can be discussed...

        public override bool Equals(object obj)
        {
            var gameResult = obj as GameResult;
            return gameResult.HomeTeam_Name.Value == this.HomeTeam_Name.Value
                && gameResult.AwayTeam_Name.Value == this.AwayTeam_Name.Value
                && gameResult.HomeTeam_Score == this.HomeTeam_Score
                && gameResult.AwayTeam_Score == this.AwayTeam_Score;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}