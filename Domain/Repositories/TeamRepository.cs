using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Repositories
{
    public sealed class TeamRepository
    {
        private List<Team> teams;
        public static TeamRepository Instance = new TeamRepository();

        private TeamRepository()
        {
            this.teams = new List<Team>();
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