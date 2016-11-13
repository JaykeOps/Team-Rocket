using Domain.Interfaces;
using Domain.Services;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Player : Person, IPresentablePlayer
    {
        private Guid teamId;
        private Dictionary<Guid, PlayerSeriesEvents> playerSeriesEvents;
        private Dictionary<Guid, PlayerSeriesStats> playerSeriesStats;
        private ShirtNumber shirtNumber;

        public PlayerPosition Position { get; set; }
        public PlayerStatus Status { get; set; }

        public string TeamName
        {
            get
            {
                return DomainService.FindTeamById(this.teamId).Name.ToString();
            }
        }

        public Guid TeamId
        {
            get
            {
                return this.teamId;
            }
            set
            {
                this.shirtNumber = new ShirtNumber(value, null);
                this.teamId = value;
            }
        }

        public IReadOnlyDictionary<Guid, PlayerSeriesEvents> PlayerSeriesEvents
        {
            get
            {
                return this.playerSeriesEvents;
            }
        }

        public IReadOnlyDictionary<Guid, PlayerSeriesStats> PlayerSeriesStats
        {
            get
            {
                return this.playerSeriesStats;
            }
        }

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
            this.playerSeriesEvents = new Dictionary<Guid, PlayerSeriesEvents>();
            this.playerSeriesStats = new Dictionary<Guid, PlayerSeriesStats>();
            this.TeamId = Guid.Empty;
        }
    }
}