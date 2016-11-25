using Domain.Entities;
using Domain.Helper_Classes;
using Domain.Repositories;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services
{
    public class MatchService
    {
        private readonly MatchRepository repository = MatchRepository.instance;

        public void Add(Match match)
        {
            if (match.IsMatchValid())
            {
                this.repository.AddMatch(match);
            }
            else
            {
                throw new FormatException("Match cannot be added. Invalid matchdata");
            }
        }

        public void Add(IEnumerable<Match> matches)
        {
            if (matches != null)
            {
                foreach (var match in matches)
                {
                    this.Add(match);
                }
            }
            else
            {
                    throw new NullReferenceException("List of matches is null");
            }
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

        public void EditMatchTime(DateTime dateTime, Guid matchId)
        {
            var matchToEdit = GetAll().ToList().Find(x => x.Id == matchId);
            matchToEdit.MatchDate = new MatchDateAndTime(dateTime);
        }

        public void EditMatchLocation(string newArenaName, Guid matchId)
        {
            var matchToEdit = GetAll().ToList().Find(x => x.Id == matchId);
            matchToEdit.Location = new ArenaName(newArenaName);
        }
    }
}