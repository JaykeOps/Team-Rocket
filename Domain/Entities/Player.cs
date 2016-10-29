using Domain.Value_Objects;
using football_series_manager.Domain.Entities;

namespace DomainTests.Entities
{
    public class Player : Person
    {
        public Player(Name name, DateOfBirth dateOfBirth, ContactInformation contactInformation,
            PlayerPosition position, PlayerStatus status, ShirtNumber shirtNumber)
            : base(name, dateOfBirth, contactInformation)
        {
            this.Position = position;
            this.Status = status;
            this.ShirtNumber = shirtNumber;
        }

        public PlayerPosition Position { get; }
        public PlayerStatus Status { get; }
        public ShirtNumber ShirtNumber { get; }

        //TODO: PlayerStats
    }
}