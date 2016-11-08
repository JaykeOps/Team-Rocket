using Domain.Entities;
using Domain.Helper_Classes;
using Domain.Repositories;
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
            var players = this.repository.GetAll();

            var result = players.Where(x =>
                x.Name.ToString().Contains(searchText, ignoreCase) ||
                x.DateOfBirth.Value.ToString().Contains(searchText, ignoreCase));

            return result;
        }
    }
}