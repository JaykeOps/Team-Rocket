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
        private ObservableCollection<PlayerStats> playersInfoStats;

        public PlayerInfoViewModel()
        {
            this.playerService = new PlayerService();
            this.seriesService = new SeriesService();
            this.players = new ObservableCollection<Player>();
            this.playersInfoStats = new ObservableCollection<PlayerStats>();
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

        public ObservableCollection<PlayerStats> PlayersInfoStats
        {
            get { return playersInfoStats; }
            set
            {
                playersInfoStats = value;
                OnPropertyChanged();
            }
        }

        public void LoadData()
        {
            Players = playerService.GetAllPlayers().ToObservableCollection();


            
        }
    }
}
