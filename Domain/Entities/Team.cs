using Domain.Interfaces;
using Domain.Services;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    [Serializable]
    public class Team : IExposableTeam
    {
        internal List<Guid> playerIds;
        private TeamMatchSchedule matchSchedules;
        private AggregatedTeamEvents events;
        private AggregatedTeamStats stats;

        public Guid Id { get; set; } //TODO: Set is for test!

        public TeamName Name { get; set; }

        public ArenaName ArenaName { get; set; }
        public EmailAddress Email { get; set; }

        public IEnumerable<Guid> PlayerIds
        {
            get { return this.playerIds; }
        }

        public TeamMatchSchedule MatchSchedules
        {
            get
            {
                return this.matchSchedules;
            }
        }

        public AggregatedTeamEvents AggregatedEvents
        {
            get
            {
                return this.events;
            }
        }

        public AggregatedTeamStats AggregatedStats
        {
            get
            {
                return this.stats;
            }
        }

        public Team(TeamName name, ArenaName arenaName, EmailAddress email)
        {
            this.Id = Guid.NewGuid();
            this.events = new AggregatedTeamEvents(this.Id);
            this.stats = new AggregatedTeamStats(this.Id);
            this.Name = name;
            this.playerIds = new List<Guid>();
            this.ArenaName = arenaName;
            this.Email = email;
            this.matchSchedules = new TeamMatchSchedule(this.Id);
        }

        public void UpdatePlayerIds()
        {
            this.playerIds = DomainService.GetAllPlayers().
                Where(x => x.TeamId == this.Id).Select(y => y.Id).ToList();
            DomainService.SaveAll();
        }

        public void AddSeries(Series series)
        {
            //this.matchSchedules.AddSeries(series);
            this.events.AddSeries(series);
            this.stats.AddSeries(series);
        }

        public override string ToString()
        {
            return $"{this.Name.Value}";
        }
    }
}