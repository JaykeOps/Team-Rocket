using Domain.Helper_Classes;
using Domain.Interfaces;
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
    public class TeamEditViewModel : ViewModelBase, IDataErrorInfo
    {
        private TeamService teamService;
        private IExposableTeam selectedTeam;
        private string teamName;
        private string arenaName;
        private string email;
        private Dictionary<string, bool> validProperties;
        private bool allPropertiesValid;

        public TeamEditViewModel()
        {
            this.teamService = new TeamService();
            Messenger.Default.Register<IExposableTeam>(this, this.OnTeamObjectRecieved);
            this.SaveTeamChangesCommand = new RelayCommand(this.EditTeam);
            validProperties = new Dictionary<string, bool>();
            validProperties.Add("TeamName", false);
            validProperties.Add("ArenaName", false);
            validProperties.Add("Email", false);
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
                    case "TeamName":
                        string teamName = this.teamName.ToString();
                        if (!teamName.IsValidTeamName(false)) // Parameter 'bool ignoreCase' set to false.
                        {
                            validProperties[columnName] = false;
                            ValidateProperties();
                            return "Must be 2-40 valid latin characters long!";
                        }
                        break;

                    case "ArenaName":
                        string arenaName = this.ArenaName.ToString();
                        if (!arenaName.IsValidArenaName(false))
                        {
                            validProperties[columnName] = false;
                            ValidateProperties();
                            return "Must be 2-40 valid latin characters long!";
                        }
                        break;

                    case "Email":
                        string email = this.Email.ToString();
                        if (!email.IsValidEmailAddress(true))
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

        public bool AllPropertiesValid
        {
            get { return this.allPropertiesValid; }
            set
            {
                if (this.allPropertiesValid != value)
                {
                    this.allPropertiesValid = value;
                    OnPropertyChanged();
                }
            }
        }

        public void ValidateProperties()
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

        public ICommand SaveTeamChangesCommand { get; }

        public IExposableTeam SelectedTeam
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

        public string TeamName
        {
            get { return this.teamName; }
            set
            {
                if (this.teamName != value)
                {
                    this.teamName = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string ArenaName
        {
            get { return this.arenaName; }
            set
            {
                if (this.arenaName != value)
                {
                    this.arenaName = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string Email
        {
            get { return this.email; }
            set
            {
                if (this.email != value)
                {
                    this.email = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private void OnTeamObjectRecieved(IExposableTeam team)
        {
            this.SelectedTeam = team;
            this.TeamName = this.SelectedTeam.Name.Value;
            this.ArenaName = this.SelectedTeam.ArenaName.Value;
            this.Email = this.SelectedTeam.Email.Value;
        }

        private void EditTeam(object obj)
        {
            this.SelectedTeam.Name = new TeamName(this.teamName);
            this.SelectedTeam.ArenaName = new ArenaName(this.arenaName);
            this.SelectedTeam.Email = new EmailAddress(this.email);
            this.teamService.Add(this.SelectedTeam);
            this.CloseDialog();
        }

        private void CloseDialog()
        {
            var window = Application.Current.Windows.OfType<Window>().
                FirstOrDefault(x => x.Name == "TeamEditViewDialog");
            window?.Close();
        }
    }
}