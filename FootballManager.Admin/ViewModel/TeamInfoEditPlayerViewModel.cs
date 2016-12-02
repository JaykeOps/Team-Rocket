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
        private PlayerService playerService;
        private IExposablePlayer player;
        private string name;
        private int shirtNumber;
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
            get { return this.player?.Name?.ToString() ?? "Player name unknown"; }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
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
            this.player = player;
            this.Name = player.Name.ToString();
            this.SelectedPlayerPosition = player.Position;
            this.SelectedPlayerStatus = player.Status;
        }
    }
}