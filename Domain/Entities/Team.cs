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
        private Dictionary<Guid, List<Match>> matchSchedule;
        private Dictionary<Guid, TeamSeriesEvents> seriesEvents;
        private Dictionary<Guid, TeamSeriesStats> seriesStats;
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

        public IReadOnlyDictionary<Guid, List<Match>> TeamsSeriesSchedule { get { return this.matchSchedule; } }
        public IReadOnlyDictionary<Guid, TeamSeriesEvents> SeriesEvents { get { return this.seriesEvents; } }
        public IReadOnlyDictionary<Guid, TeamSeriesStats> SeriesStats { get { return this.seriesStats; } }

        public ShirtNumbers ShirtNumbers { get; }

        public Team(TeamName name, ArenaName arenaName, EmailAddress email)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.playerIds = new HashSet<Guid>();
            this.ArenaName = arenaName;
            this.Email = email;
            this.matchSchedule = new Dictionary<Guid, List<Match>>();
            this.seriesEvents = new Dictionary<Guid, TeamSeriesEvents>();
            this.seriesStats = new Dictionary<Guid, TeamSeriesStats>();
            this.ShirtNumbers = new ShirtNumbers(this);
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

        public void AddSeriesSchedule(Guid seriesId, List<Match> matches)
        {
            this.AddSeriesSchedule(seriesId, matches);
            this.seriesEvents.Add(seriesId, new TeamSeriesEvents(this.Id, Guid.NewGuid()));
            this.seriesStats.Add(seriesId, new TeamSeriesStats(this.Id, Guid.NewGuid()));
        }

        public void RemoveSeriesSchedule(Guid seriesId)
        {
            this.matchSchedule.Remove(seriesId);
        }

        public void AddGameEventId(Guid seriesId, Game game)
        {
            this.seriesEvents[seriesId].AddGameId(game.Id);
        }

        public void AddGameEventId(Guid seriesId, Guid gameId)
        {
            this.seriesEvents[seriesId].AddGameId(gameId);
        }

        public void RemoveGameEventId(Guid seriesId, Game game)
        {
            this.seriesEvents[seriesId].RemoveGameId(game.Id);
        }

        public void RemoveGameEventId(Guid seriesId, Guid gameId)
        {
            this.seriesEvents[seriesId].RemoveGameId(gameId);
        }

        public void AddGoalEvent(Guid seriesId, Goal goal)
        {
            this.seriesEvents[seriesId].AddGoal(goal);
        }

        public void RemoveGoalEvent(Guid seriesId, Goal goal)
        {
            this.seriesEvents[seriesId].RemoveGoal(goal);
        }

        public override string ToString()
        {
            return $"{this.Name.Value}";
        }
    }
}