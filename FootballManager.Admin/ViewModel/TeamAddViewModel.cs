using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Domain.Entities;
using Domain.Services;
using Domain.Value_Objects;
using Domain.Helper_Classes;
using FootballManager.Admin.Utility;

namespace FootballManager.Admin.ViewModel
{
    public class TeamAddViewModel : ViewModelBase, IDataErrorInfo
    {        
        private Team team;

        private string teamName;
        private string arenaName;
        private string email;

        public ICommand AddCommand { get; }

        public TeamAddViewModel()
        {
            this.AddCommand = new RelayCommand(Add);
        }

        private void Add(object obj)
        {
            this.team = new Team(new TeamName(teamName), new ArenaName(arenaName), new EmailAddress(email));

            Messenger.Default.Send<Team>(team);
        }

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
                            return string.Empty;
                        }
                        if (!this.TeamName.IsValidTeamName(false)) // Parameter 'bool ignoreCase' set to false.
                        {
                            return "Must be 2-40 valid European characters long!";
                        }
                        break;
                    case "ArenaName":
                        if (string.IsNullOrEmpty(this.ArenaName))
                        {
                            return string.Empty;
                        }
                        if (!this.ArenaName.IsValidArenaName(false)) 
                        {
                            return "Must be 2-40 valid European characters long!";
                        }
                        break;
                    case "Email":
                        if (string.IsNullOrEmpty(this.Email))
                        {
                            return string.Empty;
                        }
                        if (!this.Email.IsValidEmailAddress(false))
                        {
                            return "Email address does not have a valid format.";
                        }
                        break;
                }
                return string.Empty;
            }
        }
        #endregion

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

        #endregion
    }
}
