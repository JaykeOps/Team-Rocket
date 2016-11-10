using Domain.Value_Objects;
using System;

namespace Domain.Interfaces
{
    internal interface IGameEvent
    {
        MatchMinute MatchMinute { get; }
        Guid PlayerId { get; }
    }
}