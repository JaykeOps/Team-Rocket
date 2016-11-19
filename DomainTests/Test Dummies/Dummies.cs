using System.Runtime.CompilerServices;
using Domain.Entities;
using Domain.Value_Objects;

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
}