using Domain.Value_Objects;
using Domain.Entities;
using System.Collections.Generic;
using Domain.Services;
using System;

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

        public void Add(Player player)
        {
            this.players.Add(player);
        }

        public IEnumerable<Player> GetAll()
        {
            return this.players;
        }

        public void LoadData()
        {
            var player1 = new Player(new Name("Zlatan", "Ibrahimovic"), new DateOfBirth("1981-10-03"), PlayerPosition.Forward, PlayerStatus.Available);
            var player2 = new Player(new Name("Cristiano", "Ronaldo"), new DateOfBirth("1981-10-03"), PlayerPosition.Forward, PlayerStatus.Injured);
            var player3 = new Player(new Name("Lionel", "Messi"), new DateOfBirth("1987-06-24"), PlayerPosition.Forward, PlayerStatus.Available);
            var player4 = new Player(new Name("Sergio", "Ramos"), new DateOfBirth("1986-03-30"), PlayerPosition.Defender, PlayerStatus.Absent);
            var player5 = new Player(new Name("Mesut", "Özil"), new DateOfBirth("1988-10-15"), PlayerPosition.MidFielder, PlayerStatus.Available);
            var player6 = new Player(new Name("Eden", "Hazard"), new DateOfBirth("1991-01-07"), PlayerPosition.MidFielder, PlayerStatus.Suspended);
            var player7 = new Player(new Name("Robert", "Lewandowski"), new DateOfBirth("1988-08-21"), PlayerPosition.Forward, PlayerStatus.Available);
            var player8 = new Player(new Name("Paul", "Pogba"), new DateOfBirth("1993-03-15"), PlayerPosition.MidFielder, PlayerStatus.Available);
            var player9 = new Player(new Name("Claudio", "Bravo"), new DateOfBirth("1983-04-13"), PlayerPosition.GoalKeeper, PlayerStatus.Available);
            var player10 = new Player(new Name("John", "Stones"), new DateOfBirth("1994-07-28"), PlayerPosition.Defender, PlayerStatus.Available);
            var events= player1.SeriesEvents[Guid.NewGuid()];
            
            player1.StatsAndEvents.AddGoal((new Goal(new MatchMinute(44), player1.TeamId, player1.Id)));
            player1.StatsAndEvents.AddGoal(new Goal(new MatchMinute(47), player1.TeamId, player1.Id));
            player1.StatsAndEvents.AddAssist(new Assist(new MatchMinute(23), player1.Id));
            player1.StatsAndEvents.AddCard(new Card(new MatchMinute(77), player1.Id, CardType.Yellow));
            player1.StatsAndEvents.AddCard(new Card(new MatchMinute(87), player1.Id, CardType.Yellow));
            player1.StatsAndEvents.AddPenalty(new Penalty(new MatchMinute(87), player1.Id));
            player1.StatsAndEvents.AddGameId(Guid.NewGuid());
            player1.StatsAndEvents.AddGameId(Guid.NewGuid());

            player2.StatsAndEvents.AddGoal(new Goal(new MatchMinute(11), player2.TeamId, player2.Id));
            player2.StatsAndEvents.AddGoal(new Goal(new MatchMinute(22), player2.TeamId, player2.Id));
            player2.StatsAndEvents.AddAssist(new Assist(new MatchMinute(44), player2.Id));
            player2.StatsAndEvents.AddCard(new Card(new MatchMinute(89), player2.Id, CardType.Red));
            player2.StatsAndEvents.AddPenalty(new Penalty(new MatchMinute(87), player2.Id));
            player2.StatsAndEvents.AddGameId(Guid.NewGuid());

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