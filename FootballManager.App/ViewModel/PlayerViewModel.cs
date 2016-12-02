using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using FootballManager.App.Extensions;
using FootballManager.App.Utility;
using FootballManager.App.View;
using Dragablz;

namespace FootballManager.App.ViewModel
{
    public class PlayerViewModel : ViewModelBase
    {
        private ObservableCollection<IExposablePlayer> players;
        private ObservableCollection<PlayerStats> playerStats;
        private PlayerService playerService;
        private TeamService teamService;
        private ICommand openPlayerAddViewCommand;
        private ICommand playerInfoCommand;
        private string playerViewSearchText;
        private string playerInfoSearchText;

        public PlayerViewModel()
        {
            this.playerStats = new ObservableCollection<PlayerStats>();
            this.playerService = new PlayerService();
            this.teamService = new TeamService();
            this.playerViewSearchText = "";
            this.playerInfoSearchText = "";

            this.LoadData();     

            Messenger.Default.Register<Player>(this, this.OnPlayerObjReceived);
        }

        #region Properties
        public ICommand OpenPlayerAddViewCommand
        {
            get
            {
                if (this.openPlayerAddViewCommand == null)
                {
                    this.openPlayerAddViewCommand = new RelayCommand(this.OpenPlayerAddView);
                }
                return this.openPlayerAddViewCommand;
            }
        }

        public ICommand PlayerInfoCommand
        {
            get
            {
                if (this.playerInfoCommand == null)
                {
                    this.playerInfoCommand = new RelayCommand(this.PlayerInfo);
                }
                return this.playerInfoCommand;
            }
        }

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
            get { return this.playerViewSearchText;}
            set
            {
                this.playerViewSearchText = value;
                this.OnPropertyChanged();
                this.LoadPlayerViewData();
            }
        }

        public string PlayerInfoSearchText
        {
            get { return this.playerInfoSearchText; }
            set
            {
                this.playerInfoSearchText = value;
                this.OnPropertyChanged();
                this.LoadPlayerInfoData();
            }
        }

        private void LoadPlayerInfoData()
        {
            this.PlayerStats = this.playerService.GetPlayerStatsFreeTextSearch(
                this.playerInfoSearchText).ToObservableCollection();
        }

        private void LoadPlayerViewData()
        {
            this.Players = this.playerService.Search(this.playerViewSearchText).ToObservableCollection();
        }

        #endregion

        #region Methods

        private void OpenPlayerAddView(object obj)
        {
            var playerAddView = new PlayerAddView();
            playerAddView.ShowDialog();
        }

        private void PlayerInfo(object obj)
        {
            TabablzControl playerViewTabablzControl = (TabablzControl)obj;
            playerViewTabablzControl.SelectedIndex = 1;
        }

        private void OnPlayerObjReceived(Player player)
        {
            this.players.Add(player);
        }

        public void LoadData()
        {
            this.Players = this.playerService.GetAllExposablePlayers().ToObservableCollection();
            this.playerStats = this.playerService.GetPlayerStatsFreeTextSearch(this.playerViewSearchText).ToObservableCollection();
        }

        #endregion

        #region Combobox population
        public IEnumerable<PlayerPosition> PlayerPositions
        {
            get { return Enum.GetValues(typeof(PlayerPosition)).Cast<PlayerPosition>(); }
        }

        public IEnumerable<PlayerStatus> PlayerStatuses
        {
            get { return Enum.GetValues(typeof(PlayerStatus)).Cast<PlayerStatus>(); }

        }

        public IEnumerable<string> TeamNames
        {
            get { return this.teamService.GetAllTeams().Select(x => x.Name.Value); } 
        }
        #endregion
    }
}
