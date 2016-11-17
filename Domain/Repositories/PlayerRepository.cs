using Domain.Entities;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Domain.Repositories
{
    public sealed class PlayerRepository
    {
        private List<Player> players;
        public static readonly PlayerRepository instance = new PlayerRepository();
        private IFormatter formatter;
        private string filePath;

        private PlayerRepository()
        {
            this.players = new List<Player>();
            this.formatter = new BinaryFormatter();
            this.filePath = @"..//..//players.bin";
            this.LoadData();
        }

        public void Add(Player player)
        {
            this.players.Add(player);
        }

        public IEnumerable<Player> GetAll()
        {
            return this.players;
        }

        public void SaveData()
        {
            try
            {
                using (
                    var streamWriter = new FileStream(this.filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(streamWriter, players);
                }
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"File missing at {this.filePath}." +
                                                "Failed to save data to file!");
            }
            catch (DirectoryNotFoundException ex)
            {
                throw ex;
            }
            catch (SerializationException ex)
            {
                throw ex;
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

        public void LoadData()
        {
            var players = new List<Player>();

            try
            {
                using (
                    var streamReader = new FileStream(this.filePath, FileMode.OpenOrCreate, FileAccess.Read,
                        FileShare.Read))
                {
                    players = (List<Player>)this.formatter.Deserialize(streamReader);
                    this.players = players;
                }
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"File missing at {this.filePath}." +
                                                "Load failed!");
            }
            catch (DirectoryNotFoundException ex)
            {
                throw ex;
            }
            catch (SerializationException ex)
            {
                throw ex;
            }
            catch (IOException ex)
            {
                throw ex;
            }

            //var player1 = new Player(new Name("Zlatan", "Ibrahimovic"), new DateOfBirth("1981-10-03"), PlayerPosition.Forward, PlayerStatus.Available);
            //var player2 = new Player(new Name("Cristiano", "Ronaldo"), new DateOfBirth("1981-10-03"), PlayerPosition.Forward, PlayerStatus.Injured);
            //var player3 = new Player(new Name("Lionel", "Messi"), new DateOfBirth("1987-06-24"), PlayerPosition.Forward, PlayerStatus.Available);
            //var player4 = new Player(new Name("Sergio", "Ramos"), new DateOfBirth("1986-03-30"), PlayerPosition.Defender, PlayerStatus.Absent);
            //var player5 = new Player(new Name("Mesut", "Özil"), new DateOfBirth("1988-10-15"), PlayerPosition.MidFielder, PlayerStatus.Available);
            //var player6 = new Player(new Name("Eden", "Hazard"), new DateOfBirth("1991-01-07"), PlayerPosition.MidFielder, PlayerStatus.Suspended);
            //var player7 = new Player(new Name("Robert", "Lewandowski"), new DateOfBirth("1988-08-21"), PlayerPosition.Forward, PlayerStatus.Available);
            //var player8 = new Player(new Name("Paul", "Pogba"), new DateOfBirth("1993-03-15"), PlayerPosition.MidFielder, PlayerStatus.Available);
            //var player9 = new Player(new Name("Claudio", "Bravo"), new DateOfBirth("1983-04-13"), PlayerPosition.GoalKeeper, PlayerStatus.Available);
            //var player10 = new Player(new Name("Karl", "Svensson"), new DateOfBirth("1997-07-28"), PlayerPosition.Defender, PlayerStatus.Available);
            //var player11 = new Player(new Name("Arne", "Anka"), new DateOfBirth("1993-07-28"), PlayerPosition.Defender, PlayerStatus.Available);
            //var player12 = new Player(new Name("Görnan", "Kropp"), new DateOfBirth("1995-02-08"), PlayerPosition.Defender, PlayerStatus.Available);
            //var player13 = new Player(new Name("Anders", "Taco"), new DateOfBirth("1991-01-11"), PlayerPosition.Defender, PlayerStatus.Available);
            //var player14 = new Player(new Name("Robin", "Söder"), new DateOfBirth("1984-01-18"), PlayerPosition.Defender, PlayerStatus.Available);
            //var player15 = new Player(new Name("Sören", "Reiks"), new DateOfBirth("1954-01-28"), PlayerPosition.Defender, PlayerStatus.Available);
            //var player16 = new Player(new Name("John", "Alvbåge"), new DateOfBirth("1994-07-09"), PlayerPosition.Defender, PlayerStatus.Available);
            //var player17 = new Player(new Name("Benny", "Biltjuv"), new DateOfBirth("1990-01-08"), PlayerPosition.Defender, PlayerStatus.Available);
            //var player18 = new Player(new Name("Johnny", "Bråttom"), new DateOfBirth("1994-07-28"), PlayerPosition.Defender, PlayerStatus.Available);
            //var player19 = new Player(new Name("Bengt", "Andersson"), new DateOfBirth("1994-07-28"), PlayerPosition.Defender, PlayerStatus.Available);
            //var player20 = new Player(new Name("Sven", "Rydell"), new DateOfBirth("1994-07-20"), PlayerPosition.Defender, PlayerStatus.Available);
            //var player21 = new Player(new Name("Erik", "Andersson"), new DateOfBirth("1971-07-21"), PlayerPosition.Defender, PlayerStatus.Available);
            //var player22 = new Player(new Name("John", "Jones"), new DateOfBirth("1982-07-26"), PlayerPosition.Defender, PlayerStatus.Available);
            //var player23 = new Player(new Name("Hard", "Stones"), new DateOfBirth("1991-07-21"), PlayerPosition.Defender, PlayerStatus.Available);
            //var player24 = new Player(new Name("Big", "Stones"), new DateOfBirth("1987-04-28"), PlayerPosition.Defender, PlayerStatus.Available);
            //var player25 = new Player(new Name("Round", "Stones"), new DateOfBirth("1984-02-08"), PlayerPosition.Defender, PlayerStatus.Available);
            //var player26 = new Player(new Name("Old", "Stones"), new DateOfBirth("1974-07-28"), PlayerPosition.Defender, PlayerStatus.Available);
            //player1.TeamId = DomainService.GetAllTeams().ToList().ElementAt(0).Id;
            //player2.TeamId = DomainService.GetAllTeams().ToList().ElementAt(1).Id;
            //player3.TeamId = DomainService.GetAllTeams().ToList().ElementAt(2).Id;
            //player4.TeamId = DomainService.GetAllTeams().ToList().ElementAt(4).Id;
            //player5.TeamId = DomainService.GetAllTeams().ToList().ElementAt(0).Id;
            //player6.TeamId = DomainService.GetAllTeams().ToList().ElementAt(1).Id;
            //player7.TeamId = DomainService.GetAllTeams().ToList().ElementAt(2).Id;
            //player8.TeamId = DomainService.GetAllTeams().ToList().ElementAt(4).Id;
            //player9.TeamId = DomainService.GetAllTeams().ToList().ElementAt(0).Id;
            //player10.TeamId = DomainService.GetAllTeams().ToList().ElementAt(1).Id;
            //player11.TeamId = DomainService.GetAllTeams().ToList().ElementAt(2).Id;
            //player12.TeamId = DomainService.GetAllTeams().ToList().ElementAt(4).Id;
            //player13.TeamId = DomainService.GetAllTeams().ToList().ElementAt(0).Id;
            //player14.TeamId = DomainService.GetAllTeams().ToList().ElementAt(1).Id;
            //player15.TeamId = DomainService.GetAllTeams().ToList().ElementAt(2).Id;
            //player16.TeamId = DomainService.GetAllTeams().ToList().ElementAt(4).Id;
            //player17.TeamId = DomainService.GetAllTeams().ToList().ElementAt(0).Id;
            //player18.TeamId = DomainService.GetAllTeams().ToList().ElementAt(1).Id;
            //player19.TeamId = DomainService.GetAllTeams().ToList().ElementAt(2).Id;
            //player20.TeamId = DomainService.GetAllTeams().ToList().ElementAt(4).Id;
            //player21.TeamId = DomainService.GetAllTeams().ToList().ElementAt(0).Id;
            //player22.TeamId = DomainService.GetAllTeams().ToList().ElementAt(1).Id;
            //player23.TeamId = DomainService.GetAllTeams().ToList().ElementAt(2).Id;
            //player24.TeamId = DomainService.GetAllTeams().ToList().ElementAt(4).Id;
            //player25.TeamId = DomainService.GetAllTeams().ToList().ElementAt(0).Id;
            //player26.TeamId = DomainService.GetAllTeams().ToList().ElementAt(1).Id;
            //player1.ShirtNumber= new ShirtNumber(1);
            //player2.ShirtNumber = new ShirtNumber(1);
            //player3.ShirtNumber = new ShirtNumber(1);
            //player4.ShirtNumber = new ShirtNumber(1);
            //player5.ShirtNumber = new ShirtNumber(2);
            //player6.ShirtNumber = new ShirtNumber(2);
            //player7.ShirtNumber = new ShirtNumber(2);
            //player8.ShirtNumber = new ShirtNumber(2);
            //player9.ShirtNumber = new ShirtNumber(3);
            //player10.ShirtNumber = new ShirtNumber(3);
            //player11.ShirtNumber = new ShirtNumber(3);
            //player12.ShirtNumber = new ShirtNumber(3);
            //player13.ShirtNumber = new ShirtNumber(4);
            //player14.ShirtNumber = new ShirtNumber(4);
            //player15.ShirtNumber = new ShirtNumber(4);
            //player16.ShirtNumber = new ShirtNumber(4);
            //player17.ShirtNumber = new ShirtNumber(5);
            //player18.ShirtNumber = new ShirtNumber(5);
            //player19.ShirtNumber = new ShirtNumber(5);
            //player20.ShirtNumber = new ShirtNumber(5);
            //player21.ShirtNumber = new ShirtNumber(6);
            //player22.ShirtNumber = new ShirtNumber(6);
            //player23.ShirtNumber = new ShirtNumber(6);
            //player24.ShirtNumber = new ShirtNumber(6);
            //player25.ShirtNumber = new ShirtNumber(7);
            //player26.ShirtNumber = new ShirtNumber(7);

            //var test = player10.ShirtNumber;

            //players.Add(player10);
            //players.Add(player1);
            //players.Add(player2);
            //players.Add(player3);
            //players.Add(player4);
            //players.Add(player5);
            //players.Add(player6);
            //players.Add(player7);
            //players.Add(player8);
            //players.Add(player9);
            //players.Add(player11);
            //players.Add(player12);
            //players.Add(player13);
            //players.Add(player14);
            //players.Add(player15);
            //players.Add(player16);
            //players.Add(player17);
            //players.Add(player18);
            //players.Add(player19);
            //players.Add(player20);
            //players.Add(player21);
            //players.Add(player22);
            //players.Add(player23);
            //players.Add(player24);
            //players.Add(player25);
            //players.Add(player26);
        }
    }
}