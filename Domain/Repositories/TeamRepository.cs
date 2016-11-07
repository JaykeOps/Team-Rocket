using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Repositories
{
    internal sealed class TeamRepository
    {
        private List<Team> teams;
        public static readonly TeamRepository Instance = new TeamRepository();

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