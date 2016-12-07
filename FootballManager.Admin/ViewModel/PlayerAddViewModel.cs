using Domain.Entities;
using Domain.Helper_Classes;
using Domain.Services;
using Domain.Value_Objects;
using FootballManager.Admin.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Globalization;

namespace FootballManager.Admin.ViewModel
{
    public class PlayerAddViewModel : ViewModelBase, IDataErrorInfo
    {
        private TeamService teamService;
        private PlayerService playerService;
        private Player player;
        private Team unAffiliatedTeam;
        private List<Team> allTeams;

        private int shirtNumber;
        private string firstName;
        private string lastName;
        private string dateOfBirth;
        private PlayerPosition selectedPlayerPosition;
        private PlayerStatus selectedPlayerStatus;
        private Team selectedTeam;
        private bool allPropertiesValid;
        private Dictionary<string, bool> validProperties;
        private object selectedItemPlayerPosition;
        private object selectedItemPlayerStatus;
        private object selectedItemTeam;

        public PlayerAddViewModel()
        {
            this.teamService = new TeamService();
            this.playerService = new PlayerService();
            this.PlayerAddCommand = new RelayCommand(this.PlayerAdd);
            this.validProperties = new Dictionary<string, bool>();
            this.allTeams = teamService.GetAllTeams().ToList();
            this.validProperties.Add("FirstName", false);
            this.validProperties.Add("LastName", false);
            this.validProperties.Add("DateOfBirth", false);
            this.validProperties.Add("SelectedItemPlayerPosition", false);
            this.validProperties.Add("SelectedItemPlayerStatus", false);
            this.validProperties.Add("SelectedItemTeam", false);
            this.SelectedItemPlayerPosition = this.PlayerPosition.ElementAt(0);
            this.SelectedItemPlayerStatus = this.PlayerStatus.ElementAt(0);
            this.unAffiliatedTeam = new Team(new TeamName("Unaffiliated"),
                new ArenaName("Unaffiliated"), new EmailAddress("unaffiliated@unaffiliated.com"));
            allTeams.Add(unAffiliatedTeam);
        }

        public string Error
        {
            get { return null; }
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
                            this.validProperties[columnName] = false;
                            ValidateProperties();
                            return string.Empty;
                        }
                        if (!this.FirstName.IsValidName(false)) // Parameter 'bool ignoreCase' set to false.
                        {
                            this.validProperties[columnName] = false;
                            ValidateProperties();
                            return "Must be 2-20 valid European characters long!";
                        }
                        break;

                    case "LastName":
                        if (string.IsNullOrEmpty(this.LastName))
                        {
                            this.validProperties[columnName] = false;
                            ValidateProperties();
                            return string.Empty;
                        }
                        if (!this.LastName.IsValidName(false))
                        {
                            this.validProperties[columnName] = false;
                            ValidateProperties();
                            return "Must be 2-20 valid European characters long!";
                        }
                        break;

                    case "DateOfBirth":
                        if (string.IsNullOrEmpty(this.DateOfBirth))
                        {
                            this.validProperties[columnName] = false;
                            ValidateProperties();
                            return string.Empty;
                        }
                        DateTime dateOfBirth;
                        if (!DateTime.TryParseExact(this.DateOfBirth, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfBirth))
                        {
                            this.validProperties[columnName] = false;
                            ValidateProperties();
                            return "Must be valid date in format \"yyyy-MM-dd\"!";
                        }
                        if (!this.DateOfBirth.IsValidBirthOfDate())
                        {
                            this.validProperties[columnName] = false;
                            ValidateProperties();
                            return "Earliest year = 1936, latest year = [current year - 4]!";
                        }
                        break;

                    case "SelectedItemPlayerPosition":
                        if (this.SelectedItemPlayerPosition == null)
                        {
                            this.validProperties[columnName] = false;
                            ValidateProperties();
                            return string.Empty;
                        }
                        break;

                    case "SelectedItemPlayerStatus":
                        if (this.SelectedItemPlayerStatus == null)
                        {
                            this.validProperties[columnName] = false;
                            ValidateProperties();
                            return string.Empty;
                        }
                        break;

                    case "SelectedItemTeam":
                        if (this.SelectedItemTeam == null)
                        {
                            this.validProperties[columnName] = false;
                            ValidateProperties();
                            return string.Empty;
                        }
                        break;
                }
                this.validProperties[columnName] = true;
                ValidateProperties();
                return string.Empty;
            }
        }

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

        public bool AllPropertiesValid
        {
            get { return this.allPropertiesValid; }
            set
            {
                if (this.allPropertiesValid != value)
                {
                    this.allPropertiesValid = value;
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

        public object SelectedItemPlayerPosition
        {
            get { return this.selectedItemPlayerPosition; }
            set
            {
                if (this.selectedItemPlayerPosition != value)
                {
                    this.selectedItemPlayerPosition = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public object SelectedItemPlayerStatus
        {
            get { return this.selectedItemPlayerStatus; }
            set
            {
                if (this.selectedItemPlayerStatus != value)
                {
                    this.selectedItemPlayerStatus = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public object SelectedItemTeam
        {
            get { return this.selectedItemTeam; }
            set
            {
                if (this.selectedItemTeam != value)
                {
                    this.selectedItemTeam = value;
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

        public List<Team> PlayerTeams
        {
            get { return allTeams; }
        }

        private void PlayerAdd(object obj)
        {
            if (this.firstName != null && this.lastName != null && this.dateOfBirth != null)
            {
                this.player = new Player(new Name(this.firstName, this.lastName), new DateOfBirth(this.dateOfBirth), this.selectedPlayerPosition, this.selectedPlayerStatus);
                Window window = Application.Current.Windows
                    .OfType<Window>().FirstOrDefault(w => w.Name == "AddPlayerWindow");
                this.playerService.Add(this.player);
                window?.Close();
                if (this.selectedTeam != null)
                {
                    if (this.selectedTeam != this.unAffiliatedTeam)
                    {
                        this.playerService.AssignPlayerToTeam(this.player, this.selectedTeam.Id);

                    }

                }
            }
        }

        private void ValidateProperties()
        {
            foreach (var isValid in validProperties.Values)
            {
                if (isValid == false)
                {
                    this.AllPropertiesValid = false;
                    return;
                }
            }
            this.AllPropertiesValid = true;
        }
    }
}