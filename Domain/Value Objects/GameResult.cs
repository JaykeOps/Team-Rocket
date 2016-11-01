using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Value_Objects
{
    class GameResult : ValueObject
    {
        public TeamName HomeTeam_Name { get; } // Maybe a field instead?
        public TeamName AwayTeam_Name { get; } // Maybe a field instead?
        public int HomeTeam_Score { get; }     // Maybe a field instead?
        public int AwayTeam_Score { get; }     // Maybe a field instead?

        public GameResult(TeamName homeTeam_Name, TeamName awayTeam_Name, int homeTeam_Score, int awayTeam_Score)
        {
            this.HomeTeam_Name = homeTeam_Name;
            this.AwayTeam_Name = awayTeam_Name;
            this.HomeTeam_Score = homeTeam_Score;
            this.AwayTeam_Score = awayTeam_Score;
        }

        public override string ToString()
        {
            return $"{HomeTeam_Name.ToString()}  {HomeTeam_Score} : {AwayTeam_Score}  {AwayTeam_Name.ToString()}"; // E.g. "Hammarby  3 : 0  Malmö"  
        }                                                                                                          // Format style can be discussed...

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(GameResult))
            {
                return false;
            }
            else
            {
                GameResult gameResultObject = (GameResult)obj;
                return (this.HomeTeam_Name.Equals(gameResultObject.HomeTeam_Name) && this.AwayTeam_Name.Equals(gameResultObject.AwayTeam_Name) 
                     && this.HomeTeam_Score == gameResultObject.HomeTeam_Score && this.AwayTeam_Score == gameResultObject.AwayTeam_Score) ? true : false; // Necessary to override TeamName.Equals()!
            }
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public static bool operator !=(GameResult gameResultOne, GameResult gameResultTwo)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(GameResult gameResultOne, GameResult gameResultTwo)
        {
            throw new NotImplementedException();
        }
    }
}
