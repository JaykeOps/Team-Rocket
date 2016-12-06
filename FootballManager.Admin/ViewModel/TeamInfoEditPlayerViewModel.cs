using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using FootballManager.Admin.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Domain.Helper_Classes;

namespace FootballManager.Admin.ViewModel
{
    public class TeamInfoEditPlayerViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly PlayerService playerService;

        public TeamInfoEditPlayerViewModel()
        {
            this.playerService = new PlayerService();

            Messenger.Default.Register<IExposablePlayer>(this, OnPlayerObjectRecieved);
        }

        #region Received Player
        private IExposablePlayer receivedPlayer;

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
        #endregion

        #region Shirt Number
        private string shirtNumber;
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
        #endregion

        #region Name
        private Name name;

        public Name Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Position
        private PlayerPosition playerPosition;

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

        public IEnumerable<PlayerPosition> PlayerPositions
        {
            get { return Enum.GetValues(typeof(PlayerPosition)).Cast<PlayerPosition>(); }
        }
        #endregion

        #region Status
        private PlayerStatus playerStatus;

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

        public IEnumerable<PlayerStatus> PlayerStatuses
        {
            get { return Enum.GetValues(typeof(PlayerStatus)).Cast<PlayerStatus>(); }
        }
        #endregion

        #region Save
        private ICommand savePlayerChanges;

        public ICommand SavePlayerChangesCommand
        {
            get
            {
                if (this.savePlayerChanges == null)
                {
                    this.savePlayerChanges = new RelayCommand(this.EditPlayer);
                }
                return this.savePlayerChanges;
            }
        }
        #endregion

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
            this.ReceivedPlayer.Position = this.playerPosition;
            this.ReceivedPlayer.Status = this.playerStatus;

            if (int.Parse(shirtNumber) != -1)
            {
                this.ReceivedPlayer.ShirtNumber =
                    new ShirtNumber(this.ReceivedPlayer.TeamId, int.Parse(shirtNumber));
            }
            this.playerService.Add((Player)this.ReceivedPlayer);
            this.CloseDialog();
        }

        private void CloseDialog()
        {
            var window = Application.Current.Windows.OfType<Window>().
                FirstOrDefault(w => w.Name == "TeamInfoEditPlayerViewDialog");
            window?.Close();
        }
        #endregion

        #region Validaiton Properties
        private bool isShirtNumberValid;

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
        #endregion

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
                            return "Only 0-99 are valid!"; 
                        }                        
                        if (ReceivedPlayer != null)
                        {
                            if (!shirtNumber.IsValidShirtNumber(ReceivedPlayer.TeamId))
                            {
                                this.IsShirtNumberValid = false;
                                return "Only 0-99 are valid!";
                            }
                        }
                        break;
                }
                return string.Empty;
            }
        }
        #endregion
    }
}