using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using FootballManager.Admin.Extensions;
using FootballManager.Admin.Utility;
using FootballManager.Admin.View;

namespace FootballManager.Admin.ViewModel
{
    public class TeamViewModel : ViewModelBase
    {
        private ObservableCollection<IExposableTeam> teams;
        private TeamService teamService;
        private Team selectedTeam;
        private ICommand openTeamAddView;
        private ICommand deleteTeamCommand;
        private ICommand openEditTeamCommand;

        public TeamViewModel()
        {
            this.teams = new ObservableCollection<IExposableTeam>();
            this.teamService = new TeamService();
            this.LoadData();

            Messenger.Default.Register<Team>(this, this.OnTeamObjReceived);
        }

        #region Properties

        public ICommand OpenTeamAddViewCommand
        {
            get
            {
                if (this.openTeamAddView == null)
                {
                    this.openTeamAddView = new RelayCommand(this.OpenTeamAddView);
                }
                return this.openTeamAddView;
            }
        }

        public ICommand OpenEditTeamViewCommand
        {
            get { return this.openEditTeamCommand ?? 
                    (this.openEditTeamCommand = new RelayCommand(this.OpenEditTeamDialog)); }
        }

        public ICommand DeleteTeamCommand
        {
            get
            {
                if (this.deleteTeamCommand == null)
                {
                    this.deleteTeamCommand = new RelayCommand(this.DeleteTeam);
                }
                return this.deleteTeamCommand;
            }
        }

        public Team SelectedTeam
        {
            get { return this.selectedTeam; }
            set
            {
                this.selectedTeam = value;
                this.OnPropertyChanged();
            }
        }



        public ObservableCollection<IExposableTeam> Teams
        {
            get { return this.teams; }
            set
            {
                this.teams = value;
                this.OnPropertyChanged();
            }
        }

        #endregion

        #region Methods

        public void LoadData()
        {
            this.Teams = this.teamService.GetAllIExposableTeams().ToObservableCollection();
        }

        private void DeleteTeam(object obj)
        {
            this.teams.Remove(this.selectedTeam);

        }

        private void OpenTeamAddView(object obj)
        {
            var teamAddView = new TeamAddView();
            teamAddView.ShowDialog();
        }

        private void OnTeamObjReceived(Team team)
        {
            this.teams.Add(team);
        }

        private void OpenEditTeamDialog(object obj)
        {
            var team = (IExposablePlayer) obj;
            var teamEditView = new TeamEditView();
            Messenger.Default.Send(team);
            teamEditView.ShowDialog();
        }

        #endregion
    }
}
