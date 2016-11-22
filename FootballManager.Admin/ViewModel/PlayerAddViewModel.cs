using System;
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
using FootballManager.Admin.Extensions;
using FootballManager.Admin.Utility;
using FootballManager.Admin.View;


namespace FootballManager.Admin.ViewModel
{
    public class PlayerAddViewModel : ViewModelBase
    {
        private TeamService teamService;
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

            this.PlayerAddCommand = new RelayCommand(PlayerAdd);
        }


        #region Properties
        public ICommand PlayerAddCommand { get; }

        public int ShirtNumber
        {
            get { return shirtNumber; }
            set
            {
                if (shirtNumber != value)
                {
                    shirtNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                if (lastName != value)
                {
                    lastName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string DateOfBirth
        {
            get { return dateOfBirth; }
            set
            {
                if (dateOfBirth != value)
                {
                    dateOfBirth = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region ComboBox properties

        public PlayerPosition SelectedPlayerPosition
        {
            get { return selectedPlayerPosition; }
            set
            {
                if (selectedPlayerPosition != value)
                {
                    selectedPlayerPosition = value;
                    OnPropertyChanged();
                }
            }
        }

        public PlayerStatus SelectedPlayerStatus
        {
            get { return selectedPlayerStatus; }
            set
            {
                if (selectedPlayerStatus != value)
                {
                    selectedPlayerStatus = value;
                    OnPropertyChanged();
                }
            }
        }

        public Team SelectedTeam
        {
            get { return selectedTeam; }
            set
            {
                if (selectedTeam != value)
                {
                    selectedTeam = value;
                    OnPropertyChanged();
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
            get { return teamService.GetAll(); }
        }
        #endregion

        #region Methods
        private void PlayerAdd(object obj)
        {
            this.player = new Player(new Name(firstName, lastName), new DateOfBirth(dateOfBirth), selectedPlayerPosition, selectedPlayerStatus);
            player.TeamId = selectedTeam.Id;
            player.ShirtNumber = new ShirtNumber(player.TeamId, shirtNumber);

            Messenger.Default.Send<Player>(player);
        }
        #endregion
    }
}
