using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Domain.Entities;
using Domain.Services;
using FootballManager.App.Extensions;
using FootballManager.App.Utility;
using FootballManager.App.View;
using Dragablz;

namespace FootballManager.App.ViewModel
{
    public class PlayerViewModel : ViewModelBase
    {
        private ObservableCollection<Player> players;
        private PlayerService playerService;
        private TeamService teamService;
        private ICommand openPlayerAddViewCommand;
        private ICommand playerInfoCommand;

        public PlayerViewModel()
        {
            this.playerService = new PlayerService();
            this.teamService = new TeamService();

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

        public ObservableCollection<Player> Players
        {
            get { return this.players; }
            set
            {
                this.players = value;
                this.OnPropertyChanged();
            }
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
            this.Players = this.playerService.GetAllPlayers().ToObservableCollection();
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
