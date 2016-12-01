using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using FootballManager.Admin.Utility;
using Domain.Entities;
using System.Collections.ObjectModel;
using Domain.Services;
using FootballManager.Admin.Extensions;
using System.Windows;
using Domain.Value_Objects;
using System.ComponentModel;
using Domain.Helper_Classes;

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
                seriesName = value;
                OnPropertyChanged();
            }
        }

        public int MatchDuration
        {
            get { return matchDuration; }
            set
            {
                matchDuration = value;
                OnPropertyChanged();
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
            var timeSpanMatchDuration = new TimeSpan(0, this.matchDuration, 0);

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

            var teamLengths = teamService.GetAllTeams().Count();
            for (int i = 0; i <= teamLengths; i++)
            {
                if (IsEven(i) && i != 0 && i != 2)
                {
                    NumberOfTeamsList.Add(i);
                }
            }

        }

        public void ResetData()
        {
            teamsToAddToSeries.Clear();
            this.SeriesName = "";
            this.MatchDuration = 0;
            this.AvailableTeams = teamService.GetAllTeams().ToObservableCollection();
        }

        public static bool IsEven(int value)
        {
            return value % 2 == 0;
        }

        #region IDataErrorInfo Implementation
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
                        if (!this.SeriesName.IsValidSeriesName(false)) // Parameter is 'bool ignoreCase'.
                        {
                            return "Must be 2-30 valid European characters long!";
                        }
                        break;
                }
                return string.Empty;
            }
        }
        #endregion
    }
}
