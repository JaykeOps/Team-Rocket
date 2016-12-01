using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Domain.Entities;
using Domain.Services;
using FootballManager.Admin.Extensions;
using FootballManager.Admin.Utility;
using FootballManager.Admin.View;

namespace FootballManager.Admin.ViewModel
{
    public class TeamViewModel : ViewModelBase
    {
        private ObservableCollection<Team> teams;
        private TeamService teamService;
        private Team selectedTeam;

        private ICommand deleteTeamCommand;

        public TeamViewModel()
        {
            this.teamService = new TeamService();

            LoadData();

            Messenger.Default.Register<Team>(this, OnTeamObjReceived);
        }

        #region Properties

        public ICommand OpenTeamAddViewCommand { get; }

        public ICommand DeleteTeamCommand
        {
            get
            {
                if (deleteTeamCommand == null)
                {
                    deleteTeamCommand = new RelayCommand(DeleteTeam);
                }
                return deleteTeamCommand;
            }
        }

        public Team SelectedTeam
        {
            get { return selectedTeam; }
            set
            {
                selectedTeam = value;
                OnPropertyChanged();
            }
        }



        public ObservableCollection<Team> Teams
        {
            get { return teams; }
            set
            {
                teams = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Methods

        private void DeleteTeam(object obj)
        {
            teams.Remove(selectedTeam);

        }

        private void OpenTeamAddView(object obj)
        {
            var teamAddView = new TeamAddView();
            teamAddView.ShowDialog();
        }

        private void OnTeamObjReceived(Team team)
        {
            teams.Add(team);
        }

        public void LoadData()
        {
            Teams = teamService.GetAll().ToObservableCollection();
        }

        #endregion
    }
}
