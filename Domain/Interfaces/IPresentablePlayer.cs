using Domain.Entities;
using Domain.Value_Objects;

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
        IPresentablePlayerSeriesEvents PresentableSeriesEvents { get; }
        IPresentablePlayerSeriesStats PresentableSeriesStats { get; }

        ShirtNumber ShirtNumber { get; }
    }
}