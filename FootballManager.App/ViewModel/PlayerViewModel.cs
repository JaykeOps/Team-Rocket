using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using Domain.Value_Objects;
using FootballManager.App.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FootballManager.App.ViewModel
{
    public class PlayerViewModel : ViewModelBase
    {
        private ObservableCollection<IExposablePlayer> players;
        private ObservableCollection<PlayerStats> playerStats;
        private ObservableCollection<Series> allSeries;
        private SeriesService seriesService;
        private PlayerService playerService;
        private string playerViewSearchText;
        private string playerInfoSearchText;
        private Series seriesForPlayerStats;

        public PlayerViewModel()
        {
            this.playerStats = new ObservableCollection<PlayerStats>();
            this.playerService = new PlayerService();
            this.seriesService = new SeriesService();
            this.playerViewSearchText = "";
            this.playerInfoSearchText = "";
            this.LoadData();
        }

        public ObservableCollection<Series> AllSeries => this.allSeries;

        public ObservableCollection<IExposablePlayer> Players
        {
            get { return this.players; }
            set
            {
                this.players = value;
                this.OnPropertyChanged();
            }
        }

        public ObservableCollection<PlayerStats> PlayerStats
        {
            get { return this.playerStats; }
            set
            {
                this.playerStats = value;
                this.OnPropertyChanged();
            }
        }

        public string PlayerViewSearchText
        {
            get { return this.playerViewSearchText; }
            set
            {
                this.playerViewSearchText = value;
                this.OnPropertyChanged();
                this.LoadPlayerViewData();
            }
        }

        public Series SeriesForPlayerStats
        {
            get { return this.seriesForPlayerStats; }
            set
            {
                this.seriesForPlayerStats = value;
                OnPropertyChanged("CbPlayerStats");
                this.FilterStatsGrid();
            }
        }

        public string PlayerInfoSearchText
        {
            get { return this.playerInfoSearchText; }
            set
            {
                this.playerInfoSearchText = value;
                this.OnPropertyChanged();
                this.FilterStatsGrid();
            }
        }

        private void FilterStatsGrid()
        {
            var allPlayers = playerService.Search(this.playerInfoSearchText);
            var playerStats = new List<PlayerStats>();
            foreach (var player in allPlayers)
            {
                if (seriesForPlayerStats != null && this.seriesForPlayerStats.TeamIds.Contains(player.TeamId))
                {
                    try
                    {
                        playerStats.Add(playerService.GetPlayerStatsInSeries(player.Id, seriesForPlayerStats.Id));
                    }
                    catch (SeriesMissingException)
                    {
                    }
                }
            }
            PlayerStats = playerStats.ToObservableCollection();
        }

        private void LoadPlayerViewData()
        {
            this.Players = this.playerService.Search(this.playerViewSearchText).ToObservableCollection();
        }

        private void LoadData()
        {
            this.allSeries = seriesService.GetAll().ToObservableCollection();
            this.Players = this.playerService.GetAllExposablePlayers().ToObservableCollection();
            this.FilterStatsGrid();
        }
    }
}