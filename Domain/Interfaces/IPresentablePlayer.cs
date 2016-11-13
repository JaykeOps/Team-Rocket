using Domain.Entities;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IPresentablePlayer
    {
        string TeamName { get; }
        Name Name { get; set; }
        DateOfBirth DateOfBirth { get; set; }
        ContactInformation ContactInformation { get; set; }

        PlayerPosition Position { get; set; }
        PlayerStatus Status { get; set; }
        IReadOnlyDictionary<Guid, PlayerSeriesEvents> PlayerSeriesEvents { get; }
        IReadOnlyDictionary<Guid, PlayerSeriesStats> PlayerSeriesStats { get; }

        ShirtNumber ShirtNumber { get; }
    }
}