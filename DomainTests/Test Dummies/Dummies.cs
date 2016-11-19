using Domain.Entities;
using Domain.Services;
using Domain.Value_Objects;
using System;
using System.Linq;
using System.Security.AccessControl;

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
                PlayerPosition.MidFielder,
                PlayerStatus.Available
                );
            this.DummyPlayerFour = new Player
                (
                new Name("Player", "Four"),
                new DateOfBirth("1999-12-25"),
                PlayerPosition.MidFielder,
                PlayerStatus.Available
                );
            this.DummyPlayerFive = new Player
                (
                new Name("Player", "Five"),
                new DateOfBirth("1994-10-10"),
                PlayerPosition.GoalKeeper,
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
                PlayerPosition.GoalKeeper,
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

            this.FillTeamsWithPlayer();
            var teamService = new TeamService();
            teamService.AddTeam(this.DummyTeamOne);
            teamService.AddTeam(this.DummyTeamTwo);
            teamService.AddTeam(this.DummyTeamThree);
            teamService.AddTeam(this.DummyTeamFour);
        }

        private void FillTeamsWithPlayer()
        {
            var dummyPlayers = new DummyPlayers();
            this.DummyTeamOne.AddPlayerId(dummyPlayers.DummyPlayerOne.Id);
            this.DummyTeamOne.AddPlayerId(dummyPlayers.DummyPlayerTwo.Id);
            this.DummyTeamOne.AddPlayerId(dummyPlayers.DummyPlayerThree.Id);
            this.DummyTeamTwo.AddPlayerId(dummyPlayers.DummyPlayerFour);
            this.DummyTeamTwo.AddPlayerId(dummyPlayers.DummyPlayerFive.Id);
            this.DummyTeamTwo.AddPlayerId(dummyPlayers.DummyPlayerSix.Id);
            this.DummyTeamThree.AddPlayerId(dummyPlayers.DummyPlayerSeven.Id);
            this.DummyTeamThree.AddPlayerId(dummyPlayers.DummyPlayerEight.Id);
            this.DummyTeamThree.AddPlayerId(dummyPlayers.DummyPlayerNine.Id);
            this.DummyTeamFour.AddPlayerId(dummyPlayers.DummyPlayerTen.Id);
            this.DummyTeamFour.AddPlayerId(dummyPlayers.DummyPlayerEleven.Id);
            this.DummyTeamFour.AddPlayerId(dummyPlayers.DummyPlayerTwelve.Id);
        }
    }

    public class DummySeries
    {
        public Series SeriesDummy { get; }
        public DummyTeams teams { get; }

        public DummySeries()
        {
            this.SeriesDummy = new Series
                (
                new MatchDuration(new TimeSpan(0, 90, 0)),
                new NumberOfTeams(4),
                "The Dummy Series"
                );
            this.teams = new DummyTeams();
            this.SeriesDummy.TeamIds.Add(teams.DummyTeamOne.Id);
            this.SeriesDummy.TeamIds.Add(teams.DummyTeamTwo.Id);
            this.SeriesDummy.TeamIds.Add(teams.DummyTeamThree.Id);
            this.SeriesDummy.TeamIds.Add(teams.DummyTeamFour.Id);
            var seriesService = new SeriesService();
            seriesService.AddSeries(this.SeriesDummy);
        }

        public void GeneratDummySeriesSchedual()
        {
            var teamService = new TeamService();
            teamService.AddTeam(teams.DummyTeamOne);
            teamService.AddTeam(teams.DummyTeamTwo);
            teamService.AddTeam(teams.DummyTeamThree);
            teamService.AddTeam(teams.DummyTeamFour);
            this.SeriesDummy.Schedule = DomainService.ScheduleGenerator(this.SeriesDummy.Id);
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

        public DummyGames()
        {

            var dummySeries = new DummySeries();
            dummySeries.GeneratDummySeriesSchedual();

            this.GameOne = new Game(dummySeries.SeriesDummy.Schedule[0].ElementAt(0));
            this.GameOne.Protocol.Goals.Add(new Goal(new MatchMinute(28), this.GameOne.HomeTeamId, DomainService.FindTeamById(this.GameOne.HomeTeamId).Players.ElementAt(0).Id));
            this.GameOne.Protocol.Goals.Add(new Goal(new MatchMinute(48), this.GameOne.HomeTeamId, DomainService.FindTeamById(this.GameOne.HomeTeamId).Players.ElementAt(1).Id));
            this.GameOne.Protocol.Cards.Add(new Card(new MatchMinute(75), DomainService.FindTeamById(this.GameOne.AwayTeamId).Players.ElementAt(2).Id, CardType.Red));

            this.GameTwo = new Game(dummySeries.SeriesDummy.Schedule[0].ElementAt(1));

            this.GameThree = new Game(dummySeries.SeriesDummy.Schedule[1].ElementAt(0));
            this.GameThree.Protocol.Goals.Add(new Goal(new MatchMinute(30), this.GameThree.AwayTeamId, DomainService.FindTeamById(this.GameThree.AwayTeamId).Players.ElementAt(1).Id));
            this.GameThree.Protocol.Goals.Add(new Goal(new MatchMinute(50), this.GameThree.HomeTeamId, DomainService.FindTeamById(this.GameThree.HomeTeamId).Players.ElementAt(2).Id));
            this.GameThree.Protocol.Cards.Add(new Card(new MatchMinute(75), DomainService.FindTeamById(this.GameThree.AwayTeamId).Players.ElementAt(2).Id, CardType.Yellow));

            this.GameFour = new Game(dummySeries.SeriesDummy.Schedule[1].ElementAt(1));

            this.GameFive = new Game(dummySeries.SeriesDummy.Schedule[2].ElementAt(0));
            this.GameFive.Protocol.Goals.Add(new Goal(new MatchMinute(20), this.GameFive.AwayTeamId, DomainService.FindTeamById(this.GameFive.AwayTeamId).Players.ElementAt(1).Id));
            this.GameFive.Protocol.Goals.Add(new Goal(new MatchMinute(45), this.GameFive.HomeTeamId, DomainService.FindTeamById(this.GameFive.HomeTeamId).Players.ElementAt(0).Id));
            this.GameFive.Protocol.Cards.Add(new Card(new MatchMinute(70), DomainService.FindTeamById(this.GameFive.AwayTeamId).Players.ElementAt(2).Id, CardType.Red));
            this.GameFive.Protocol.Goals.Add(new Goal(new MatchMinute(74), this.GameFive.AwayTeamId, DomainService.FindTeamById(this.GameOne.AwayTeamId).Players.ElementAt(1).Id));
            this.GameFive.Protocol.Goals.Add(new Goal(new MatchMinute(80), this.GameFive.HomeTeamId, DomainService.FindTeamById(this.GameOne.HomeTeamId).Players.ElementAt(1).Id));
            this.GameFive.Protocol.Cards.Add(new Card(new MatchMinute(89), DomainService.FindTeamById(this.GameFive.HomeTeamId).Players.ElementAt(2).Id, CardType.Yellow));
            this.GameSix = new Game(dummySeries.SeriesDummy.Schedule[2].ElementAt(1));




        }
    }
}