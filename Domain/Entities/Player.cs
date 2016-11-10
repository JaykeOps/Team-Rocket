using Domain.Value_Objects;
using System;

namespace Domain.Entities
{
    public class Player : Person
    {
        public PlayerPosition Position { get; set; }
        public PlayerStatus Status { get; set; }
        public ShirtNumber ShirtNumber { get; set; }
        public Guid TeamId { get; set; } = Guid.Empty;
        public PlayerStats Stats { get; set; }

        public Player(Name name, DateOfBirth dateOfBirth, PlayerPosition position,
            PlayerStatus status) : base(name, dateOfBirth)
        {
            this.Position = position;
            this.Status = status;
            this.TeamId = Guid.Empty;
            this.ShirtNumber = new ShirtNumber();
        }

        public Player(Name name, DateOfBirth dateOfBirth, PlayerPosition position,
            PlayerStatus status, ShirtNumber shirtNumber) : this(name, dateOfBirth,
                position, status)
        {
            this.ShirtNumber = shirtNumber;
        }
    }
}