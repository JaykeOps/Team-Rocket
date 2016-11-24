using Domain.Entities;
using Domain.Value_Objects;

namespace Domain.Interfaces
{
    public interface IPresentablePlayer
    {
        TeamName AffiliatedTeamName { get; }
        Name Name { get; set; }
        DateOfBirth DateOfBirth { get; set; }
        ContactInformation ContactInformation { get; }
        AggregatedPlayerEvents AggregatedEvents { get; }
        AggregatedPlayerStats AggregatedStats { get; }
        PlayerPosition Position { get; }
        PlayerStatus Status { get; }

        ShirtNumber ShirtNumber { get; }
    }
}