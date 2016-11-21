using Domain.Value_Objects;
using System;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IPresentableTeamSeriesEvents
    {
        TeamEvents this[Guid seriesId] { get; }
        IEnumerable<TeamEvents> this[params Guid[] seriesIds] { get; }
    }
}