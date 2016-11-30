using Domain.Interfaces;
using Domain.Services;
using Domain.Value_Objects;
using System;

namespace Domain.Entities
{
    [Serializable]
    public class Player : Person, IExposablePlayer
    {
        private readonly AggregatedPlayerEvents aggregatedEvents;
        private readonly AggregatedPlayerStats aggregatedStats;
        private Guid teamId;
        private TeamName affiliatedTeamName;
        private ShirtNumber shirtNumber;
        public PlayerPosition Position { get; set; }
        public PlayerStatus Status { get; set; }

        

        public TeamName AffiliatedTeamName
        {
            get { return this.teamId == Guid.Empty ? new TeamName("Unaffiliated") : this.affiliatedTeamName; }
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

        public AggregatedPlayerEvents AggregatedEvents //Will be internal
        {
            get { return this.aggregatedEvents; }
        }

        public AggregatedPlayerStats AggregatedStats //Will be internal
        {
            get { return this.aggregatedStats; }
        }

        public ShirtNumber ShirtNumber
        {
            get
            {
                return this.shirtNumber;
            }
            set
            {
                var team = DomainService.FindTeamById(this.teamId);
                try
                {
                    value = team.ShirtNumbers[value.Value];
                }
                catch (ShirtNumberAlreadyInUseException ex)
                {
                    throw ex;
                }
                this.shirtNumber = value ?? new ShirtNumber(this.teamId, null);
            }
        }

        public Player(Name name, DateOfBirth dateOfBirth, PlayerPosition position,
            PlayerStatus status) : base(name, dateOfBirth)
        {
            this.Position = position;
            this.Status = status;
            this.TeamId = Guid.Empty;
            this.affiliatedTeamName = new TeamName("Unaffiliated");
            this.aggregatedEvents = new AggregatedPlayerEvents();
            this.aggregatedStats = new AggregatedPlayerStats();
        }

        public Player(Name name, DateOfBirth dateOfBirth, PlayerPosition position,
            PlayerStatus status, Guid id) : base(name, dateOfBirth, id) //Id for tests!
        {
            this.Position = position;
            this.Status = status;
            this.TeamId = Guid.Empty;
            this.affiliatedTeamName = new TeamName("Unaffiliated");
            this.aggregatedEvents = new AggregatedPlayerEvents();
            this.aggregatedStats = new AggregatedPlayerStats();
        }

        public void AddSeries(Series series)
        {
            this.aggregatedEvents.AddSeries(series, this.teamId, this.Id);
            this.aggregatedStats.AddSeries(series, this.teamId, this);
        }

        public void UpdateTeamAffiliation(Team newTeam)
        {
            this.TeamId = newTeam.Id;
            this.affiliatedTeamName = newTeam.Name;
        }

       
    }
}