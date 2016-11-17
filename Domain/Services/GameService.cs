using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Helper_Classes;

namespace Domain.Services
{
    public class GameService
    {
        private readonly GameRepository repository;

        public GameService()
        {
            repository = GameRepository.instance;
        }

        public void Add(Game game)
        {
            this.repository.Add(game);
        }

        public IEnumerable<Game> GetAll()
        {
            return this.repository.GetAll();
        }

        public Game FindById(Guid id)
        {
            return this.GetAll().ToList().Find(g => g.Id == id);
        }

        public IEnumerable<Game> SearchGame(string searchText, StringComparison comp)
        {
            return GetAll().Where(g => g.ToString().Contains(searchText, comp));
        }
    }
}