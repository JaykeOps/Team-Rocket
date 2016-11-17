using Domain.CustomExceptions;
using Domain.Entities;
using Domain.Services;
using Domain.Value_Objects;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

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

            for (int i = 0; i < 144; i++)
            {
                this.games.Add(new Game(matches.ElementAt(i)));
            }

            var team1 = DomainService.FindTeamById(this.games.ElementAt(0).HomeTeamId);
            var team2 = DomainService.FindTeamById(this.games.ElementAt(0).AwayTeamId);
            var team3 = DomainService.FindTeamById(this.games.ElementAt(1).HomeTeamId);
            var team4 = DomainService.FindTeamById(this.games.ElementAt(1).AwayTeamId);
            var team5 = DomainService.FindTeamById(this.games.ElementAt(2).HomeTeamId);
            var team6 = DomainService.FindTeamById(this.games.ElementAt(2).AwayTeamId);
            var team7 = DomainService.FindTeamById(this.games.ElementAt(3).HomeTeamId);
            var team8 = DomainService.FindTeamById(this.games.ElementAt(3).AwayTeamId);



            var game1 = this.games.ElementAt(0);
            var game2 = this.games.ElementAt(1);
            var game3 = this.games.ElementAt(2);
            var game4 = this.games.ElementAt(3);

            game1.Protocol.Goals.Add(new Goal(new MatchMinute(14), game1.HomeTeamId, 
                team1.Players.ElementAt(0).Id));
            game1.Protocol.Assists.Add(new Assist(new MatchMinute(14),
                team1.Players.ElementAt(0).Id));
            game1.Protocol.Goals.Add(new Goal(new MatchMinute(82), game1.HomeTeamId,
                team1.Players.ElementAt(0).Id));
            game1.Protocol.Assists.Add(new Assist(new MatchMinute(82),
                team1.Players.ElementAt(0).Id));
            game1.Protocol.Cards.Add(new Card(new MatchMinute(75), team2.Players.ElementAt(0).Id,
                CardType.Red));

        }

        public void Add(Game game)
        {
            if (this.IsAdded(game))
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

            foreach (var game in this.games)
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