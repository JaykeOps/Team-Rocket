using System;
using Domain.Entities;
using Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services
{
    public class GameService
    {
        private readonly GameRepository repository;

        public GameService()
        {
                this.repository = GameRepository.instance;
        }
        public void Add(Game game)
        {
            this.repository.Add(game);
        }

        public IEnumerable<Game> GetAll()
        {
            return this.repository.GetAll();
        }

        public Game FindById(Guid gameId)
        {
            return GetAll().ToList().Find(g => g.Id.Equals(gameId));
        }
    }
}