using Domain.Entities;
using Domain.Value_Objects;

namespace Domain.Interfaces
{
    public interface IPresentablePlayer
    {
        Name Name { get; set; }
        DateOfBirth DateOfBirth { get; set; }
        ContactInformation ContactInformation { get; set; }
        string TeamName { get; }
        PlayerPosition Position { get; set; }
        PlayerStatus Status { get; set; }
        IPresentablePlayerStats Stats { get; }
        IPresentablePlayerEvents Events { get; }
        ShirtNumber ShirtNumber { get; }
    }
}