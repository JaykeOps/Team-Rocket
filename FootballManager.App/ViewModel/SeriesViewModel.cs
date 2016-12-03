using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Domain.Entities;
using Domain.Services;
using Domain.Value_Objects;
using FootballManager.App.Extensions;
using FootballManager.App.Utility;
using FootballManager.App.View;

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
            {
                return this.leagueTable;
            }
            set
            {
                this.leagueTable = value;
                OnPropertyChanged("LeagueTableGrid");
            }
        }

        public ObservableCollection<PlayerStats> TopScorers { get { return this.topScorers; } set { } }
        public ObservableCollection<PlayerStats> TopAssists { get { return this.topAssists; } set { } }
        public ObservableCollection<PlayerStats> TopYellowCards { get { return this.TopYellowCards; } set { } }
        public ObservableCollection<PlayerStats> TopRedCards { get { return this.topRedCards; } set {} }

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
            }
        }
        public SeriesViewModel()
        {
            this.playerService = new PlayerService();
            this.seriesService = new SeriesService();
            this.allSeries = seriesService.GetAll().ToObservableCollection();
        }

        public void LoadLeagueTable()
        {
            var test= seriesService.GetLeagueTablePlacement(seriesForLeagueTable.Id).ToObservableCollection();
            this.LeagueTable = test;

        }

    }
}
