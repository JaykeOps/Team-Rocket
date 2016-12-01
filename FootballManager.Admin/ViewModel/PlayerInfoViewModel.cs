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
        private string filterString;
        private ICommand searchCommand;
        private ICollectionView playerInfoStats;

        public ICollectionView PlayerInfoStats
        {
            get { return playerInfoStats; }
        }

        public PlayerInfoViewModel()
        {
            this.playerService = new PlayerService();

            playerInfoStats = CollectionViewSource.GetDefaultView(LoadData());
            playerInfoStats.Filter = FilterData;
        }

        public string FilterString
        {
            get { return filterString; }
            set
            {
                filterString = value;
                OnPropertyChanged();
                PlayerInfoStats.Refresh();
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                if (searchCommand == null)
                {
                    searchCommand = new RelayCommand(Search);
                }
                return searchCommand;
            }
        }

        private bool FilterData(object obj)
        {
            var playerStats = obj as PlayerStats;

            if (playerStats != null)
            {
                if (!string.IsNullOrEmpty(filterString))
                {
                    return playerStats.PlayerName.IndexOf(filterString, StringComparison.InvariantCultureIgnoreCase) >= 0;
                }
                return true;
            }
            return false;
        }

        private void Search(object param)
        {
            var text = (string)param;
            FilterString = text;
        }

        public ObservableCollection<PlayerStats> LoadData()
        {
            var players = playerService.GetAllPlayers();
            var playerInfoData = new ObservableCollection<PlayerStats>();
            foreach (var p in players)
            {
                var collectionOfPlayerStats = playerService.GetPlayerStatsFreeTextSearch(p.Name.ToString());
                foreach (var playerStats in collectionOfPlayerStats)
                {
                    playerInfoData.Add(playerStats);
                }
            }
            return playerInfoData;
        }
    }
}
