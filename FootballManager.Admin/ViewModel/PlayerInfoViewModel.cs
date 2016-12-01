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
            get { return this.playerInfoStats; }
        }

        public PlayerInfoViewModel()
        {
            this.playerService = new PlayerService();

            this.playerInfoStats = CollectionViewSource.GetDefaultView(this.LoadData());
            this.playerInfoStats.Filter = this.FilterData;
        }

        public string FilterString
        {
            get { return this.filterString; }
            set
            {
                this.filterString = value;
                this.OnPropertyChanged();
                this.PlayerInfoStats.Refresh();
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                if (this.searchCommand == null)
                {
                    this.searchCommand = new RelayCommand(this.Search);
                }
                return this.searchCommand;
            }
        }

        private bool FilterData(object obj)
        {
            var playerStats = obj as PlayerStats;

            if (playerStats != null)
            {
                if (!string.IsNullOrEmpty(this.filterString))
                {
                    return playerStats.PlayerName.IndexOf(this.filterString, StringComparison.InvariantCultureIgnoreCase) >= 0;
                }
                return true;
            }
            return false;
        }

        private void Search(object param)
        {
            var text = (string)param;
            this.FilterString = text;
        }

        public ObservableCollection<PlayerStats> LoadData()
        {
            var players = this.playerService.GetAllPlayers();
            var playerInfoData = new ObservableCollection<PlayerStats>();
            foreach (var p in players)
            {
                var collectionOfPlayerStats = this.playerService.GetPlayerStatsFreeTextSearch(p.Name.ToString());
                foreach (var playerStats in collectionOfPlayerStats)
                {
                    playerInfoData.Add(playerStats);
                }
            }
            return playerInfoData;
        }
    }
}
