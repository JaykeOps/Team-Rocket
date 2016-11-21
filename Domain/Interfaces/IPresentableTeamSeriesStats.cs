using Domain.Value_Objects;
using System;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IPresentableTeamSeriesStats
    {
        TeamStats this[Guid seriesId] { get; }
        IEnumerable<TeamStats> this[params Guid[] seriesId] { get; }
    }
}