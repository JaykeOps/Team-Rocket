using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Services;
using FootballManager.App.Extensions;

namespace FootballManager.App.ViewModel
{
    public class MatchViewModel
    {
        private ObservableCollection<Match> matches;
        private ObservableCollection<Game> games;
        private GameService gameService;
        private MatchService matchService;
        private string searchText;

        public ObservableCollection<Match> Matches => this.matches;
        public ObservableCollection<Game> Games => this.games;

        public MatchViewModel()
        {
            this.gameService= new GameService();
            this.matchService=new MatchService();
            this.LoadData();
        }

        public void LoadData()
        {
            this.games = gameService.GetAll().ToObservableCollection();
            this.matches = matchService.GetAll().ToObservableCollection();
        }
    }
}
