using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IPresentablePlayerSeriesStats
    {
        PlayerStats this[Guid seriesId] { get; }
        IEnumerable<PlayerStats> this[params Guid[] seriesIds] { get; }
    }
}