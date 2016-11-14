using Domain.Interfaces;
using Domain.Services;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Team : IPresentableTeam
    {
        private Guid id;
        private HashSet<Guid> playerIds;
        private Dictionary<Guid, List<Guid>> matchSchedule;
        private TeamSeriesEvents seriesEvents;
        private TeamSeriesStats seriesStats;

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
            this.id = Guid.NewGuid();
            this.seriesEvents = new TeamSeriesEvents();
            this.seriesStats = new TeamSeriesStats();
            this.Name = name;
            this.playerIds = new HashSet<Guid>();
            this.ArenaName = arenaName;
            this.Email = email;
            this.matchSchedule = new Dictionary<Guid, List<Guid>>();
            this.ShirtNumbers = new ShirtNumbers(this.id);
        }

        public void AddPlayerId(Guid playerId)
        {
            this.playerIds.Add(playerId);
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

        public override string ToString()
        {
            return $"{this.Name.Value}";
        }
    }
}