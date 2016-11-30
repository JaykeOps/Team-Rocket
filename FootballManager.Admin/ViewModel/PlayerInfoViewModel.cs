using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Domain.Entities;
using Domain.Services;
using FootballManager.Admin.Extensions;
using FootballManager.Admin.Utility;

namespace FootballManager.Admin.ViewModel
{
    public class PlayerInfoViewModel : ViewModelBase
    {
        private PlayerService playerService;

        private ICollectionView playerStatsCollection;
        private string searchString;

        public PlayerInfoViewModel()
        {
            this.playerService = new PlayerService();
            PlayerStatsCollection = CollectionViewSource.GetDefaultView(LoadData());
            PlayerStatsCollection.Filter = new Predicate<object>(Filter);
        }

        public ICollectionView PlayerStatsCollection
        {
            get { return playerStatsCollection; }
            set
            {
                playerStatsCollection = value;
                OnPropertyChanged();
            }
        }

        public string SearchString
        {
            get { return searchString; }
            set
            {
                searchString = value;
                OnPropertyChanged();
                FilterCollection();
            }
        }

        private void FilterCollection()
        {
            if (playerStatsCollection != null)
            {
                playerStatsCollection.Refresh();
            }
        }

        private bool Filter(object obj)
        {
            var data = obj as PlayerStats;
            if (data != null)
            {
                if (!string.IsNullOrEmpty(searchString))
                {
                    return data.PlayerName.Contains(searchString);
                }
                return true;
            }
            return false;
        }


        public List<PlayerStats> LoadData()
        {
            var players = playerService.GetAllPlayers();
            var playerInfoData = new List<PlayerStats>();
            foreach (var p in players)
            {
                var playerStats = playerService.GetAllPlayerStats(p.Id).First();
                playerInfoData.Add(playerStats);
            }
            return playerInfoData;
        }
    }
}
