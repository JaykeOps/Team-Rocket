using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using FootballManager.Admin.Extensions;
using FootballManager.Admin.Utility;
using FootballManager.Admin.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace FootballManager.Admin.ViewModel
{
    public class PlayerViewModel : ViewModelBase
    {
        private ObservableCollection<IExposablePlayer> players;
        private PlayerService playerService;
        private TeamService teamService;
        private ICommand openPlayerAddViewCommand;
        private ICommand deletePlayerCommand;
        private ICommand editPlayerCommand;
        private string searchText;
        private Player selectedPlayer;

        public PlayerViewModel()
        {
            this.playerService = new PlayerService();
            this.teamService = new TeamService();

            this.LoadData();
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

        public ICommand DeletePlayerCommand
        {
            get
            {
                if (this.deletePlayerCommand == null)
                {
                    this.deletePlayerCommand = new RelayCommand(this.DeletePlayer);
                }
                return this.deletePlayerCommand;
            }
        }

        public ICommand EditPlayerCommand
        {
            get
            {
                if (this.editPlayerCommand == null)
                {
                    this.editPlayerCommand = new RelayCommand(this.OpenEditPlayerView);
                }
                return this.editPlayerCommand;
            }
        }

        public ObservableCollection<IExposablePlayer> Players
        {
            get { return players; }
            set
            {
                players = value;
                OnPropertyChanged();
            }
        }

        public string SearchText
        {
            get { return this.searchText; }
            set
            {
                this.searchText = value;
                OnPropertyChanged();
                FilterData();
            }
        }

        public Player SelectedPlayer
        {
            get { return this.selectedPlayer; }
            set
            {
                this.selectedPlayer = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties

        #region Methods

        public void FilterData()
        {
            Players = playerService.Search(SearchText).ToObservableCollection();
        }

        private void DeletePlayer(object obj)
        {
            IList playersSelectedIList = (IList)obj;
            List<Player> playersSelectedList = playersSelectedIList.Cast<Player>().ToList();

            foreach (var player in playersSelectedList)
            {
                this.players.Remove(player);
                this.playerService.RemovePlayer(player.Id);
            }
        }

        private void OpenEditPlayerView(object obj)
        {
            var playerEditView = new PlayerEditView();
            Messenger.Default.Send<Player>(this.selectedPlayer);
            playerEditView.ShowDialog();
            this.LoadData();
        }

        private void OpenPlayerAddView(object obj)
        {
            var playerAddView = new PlayerAddView();
            playerAddView.ShowDialog();
            this.LoadData();
        }

        public void LoadData()
        {
            var exposedPlayers = playerService.GetAllExposablePlayers();

            Players = exposedPlayers.ToObservableCollection();
        }

        #endregion Methods

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

        #endregion Combobox population
    }
}