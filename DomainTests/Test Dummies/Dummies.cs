using Domain.Entities;
using Domain.Value_Objects;

namespace DomainTests.Test_Dummies
{
    public class DummyPlayers
    {
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
    }
}