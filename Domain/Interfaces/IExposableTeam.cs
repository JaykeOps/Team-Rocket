using System;
using Domain.Entities;
using Domain.Value_Objects;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IExposableTeam
    {
        TeamName Name { get; }
        IEnumerable<Guid> PlayerIds { get; }
        ArenaName ArenaName { get; }
        EmailAddress Email { get; }

        IPresentableTeamSeriesEvents PresentableSeriesEvents { get; }
        IPresentableTeamSeriesStats PresentableSeriesStats { get; }

        ShirtNumbers ShirtNumbers { get; }
    }
}