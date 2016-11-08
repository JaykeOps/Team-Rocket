using Domain.Value_Objects;
using Domain.Entities;
using System.Collections.Generic;
using Domain.Services;

namespace Domain.Repositories
{
    internal sealed class PlayerRepository
    {
        private List<Player> players;
        public static readonly PlayerRepository instance = new PlayerRepository();
        

        private PlayerRepository()
        {
            this.players = new List<Player>();
            LoadData();
        }

        public void Add(Player team)
        {
            this.players.Add(team);
        }

        public IEnumerable<Player> GetAll()
        {
            return this.players;
        }

        public void LoadData()
        {            
            var player1 = new Player(new Name("Zlatan", "Ibrahimovic"), new DateOfBirth("1981-10-03"), PlayerPosition.Forward, PlayerStatus.Available, new ShirtNumber(1));
            var player2 = new Player(new Name("Cristiano", "Ronaldo"), new DateOfBirth("1981-10-03"), PlayerPosition.Forward, PlayerStatus.Injured, new ShirtNumber(2));
            var player3 = new Player(new Name("Lionel", "Messi"), new DateOfBirth("1987-06-24"), PlayerPosition.Forward, PlayerStatus.Available, new ShirtNumber(3));
            var player4 = new Player(new Name("Sergio", "Ramos"), new DateOfBirth("1986-03-30"), PlayerPosition.Defender, PlayerStatus.Absent, new ShirtNumber(4));
            var player5 = new Player(new Name("Mesut", "Özil"), new DateOfBirth("1988-10-15"), PlayerPosition.MidFielder, PlayerStatus.Available, new ShirtNumber(5));
            var player6 = new Player(new Name("Eden", "Hazard"), new DateOfBirth("1991-01-07"), PlayerPosition.MidFielder, PlayerStatus.Suspended, new ShirtNumber(6));
            var player7 = new Player(new Name("Robert", "Lewandowski"), new DateOfBirth("1988-08-21"), PlayerPosition.Forward, PlayerStatus.Available, new ShirtNumber(7));
            var player8 = new Player(new Name("Paul", "Pogba"), new DateOfBirth("1993-03-15"), PlayerPosition.MidFielder, PlayerStatus.Available, new ShirtNumber(8));
            var player9 = new Player(new Name("Claudio", "Bravo"), new DateOfBirth("1983-04-13"), PlayerPosition.GoalKeeper, PlayerStatus.Available, new ShirtNumber(9));
            var player10 = new Player(new Name("John", "Stones"), new DateOfBirth("1994-07-28"), PlayerPosition.Defender, PlayerStatus.Available, new ShirtNumber(10));

            players.Add(player1);
            players.Add(player2);
            players.Add(player3);
            players.Add(player4);
            players.Add(player5);
            players.Add(player6);
            players.Add(player7);
            players.Add(player8);
            players.Add(player9);
            players.Add(player10);
        }
    }
}
