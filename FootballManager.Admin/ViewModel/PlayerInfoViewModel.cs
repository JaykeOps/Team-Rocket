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
        //TODO: Validering av sökfältet

        private PlayerService playerService;
        private ObservableCollection<PlayerStats> playerStats;
        private string searchText;

        public PlayerInfoViewModel()
        {
            this.playerService = new PlayerService();
            this.playerStats = new ObservableCollection<PlayerStats>();
            LoadData();
        }

        public ObservableCollection<PlayerStats> PlayerStats
        {
            get { return playerStats; }
            set
            {
                playerStats = value;
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
                FilterData();
            }
        }

        public void FilterData()
        {
            PlayerStats = playerService.GetPlayerStatsFreeTextSearch(SearchText).ToObservableCollection();
        }

        public void LoadData()
        {
            PlayerStats = playerService.GetPlayerStatsFreeTextSearch("").ToObservableCollection();          
        }
    }
}
