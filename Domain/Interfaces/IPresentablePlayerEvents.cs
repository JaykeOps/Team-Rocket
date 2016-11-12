using Domain.Entities;
using Domain.Value_Objects;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IPresentablePlayerEvents
    {
        string PlayerName { get; }
        IEnumerable<Goal> Goals { get; }
        IEnumerable<Assist> Assists { get; }
        IEnumerable<Card> Cards { get; }
        IEnumerable<Penalty> Penalties { get; }
        IEnumerable<Game> Games { get; }
    }
}