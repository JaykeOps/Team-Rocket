using Domain.Entities;
using Domain.Helper_Classes;
using Domain.Services;
using Domain.Value_Objects;
using FootballManager.Admin.Utility;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace FootballManager.Admin.ViewModel
{
    public class TeamAddViewModel : ViewModelBase, IDataErrorInfo
    {
        private string teamName;
        private string arenaName;
        private string email;
        private bool allPropertiesValid;
        private Dictionary<string, bool> validProperties;
        private TeamService teamService;

        public TeamAddViewModel()
        {
            this.AddCommand = new RelayCommand(this.Add);
            this.validProperties = new Dictionary<string, bool>();
            this.validProperties.Add("TeamName", false);
            this.validProperties.Add("ArenaName", false);
            this.validProperties.Add("Email", false);
            this.teamService = new TeamService();
        }

        public ICommand AddCommand { get; }

        #region IDataErrorInfo implementation

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
                    case "TeamName":
                        if (string.IsNullOrEmpty(this.TeamName))
                        {
                            validProperties[columnName] = false;
                            ValidateProperties();
                            return string.Empty;
                        }
                        if (!this.TeamName.IsValidTeamName(false)) // Parameter 'bool ignoreCase' set to false.
                        {
                            validProperties[columnName] = false;
                            ValidateProperties();
                            return "Must be 2-40 valid latin characters long!";
                        }
                        break;

                    case "ArenaName":
                        if (string.IsNullOrEmpty(this.ArenaName))
                        {
                            validProperties[columnName] = false;
                            ValidateProperties();
                            return string.Empty;
                        }
                        if (!this.ArenaName.IsValidArenaName(false))
                        {
                            validProperties[columnName] = false;
                            ValidateProperties();
                            return "Must be 2-40 valid latin characters long!";
                        }
                        break;

                    case "Email":
                        if (string.IsNullOrEmpty(this.Email))
                        {
                            validProperties[columnName] = false;
                            ValidateProperties();
                            return string.Empty;
                        }
                        if (!this.Email.IsValidEmailAddress(false))
                        {
                            validProperties[columnName] = false;
                            ValidateProperties();
                            return "Email address does not have a valid format.";
                        }
                        break;
                }
                validProperties[columnName] = true;
                ValidateProperties();
                return string.Empty;
            }
        }

        #endregion IDataErrorInfo implementation

        #region Team Properties

        public string TeamName
        {
            get { return teamName; }
            set
            {
                if (teamName != value)
                {
                    teamName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ArenaName
        {
            get { return arenaName; }
            set
            {
                if (arenaName != value)
                {
                    arenaName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion Team Properties

        public bool AllPropertiesValid
        {
            get { return allPropertiesValid; }
            set
            {
                if (allPropertiesValid != value)
                {
                    allPropertiesValid = value;
                    OnPropertyChanged();
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

        private void Add(object obj)
        {
            var team = new Team(new TeamName(this.teamName), new ArenaName(this.arenaName), new EmailAddress(this.email));
            this.teamService.Add(team);
            this.CloseDialog();
        }

        private void CloseDialog()
        {
            var window = Application.Current.Windows.OfType<Window>().
                FirstOrDefault(w => w.Name == "TeamAddViewModel");
            window?.Close();
        }
    }
}