using Domain.Entities;
using Domain.Helper_Classes;
using Domain.Repositories;
using Domain.Value_Objects;
using System;
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

        public Guid Add(Guid matchId)
        {
            var match = DomainService.FindMatchById(matchId);
            Game game;
            if (match != null)
            {
                game = new Game(match);
                this.repository.Add(game);
                return game.Id;
            }
            else
            {
                throw new ArgumentException("Invalid matchId");
            }


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
            return this.GetAll().Where(g => g.ToString().Contains(searchText, comp));
        }

        public void AddGoalToGame(Guid gameId, Guid playerId, int matchMinute)
        {
            var game = this.FindById(gameId);
            var goal = new Goal(new MatchMinute(matchMinute), DomainService.FindPlayerById(playerId).TeamId, playerId);
            game.Protocol.Goals.Add(goal);
        }

        public void AddAssistToGame(Guid gameId, Guid playerId, int matchMinute)
        {
            var game = this.FindById(gameId);
            var assist = new Assist(new MatchMinute(matchMinute), playerId);
            game.Protocol.Assists.Add(assist);
        }

        public void AddRedCardToGame(Guid gameId, Guid playerId, int matchMinute)
        {
            var game = this.FindById(gameId);
            var card = new Card(new MatchMinute(matchMinute), playerId, CardType.Red);
            game.Protocol.Cards.Add(card);
        }

        public void AddYellowCardToGame(Guid gameId, Guid playerId, int matchMinute)
        {
            var game = this.FindById(gameId);
            var card = new Card(new MatchMinute(matchMinute), playerId, CardType.Yellow);
            game.Protocol.Cards.Add(card);
        }

        public void AddPenaltyToGame(Guid gameId, Guid playerId, int matchMinute)
        {
            var game = this.FindById(gameId);

            var penalty = new Penalty(new MatchMinute(matchMinute), playerId, true, game, DomainService.FindPlayerById(playerId).TeamId);
            game.Protocol.Penalties.Add(penalty);
        }
    }
}