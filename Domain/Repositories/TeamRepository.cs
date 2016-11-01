using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public sealed class TeamRepository
    {

        public List<Team> Teams { get; }
        public static TeamRepository Instance = new TeamRepository();

        private TeamRepository()
        {
            this.Teams = new List<Team>();
        }

        public void Add(Team team)
        {
            this.Teams.Add(team);
        }

        private void Load()
        {
        }

        internal void Save()
        {

        }
    }
}
