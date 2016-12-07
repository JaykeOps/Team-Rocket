using Domain.Entities;
using Domain.Helper_Classes;
using Domain.Services;
using Domain.Value_Objects;
using FootballManager.Admin.Extensions;
using FootballManager.Admin.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

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
        private string seriesAddedConfirmText;
        private object selectedItem;
        private bool allPropertiesValid;
        private Dictionary<string, bool> validProperties;

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
            validProperties = new Dictionary<string, bool>();
            validProperties.Add("SeriesName", false);
            validProperties.Add("MatchDuration", false);
            validProperties.Add("SelectedItem", false);
            validProperties.Add("TeamsToAddToSeries", false);
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
            get { return this.teamsToAddToSeries; }
            set
            {
                if (this.teamsToAddToSeries != value)
                {
                    this.teamsToAddToSeries = value;
                    OnPropertyChanged();
                }
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
                    OnPropertyChanged("TeamsToAddToSeries");
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

        public string SeriesAddedConfirmText
        {
            get { return seriesAddedConfirmText; }
            set
            {
                seriesAddedConfirmText = value;
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
                OnPropertyChanged("TeamsToAddToSeries");
                this.SeriesAddedConfirmText = "";
            }
        }

        private void DeleteTeam(object obj)
        {
            if (selectedTeam != null && !(availableTeams.Contains(selectedTeam)))
            {
                availableTeams.Add(selectedTeam);
                teamsToAddToSeries.Remove(selectedTeam);
                OnPropertyChanged("TeamsToAddToSeries");
            }
        }

        private void AddSeriesTeam(object obj)
        {
            var timeSpanMatchDuration = new TimeSpan(0, Convert.ToInt32(matchDuration), 0);

            var seriesSeriesName = new SeriesName(this.seriesName);
            var seriesNumberOfTeams = new NumberOfTeams(this.selectedNumberOfTeams);
            var seriesMatchDuration = new MatchDuration(timeSpanMatchDuration);

            Series seriesToAdd = new Series(seriesMatchDuration, seriesNumberOfTeams, seriesSeriesName);

            foreach (var team in teamsToAddToSeries)
            {
                
                this.seriesService.AddTeamToSeries(seriesToAdd, team.Id);
                team.UpdatePlayerIds();
            }

            this.seriesService.Add(seriesToAdd);

            Messenger.Default.Send<Series>(seriesToAdd);
            ResetData();
            this.SeriesAddedConfirmText = "Series Added!";
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
            OnPropertyChanged("TeamsToAddToSeries");
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
                            validProperties[columnName] = false;
                            ValidateProperties();
                            return string.Empty;
                        }
                        if (!this.SeriesName.IsValidSeriesName(false)) // Parameter 'bool ignoreCase' set to false.
                        {
                            validProperties[columnName] = false;
                            ValidateProperties();
                            return "Must be 2-30 valid European characters long!";
                        }
                        break;

                    case "MatchDuration":
                        if (string.IsNullOrEmpty(this.MatchDuration))
                        {
                            validProperties[columnName] = false;
                            ValidateProperties();
                            return string.Empty;
                        }
                        int matchMinutes;
                        if (!int.TryParse(this.MatchDuration, out matchMinutes))
                        {
                            validProperties[columnName] = false;
                            ValidateProperties();
                            return "Must be an integer between 10 and 90!";
                        }
                        else
                        {
                            TimeSpan timeSpan = new TimeSpan(0, matchMinutes, 0);
                            if (!timeSpan.IsValidMatchDuration())
                            {
                                validProperties[columnName] = false;
                                ValidateProperties();
                                return "Must be an integer between 10 and 90!";
                            }
                        }
                        break;

                    case "SelectedItem":
                        if (this.SelectedItem == null)
                        {
                            validProperties[columnName] = false;
                            ValidateProperties();
                            return string.Empty;
                        }
                        break;

                    case "TeamsToAddToSeries":
                        if (this.SelectedItem == null)
                        {
                            return string.Empty;
                        }
                        if (this.TeamsToAddToSeries.Count() == 0)
                        {
                            validProperties[columnName] = false;
                            ValidateProperties();
                            return string.Empty;
                        }
                        if ((this.TeamsToAddToSeries.Count() < this.SelectedNumberOfTeams))
                        {
                            validProperties[columnName] = false;
                            ValidateProperties();
                            return "You have added too few teams to the list!";
                        }
                        if ((this.TeamsToAddToSeries.Count() > this.SelectedNumberOfTeams))
                        {
                            validProperties[columnName] = false;
                            ValidateProperties();
                            return "You have added too many teams to the list!";
                        }
                        break;
                }
                validProperties[columnName] = true;
                ValidateProperties();
                return string.Empty;
            }
        }

        #endregion IDataErrorInfo implementation

        public bool AllPropertiesValid
        {
            get { return this.allPropertiesValid; }
            set
            {
                if (this.allPropertiesValid != value)
                {
                    this.allPropertiesValid = value;
                    OnPropertyChanged();
                }
            }
        }

        public object SelectedItem
        {
            get { return this.selectedItem; }
            set
            {
                if (this.selectedItem != value)
                {
                    this.selectedItem = value;
                    OnPropertyChanged();
                }
            }
        }

        private void ValidateProperties()
        {
            foreach (var isValid in validProperties.Values)
            {
                if (isValid == false)
                {
                    this.AllPropertiesValid = false;
                    return;
                }
            }
            this.AllPropertiesValid = true;
        }
    }
}