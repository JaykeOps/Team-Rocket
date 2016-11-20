using Domain.Interfaces;
using Domain.Services;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    [Serializable]
    public class Team : IPresentableTeam
    {
        private HashSet<Guid> playerIds;
        private TeamMatchSchedule matchSchedules;
        private TeamSeriesEvents seriesEvents;
        private TeamSeriesStats seriesStats;

        public Guid Id { get; set; }

        public TeamName Name { get; set; }

        public ArenaName ArenaName { get; set; }
        public EmailAddress Email { get; set; }
        public ShirtNumbers ShirtNumbers { get; }

        public IEnumerable<Player> Players
        {
            get
            {
                var players = new List<Player>();
                foreach (var playerId in this.playerIds)
                {
                    players.Add(DomainService.FindPlayerById(playerId));
                }
                return players;
            }
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
            this.playerIds.Add(playerId);
            DomainService.AddTeamToPlayer(this, playerId);
        }

        public void AddPlayerId(Player player)
        {
            this.playerIds.Add(player.Id);
        }

        public void RemovePlayerId(Player player)
        {
            this.playerIds.Remove(player.Id);
        }

        public void RemovePlayerId(Guid playerId)
        {
            this.playerIds.Remove(playerId);
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