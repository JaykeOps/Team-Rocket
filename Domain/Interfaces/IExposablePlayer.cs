using Domain.Entities;
using Domain.Value_Objects;
using System;

namespace Domain.Interfaces
{
    public interface IExposablePlayer
    {
        Guid Id { get; }
        TeamName AffiliatedTeamName { get; }
        Name Name { get; set; }
        DateOfBirth DateOfBirth { get; set; }
        ContactInformation ContactInformation { get; }
        AggregatedPlayerEvents AggregatedEvents { get; }
        AggregatedPlayerStats AggregatedStats { get; }
        PlayerPosition Position { get; set; }
        PlayerStatus Status { get; set; }

        ShirtNumber ShirtNumber { get; }
    }
}