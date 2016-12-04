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
        private string playerViewSearchText;
        private string playerInfoSearchText;

        public TeamViewModel()
        {
            this.teamService = new TeamService();

            this.OpenTeamAddViewCommand = new RelayCommand(this.OpenTeamAddView);
            this.DeleteTeamCommand = new RelayCommand(this.DeleteTeam);
            this.LoadData();

            Messenger.Default.Register<Team>(this, this.OnTeamObjReceived);
        }

        #region Properties

        public ICommand OpenTeamAddViewCommand { get; }
        public ICommand DeleteTeamCommand { get; }

        public ICommand TeamInfoCommand
        {
            get
            {
                if (this.teamInfoCommand == null)
                {
                    this.teamInfoCommand = new RelayCommand(this.TeamInfo);
                }
                return this.teamInfoCommand;
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

        public ObservableCollection<Team> Teams
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

        private void DeleteTeam(object obj)
        {
            this.teams.Remove(this.selectedTeam);

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
            this.teams.Add(team);
        }

        public void LoadData()
        {
            this.Teams = this.teamService.GetAllTeams().ToObservableCollection();
        }

        #endregion
    }
}
