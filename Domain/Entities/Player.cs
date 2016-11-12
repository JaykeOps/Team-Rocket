using Domain.Services;
using Domain.Value_Objects;
using System;
using System.Linq;

namespace Domain.Entities
{
    public class Player : Person
    {
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

        public PlayerStats Stats { get; set; }

        public ShirtNumber ShirtNumber
        {
            get { return this.shirtNumber; }
            set
            {
                var team = DomainService.FindTeamById(this.teamId);
                //var teamService = new TeamService();
                //var team = teamService.GetAll().Where(x => x.Id.Equals(this.TeamId)).FirstOrDefault();
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
            this.Stats = new PlayerStats();
            this.teamId = Guid.Empty;
            this.shirtNumber = new ShirtNumber(this.TeamId, null);
        }
    }
}