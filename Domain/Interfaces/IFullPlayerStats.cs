using Domain.Value_Objects;
using System;
using System.Collections.Generic;

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