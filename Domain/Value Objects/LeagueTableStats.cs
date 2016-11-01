using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Value_Objects
{
    
    public class LeagueTableStats
    {
        public Team Team { get; }
        public int GamesPlayed { get; }
        public int Points { get; }
        public int Wins { get; }
        public int Draws { get; }
        public int Losses { get; }
        public int GoalsFor { get; }
        public int GoalsAgainst { get; }
        public int GoalDifference { get; }
        

        
        public LeagueTableStats()
        {
            
        }

        public override string ToString()
        {
            return $"{Team.Name} : {GamesPlayed.ToString()} : {Points.ToString()} : {Wins.ToString()} "
                    + $": {Draws.ToString()} : {Losses.ToString()} : {GoalsFor.ToString()} "
                    + $": {GoalsAgainst.ToString()} : {GoalDifference.ToString()}";
        }
    }
}
