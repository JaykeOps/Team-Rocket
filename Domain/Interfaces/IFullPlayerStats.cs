using Domain.Entities;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IFullPlayerStats
    {
        List<Goal> GoalStats { get; }
        List<Assist> AssistStats { get; }
        List<Card> CardStats { get; }
        List<Penalty> PenaltyStats { get; }
        List<Guid> GamesPlayedIds { get; }
    }
}
