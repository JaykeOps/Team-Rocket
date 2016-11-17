using System;
using Domain.Entities;
using Domain.Value_Objects;
using System.Collections.Generic;
using System.Linq;
using Domain.Services;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Domain.Repositories
{
    public sealed class TeamRepository
    {
        private List<Team> teams;
        public static readonly TeamRepository instance = new TeamRepository();
        private IFormatter formatter;
        private string filePath;

        private TeamRepository()
        {
            this.teams = new List<Team>();
            this.formatter = new BinaryFormatter();
            this.filePath = @"..//..//teams.bin";
            LoadData();
        }

        public void Add(Team team)
        {
            this.teams.Add(team);
        }

        public IEnumerable<Team> GetAll()
        {
            return this.teams;
        }

        

        public void LoadData()
        {
            var ManchesterUnited = new Team(new TeamName("Manchester United"), new ArenaName("Old Trafford"), new EmailAddress("ManchesterUnited@gmail.com"));
            var RealMadrid = new Team(new TeamName("Real Madrid"), new ArenaName("Santiago Bernabeu"), new EmailAddress("RealMadrid@gmail.com"));
            var FCBarcelona = new Team(new TeamName("FC Barcelona"), new ArenaName("Camp Noud"), new EmailAddress("FCBarcelona@gmail.com"));
            var Arsenal = new Team(new TeamName("Arsenal"), new ArenaName("Emirates Stadium"), new EmailAddress("Arsenal@gmail.com"));
            var Chelsea = new Team(new TeamName("Chelsea"), new ArenaName("Stamford Bridge"), new EmailAddress("Chelsea@gmail.com"));
            var BayernMunchen = new Team(new TeamName("Bayern Munchen"), new ArenaName("Allianz Arena"), new EmailAddress("BayernMunchen@gmail.com"));
            var ManchesterCity = new Team(new TeamName("Manchester City"), new ArenaName("Etihad Stadium"), new EmailAddress("ManchesterCity@gmail.com"));
            var GAIS = new Team(new TeamName("GAIS"), new ArenaName("Ullevi"), new EmailAddress("gais@gais.se"));
            var Ifk = new Team(new TeamName("Ifk Göteborg"), new ArenaName("Ullevi"), new EmailAddress("Ifk@Ifk.se"));
            var hacken = new Team(new TeamName("Häcken"), new ArenaName("Bravida"), new EmailAddress("hacken@gais.se"));
            var Ois = new Team(new TeamName("Öis"), new ArenaName("Ullevi"), new EmailAddress("ois@ois.se"));
            var IfkNorr = new Team(new TeamName("Ifk Norrköping"), new ArenaName("Norrporten"), new EmailAddress("ifkn@ifkn.se"));

            var allPlayers = DomainService.GetAllPlayers();

            ManchesterUnited.AddPlayerId(allPlayers.ElementAt(0));
            ManchesterUnited.AddPlayerId(allPlayers.ElementAt(1));
            RealMadrid.AddPlayerId(allPlayers.ElementAt(2));
            RealMadrid.AddPlayerId(allPlayers.ElementAt(3));
            FCBarcelona.AddPlayerId(allPlayers.ElementAt(4));
            FCBarcelona.AddPlayerId(allPlayers.ElementAt(5));
            Arsenal.AddPlayerId(allPlayers.ElementAt(6));
            Arsenal.AddPlayerId(allPlayers.ElementAt(7));
            Chelsea.AddPlayerId(allPlayers.ElementAt(8));
            Chelsea.AddPlayerId(allPlayers.ElementAt(9));
            BayernMunchen.AddPlayerId(allPlayers.ElementAt(10));
            BayernMunchen.AddPlayerId(allPlayers.ElementAt(11));
            ManchesterCity.AddPlayerId(allPlayers.ElementAt(12));
            ManchesterCity.AddPlayerId(allPlayers.ElementAt(13));
            GAIS.AddPlayerId(allPlayers.ElementAt(14));
            GAIS.AddPlayerId(allPlayers.ElementAt(15));
            Ifk.AddPlayerId(allPlayers.ElementAt(16));
            Ifk.AddPlayerId(allPlayers.ElementAt(17));
            hacken.AddPlayerId(allPlayers.ElementAt(18));
            hacken.AddPlayerId(allPlayers.ElementAt(19));
            Ois.AddPlayerId(allPlayers.ElementAt(20));
            Ois.AddPlayerId(allPlayers.ElementAt(21));
            IfkNorr.AddPlayerId(allPlayers.ElementAt(22));
            IfkNorr.AddPlayerId(allPlayers.ElementAt(23));

            teams.Add(ManchesterUnited);
            teams.Add(RealMadrid);
            teams.Add(FCBarcelona);
            teams.Add(Arsenal);
            teams.Add(Chelsea);
            teams.Add(BayernMunchen);
            teams.Add(ManchesterCity);
            teams.Add(GAIS);
            teams.Add(Ifk);
            teams.Add(hacken);
            teams.Add(Ois);
            teams.Add(IfkNorr);
        }
    }
}