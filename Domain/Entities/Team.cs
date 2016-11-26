using Domain.Interfaces;
using Domain.Services;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    [Serializable]
    public class Team : IPresentableTeam
    {
        internal HashSet<Guid> playerIds;
        private TeamMatchSchedule matchSchedules;
        private TeamSeriesEvents seriesEvents;
        private TeamSeriesStats seriesStats;

        public Guid Id { get; }

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
                return this.seriesEvents;
            }
        }

        public IPresentableTeamSeriesStats PresentableSeriesStats
        {
            get
            {
                return this.seriesStats;
            }
        }

        public TeamMatchSchedule MatchSchedules
        {
            get
            {
                return this.matchSchedules;
            }
        }

        public TeamSeriesEvents SeriesEvents
        {
            get
            {
                return this.seriesEvents;
            }
        }

        public TeamSeriesStats SeriesStats
        {
            get
            {
                return this.seriesStats;
            }
        }

        public Team(TeamName name, ArenaName arenaName, EmailAddress email)
        {
            this.Id = Guid.NewGuid();
            this.seriesEvents = new TeamSeriesEvents(this.Id);
            this.seriesStats = new TeamSeriesStats(this.Id);
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
            var player = DomainService.FindPlayerById(playerId);;
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
            this.seriesEvents.AddSeries(series);
            this.seriesStats.AddSeries(series);
        }

        public override string ToString()
        {
            return $"{this.Name.Value}";
        }
    }
}