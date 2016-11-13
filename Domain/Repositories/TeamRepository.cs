using Domain.Entities;
using Domain.Value_Objects;
using System.Collections.Generic;

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

            teams.Add(ManchesterUnited);
            teams.Add(RealMadrid);
            teams.Add(FCBarcelona);
            teams.Add(Arsenal);
            teams.Add(Chelsea);
            teams.Add(BayernMunchen);
            teams.Add(ManchesterCity);
        }
    }
}