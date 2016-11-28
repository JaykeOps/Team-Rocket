using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Domain.Entities;
using Domain.Services;
using FootballManager.Admin.Extensions;
using FootballManager.Admin.Utility;
using FootballManager.Admin.View;

namespace FootballManager.Admin.ViewModel
{
    public class PlayerViewModel : ViewModelBase
    {
        private ObservableCollection<Player> players;
        private PlayerService playerService;
        private TeamService teamService;
        private ICommand openPlayerAddViewCommand;
        private ICommand deletePlayerCommand;
        private string searchText;

        public PlayerViewModel()
        {
            this.playerService = new PlayerService();
            this.teamService = new TeamService();

            LoadData();     

            Messenger.Default.Register<Player>(this, OnPlayerObjReceived);
        }



        #region Properties
        public ICommand OpenPlayerAddViewCommand
        {
            get
            {
                if (openPlayerAddViewCommand == null)
                {
                    openPlayerAddViewCommand = new RelayCommand(OpenPlayerAddView);
                }
                return openPlayerAddViewCommand;
            }
        }

        public ICommand DeletePlayerCommand
        {
            get
            {
                if (deletePlayerCommand == null)
                {
                    deletePlayerCommand = new RelayCommand(DeletePlayer);
                }
                return deletePlayerCommand;
            }            
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

        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region Methods
        private void DeletePlayer(object obj)
        {
            IList playersSelectedIList = (IList)obj;
            List<Player> playersSelectedList = playersSelectedIList.Cast<Player>().ToList();

            foreach (var player in playersSelectedList)
            {
                players.Remove(player);
            }
        }

        private void OpenPlayerAddView(object obj)
        {
            var playerAddView = new PlayerAddView();
            playerAddView.ShowDialog();
        }

        private void OnPlayerObjReceived(Player player)
        {
            playerService.Add(player);
            LoadData();
        }

        public void LoadData()
        {
            Players = playerService.GetAllPlayers().ToObservableCollection();
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
            get { return teamService.GetAll().Select(x => x.Name.Value); } 
        }
        #endregion
    }
}
