using Domain.Entities;
using Domain.Value_Objects;
using System;

namespace Domain.Interfaces
{
    public interface IExposablePlayer
    {
        Guid Id { get; }
        Guid TeamId { get; }
        TeamName AffiliatedTeamName { get; }
        Name Name { get; set; }
        DateOfBirth DateOfBirth { get; set; }
        ContactInformation ContactInformation { get; }
        PlayerPosition Position { get; set; }
        PlayerStatus Status { get; set; }

        ShirtNumber ShirtNumber { get; set; }
    }
}