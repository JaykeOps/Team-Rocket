using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Domain.Entities;
using Domain.Services;
using Domain.Value_Objects;
using Domain.Helper_Classes;
using FootballManager.Admin.Extensions;
using FootballManager.Admin.Utility;
using FootballManager.Admin.View;


namespace FootballManager.Admin.ViewModel
{
    public class PlayerAddViewModel : ViewModelBase, IDataErrorInfo
    {
        private TeamService teamService;
        private PlayerService playerService;
        private Player player;

        private int shirtNumber;
        private string firstName;
        private string lastName;
        private string dateOfBirth;
        private PlayerPosition selectedPlayerPosition;
        private PlayerStatus selectedPlayerStatus;
        private Team selectedTeam;

        public PlayerAddViewModel()
        {           
            this.teamService = new TeamService();
            this.playerService = new PlayerService();
            this.PlayerAddCommand = new RelayCommand(this.PlayerAdd);
        }

        #region Properties
        public ICommand PlayerAddCommand { get; }

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

        public string FirstName
        {
            get { return this.firstName; }
            set
            {
                if (this.firstName != value)
                {
                    this.firstName = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string LastName
        {
            get { return this.lastName; }
            set
            {
                if (this.lastName != value)
                {
                    this.lastName = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string DateOfBirth
        {
            get { return this.dateOfBirth; }
            set
            {
                if (this.dateOfBirth != value)
                {
                    this.dateOfBirth = value;
                    this.OnPropertyChanged();
                }
            }
        }
        #endregion

        #region ComboBox properties

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

        public Team SelectedTeam
        {
            get { return this.selectedTeam; }
            set
            {
                if (this.selectedTeam != value)
                {
                    this.selectedTeam = value;
                    this.OnPropertyChanged();
                }
            }
        }
        #endregion

        #region ComboBox population
        public IEnumerable<PlayerPosition> PlayerPosition
        {
            get { return Enum.GetValues(typeof(PlayerPosition)).Cast<PlayerPosition>(); }
        }

        public IEnumerable<PlayerStatus> PlayerStatus
        {
            get { return Enum.GetValues(typeof(PlayerStatus)).Cast<PlayerStatus>(); }
        }

        public IEnumerable<Team> PlayerTeams
        {
            get { return this.teamService.GetAllTeams(); }
        }
        #endregion

        #region Methods
        private void PlayerAdd(object obj)
        {
            this.player = new Player(new Name(this.firstName, this.lastName), new DateOfBirth(this.dateOfBirth), this.selectedPlayerPosition, this.selectedPlayerStatus);
            this.player.TeamId = this.selectedTeam.Id;

            Messenger.Default.Send<Player>(this.player);
            this.playerService.Add(this.player);
        }
        #endregion

        #region IDataErrorInfo implementation
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
                    case "FirstName":
                        if (string.IsNullOrEmpty(this.FirstName))
                        {
                            return string.Empty;
                        }
                        if (!this.FirstName.IsValidName(false)) // Parameter 'bool ignoreCase' set to false.
                        {
                            return "Must be 2-20 valid European characters long!";
                        }
                        break;
                    case "LastName":
                        if (string.IsNullOrEmpty(this.LastName))
                        {
                            return string.Empty;
                        }
                        if (!this.LastName.IsValidName(false))
                        {
                            return "Must be 2-20 valid European characters long!";
                        }
                        break;
                    case "DateOfBirth":
                        if (string.IsNullOrEmpty(this.DateOfBirth))
                        {
                            return string.Empty;
                        }
                        if (!this.DateOfBirth.IsValidDateOfBirth())
                        {
                            return "Must be valid date in format \"yyyy-MM-dd\"";
                        }
                        break;
                }
                return string.Empty;
            }
        }
        #endregion
    }
}
