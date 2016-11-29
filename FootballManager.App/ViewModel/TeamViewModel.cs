using System.Collections.ObjectModel;
using System.Windows.Input;
using Domain.Entities;
using Domain.Services;
using FootballManager.App.Extensions;
using FootballManager.App.Utility;
using FootballManager.App.View;
using Dragablz;

namespace FootballManager.App.ViewModel
{
    public class TeamViewModel : ViewModelBase
    {
        private ObservableCollection<Team> teams;
        private TeamService teamService;
        private Team selectedTeam;
        private ICommand teamInfoCommand;

        public TeamViewModel()
        {
            this.teamService = new TeamService();

            this.OpenTeamAddViewCommand = new RelayCommand(OpenTeamAddView);
            this.DeleteTeamCommand = new RelayCommand(DeleteTeam);
            LoadData();

            Messenger.Default.Register<Team>(this, OnTeamObjReceived);
        }

        #region Properties

        public ICommand OpenTeamAddViewCommand { get; }
        public ICommand DeleteTeamCommand { get; }

        public ICommand TeamInfoCommand
        {
            get
            {
                if (teamInfoCommand == null)
                {
                    teamInfoCommand = new RelayCommand(TeamInfo);
                }
                return teamInfoCommand;
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

        private void TeamInfo(object obj)
        {
            TabablzControl teamViewTabablzControl = (TabablzControl)obj;
            teamViewTabablzControl.SelectedIndex = 1;
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
