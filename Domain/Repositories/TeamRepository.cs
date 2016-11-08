using Domain.Entities;
using System.Collections.Generic;
using Domain.Value_Objects;

namespace Domain.Repositories
{
    internal sealed class TeamRepository
    {
        private List<Team> teams;
        public static readonly TeamRepository instance = new TeamRepository();

        private TeamRepository()
        {
            this.teams = new List<Team>
            {
                new Team(new TeamName("Ifk Göteborg"),new ArenaName("Ullevi"),new EmailAddress("ifkgoteborg@gmail.com")),
                new Team(new TeamName("Bk Häcken"),new ArenaName("BravidaArena"),new EmailAddress("hacken@gmail.com")),
                new Team(new TeamName("Ifk Norrköping"),new ArenaName("Östgötaporten"),new EmailAddress("ifknorrkoping@gmail.com")),
                new Team(new TeamName("Kalmar FF"),new ArenaName("GuldfågelnArena"),new EmailAddress("kff@gmail.com")),
                
            };
        }

        public void Add(Team team)
        {
            this.teams.Add(team);
        }

        public IEnumerable<Team> GetAll()
        {
            return this.teams;
        }
    }
}