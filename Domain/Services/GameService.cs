using Domain.Entities;
using Domain.Repositories;
using System.Collections.Generic;

namespace Domain.Services
{
    public class GameService
    {
        private readonly GameRepository repository = GameRepository.instance;
        
        public void Add(Game game)
        {
            this.repository.Add(game);
        }

        public IEnumerable<Game> GetAll()
        {
            return this.repository.GetAll();
        }
    }
}
