using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FootballManager.Admin.Utility;
using FootballManager.Admin.View;
using MaterialDesignThemes.Wpf;
using Domain.Entities;
using System.Collections.ObjectModel;
using Domain.Services;
using FootballManager.Admin.Extensions;
using System.Collections;
using Domain.Value_Objects;

namespace FootballManager.Admin.ViewModel
{
    public class SeriesViewModel : ViewModelBase
    {
        private ObservableCollection<Team> availableTeams;
        private ObservableCollection<Team> teamsToAddToSeries;
        private List<int> numberOfTeamsList;
        private TeamService teamService;
        private SeriesService seriesService;
        private Team selectedTeam;
        private string seriesName;
        private int matchDuration;
        private string searchText;
        private int selectedNumberOfTeams;

        public SeriesViewModel()
        {
            this.teamsToAddToSeries = new ObservableCollection<Team>();
            this.numberOfTeamsList = new List<int>();
            this.teamService = new TeamService();
            this.seriesService = new SeriesService();
            this.AddTeamCommand = new RelayCommand(this.AddTeam);
            this.DeleteTeamCommand = new RelayCommand(this.DeleteTeam);
            this.AddSeriesCommand = new RelayCommand(this.AddSeriesTeam);
            this.LoadData();
        }

        public ICommand DeleteTeamCommand { get; }

        public ICommand AddTeamCommand { get; }

        public ICommand AddSeriesCommand { get; }

        public ObservableCollection<Team> AvailableTeams
        {
            get { return this.availableTeams; }
            set
            {
                this.availableTeams = value;
                this.OnPropertyChanged();                
            }
        }

        public ObservableCollection<Team> TeamsToAddToSeries
        {
            get { return this.teamsToAddToSeries; }
            set
            {
                this.teamsToAddToSeries = value;
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
                this.AvailableTeams = this.teamService.GetAllTeams().
                    Where(x => x.Name.Value.ToLower().
                    Contains(this.searchText.ToLower())).ToObservableCollection();
            }
        }

        public int SelectedNumberOfTeams
        {
            get { return this.selectedNumberOfTeams; }
            set
            {
                if (this.selectedNumberOfTeams != value)
                {
                    this.selectedNumberOfTeams = value;
                    this.OnPropertyChanged();
                }
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

        public string SeriesName
        {
            get { return this.seriesName; }
            set
            {
                this.seriesName = value;
                this.OnPropertyChanged();
            }
        }

        public int MatchDuration
        {
            get { return this.matchDuration; }
            set
            {
                this.matchDuration = value;
                this.OnPropertyChanged();
            }
        }

        public List<int> NumberOfTeamsList
        {
            get { return this.numberOfTeamsList; }
            set
            {
                this.numberOfTeamsList = value;
                this.OnPropertyChanged();
            }
        }

        private void AddTeam(object obj)
        {
            if (this.selectedTeam != null && !(this.teamsToAddToSeries.Contains(this.selectedTeam)))
            {
                this.teamsToAddToSeries.Add(this.selectedTeam);
                this.availableTeams.Remove(this.selectedTeam);
            }
        }

        private void DeleteTeam(object obj)
        {
            if (this.selectedTeam != null && !(this.availableTeams.Contains(this.selectedTeam)))
            {
                this.availableTeams.Add(this.selectedTeam);
                this.teamsToAddToSeries.Remove(this.selectedTeam);
                
            }

        }

        private void AddSeriesTeam(object obj)
        {
            var seriesSeriesName = new SeriesName(this.seriesName);
            var seriesNumberOfTeams = new NumberOfTeams(this.SelectedNumberOfTeams);
            var seriesMatchDuration = new MatchDuration(new TimeSpan(0, this.matchDuration, 0));

            var seriesToAdd = new Series(seriesMatchDuration, seriesNumberOfTeams, seriesSeriesName);

            foreach (var team in this.teamsToAddToSeries)
            {
                seriesToAdd.TeamIds.Add(team.Id);
            }
            this.seriesService.Add(seriesToAdd);
            this.seriesService.ScheduleGenerator(seriesToAdd.Id);
            this.teamsToAddToSeries.Clear();
            this.SeriesName = "";
            this.MatchDuration = 0;
            this.AvailableTeams = this.teamService.GetAllTeams().ToObservableCollection();
        }

        public void LoadData()
        {
            this.AvailableTeams = this.teamService.GetAllTeams().ToObservableCollection();

            var teamLengths = this.teamService.GetAllTeams().Count();
            for (int i = 0; i <= teamLengths; i++)
            {
                if (IsEven(i) && i != 0 && i != 2)
                {
                    this.NumberOfTeamsList.Add(i);
                }
            }

        }

        public static bool IsEven(int value)
        {
            return value % 2 == 0;
        }

    }
}
