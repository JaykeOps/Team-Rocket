using Domain.Interfaces;
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
                this.ShirtNumber = new ShirtNumber(value);
                this.teamId = value;
            }
        }

        public AggregatedPlayerEvents AggregatedEvents
        {
            get { return this.aggregatedEvents; }
        }

        public AggregatedPlayerStats AggregatedStats
        {
            get { return this.aggregatedStats; }
        }

        public ShirtNumber ShirtNumber { get; set; }

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
            PlayerStatus status, Guid id) : base(name, dateOfBirth, id)
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
            this.TeamId = newTeam?.Id ?? Guid.Empty;
            this.affiliatedTeamName = newTeam?.Name ?? new TeamName("Unaffiliated");
        }
    }
}