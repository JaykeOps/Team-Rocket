using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Value_Objects;

namespace Domain.Repositories
{
    public sealed class MatchRepository
    {
        private List<Match> matches;
        public static readonly MatchRepository instance = new MatchRepository();

        private MatchRepository()
        {
            this.matches = new List<Match>();
        }

        
        public IEnumerable<Match> GetAll()
        {
            return this.matches;
        }

        public void AddMatch(Match newMatch)
        {
            this.matches.Add(newMatch);
        }
    }
}
