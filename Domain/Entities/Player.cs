using Domain.Interfaces;
using Domain.Services;
using Domain.Value_Objects;
using System;

namespace Domain.Entities
{
    public class Player : Person
    {
        private PlayerStats statsAndEvents = new PlayerStats();
        private Guid teamId;
        private ShirtNumber shirtNumber;
        public PlayerPosition Position { get; set; }
        public PlayerStatus Status { get; set; }

        public Guid TeamId
        {
            get { return this.teamId; }
            set
            {
                this.shirtNumber = new ShirtNumber(value, null);
                this.teamId = value;
            }
        }

        public IPlayerStats Stats { get { return this.statsAndEvents; } }

        public IEvents Events { get { return this.statsAndEvents; } }

        public ShirtNumber ShirtNumber
        {
            get { return this.shirtNumber; }
            set
            {
                var team = DomainService.FindTeamById(this.teamId);
                try
                {
                    value = team.ShirtNumbers[value.Value];
                }
                catch (ShirtNumberAlreadyInUseException ex)
                {
                    this.shirtNumber = new ShirtNumber(this.TeamId, null);
                    throw ex;
                }
                if (value == null)
                {
                    this.shirtNumber = new ShirtNumber(this.TeamId, null);
                }
                else
                {
                    this.shirtNumber = value;
                }
            }
        }

        public Player(Name name, DateOfBirth dateOfBirth, PlayerPosition position,
            PlayerStatus status) : base(name, dateOfBirth)
        {
            this.Position = position;
            this.Status = status;
            this.statsAndEvents = new PlayerStats();
            this.teamId = Guid.Empty;
            this.shirtNumber = new ShirtNumber(this.TeamId, null);
        }
    }
}