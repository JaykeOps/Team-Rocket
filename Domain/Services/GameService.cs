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
            if (game.IsValidGame())
            {
                this.repository.Add(game);
            }
            else
            {
                throw new FormatException("Game cannot be added. Invalid gamedata");
            }
        }

        public void Add(IEnumerable<Game> games)
        {
            if (games != null)
            {
                foreach (var game in games)
                {
                    this.Add(game);
                }
            }
            else
            {
                throw new NullReferenceException("List of games is null");
            }
        }

        public Guid Add(Guid matchId)
        {
            var match = DomainService.FindMatchById(matchId);

            if (match != null)
            {
                var game = new Game(match);
                this.repository.Add(game);
                return game.Id;
            }
            else
            {
                throw new ArgumentException("Invalid matchId");
            }
        }

        public IEnumerable<Guid> AddList(IEnumerable<Guid> matchIds)
        {
            var gameIds = new List<Guid>();
            foreach (var matchId in matchIds)
            {
                var match = DomainService.FindMatchById(matchId);
                if (match != null)
                {
                    var game = new Game(match);
                    this.repository.Add(game);
                    gameIds.Add(game.Id);
                }
                else
                {
                    throw new ArgumentException("Invalid matchId");
                }
            }
            return gameIds;
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

        public void AddPenaltyToGame(Guid gameId, Guid playerId, int matchMinute, bool isGoal)
        {
            var game = this.FindById(gameId);

            var penalty = new Penalty(new MatchMinute(matchMinute), playerId, isGoal, game, DomainService.FindPlayerById(playerId).TeamId);
            game.Protocol.Penalties.Add(penalty);
        }

        public void RemoveGoalFromGame(Guid gameId, Guid playerId, int matchMinute)
        {
            var matchMin = new MatchMinute(matchMinute);
            var game = this.FindById(gameId);
            foreach (var goal in game.Protocol.Goals)
            {
                if (goal.PlayerId == playerId && goal.MatchMinute == matchMin)
                {
                    game.Protocol.Goals.Remove(goal);
                    break;
                }
            }
        }

        public void RemoveAssistFromGame(Guid gameId, Guid playerId, int matchMinute)
        {
            var matchMin = new MatchMinute(matchMinute);
            var game = this.FindById(gameId);
            foreach (var assist in game.Protocol.Assists)
            {
                if (assist.PlayerId == playerId && assist.MatchMinute == matchMin)
                {
                    game.Protocol.Assists.Remove(assist);
                    break;
                }
            }
        }

        public void RemoveRedCardFromGame(Guid gameId, Guid playerId, int matchMinute)
        {
            var matchMin = new MatchMinute(matchMinute);
            var game = this.FindById(gameId);
            foreach (var card in game.Protocol.Cards)
            {
                if (card.PlayerId == playerId && card.MatchMinute == matchMin && card.CardType == CardType.Red)
                {
                    game.Protocol.Cards.Remove(card);
                    break;
                }
            }
        }

        public void RemoveYellowCardFromGame(Guid gameId, Guid playerId, int matchMinute)
        {
            var matchMin = new MatchMinute(matchMinute);
            var game = this.FindById(gameId);
            foreach (var card in game.Protocol.Cards)
            {
                if (card.PlayerId == playerId && card.MatchMinute == matchMin && card.CardType == CardType.Yellow)
                {
                    game.Protocol.Cards.Remove(card);
                    break;
                }
            }
        }

        public void RemovePenaltyFromGame(Guid gameId, Guid playerId, int matchMinute)
        {
            var matchMin = new MatchMinute(matchMinute);
            var game = this.FindById(gameId);
            foreach (var penalty in game.Protocol.Penalties)
            {
                if (penalty.PlayerId == playerId && penalty.MatchMinute == matchMin)
                {
                    game.Protocol.Penalties.Remove(penalty);
                    if (penalty.IsGoal)
                    {
                        this.RemoveGoalFromGame(gameId, playerId, matchMinute);
                    }
                    break;
                }
            }
        }

        public IEnumerable<Game> Search(string searchText, StringComparison comparison
            = StringComparison.InvariantCultureIgnoreCase)
        {
            return this.GetAll().Where(x => x.Location.ToString().Contains(searchText, comparison)
            || 
            x.MatchDate.ToString().Contains(searchText, comparison)
            ||
            DomainService.FindSeriesById(x.SeriesId).SeriesName.Contains(searchText, comparison)
            ||
            x.Protocol.Goals.Count.ToString().Contains(searchText, comparison)
            ||
            x.Protocol.GameResult.ToString().Contains(searchText, comparison)
            ||
            DomainService.FindTeamById(x.HomeTeamId).ToString().Contains(searchText, comparison)
            ||
            DomainService.FindTeamById(x.AwayTeamId).ToString().Contains(searchText, comparison)
            || 
            x.Protocol.HomeTeamActivePlayers.Any(y => DomainService.FindPlayerById(y).ToString()
                .Contains(searchText, comparison)
            ||
            x.Protocol.AwayTeamActivePlayers.Any(z => DomainService.FindPlayerById(z).ToString()
                .Contains(searchText, comparison))));
            
        }
    }
}