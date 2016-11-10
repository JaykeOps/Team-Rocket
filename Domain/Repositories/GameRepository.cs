using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Value_Objects;

namespace Domain.Repositories
{
    internal sealed class GameRepository
    {
        private List<Game> games;
        public static readonly GameRepository instance = new GameRepository();

        private GameRepository()
        {
            MatchDuration matchDuration90Minutes = new MatchDuration(new TimeSpan(0, 90, 0));
            Team teamYellow = new Team(new TeamName("YellowTeam"), new ArenaName("YellowArena"), new EmailAddress("yellow@gmail.se"));
            Team teamRed = new Team(new TeamName("RedTeam"), new ArenaName("RedArena"), new EmailAddress("red@gmail.se"));
            Team teamGreen = new Team(new TeamName("GreenTeam"), new ArenaName("GreenArena"), new EmailAddress("green@gmail.se"));
            Team teamBlue = new Team(new TeamName("BlueTeam"), new ArenaName("BlueArena"), new EmailAddress("blue@gmail.se"));
            Team teamWhite = new Team(new TeamName("WhiteTeam"), new ArenaName("WhiteArena"), new EmailAddress("white@gmail.se"));
            Team teamBlack = new Team(new TeamName("BlackTeam"), new ArenaName("BlackArena"), new EmailAddress("black@gmail.se"));

            this.games = new List<Game>()
            {
                new Game(matchDuration90Minutes, teamYellow.Id, teamRed.Id),
                new Game(matchDuration90Minutes, teamGreen.Id, teamBlue.Id),
                new Game(matchDuration90Minutes, teamWhite.Id, teamBlack.Id),
                // Returmöten:
                new Game(matchDuration90Minutes, teamRed.Id, teamYellow.Id),
                new Game(matchDuration90Minutes, teamBlue.Id, teamGreen.Id),
                new Game(matchDuration90Minutes, teamBlack.Id, teamWhite.Id)
            };
        }

        public void Add(Game game)
        {
            this.games.Add(game);
        }

        public IEnumerable<Game> GetAll()
        {
            return this.games;
        }
    }
}