using Domain.Entities;
using Domain.Services;
using Domain.Value_Objects;
using System;
using System.Linq;

namespace DomainTests.Test_Dummies
{
    public class DummyPlayers
    {
        public Player DummyPlayerOne { get; }
        public Player DummyPlayerTwo { get; }
        public Player DummyPlayerThree { get; }
        public Player DummyPlayerFour { get; }
        public Player DummyPlayerFive { get; }
        public Player DummyPlayerSix { get; }
        public Player DummyPlayerSeven { get; }
        public Player DummyPlayerEight { get; }
        public Player DummyPlayerNine { get; }
        public Player DummyPlayerTen { get; }
        public Player DummyPlayerEleven { get; }
        public Player DummyPlayerTwelve { get; }

        public DummyPlayers()
        {
            this.DummyPlayerOne = new Player
                (
                new Name("Player", "One"),
                new DateOfBirth("1991-02-24"),
                PlayerPosition.Forward,
                PlayerStatus.Available
                );
            this.DummyPlayerTwo = new Player
                (
                new Name("Player", "Two"),
                new DateOfBirth("1981-07-15"),
                PlayerPosition.Forward,
                PlayerStatus.Available
                );
            this.DummyPlayerThree = this.DummyPlayerTwo = new Player
                (
                new Name("Player", "Three"),
                new DateOfBirth("1995-01-02"),
                PlayerPosition.Midfielder,
                PlayerStatus.Available
                );
            this.DummyPlayerFour = new Player
                (
                new Name("Player", "Four"),
                new DateOfBirth("1999-12-25"),
                PlayerPosition.Midfielder,
                PlayerStatus.Available
                );
            this.DummyPlayerFive = new Player
                (
                new Name("Player", "Five"),
                new DateOfBirth("1994-10-10"),
                PlayerPosition.Goalkeeper,
                PlayerStatus.Available
                );

            this.DummyPlayerSix = new Player
                (
                new Name("Player", "Six"),
                new DateOfBirth("1985-07-05"),
                PlayerPosition.Forward,
                PlayerStatus.Available
                );
            this.DummyPlayerSeven = new Player
                (
                new Name("Player", "Seven"),
                new DateOfBirth("1989-07-28"),
                PlayerPosition.Defender,
                PlayerStatus.Available
                );
            this.DummyPlayerEight = new Player
                (
                new Name("Player", "Eight"),
                new DateOfBirth("1979-04-21"),
                PlayerPosition.Defender,
                PlayerStatus.Available
                );
            this.DummyPlayerNine = new Player
                (
                new Name("Player", "Nine"),
                new DateOfBirth("1975-03-07"),
                PlayerPosition.Forward,
                PlayerStatus.Available
                );
            this.DummyPlayerTen = new Player
                (
                new Name("Player", "Ten"),
                new DateOfBirth("1988-11-17"),
                PlayerPosition.Goalkeeper,
                PlayerStatus.Available
                );
            this.DummyPlayerEleven = new Player
                (
                new Name("Player", "Eleven"),
                new DateOfBirth("1989-07-05"),
                PlayerPosition.Forward,
                PlayerStatus.Available
                );
            this.DummyPlayerTwelve = new Player
                (
                new Name("Player", "Twelve"),
                new DateOfBirth("1994-05-12"),
                PlayerPosition.Forward,
                PlayerStatus.Available
                );

            var playerService = new PlayerService();
            playerService.Add(this.DummyPlayerOne);
            playerService.Add(this.DummyPlayerTwo);
            playerService.Add(this.DummyPlayerThree);
            playerService.Add(this.DummyPlayerFour);
            playerService.Add(this.DummyPlayerFive);
            playerService.Add(this.DummyPlayerSix);
            playerService.Add(this.DummyPlayerSeven);
            playerService.Add(this.DummyPlayerEight);
            playerService.Add(this.DummyPlayerNine);
            playerService.Add(this.DummyPlayerTen);
            playerService.Add(this.DummyPlayerEleven);
            playerService.Add(this.DummyPlayerTwelve);
        }
    }

    public class DummyTeams
    {
        public Team DummyTeamOne { get; }
        public Team DummyTeamTwo { get; }
        public Team DummyTeamThree { get; }
        public Team DummyTeamFour { get; }

        public DummyTeams()
        {
            this.DummyTeamOne = new Team
                (
                new TeamName("Dummy TeamOne"),
                new ArenaName("Dummy ArenaOne"),
                new EmailAddress("dummy_TeamOne@dummies.tp")
                );
            this.DummyTeamTwo = new Team
                (
                new TeamName("Dummy TeamTwo"),
                new ArenaName("Dummy ArenaTwo"),
                new EmailAddress("dummy_TeamTwo@dummies.tp")
                );
            this.DummyTeamThree = new Team
                (
                new TeamName("Dummy TeamThree"),
                new ArenaName("Dummy ArenaThree"),
                new EmailAddress("dummy_TeamThree@dummies.tp")
                );
            this.DummyTeamFour = new Team
                (
                new TeamName("Dummy TeamFour"),
                new ArenaName("Dummy ArenaFour"),
                new EmailAddress("dummy_TeamFour@dummies.tp")
                );

            var teamService = new TeamService();
            teamService.Add(this.DummyTeamOne);
            teamService.Add(this.DummyTeamTwo);
            teamService.Add(this.DummyTeamThree);
            teamService.Add(this.DummyTeamFour);
            this.FillTeamsWithPlayer();
        }

        private void FillTeamsWithPlayer()
        {
            var playerService = new PlayerService();
            var dummyPlayers = new DummyPlayers();
            playerService.AssignPlayerToTeam(dummyPlayers.DummyPlayerOne, this.DummyTeamOne.Id);
            playerService.AssignPlayerToTeam(dummyPlayers.DummyPlayerTwo, this.DummyTeamOne.Id);
            playerService.AssignPlayerToTeam(dummyPlayers.DummyPlayerThree, this.DummyTeamOne.Id);
            playerService.AssignPlayerToTeam(dummyPlayers.DummyPlayerFour, this.DummyTeamTwo.Id);
            playerService.AssignPlayerToTeam(dummyPlayers.DummyPlayerFive, this.DummyTeamTwo.Id);
            playerService.AssignPlayerToTeam(dummyPlayers.DummyPlayerSix, this.DummyTeamTwo.Id);
            playerService.AssignPlayerToTeam(dummyPlayers.DummyPlayerSeven, this.DummyTeamThree.Id);
            playerService.AssignPlayerToTeam(dummyPlayers.DummyPlayerEight, this.DummyTeamThree.Id);
            playerService.AssignPlayerToTeam(dummyPlayers.DummyPlayerNine, this.DummyTeamThree.Id);
            playerService.AssignPlayerToTeam(dummyPlayers.DummyPlayerTen, this.DummyTeamFour.Id);
            playerService.AssignPlayerToTeam(dummyPlayers.DummyPlayerEleven, this.DummyTeamFour.Id);
            playerService.AssignPlayerToTeam(dummyPlayers.DummyPlayerTwelve, this.DummyTeamFour.Id);
        }
    }

    public class DummySeries
    {
        public Series SeriesDummy { get; }
        public DummyTeams DummyTeams { get; }

        public DummyGames DummyGames { get; }

        public DummySeries()
        {
            this.SeriesDummy = new Series
                (
                new MatchDuration(new TimeSpan(0, 90, 0)),
                new NumberOfTeams(4),
                new SeriesName("The Dummy Series")
                );
            this.DummyTeams = new DummyTeams();
            this.SeriesDummy.TeamIds.Add(this.DummyTeams.DummyTeamOne.Id);
            this.SeriesDummy.TeamIds.Add(this.DummyTeams.DummyTeamTwo.Id);
            this.SeriesDummy.TeamIds.Add(this.DummyTeams.DummyTeamThree.Id);
            this.SeriesDummy.TeamIds.Add(this.DummyTeams.DummyTeamFour.Id);
            var seriesService = new SeriesService();
            seriesService.Add(this.SeriesDummy);
            this.GeneratDummySeriesSchedual();
            this.DummyGames = new DummyGames(this);
            DomainService.AddSeriesToTeam(this.SeriesDummy);

            //TODO: Un-comment and run first test in PlayerService to generate bin-files!
            //PlayerRepository.instance.SaveData();
            //TeamRepository.instance.SaveData();
            //GameRepository.instance.SaveData();
            //MatchRepository.instance.SaveData();
            //SeriesRepository.instance.SaveData();
        }

        private void GeneratDummySeriesSchedual()
        {
            var teamService = new TeamService();
            teamService.Add(this.DummyTeams.DummyTeamOne);
            teamService.Add(this.DummyTeams.DummyTeamTwo);
            teamService.Add(this.DummyTeams.DummyTeamThree);
            teamService.Add(this.DummyTeams.DummyTeamFour);
            DomainService.ScheduleGenerator(this.SeriesDummy.Id);
        }
    }

    public class DummyGames
    {
        public Game GameOne { get; }
        public Game GameTwo { get; }
        public Game GameThree { get; }
        public Game GameFour { get; }
        public Game GameFive { get; }
        public Game GameSix { get; }
        public Game GameSeven { get; }
        public Game GameEight { get; }
        public Game GameNine { get; }
        public Game GameTen { get; }
        public Game GameEleven { get; }
        public Game GameTwelve { get; }

        public DummyGames(DummySeries dummySeries)
        {
            this.GameOne = new Game(dummySeries.SeriesDummy.Schedule.ElementAt(0));
            this.GameTwo = new Game(dummySeries.SeriesDummy.Schedule.ElementAt(1));
            this.GameThree = new Game(dummySeries.SeriesDummy.Schedule.ElementAt(2));
            this.GameFour = new Game(dummySeries.SeriesDummy.Schedule.ElementAt(3));
            this.GameFive = new Game(dummySeries.SeriesDummy.Schedule.ElementAt(4));
            this.GameSix = new Game(dummySeries.SeriesDummy.Schedule.ElementAt(5));
            this.GameSeven = new Game(dummySeries.SeriesDummy.Schedule.ElementAt(6));
            this.GameEight = new Game(dummySeries.SeriesDummy.Schedule.ElementAt(7));
            this.GameNine = new Game(dummySeries.SeriesDummy.Schedule.ElementAt(8));
            this.GameTen = new Game(dummySeries.SeriesDummy.Schedule.ElementAt(9));
            this.GameEleven = new Game(dummySeries.SeriesDummy.Schedule.ElementAt(10));
            this.GameTwelve = new Game(dummySeries.SeriesDummy.Schedule.ElementAt(11));

            this.GameOne.Protocol.Goals.Add(new Goal(new MatchMinute(28), this.GameOne.HomeTeamId,
                DomainService.FindTeamById(this.GameOne.HomeTeamId).PlayerIds.ElementAt(0)));
            this.GameOne.Protocol.Goals.Add(new Goal(new MatchMinute(48), this.GameOne.HomeTeamId,
                DomainService.FindTeamById(this.GameOne.HomeTeamId).PlayerIds.ElementAt(1)));
            this.GameOne.Protocol.Cards.Add(new Card(new MatchMinute(75), this.GameOne.HomeTeamId,
                DomainService.FindTeamById(this.GameOne.AwayTeamId).PlayerIds.ElementAt(2), CardType.Red));

            this.GameThree.Protocol.Goals.Add(new Goal(new MatchMinute(30), this.GameThree.AwayTeamId,
                DomainService.FindTeamById(this.GameThree.AwayTeamId).PlayerIds.ElementAt(1)));
            this.GameThree.Protocol.Goals.Add(new Goal(new MatchMinute(50), this.GameThree.HomeTeamId,
                DomainService.FindTeamById(this.GameThree.HomeTeamId).PlayerIds.ElementAt(2)));
            this.GameThree.Protocol.Cards.Add(new Card(new MatchMinute(75), this.GameThree.HomeTeamId,
                DomainService.FindTeamById(this.GameThree.AwayTeamId).PlayerIds.ElementAt(1), CardType.Yellow));

            this.GameFive.Protocol.Goals.Add(new Goal(new MatchMinute(20), this.GameFive.AwayTeamId,
                DomainService.FindTeamById(this.GameFive.AwayTeamId).PlayerIds.ElementAt(1)));
            this.GameFive.Protocol.Goals.Add(new Goal(new MatchMinute(45), this.GameFive.HomeTeamId,
                DomainService.FindTeamById(this.GameFive.HomeTeamId).PlayerIds.ElementAt(0)));
            this.GameFive.Protocol.Cards.Add(new Card(new MatchMinute(70), this.GameFive.HomeTeamId,
                DomainService.FindTeamById(this.GameFive.AwayTeamId).PlayerIds.ElementAt(1), CardType.Red));
            this.GameFive.Protocol.Goals.Add(new Goal(new MatchMinute(74), this.GameFive.AwayTeamId,
                DomainService.FindTeamById(this.GameOne.AwayTeamId).PlayerIds.ElementAt(1)));
            this.GameFive.Protocol.Goals.Add(new Goal(new MatchMinute(80), this.GameFive.HomeTeamId,
                DomainService.FindTeamById(this.GameOne.HomeTeamId).PlayerIds.ElementAt(1)));
            this.GameFive.Protocol.Cards.Add(new Card(new MatchMinute(89), this.GameFive.HomeTeamId,
                DomainService.FindTeamById(this.GameFive.HomeTeamId).PlayerIds.ElementAt(0), CardType.Yellow));

            this.GameSeven.Protocol.Goals.Add(new Goal(new MatchMinute(11),
                this.GameSeven.HomeTeamId,
                DomainService.FindTeamById(this.GameSeven.HomeTeamId).PlayerIds.ElementAt(0)));
            this.GameSeven.Protocol.Goals.Add(new Goal(new MatchMinute(35),
                this.GameSeven.HomeTeamId,
                DomainService.FindTeamById(this.GameSeven.HomeTeamId).PlayerIds.ElementAt(1)));
            this.GameSeven.Protocol.Assists.Add(new Assist(new MatchMinute(35), this.GameSeven.HomeTeamId,
                DomainService.FindTeamById(this.GameSeven.HomeTeamId).PlayerIds.ElementAt(0)));
            this.GameSeven.Protocol.Cards.Add(new Card(new MatchMinute(75), this.GameSeven.HomeTeamId,
                DomainService.FindTeamById(this.GameSeven.AwayTeamId).PlayerIds.ElementAt(0),
                CardType.Red));

            this.GameNine.Protocol.Cards.Add(new Card(new MatchMinute(55), this.GameNine.HomeTeamId,
                DomainService.FindTeamById(this.GameNine.HomeTeamId).PlayerIds.ElementAt(2),
                CardType.Yellow));
            this.GameNine.Protocol.Cards.Add(new Card(new MatchMinute(67), this.GameNine.HomeTeamId,
                DomainService.FindTeamById(this.GameNine.AwayTeamId).PlayerIds.ElementAt(1),
                CardType.Yellow));
            this.GameNine.Protocol.Goals.Add(new Goal(new MatchMinute(87),
                this.GameNine.Id,
                DomainService.FindTeamById(this.GameNine.AwayTeamId).PlayerIds.ElementAt(2)));

            this.GameEleven.Protocol.Goals.Add(new Goal(new MatchMinute(35),
                this.GameEleven.AwayTeamId,
                DomainService.FindTeamById(this.GameEleven.AwayTeamId).PlayerIds.ElementAt(0)));
            this.GameEleven.Protocol.Goals.Add(new Goal(new MatchMinute(45),
                this.GameEleven.AwayTeamId,
                DomainService.FindTeamById(this.GameEleven.AwayTeamId).PlayerIds.ElementAt(0)));
            this.GameEleven.Protocol.Goals.Add(new Goal(new MatchMinute(55),
                this.GameEleven.AwayTeamId,
                DomainService.FindTeamById(this.GameEleven.AwayTeamId).PlayerIds.ElementAt(0)));
            this.GameEleven.Protocol.Goals.Add(new Goal(new MatchMinute(62),
                this.GameEleven.HomeTeamId,
                DomainService.FindTeamById(this.GameEleven.HomeTeamId).PlayerIds.ElementAt(0)));
            this.GameEleven.Protocol.Goals.Add(new Goal(new MatchMinute(88),
                this.GameEleven.HomeTeamId,
                DomainService.FindTeamById(this.GameEleven.HomeTeamId).PlayerIds.ElementAt(1)));

            var gameService = new GameService();
            gameService.Add(this.GameOne);
            gameService.Add(this.GameTwo);
            gameService.Add(this.GameThree);
            gameService.Add(this.GameFour);
            gameService.Add(this.GameFive);
            gameService.Add(this.GameSix);
            gameService.Add(this.GameSeven);
            gameService.Add(this.GameEight);
            gameService.Add(this.GameNine);
            gameService.Add(this.GameTen);
            gameService.Add(this.GameEleven);
            gameService.Add(this.GameTwelve);
        }
    }
}