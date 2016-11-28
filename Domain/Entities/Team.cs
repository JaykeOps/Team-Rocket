using Domain.Interfaces;
using Domain.Services;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    [Serializable]
    public class Team : IExposableTeam
    {
        internal HashSet<Guid> playerIds;
        private TeamMatchSchedule matchSchedules;
        private AggregatedTeamEvents events;
        private AggregatedTeamStats stats;

        public Guid Id { get; set; } //TODO: Set is for test!

        public TeamName Name { get; set; }

        public ArenaName ArenaName { get; set; }
        public EmailAddress Email { get; set; }
        public ShirtNumbers ShirtNumbers { get; }

        public IEnumerable<Guid> PlayerIds
        {
            get { return this.playerIds; }
        }

        public IPresentableTeamSeriesEvents PresentableSeriesEvents
        {
            get
            {
                return this.events;
            }
        }

        public IPresentableTeamSeriesStats PresentableSeriesStats
        {
            get
            {
                return this.stats;
            }
        }

        public TeamMatchSchedule MatchSchedules
        {
            get
            {
                return this.matchSchedules;
            }
        }

        public AggregatedTeamEvents Events
        {
            get
            {
                return this.events;
            }
        }

        public AggregatedTeamStats Stats
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
            this.playerIds = new HashSet<Guid>();
            this.ArenaName = arenaName;
            this.Email = email;
            this.matchSchedules = new TeamMatchSchedule(this.Id);
            this.ShirtNumbers = new ShirtNumbers(this.Id);
        }

        public void AddPlayerId(Guid playerId)
        {
            this.RemovePlayerIdFromOldTeam(playerId);
            this.playerIds.Add(playerId);
            DomainService.AddTeamToPlayer(this, playerId);
        }

        private void RemovePlayerIdFromOldTeam(Guid playerId)
        {
            var player = DomainService.FindPlayerById(playerId); ;
        }

        public void RemovePlayerId(Guid playerId)
        {
            this.playerIds.Remove(playerId);
            this.RemoveTeamIdFromPlayerToBeRemoved(playerId);
        }

        private void RemoveTeamIdFromPlayerToBeRemoved(Guid playerId)
        {
            var player = DomainService.FindPlayerById(playerId);
            player.TeamId = Guid.Empty;
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