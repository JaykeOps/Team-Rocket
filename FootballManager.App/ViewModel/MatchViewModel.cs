using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Services;

namespace FootballManager.App.ViewModel
{
    public class MatchViewModel : ViewModelBase
    {
        private ObservableCollection<Match> matches;
        private ObservableCollection<Game> games;
        private GameService gameService;
        private MatchService matchService;

        public MatchViewModel()
        {
            this.gameService=new GameService();
            this.matchService=new MatchService();

        }
    }
}
