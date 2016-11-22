using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Domain.Entities;
using Domain.Services;
using Domain.Value_Objects;
using FootballManager.Admin.Utility;

namespace FootballManager.Admin.ViewModel
{
    public class TeamAddViewModel : ViewModelBase
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
