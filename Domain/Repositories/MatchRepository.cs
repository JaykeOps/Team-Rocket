using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Services;
using Domain.Value_Objects;

namespace Domain.Repositories
{
    public sealed class MatchRepository
    {
        private List<Match> matches;
        public static readonly MatchRepository instance = new MatchRepository();

        private MatchRepository()
        {
            this.matches = new List<Match> ();
            Load();
        }

        private void Load()
        {
            var allTeams = DomainService.GetAllTeams().ToList();
            var series = DomainService.GetAllSeries().First();
            var match1= new Match(new ArenaName("Ullevi"),allTeams.ElementAt(0).Id,allTeams.ElementAt(1).Id,series,new MatchDateAndTime(new DateTime(2017, 01, 01, 19, 30, 00)));
            var match2 = new Match(new ArenaName("FriendsArena"), allTeams.ElementAt(2).Id, allTeams.ElementAt(3).Id, series, new MatchDateAndTime(new DateTime(2017, 01, 01, 19, 30, 00)));
            var match3 = new Match(new ArenaName("Swedbank"), allTeams.ElementAt(4).Id, allTeams.ElementAt(5).Id, series, new MatchDateAndTime(new DateTime(2017, 01, 01, 16, 30, 00)));
            var match4 = new Match(new ArenaName("GuldfågelArena"), allTeams.ElementAt(6).Id, allTeams.ElementAt(7).Id, series, new MatchDateAndTime(new DateTime(2017, 01, 08, 19, 30, 00)));
            var match5 = new Match(new ArenaName("LättölArena"), allTeams.ElementAt(8).Id, allTeams.ElementAt(9).Id, series, new MatchDateAndTime(new DateTime(2017, 01, 08, 19, 30, 00)));
            var match6 = new Match(new ArenaName("FalconArena"), allTeams.ElementAt(10).Id, allTeams.ElementAt(11).Id, series, new MatchDateAndTime(new DateTime(2017, 01, 08, 16, 30, 00)));
            var match7 = new Match(new ArenaName("Rambergsvallen"), allTeams.ElementAt(0).Id, allTeams.ElementAt(7).Id, series, new MatchDateAndTime(new DateTime(2017, 01, 12, 19, 30, 00)));
            var match8 = new Match(new ArenaName("Tavlebordsvallen"), allTeams.ElementAt(5).Id, allTeams.ElementAt(1).Id, series, new MatchDateAndTime(new DateTime(2017, 01, 12, 19, 30, 00)));
            var match9 = new Match(new ArenaName("PrioritenArena"), allTeams.ElementAt(4).Id, allTeams.ElementAt(9).Id, series, new MatchDateAndTime(new DateTime(2017, 01, 12, 16, 30, 00)));
            var match10 = new Match(new ArenaName("Kuken"), allTeams.ElementAt(2).Id, allTeams.ElementAt(0).Id, series, new MatchDateAndTime(new DateTime(2017, 01,14, 22, 30, 00)));
            this.matches.Add(match1);
            this.matches.Add(match2);
            this.matches.Add(match3);
            this.matches.Add(match4);
            this.matches.Add(match5);
            this.matches.Add(match6);
            this.matches.Add(match7);
            this.matches.Add(match8);
            this.matches.Add(match9);
            this.matches.Add(match10);


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
