using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Helper_Classes;

namespace Domain.Services
{
    public class MatchService
    {

        private readonly MatchRepository repository = MatchRepository.instance;


        public void AddMatch(Match match)
        {
            this.repository.AddMatch(match);
        }

        public void AddMatches(IEnumerable<Match> matches)
        {
            this.repository.AddMatches(matches);
        }

        public IEnumerable<Match> GetAll()
        {
            return this.repository.GetAll();
        }

        public Match FindById(Guid id)
        {
            return this.GetAll().ToList().Find(m => m.Id == id);
        }

        public IEnumerable<Match> SearchMatch(string searchText, StringComparison comp)
        {
            return this.GetAll().Where(m => m.ToString().Contains(searchText, comp));
        }
       
    }
}
