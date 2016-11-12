using Domain.Value_Objects;
using System;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IEvents
    {
        List<Goal> Goals { get; }
        List<Assist> Assists { get; }
        List<Card> Cards { get; }
        List<Penalty> Penalties { get; }
        List<Guid> Games { get; }
    }
}