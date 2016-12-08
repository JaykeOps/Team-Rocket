using Domain.Interfaces;
using Domain.Services;
using FootballManager.Admin.Extensions;
using FootballManager.Admin.Utility;
using FootballManager.Admin.View;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace FootballManager.Admin.ViewModel
{
    public class TeamViewModel : ViewModelBase
    {
        private ObservableCollection<IExposableTeam> teams;
        private TeamService teamService;
        private SeriesService seriesService;
        private IExposableTeam selectedTeam;
        private ICommand openTeamAddView;
        private ICommand deleteTeamCommand;
        private ICommand openEditTeamCommand;
        private string searchText; //TODO:Continue

        public TeamViewModel()
        {
            this.teams = new ObservableCollection<IExposableTeam>();
            this.teamService = new TeamService();
            this.seriesService = new SeriesService();
            this.LoadData();

            Messenger.Default.Register<IExposableTeam>(this, this.OnTeamObjReceived);
        }

        public ICommand OpenTeamAddViewCommand
        {
            get
            {
                return this.openTeamAddView ??
                  (this.openTeamAddView = new RelayCommand(this.OpenTeamAddView));
            }
        }

        public ICommand OpenEditTeamViewCommand
        {
            get
            {
                return this.openEditTeamCommand ??
                  (this.openEditTeamCommand = new RelayCommand(this.OpenEditTeamDialog));
            }
        }

        public ICommand DeleteTeamCommand
        {
            get
            {
                return this.deleteTeamCommand ??
                    (this.deleteTeamCommand = new RelayCommand(this.DeleteTeam));
            }
        }

        public IExposableTeam SelectedTeam
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

        public string SearchText
        {
            get { return this.searchText; }
            set
            {
                this.searchText = value;
                this.OnPropertyChanged();
                this.FilterTeams();
            }
        }

        public void LoadData()
        {
            this.Teams = this.teamService.GetAllIExposableTeams().ToObservableCollection();
        }

        private void DeleteTeam(object obj)
        {
            var allSeries = this.seriesService.GetAll();

            if (allSeries.Count() != 0)
            {
                foreach (var series in allSeries)
                {
                    if (series.TeamIds.Contains(this.selectedTeam.Id))
                    {
                        MessageBox.Show($"Cannot delete {this.selectedTeam}\n" +
                        $"{this.selectedTeam} exists in series {series}");
                    }
                }
            }
            else
            {
                this.teams.Remove(this.selectedTeam);
                this.teamService.RemoveTeam(this.selectedTeam.Id);
            }            
        }
    
    

        private void OpenTeamAddView(object obj)
        {
            var teamAddView = new TeamAddView();
            teamAddView.ShowDialog();
            this.LoadData();
        }

        private void OnTeamObjReceived(IExposableTeam team)
        {
            this.SelectedTeam = team;
        }

        private void OpenEditTeamDialog(object obj)
        {
            var teamEditView = new TeamEditView();
            Messenger.Default.Send(this.SelectedTeam);
            teamEditView.ShowDialog();
            this.LoadData();
        }

        private void FilterTeams()
        {
            this.Teams = this.teamService.Search(this.searchText).ToObservableCollection();
        }
    }
}