using Domain.Entities;
using Domain.Value_Objects;

namespace Domain.Interfaces
{
    internal interface IGameEvent
    {
        MatchMinute MatchMinute { get; }
        Player Player { get; } // Alternatively, Player.Name could perhaps be used instead.
    }
}