using Domain.Value_Objects;
using System;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IPresentablePlayerSeriesEvents
    {
        PlayerEvents this[Guid seriesId] { get; }
        IEnumerable<PlayerEvents> this[params Guid[] seriesIds] { get; }
    }
}