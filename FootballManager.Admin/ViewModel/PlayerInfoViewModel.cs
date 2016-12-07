using Domain.Entities;
using Domain.Services;
using Domain.Value_Objects;
using FootballManager.Admin.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FootballManager.Admin.ViewModel
{
    public class PlayerInfoViewModel : ViewModelBase
    {
        //TODO: Validering av sökfält

        private PlayerService playerService;
        private ObservableCollection<PlayerStats> playerStats;
        private ObservableCollection<Series> allSeries;
        private SeriesService seriesService;
        private string searchText = "";
        private Series seriesForPlayerStats;
        public ObservableCollection<Series> AllSeries => this.allSeries;

        public ObservableCollection<PlayerStats> PlayerStats
        {
            get { return playerStats; }
            set
            {
                playerStats = value;
                OnPropertyChanged();
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

        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged();
                FilterStatsGrid();
            }
        }

        private void FilterStatsGrid()
        {
            var allPlayers = playerService.Search(this.searchText);
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

        public void LoadData()
        {
            PlayerStats = playerService.GetPlayerStatsFreeTextSearch("").ToObservableCollection();
            allSeries = seriesService.GetAll().ToObservableCollection();
            this.FilterStatsGrid();
        }

        public PlayerInfoViewModel()
        {
            this.playerService = new PlayerService();
            this.seriesService = new SeriesService();
            this.playerStats = new ObservableCollection<PlayerStats>();
            LoadData();
        }
    }
}