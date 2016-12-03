using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using FootballManager.Admin.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace FootballManager.Admin.ViewModel
{
    public class TeamInfoEditPlayerViewModel : ViewModelBase
    {
        private readonly PlayerService playerService;
        private IExposablePlayer selectedPlayer;
        private Name name;
        private int shirtNumber;
        private PlayerPosition playerPosition;
        private PlayerStatus playerStatus;

        public TeamInfoEditPlayerViewModel()
        {
            this.playerService = new PlayerService();
            Messenger.Default.Register<IExposablePlayer>(this, this.OnPlayerObjectRecieved);
            this.SavePlayerChangesCommand = new RelayCommand(this.EditPlayer);
        }

        public ICommand SavePlayerChangesCommand { get; }

        public Name Name
        {
            get { return this.selectedPlayer?.Name ?? new Name("Not", "Available"); }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public IExposablePlayer SelectedPlayer
        {
            get { return this.selectedPlayer; }
            set
            {
                if (this.selectedPlayer != value)
                {
                    this.selectedPlayer = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public int ShirtNumber
        {
            get { return this.shirtNumber; }
            set
            {
                if (this.shirtNumber != value)
                {
                    this.shirtNumber = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public PlayerPosition SelectedPlayerPosition
        {
            get { return this.playerPosition; }
            set
            {
                if (this.playerPosition != value)
                {
                    this.playerPosition = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public PlayerStatus SelectedPlayerStatus
        {
            get { return this.playerStatus; }
            set
            {
                if (this.playerStatus != value)
                {
                    this.playerStatus = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public IEnumerable<PlayerPosition> PlayerPositions
        {
            get { return Enum.GetValues(typeof(PlayerPosition)).Cast<PlayerPosition>(); }
        }

        public IEnumerable<PlayerStatus> PlayerStatuses
        {
            get { return Enum.GetValues(typeof(PlayerStatus)).Cast<PlayerStatus>(); }
        }

        public void OnPlayerObjectRecieved(IExposablePlayer player)
        {
            this.SelectedPlayer = player;
            this.Name = player.Name;
            this.ShirtNumber = player.ShirtNumber.Value;
            this.SelectedPlayerPosition = player.Position;
            this.SelectedPlayerStatus = player.Status;
        }

        private void EditPlayer(object obj)
        {
            this.SelectedPlayer.Position = this.playerPosition;
            this.SelectedPlayer.Status = this.playerStatus;
            this.SelectedPlayer.ShirtNumber = new ShirtNumber(this.SelectedPlayer.TeamId, this.shirtNumber);
            this.playerService.Add((Player)this.SelectedPlayer);
        }
    }
}