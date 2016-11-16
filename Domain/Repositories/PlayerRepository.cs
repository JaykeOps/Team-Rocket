using Domain.Entities;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Services;

namespace Domain.Repositories
{
    public sealed class PlayerRepository
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
            var player10 = new Player(new Name("Karl", "Svensson"), new DateOfBirth("1997-07-28"), PlayerPosition.Defender, PlayerStatus.Available);
            var player11 = new Player(new Name("Arne", "Anka"), new DateOfBirth("1993-07-28"), PlayerPosition.Defender, PlayerStatus.Available);
            var player12 = new Player(new Name("Görnan", "Kropp"), new DateOfBirth("1995-02-08"), PlayerPosition.Defender, PlayerStatus.Available);
            var player13 = new Player(new Name("Anders", "Taco"), new DateOfBirth("1991-01-11"), PlayerPosition.Defender, PlayerStatus.Available);
            var player14 = new Player(new Name("Robin", "Söder"), new DateOfBirth("1984-01-18"), PlayerPosition.Defender, PlayerStatus.Available);
            var player15 = new Player(new Name("Sören", "Reiks"), new DateOfBirth("1954-01-28"), PlayerPosition.Defender, PlayerStatus.Available);
            var player16 = new Player(new Name("John", "Alvbåge"), new DateOfBirth("1994-07-09"), PlayerPosition.Defender, PlayerStatus.Available);
            var player17 = new Player(new Name("Benny", "Biltjuv"), new DateOfBirth("1990-01-08"), PlayerPosition.Defender, PlayerStatus.Available);
            var player18 = new Player(new Name("Johnny", "Bråttom"), new DateOfBirth("1994-07-28"), PlayerPosition.Defender, PlayerStatus.Available);
            var player19 = new Player(new Name("Bengt", "Andersson"), new DateOfBirth("1994-07-28"), PlayerPosition.Defender, PlayerStatus.Available);
            var player20 = new Player(new Name("Sven", "Rydell"), new DateOfBirth("1994-07-20"), PlayerPosition.Defender, PlayerStatus.Available);
            var player21 = new Player(new Name("Erik", "Andersson"), new DateOfBirth("1971-07-21"), PlayerPosition.Defender, PlayerStatus.Available);
            var player22 = new Player(new Name("John", "Jones"), new DateOfBirth("1982-07-26"), PlayerPosition.Defender, PlayerStatus.Available);
            var player23 = new Player(new Name("Hard", "Stones"), new DateOfBirth("1991-07-21"), PlayerPosition.Defender, PlayerStatus.Available);
            var player24 = new Player(new Name("Big", "Stones"), new DateOfBirth("1987-04-28"), PlayerPosition.Defender, PlayerStatus.Available);
            var player25 = new Player(new Name("Round", "Stones"), new DateOfBirth("1984-02-08"), PlayerPosition.Defender, PlayerStatus.Available);
            var player26 = new Player(new Name("Old", "Stones"), new DateOfBirth("1974-07-28"), PlayerPosition.Defender, PlayerStatus.Available);
            player10.TeamId = DomainService.GetAllTeams().ToList().First().Id;
            player10.ShirtNumber= new ShirtNumber(13);
            var test = player10.ShirtNumber;
            //player1.StatsAndEvents.AddGoal((new Goal(new MatchMinute(44), player1.TeamId, player1.Id)));
            //player1.StatsAndEvents.AddGoal(new Goal(new MatchMinute(47), player1.TeamId, player1.Id));
            //player1.StatsAndEvents.AddAssist(new Assist(new MatchMinute(23), player1.Id));
            //player1.StatsAndEvents.AddCard(new Card(new MatchMinute(77), player1.Id, CardType.Yellow));
            //player1.StatsAndEvents.AddCard(new Card(new MatchMinute(87), player1.Id, CardType.Yellow));
            //player1.StatsAndEvents.AddPenalty(new Penalty(new MatchMinute(87), player1.Id));
            //player1.StatsAndEvents.AddGameId(Guid.NewGuid());
            //player1.StatsAndEvents.AddGameId(Guid.NewGuid());

            //player2.StatsAndEvents.AddGoal(new Goal(new MatchMinute(11), player2.TeamId, player2.Id));
            //player2.StatsAndEvents.AddGoal(new Goal(new MatchMinute(22), player2.TeamId, player2.Id));
            //player2.StatsAndEvents.AddAssist(new Assist(new MatchMinute(44), player2.Id));
            //player2.StatsAndEvents.AddCard(new Card(new MatchMinute(89), player2.Id, CardType.Red));
            //player2.StatsAndEvents.AddPenalty(new Penalty(new MatchMinute(87), player2.Id));
            
            players.Add(player10);
            players.Add(player1);
            players.Add(player2);
            players.Add(player3);
            players.Add(player4);
            players.Add(player5);
            players.Add(player6);
            players.Add(player7);
            players.Add(player8);
            players.Add(player9);
            players.Add(player11);
            players.Add(player12);
            players.Add(player13);
            players.Add(player14);
            players.Add(player15);
            players.Add(player16);
            players.Add(player17);
            players.Add(player18);
            players.Add(player19);
            players.Add(player20);
            players.Add(player21);
            players.Add(player22);
            players.Add(player23);
            players.Add(player24);
            players.Add(player25);
            players.Add(player26);

        }
       
    }
}