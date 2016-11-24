using Domain.Entities;
using Domain.Value_Objects;

namespace Domain.Interfaces
{
    public interface IPresentablePlayer
    {
        string TeamName { get; }
        Name Name { get; set; }
        DateOfBirth DateOfBirth { get; set; }
        ContactInformation ContactInformation { get; }
        IPresentablePlayerSeriesEvents AllPresentableSeriesEvents { get; }
        IPresentablePlayerSeriesStats AllPresentableSeriesStats { get; }

        PlayerPosition Position { get; }
        PlayerStatus Status { get; }

        ShirtNumber ShirtNumber { get; }
    }
}