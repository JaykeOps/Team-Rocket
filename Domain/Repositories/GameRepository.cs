using Domain.CustomExceptions;
using Domain.Entities;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;

namespace Domain.Repositories
{
    public sealed class GameRepository
    {
        private List<Game> games;
        public static readonly GameRepository instance = new GameRepository();

        private GameRepository()
        {
            Series series = new Series(new MatchDuration(new TimeSpan(0, 90, 0)), new NumberOfTeams(16),"Allsvenskan");
            
            MatchDateAndTime date = new MatchDateAndTime(DateTime.Now + TimeSpan.FromDays(1));
            Team teamYellow = new Team(new TeamName("YellowTeam"), new ArenaName("YellowArena"), new EmailAddress("yellow@gmail.se"));
            Team teamRed = new Team(new TeamName("RedTeam"), new ArenaName("RedArena"), new EmailAddress("red@gmail.se"));
            Team teamGreen = new Team(new TeamName("GreenTeam"), new ArenaName("GreenArena"), new EmailAddress("green@gmail.se"));
            Team teamBlue = new Team(new TeamName("BlueTeam"), new ArenaName("BlueArena"), new EmailAddress("blue@gmail.se"));
            Team teamWhite = new Team(new TeamName("WhiteTeam"), new ArenaName("WhiteArena"), new EmailAddress("white@gmail.se"));
            Team teamBlack = new Team(new TeamName("BlackTeam"), new ArenaName("BlackArena"), new EmailAddress("black@gmail.se"));
            Match matchOne = new Match(teamYellow.ArenaName, teamYellow.Id, teamRed.Id, series, date);
            Match matchTwo = new Match(teamGreen.ArenaName, teamGreen.Id, teamBlue.Id, series, date);
            Match matchThree = new Match(teamWhite.ArenaName, teamWhite.Id, teamBlack.Id, series, date);
            Match matchFour = new Match(teamRed.ArenaName, teamRed.Id, teamYellow.Id, series, date);
            Match matchFive = new Match(teamBlue.ArenaName, teamBlue.Id, teamGreen.Id, series, date);
            Match matchSix = new Match(teamBlack.ArenaName, teamBlack.Id, teamWhite.Id, series, date);
            this.games = new List<Game>()
            {
                new Game(matchOne),
                new Game(matchTwo),
                new Game(matchThree),
                // Returmöten:
                new Game(matchFour),
                new Game(matchFive),
                new Game(matchSix)
            };
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