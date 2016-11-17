using Domain.CustomExceptions;
using Domain.Entities;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Services;

namespace Domain.Repositories
{
    public sealed class GameRepository
    {
        private List<Game> games;
        public static readonly GameRepository instance = new GameRepository();

        private GameRepository()
        {
            //Series series = new Series(new MatchDuration(new TimeSpan(0, 90, 0)), new NumberOfTeams(16), "Allsvenskan");

            
            var matches = DomainService.GetAllMatches();

            var game1 = new Game(matches.ElementAt(0));
            var game2 = new Game(matches.ElementAt(1));
            var game3 = new Game(matches.ElementAt(2));
            var game4 = new Game(matches.ElementAt(3));
            var game5 = new Game(matches.ElementAt(4));
            var game6 = new Game(matches.ElementAt(5));
            var game7 = new Game(matches.ElementAt(6));
            var game8 = new Game(matches.ElementAt(7));
            var game9 = new Game(matches.ElementAt(8));
            var game10 = new Game(matches.ElementAt(9));
           
            

        }

        public void Add(Game game)
        {
            if (IsAdded(game))
            {
                throw new GameAlreadyAddedException();
            }
            else
            {
                this.games.Add(game);
            }
        }

        public bool IsAdded(Game newGame)
        {
            var isAdded = false;

            foreach (var game in games)
            {
                if (newGame.Id == game.Id)
                {
                    isAdded = true;
                }
            }

            return isAdded;
        }

        public IEnumerable<Game> GetAll()
        {
            return this.games;
        }
    }
}