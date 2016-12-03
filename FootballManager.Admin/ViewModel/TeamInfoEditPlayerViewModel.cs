using Domain.Interfaces;
using Domain.Services;
using FootballManager.Admin.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Domain.Entities;

namespace FootballManager.Admin.ViewModel
{
    public class TeamInfoEditPlayerViewModel : ViewModelBase
    {
        private PlayerService playerService;
        private IExposablePlayer selectedPlayer;
        private string name;
        private ShirtNumber shirtNumber;
        private PlayerPosition selectedPlayerPosition;
        private PlayerStatus selectedPlayerStatus;

        public TeamInfoEditPlayerViewModel()
        {
            this.playerService = new PlayerService();
            Messenger.Default.Register<IExposablePlayer>(this, this.OnPlayerObjectRecieved);
        }

        public ICommand SavePlayerChangesCommand { get; }

        public string Name
        {
            get { return this.selectedPlayer?.Name?.ToString() ?? "Player name unknown"; }
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

        public ShirtNumber ShirtNumber
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
            get { return this.selectedPlayerPosition; }
            set
            {
                if (this.selectedPlayerPosition != value)
                {
                    this.selectedPlayerPosition = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public PlayerStatus SelectedPlayerStatus
        {
            get { return this.selectedPlayerStatus; }
            set
            {
                if (this.selectedPlayerStatus != value)
                {
                    this.selectedPlayerStatus = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public IEnumerable<PlayerPosition> PlayerPosition
        {
            get { return Enum.GetValues(typeof(PlayerPosition)).Cast<PlayerPosition>(); }
        }

        public IEnumerable<PlayerStatus> PlayerStatus
        {
            get { return Enum.GetValues(typeof(PlayerStatus)).Cast<PlayerStatus>(); }
        }

        public void OnPlayerObjectRecieved(IExposablePlayer player)
        {
            this.SelectedPlayer = player;
            this.Name = player.Name.ToString();
            this.ShirtNumber = player.ShirtNumber;
            this.SelectedPlayerPosition = player.Position;
            this.SelectedPlayerStatus = player.Status;
        }
    }
}