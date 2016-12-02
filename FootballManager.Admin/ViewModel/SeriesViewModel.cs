using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FootballManager.Admin.Utility;
using FootballManager.Admin.View;
using MaterialDesignThemes.Wpf;
using System.ComponentModel;
using System.Windows;
using Domain.Entities;
using Domain.Helper_Classes;
using Domain.Services;
using Domain.Value_Objects;
using FootballManager.Admin.Extensions;

namespace FootballManager.Admin.ViewModel
{
    public class SeriesViewModel : ViewModelBase, IDataErrorInfo
    {
        private ObservableCollection<Team> availableTeams;
        private ObservableCollection<Team> teamsToAddToSeries;
        private List<int> numberOfTeamsList;
        private TeamService teamService;
        private SeriesService seriesService;
        private Team selectedTeam;
        private string searchText;
        private int selectedNumberOfTeams;
        private string seriesName;
        private string matchDuration;

        public SeriesViewModel()
        {
            this.teamsToAddToSeries = new ObservableCollection<Team>();
            this.numberOfTeamsList = new List<int>();
            this.teamService = new TeamService();
            this.seriesService = new SeriesService();
            this.AddTeamCommand = new RelayCommand(AddTeam);
            this.DeleteTeamCommand = new RelayCommand(DeleteTeam);
            this.AddSeriesCommand = new RelayCommand(AddSeriesTeam);
            LoadData();
        }

        public ICommand DeleteTeamCommand { get; }

        public ICommand AddTeamCommand { get; }

        public ICommand AddSeriesCommand { get; }

        public ObservableCollection<Team> AvailableTeams
        {
            get { return availableTeams; }
            set
            {
                availableTeams = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Team> TeamsToAddToSeries
        {
            get { return teamsToAddToSeries; }
            set
            {
                teamsToAddToSeries = value;
                OnPropertyChanged();
            }
        }

        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged();
                AvailableTeams = teamService.GetAllTeams().
                    Where(x => x.Name.Value.ToLower().
                        Contains(searchText.ToLower())).ToObservableCollection();
            }
        }

        public int SelectedNumberOfTeams
        {
            get { return selectedNumberOfTeams; }
            set
            {
                if (selectedNumberOfTeams != value)
                {
                    selectedNumberOfTeams = value;
                    OnPropertyChanged();
                }
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

        public string SeriesName
        {
            get { return seriesName; }
            set
            {
                if (seriesName != value)
                {
                    seriesName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string MatchDuration
        {
            get { return matchDuration; }
            set
            {
                if (matchDuration != value)
                {
                    matchDuration = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<int> NumberOfTeamsList
        {
            get { return numberOfTeamsList; }
            set
            {
                numberOfTeamsList = value;
                OnPropertyChanged();
            }
        }

        private void AddTeam(object obj)
        {
            if (selectedTeam != null && !(teamsToAddToSeries.Contains(selectedTeam)))
            {
                teamsToAddToSeries.Add(selectedTeam);
                availableTeams.Remove(selectedTeam);
            }
        }

        private void DeleteTeam(object obj)
        {
            if (selectedTeam != null && !(availableTeams.Contains(selectedTeam)))
            {
                availableTeams.Add(selectedTeam);
                teamsToAddToSeries.Remove(selectedTeam);
            }

        }

        private void AddSeriesTeam(object obj)
        {
            var timeSpanMatchDuration = new TimeSpan(0, Convert.ToInt32(matchDuration), 0);

            try
            {
                var seriesSeriesName = new SeriesName(this.seriesName);
                var seriesNumberOfTeams = new NumberOfTeams(this.SelectedNumberOfTeams);
                var seriesMatchDuration = new MatchDuration(timeSpanMatchDuration);

                Series seriesToAdd = new Series(seriesMatchDuration, seriesNumberOfTeams, seriesSeriesName);
                foreach (var team in teamsToAddToSeries)
                {
                    seriesToAdd.TeamIds.Add(team.Id);
                }
                Messenger.Default.Send<Series>(seriesToAdd);
                ResetData();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


        }


        public void LoadData()
        {
            this.AvailableTeams = teamService.GetAllTeams().ToObservableCollection();

            var teamLengths = AvailableTeams.Count();
            for (int i = 4; i <= teamLengths; i++)
            {
                if (IsEven(i))
                {
                    this.NumberOfTeamsList.Add(i);
                }
            }

        }

        public void ResetData()
        {
            teamsToAddToSeries.Clear();
            this.SeriesName = "";
            this.MatchDuration = "";
            this.AvailableTeams = teamService.GetAllTeams().ToObservableCollection();
        }

        public static bool IsEven(int value)
        {
            return value % 2 == 0;
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
                    case "SeriesName":
                        if (string.IsNullOrEmpty(this.SeriesName))
                        {
                            return string.Empty;
                        }
                        if (!this.SeriesName.IsValidSeriesName(false)) // Parameter 'bool ignoreCase' set to false.
                        {
                            return "Must be 2-30 valid European characters long!";
                        }
                        break;
                    case "MatchDuration":
                        if (string.IsNullOrEmpty(this.MatchDuration))
                        {
                            return string.Empty;
                        }
                        int matchMinutes;
                        if (!int.TryParse(this.MatchDuration, out matchMinutes))
                        {
                            return "Must be an integer between 10 and 90!";
                        }
                        else
                        {
                            TimeSpan timeSpan = new TimeSpan(0, matchMinutes, 0);
                            if (!timeSpan.IsValidMatchDuration())
                            {
                                return "Must be an integer between 10 and 90!";
                            }
                        }
                        break;
                }
                return string.Empty;
            }
        }
    }
        #endregion
}
