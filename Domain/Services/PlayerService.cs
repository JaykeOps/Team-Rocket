using Domain.Entities;
using Domain.Helper_Classes;
using Domain.Repositories;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class PlayerService
    {
        private readonly PlayerRepository repository = PlayerRepository.instance;
        private readonly IEnumerable<Player> allPlayers;

        public PlayerService()
        {
            allPlayers = repository.GetAll();
        }

        public void Add(Player player)
        {
            this.repository.Add(player);
        }

        public IEnumerable<Player> GetAll()
        {
            return this.repository.GetAll();
        }

        public IEnumerable<Player> FindPlayer(string searchText, bool ignoreCase)
        {
            var result = allPlayers.Where(x =>
                x.Name.ToString().Contains(searchText, ignoreCase) ||
                x.DateOfBirth.Value.ToString().Contains(searchText, ignoreCase));
            return result;
        }
        public string GetPlayerName(Guid playerId)
        {
            var result = allPlayers.Where(x => x.Id == playerId).Select(x => x.Name.ToString()).First();
            return result;
        }
        public Guid GetPlayerTeamId(Guid playerId)
        {
            var result = allPlayers.Where(x => x.Id == playerId).Select(x => x.TeamId).First();
            return result;
        }

        public IEnumerable<Guid> GetPlayerGamesPlayedIds(Guid playerId)
        {
            var playerStats = allPlayers.Where(x => x.Id == playerId).Select(x => x.Stats);
            var result = playerStats.SelectMany(x => x.GamesPlayedIds);
            return result;
        }

        public int GetPlayerTotalYellowCards(Guid playerId)
        {
            var playerStats = allPlayers.Where(x => x.Id == playerId).Select(x => x.Stats);
            var result = playerStats.Select(x => x.YellowCardCount).First();
            return result;
        }

        public int GetPlayerTotalRedCards(Guid playerId)
        {
            var playerStats = allPlayers.Where(x => x.Id == playerId).Select(x => x.Stats);
            var result = playerStats.Select(x => x.RedCardCount).First();
            return result;
        }
        public int GetPlayerTotalGoals(Guid playerId)
        {
            var playerStats = allPlayers.Where(x => x.Id == playerId).Select(x => x.Stats);
            var result = playerStats.Select(x => x.GoalCount).First();
            return result;
        }

        public int GetPlayerTotalAssists(Guid playerId)
        {
            var playerStats = allPlayers.Where(x => x.Id == playerId).Select(x => x.Stats);
            var result = playerStats.Select(x => x.AssistCount).First();
            return result;
        }

        public int GetPlayerTotalPenalties(Guid playerId)
        {
            var playerStats = allPlayers.Where(x => x.Id == playerId).Select(x => x.Stats);
            var result = playerStats.Select(x => x.AssistCount).First();
            return result;
        }
    }
}