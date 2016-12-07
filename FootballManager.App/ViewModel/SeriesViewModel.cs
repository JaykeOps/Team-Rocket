using Domain.Entities;
using Domain.Services;
using Domain.Value_Objects;
using FootballManager.App.Extensions;
using System.Collections.ObjectModel;

namespace FootballManager.App.ViewModel
{
    public class SeriesViewModel : ViewModelBase
    {
        private PlayerService playerService;

        private SeriesService seriesService;
        private ObservableCollection<Series> allSeries;
        private ObservableCollection<TeamStats> leagueTable;
        private ObservableCollection<PlayerStats> topScorers;
        private ObservableCollection<PlayerStats> topAssists;
        private ObservableCollection<PlayerStats> topYellowCards;
        private ObservableCollection<PlayerStats> topRedCards;
        private Series seriesForLeagueTable;
        private Series seriesForPlayerStats;

        public ObservableCollection<Series> AllSeries => this.allSeries;

        public ObservableCollection<TeamStats> LeagueTable
        {
            get
            { return this.leagueTable; }
            set
            {
                this.leagueTable = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<PlayerStats> TopScorers
        {
            get
            { return this.topScorers; }
            set
            {
                this.topScorers = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<PlayerStats> TopAssists
        {
            get
            {
                return this.topAssists;
            }
            set
            {
                this.topAssists = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<PlayerStats> TopYellowCards
        {
            get
            {
                return this.topYellowCards;
            }
            set
            {
                this.topYellowCards = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<PlayerStats> TopRedCards
        {
            get
            {
                return this.topRedCards;
            }
            set
            {
                this.topRedCards = value;
                OnPropertyChanged();
            }
        }

        public Series SeriesForLeagueTable
        {
            get { return this.seriesForLeagueTable; }
            set
            {
                this.seriesForLeagueTable = value;
                OnPropertyChanged("CbLeagueTable");
                this.LoadLeagueTable();
            }
        }

        public Series SeriesForPlayerStats
        {
            get { return this.seriesForPlayerStats; }
            set
            {
                this.seriesForPlayerStats = value;
                OnPropertyChanged("CbPlayerStats");
                this.LoadPlayersTables();
            }
        }

        public SeriesViewModel()
        {
            this.playerService = new PlayerService();
            this.seriesService = new SeriesService();
            this.allSeries = seriesService.GetAll().ToObservableCollection();
        }

        public void LoadPlayersTables()
        {
            this.TopScorers = playerService.GetTopScorersForSeries(seriesForPlayerStats.Id).ToObservableCollection();
            this.TopAssists = playerService.GetTopAssistsForSeries(seriesForPlayerStats.Id).ToObservableCollection();
            this.TopYellowCards =
            playerService.GetTopYellowCardsForSeries(seriesForPlayerStats.Id).ToObservableCollection();
            this.TopRedCards = playerService.GetTopRedCardsForSeries(seriesForPlayerStats.Id).ToObservableCollection();
        }

        public void LoadLeagueTable()
        {
            var test = seriesService.GetLeagueTablePlacement(seriesForLeagueTable.Id).ToObservableCollection();
            this.LeagueTable = test;
        }
    }
}