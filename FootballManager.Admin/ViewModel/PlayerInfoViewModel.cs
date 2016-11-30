using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Services;
using FootballManager.Admin.Extensions;

namespace FootballManager.Admin.ViewModel
{
    public class PlayerInfoViewModel : ViewModelBase
    {
        private PlayerService playerService;
        private SeriesService seriesService;
        private ObservableCollection<Player> players;
        private ObservableCollection<PlayerStats> playerStats;

        public PlayerInfoViewModel()
        {
            this.playerService = new PlayerService();
            this.seriesService = new SeriesService();
            this.players = new ObservableCollection<Player>();
            this.playerStats = new ObservableCollection<PlayerStats>();
            LoadData();
        }

        public ObservableCollection<Player> Players
        {
            get { return players; }
            set
            {
                players = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<PlayerStats> PlayersStats
        {
            get { return playerStats; }
            set
            {
                playerStats = value;
                OnPropertyChanged();
            }
        }

        public void LoadData()
        {
            Players = playerService.GetAllPlayers().ToObservableCollection();

            //var allSeries = seriesService.Search();
            //foreach (var player in players)
            //{   
            //    var t = seriesService.Search(player.Name.);
            //    foreach (var series in allSeries)
            //    {
            //        var stats = playerService.GetPlayerStatsInSeries(player.Id, series.Id);
            //        PlayersStats.Add(stats);
            //    }
            //}
        }
    }
}
