using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Helper_Classes;
using Domain.Value_Objects;

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

        public void Add(Guid matchId)
        {
            var match = DomainService.FindMatchById(matchId);
            Game game;
            if (match != null)
            {
                game = new Game(match);
            }
            else
            {
                throw new ArgumentException("Invalid matchId");
            }

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
            return this.GetAll().Where(g => g.ToString().Contains(searchText, comp));
        }

        public void AddGoalToGame(Guid gameId, Guid teamId, Guid playerId, int matchMinute)
        {
            var match = this.FindById(gameId);
            var goal = new Goal(new MatchMinute(matchMinute), teamId, playerId);
            match.Protocol.Goals.Add(goal);

        }

        public void AddAssistToGame(Guid gameId, Guid playerId, int matchMinute)
        {
            var match = this.FindById(gameId);
            var assist = new Assist(new MatchMinute(matchMinute), playerId);
            match.Protocol.Assists.Add(assist);
        }

        public void AddRedCardToGame(Guid gameId, Guid playerId, int matchMinute)
        {
            var match = this.FindById(gameId);
            var card = new Card(new MatchMinute(matchMinute), playerId, CardType.Red);
            match.Protocol.Cards.Add(card);
        }

        public void AddYellowCardToGame(Guid gameId, Guid playerId, int matchMinute)
        {
            var match = this.FindById(gameId);
            var card = new Card(new MatchMinute(matchMinute), playerId, CardType.Yellow);
            match.Protocol.Cards.Add(card);
        }
        public void AddPenaltyToGame(Guid gameId, Guid playerId, int matchMinute)
        {
            var match = this.FindById(gameId);
            var penalty = new Penalty(new MatchMinute(matchMinute), playerId);
            match.Protocol.Penalties.Add(penalty);
        }

    }
}