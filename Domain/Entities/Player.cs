using Domain.Services;
using Domain.Value_Objects;
using System;
using System.Linq;

namespace Domain.Entities
{
    public class Player : Person
    {
        private ShirtNumber shirtNumber;
        public PlayerPosition Position { get; set; }
        public PlayerStatus Status { get; set; }
        public ShirtNumber ShirtNumber
        {
            get
            {
                return shirtNumber;
            }
            set
            {
                var teamService = new TeamService();
                var team = teamService.GetAll().Where(x => x.Id.Equals(this.TeamId)).FirstOrDefault();
                value = team.ShirtNumbers[value.Value];
                if (value == null)
                {
                    throw new ShirtNumberAlreadyInUseException("The shirt number you tried to assign is already " +
                        "being used by another player on the team.");
                }
                this.shirtNumber = value;
            }
        }
        public Guid TeamId { get; set; } = Guid.Empty;
        public PlayerStats Stats { get; set; }

        public Player(Name name, DateOfBirth dateOfBirth, PlayerPosition position,
            PlayerStatus status) : base(name, dateOfBirth)
        {
            this.Position = position;
            this.Status = status;
            this.TeamId = Guid.Empty;
            //this.shirtNumber = new ShirtNumber();
        }

        public Player(Name name, DateOfBirth dateOfBirth, PlayerPosition position,
            PlayerStatus status, ShirtNumber shirtNumber) : this(name, dateOfBirth,
                position, status)
        {
            this.shirtNumber = shirtNumber;
        }
    }
}