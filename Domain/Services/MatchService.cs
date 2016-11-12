using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class MatchService
    {

        private readonly MatchRepository repository = MatchRepository.instance;


        public void AddMatch(Match match)
        {
            this.repository.AddMatch(match);
        }

        public IEnumerable<Match> GetAll()
        {
            return this.repository.GetAll();
        }

        public Match FindById(Guid id)
        {
            return this.GetAll().ToList().Find(m => m.Id == id);
        }
       
    }
}
