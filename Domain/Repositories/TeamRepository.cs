using System;
using Domain.Entities;
using Domain.Value_Objects;
using System.Collections.Generic;
using System.Linq;
using Domain.Services;

namespace Domain.Repositories
{
    public sealed class TeamRepository
    {
        private List<Team> teams;
        public static readonly TeamRepository instance = new TeamRepository();

        private TeamRepository()
        {
            this.teams = new List<Team>();
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

            //ManchesterUnited.AddSeries(DomainService.GetAllSeries().ToList().First(),);
            teams.Add(ManchesterUnited);
            teams.Add(RealMadrid);
            teams.Add(FCBarcelona);
            teams.Add(Arsenal);
            teams.Add(Chelsea);
            teams.Add(BayernMunchen);
            teams.Add(ManchesterCity);
            teams.Add(GAIS);
        }
    }
}