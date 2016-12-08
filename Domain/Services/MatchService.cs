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

        public void EditMatchTime(DateTime dateTime, Guid matchId)
        {
            var matchToEdit = this.GetAll().ToList().Find(x => x.Id == matchId);
            matchToEdit.MatchDate = new MatchDateAndTime(dateTime);
        }

        public void EditMatchLocation(string newArenaName, Guid matchId)
        {
            var matchToEdit = this.GetAll().ToList().Find(x => x.Id == matchId);
            matchToEdit.Location = new ArenaName(newArenaName);
        }

        public void RemoveMatch(Guid matchId)
        {
            if (matchId != Guid.Empty)
            {
                this.repository.RemoveMatch(DomainService.FindMatchById(matchId));
                
            }
        }

        public IEnumerable<Match> Search(string searchText, StringComparison comparison
            = StringComparison.InvariantCultureIgnoreCase)
        {
            return this.GetAll().Where(x =>
            x.Id != Guid.Empty && x.SeriesId != Guid.Empty
            && x.HomeTeamId != Guid.Empty && x.AwayTeamId != Guid.Empty
            &&
                (DomainService.FindSeriesById(x.SeriesId).SeriesName.ToString().Contains(searchText, comparison)
                || DomainService.FindTeamById(x.HomeTeamId).Name.ToString().Contains(searchText, comparison)
                || DomainService.FindTeamById(x.AwayTeamId).Name.ToString().Contains(searchText, comparison)
                || x.Location.ToString().Contains(searchText, comparison)
                || x.MatchDate.ToString().Contains(searchText, comparison)));
        }
        internal void Save()
        {
            repository.SaveData();
        }
    }
}