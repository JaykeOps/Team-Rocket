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

        PlayerPosition Position { set; }
        PlayerStatus Status { get; }
        IPresentablePlayerSeriesEvents PresentableSeriesEvents { get; }
        IPresentablePlayerSeriesStats PresentableSeriesStats { get; }

        ShirtNumber ShirtNumber { get; }
    }
}