using Domain.Entities;
using Domain.Helper_Classes;
using Domain.Interfaces;
using Domain.Services;
using FootballManager.Admin.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace FootballManager.Admin.ViewModel
{
    public class TeamInfoEditPlayerViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly PlayerService playerService;

        private IExposablePlayer receivedPlayer;
        private string shirtNumber;
        private Name name;
        private PlayerPosition? selectePlayerPosition;
        private PlayerStatus? selectedPlayerStatus;

        private ICommand savePlayerChanges;

        private bool isShirtNumberValid;

        public TeamInfoEditPlayerViewModel()
        {
            this.playerService = new PlayerService();

            Messenger.Default.Register<IExposablePlayer>(this, this.OnPlayerObjectRecieved);
        }

        #region Properties

        public IExposablePlayer ReceivedPlayer
        {
            get { return this.receivedPlayer; }
            set
            {
                if (this.receivedPlayer != value)
                {
                    this.receivedPlayer = value;
                }
            }
        }

        public string ShirtNumber
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

        public Name Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                OnPropertyChanged();
            }
        }

        public PlayerPosition SelectedPlayerPosition
        {
            get { return selectePlayerPosition.GetValueOrDefault(); }
            set
            {
                if (this.selectePlayerPosition != value)
                {
                    this.selectePlayerPosition = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public PlayerStatus SelectedPlayerStatus
        {
            get { return this.selectedPlayerStatus.GetValueOrDefault(); }
            set
            {
                if (this.selectedPlayerStatus != value)
                {
                    this.selectedPlayerStatus = value;
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

        #endregion Properties

        #region Validaiton Properties

        public bool IsShirtNumberValid
        {
            get { return isShirtNumberValid; }
            set
            {
                if (isShirtNumberValid != value)
                {
                    isShirtNumberValid = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion Validaiton Properties

        #region Commands

        public ICommand SavePlayerChangesCommand
        {
            get
            {
                if (this.savePlayerChanges == null)
                {
                    this.savePlayerChanges = new RelayCommand(this.EditPlayer, this.CanEditPlayer);
                }
                return this.savePlayerChanges;
            }
        }

        #endregion Commands

        #region Methods

        public void OnPlayerObjectRecieved(IExposablePlayer player)
        {
            this.ReceivedPlayer = player;
            this.Name = player.Name;
            this.ShirtNumber = player.ShirtNumber.Value.ToString();
            this.SelectedPlayerPosition = player.Position;
            this.SelectedPlayerStatus = player.Status;
        }

        private void EditPlayer(object obj)
        {
            if (selectePlayerPosition != null || selectedPlayerStatus != null)
            {
                this.ReceivedPlayer.Position = this.selectePlayerPosition.GetValueOrDefault();
                this.ReceivedPlayer.Status = this.selectedPlayerStatus.GetValueOrDefault();
            }

            if (int.Parse(shirtNumber) != -1)
            {
                this.ReceivedPlayer.ShirtNumber =
                    new ShirtNumber(this.ReceivedPlayer.TeamId, int.Parse(shirtNumber));
            }
            this.playerService.Add((Player)this.ReceivedPlayer);
            this.CloseDialog();
        }

        private bool CanEditPlayer(object obj)
        {
            if (selectePlayerPosition == null || selectedPlayerStatus == null)
            {
                return false;
            }
            return true;
        }

        private void CloseDialog()
        {
            var window = Application.Current.Windows.OfType<Window>().
                FirstOrDefault(w => w.Name == "TeamInfoEditPlayerViewDialog");
            window?.Close();
        }

        #endregion Methods

        #region IDataErrorInfo implemetation

        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "ShirtNumber":
                        this.IsShirtNumberValid = true;
                        if (string.IsNullOrEmpty(this.ShirtNumber))
                        {
                            this.IsShirtNumberValid = false;
                            return string.Empty;
                        }
                        int shirtNumber;
                        if (!int.TryParse(this.ShirtNumber, out shirtNumber))
                        {
                            this.IsShirtNumberValid = false;
                            return "Only 0-99 are allowed shirtnumbers! \nEnter '-1' or 'n/a' for unassigned shirtnumber.";
                        }
                        if (ReceivedPlayer != null)
                        {
                            if (!shirtNumber.IsValidShirtNumber(ReceivedPlayer, ReceivedPlayer.TeamId))
                            {
                                this.IsShirtNumberValid = false;
                                return "Only 0-99 are allowed shirtnumbers! \nEnter '-1' or 'n/a' for unassigned shirtnumber.";
                            }
                        }
                        break;
                }
                return string.Empty;
            }
        }

        #endregion IDataErrorInfo implemetation
    }
}