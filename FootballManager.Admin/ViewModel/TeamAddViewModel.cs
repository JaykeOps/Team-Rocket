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
            this.AddCommand = new RelayCommand(this.Add);
        }

        private void Add(object obj)
        {
            this.team = new Team(new TeamName(this.teamName), new ArenaName(this.arenaName), new EmailAddress(this.email));

            Messenger.Default.Send<Team>(this.team);
        }


        #region Team Properties       
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

        #endregion
    }
}
