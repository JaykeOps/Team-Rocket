using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Domain.Entities;
using Domain.Services;
using FootballManager.App.Extensions;
using FootballManager.App.Utility;
using FootballManager.App.View;

namespace FootballManager.App.ViewModel
{
    public class MatchViewModel : ViewModelBase
    {
        private GameService gameService;
        private MatchService matchService;
        private ObservableCollection<Match> matches;
        private ObservableCollection<Game> games;
        private string searchText;
        private ICommand openMatchProtocolCommand;

        public ObservableCollection<Match> Matches
        {
            get { return this.matches; }
            set
            {
                this.matches = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Game> Games
        {
            get { return this.games; }
            set
            {
                this.games = value;
                OnPropertyChanged();
            }
        }
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged();
                FilterGames();
                FilterMatches();
            }
        }

        public ICommand OpenMatchProtocolCommand
        {
            get
            {
                if (this.openMatchProtocolCommand == null)
                {
                    this.openMatchProtocolCommand = new RelayCommand(OpenMatchProtocolView);
                }
                return this.openMatchProtocolCommand;
            }
        }

        public MatchViewModel()
        {
            this.gameService = new GameService();
            this.matchService = new MatchService();
            this.LoadData();
        }

        public void LoadData()
        {
            this.games = gameService.GetAll().ToObservableCollection();
            this.matches = matchService.GetAll().ToObservableCollection();
        }
        public void FilterMatches()
        {
            this.Matches = matchService.Search(SearchText).ToObservableCollection();
        }

        public void FilterGames()
        {
            this.Games = gameService.Search(SearchText).ToObservableCollection();
        }

        private void OpenMatchProtocolView(object obj)
        {
            var matchProtocolView = new MatchProtocolView();
            matchProtocolView.ShowDialog();
        }
    }
}
