using Domain.Value_Objects;
using football_series_manager.Domain.Entities;
using System;

namespace DomainTests.Entities
{
    public class Player : Person
    {
        public PlayerPosition Position { get; set; }
        public PlayerStatus Status { get; set; }
        public ShirtNumber ShirtNumber { get; set; }
        public Guid TeamId { get; set; } = Guid.Empty;
        public PlayerStats Stats { get; set; }

        public Player(Name name, DateOfBirth dateOfBirth, PlayerPosition position,
            PlayerStatus status, ShirtNumber shirtNumber)
            : base(name, dateOfBirth)
        {
            this.Position = position;
            this.Status = status;
            this.ShirtNumber = shirtNumber;
            this.TeamId = Guid.Empty;
        }
    }
}