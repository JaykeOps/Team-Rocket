using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICountablePlayerStat
    {
        int GoalCount { get; }
        int AssistCount { get; }
        int YellowCardCount { get; }
        int RedCardCount { get; }
        int PenaltyCount { get; }
    }
}
