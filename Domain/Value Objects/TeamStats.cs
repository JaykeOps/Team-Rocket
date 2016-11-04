using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Value_Objects
{
    
    public class TeamStats
    {
        private int goalDiff;

        public int GamesPlayed { get; }

        public int Points { get; }

        public int Wins { get; }

        public int Draws { get; }

        public int Losses { get; }

        public int GoalsFor { get; }

        public int GoalsAgainst { get; }

        public int GoalDifference
        {
            get
            {
                return goalDiff;
            }
            set
            {
                goalDiff = this.GoalsFor - this.GoalsAgainst;
            }
        }
        

        
        public TeamStats()
        {
            
        }

        public override string ToString()
        {
            return $"{this.GamesPlayed.ToString()} : {this.Points.ToString()} : {this.Wins.ToString()} "
                    + $": {this.Draws.ToString()} : {this.Losses.ToString()} : {this.GoalsFor.ToString()} "
                    + $": {this.GoalsAgainst.ToString()} : {this.GoalDifference.ToString()}";
        }
    }
}
