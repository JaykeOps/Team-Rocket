using Domain.Interfaces;
using Domain.Services;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Team : IPresentableTeam
    {
        private HashSet<Guid> playerIds;
        private TeamStats teamStatsAndEvents;
        public Guid Id { get; }

        public TeamName Name { get; set; }

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

        public ArenaName ArenaName { get; set; }
        public EmailAddress Email { get; set; }

        public TeamStats StatsAndEvents { get { return this.teamStatsAndEvents; } }
        public IPresentableTeamStats Stats { get { return this.teamStatsAndEvents; } }
        public IPresentableTeamEvents Events { get { return this.teamStatsAndEvents; } }

        public ShirtNumbers ShirtNumbers { get; }

        public Team(TeamName name, ArenaName arenaName, EmailAddress email)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.playerIds = new HashSet<Guid>();
            this.ArenaName = arenaName;
            this.Email = email;
            this.teamStatsAndEvents = new TeamStats(this.Id);
            this.ShirtNumbers = new ShirtNumbers(this);
        }

        public void AddPlayerId(Guid playerId)
        {
            this.playerIds.Add(playerId);
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