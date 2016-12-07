using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using FootballManager.Admin.Extensions;
using FootballManager.Admin.Utility;
using FootballManager.Admin.View;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace FootballManager.Admin.ViewModel
{
    public class TeamInfoViewModel : ViewModelBase
    {
        private readonly SeriesService seriesService;
        private readonly TeamService teamService;
        private readonly PlayerService playerService;
        private ObservableCollection<Series> seriesCollection;
        private ObservableCollection<IExposableTeam> teamsBySeriesCollection;
        private ObservableCollection<IExposablePlayer> playersByTeamCollection;
        private ObservableCollection<IExposablePlayer> teamlessPlayers;
        private ObservableCollection<IExposableTeam> allTeamsCollection;
        private ObservableCollection<IExposableTeam> teamsCollection;
        private ICommand openTeamInfoEditPlayerViewCommand;
        private ICommand dismissPlayerFromTeamCommand;
        private ICommand assignPlayerToTeamCommand;
        private ICommand shallFilterBySeriesCommand;
        private string teamlessPlayersSearchText;
        private Series selectedSeries;
        private Team selectedTeam;
        private IExposablePlayer selectedPlayer;
        private IExposablePlayer selectedTeamlessPlayer;
        private bool shallFilterBySeriesCheckBox;
        private string arenaName;
        private string email;

        public TeamInfoViewModel()
        {
            this.allTeamsCollection = new ObservableCollection<IExposableTeam>();
            this.seriesCollection = new ObservableCollection<Series>();
            this.teamsBySeriesCollection = new ObservableCollection<IExposableTeam>();
            this.playersByTeamCollection = new ObservableCollection<IExposablePlayer>();
            this.teamlessPlayers = new ObservableCollection<IExposablePlayer>();

            this.seriesService = new SeriesService();
            this.teamService = new TeamService();
            this.playerService = new PlayerService();
            Messenger.Default.Register<IExposablePlayer>(this, this.OnPlayerObjectRecieved);

            this.LoadData();
            this.TeamsCollection = this.allTeamsCollection;
        }

        public ICommand OpenTeamInfoEditPlayerViewCommand =>
            this.openTeamInfoEditPlayerViewCommand ??
            (this.openTeamInfoEditPlayerViewCommand = new RelayCommand(this.OpenTeamInfoEditPlayerView));

        public ICommand DismissPlayerFromTeamCommand =>
            this.dismissPlayerFromTeamCommand ??
           (this.dismissPlayerFromTeamCommand = new RelayCommand(this.DismissPlayerFromTeam));

        public ICommand AssignPlayerToTeamCommand =>
            this.assignPlayerToTeamCommand ??
            (this.assignPlayerToTeamCommand = new RelayCommand(this.AssignPlayerToTeam));

        public ICommand FilterBySeriesCommand =>
            this.shallFilterBySeriesCommand ??
            (this.shallFilterBySeriesCommand = new RelayCommand(this.ShallFilterBySeries));

        public string TeamlessPlayersSearchText
        {
            get { return this.teamlessPlayersSearchText; }
            set
            {
                this.teamlessPlayersSearchText = value;
                this.OnPropertyChanged();
                this.FilterTeamlessPlayersSearchData();
            }
        }

        public IExposablePlayer SelectedPlayer
        {
            get { return this.selectedPlayer; }
            set
            {
                if (this.selectedPlayer != value)
                {
                    this.selectedPlayer = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public IExposablePlayer SelectedTeamlessPlayer
        {
            get { return this.selectedTeamlessPlayer; }
            set
            {
                if (this.selectedTeamlessPlayer != value)
                {
                    this.selectedTeamlessPlayer = value;
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
                if (this.arenaName != value)
                {
                    this.email = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Series SelectedSeries
        {
            get { return this.selectedSeries; }
            set
            {
                this.selectedSeries = value;
                this.OnPropertyChanged();
                this.FilterTeamsBySeries();
            }
        }

        public Team SelectedTeam
        {
            get { return this.selectedTeam; }
            set
            {
                this.selectedTeam = value;
                this.OnPropertyChanged();
                this.FilterPlayersByTeam();
            }
        }

        public bool ShallFilterBySeriesCheckBox
        {
            get { return this.shallFilterBySeriesCheckBox; }
            set
            {
                this.shallFilterBySeriesCheckBox = value;
                this.OnPropertyChanged();
            }
        }

        public ObservableCollection<IExposablePlayer> TeamlessPlayers
        {
            get { return this.teamlessPlayers; }
            set
            {
                this.teamlessPlayers = value;
                this.OnPropertyChanged();
            }
        }

        public ObservableCollection<Series> SeriesCollection
        {
            get { return this.seriesCollection; }
            set
            {
                this.seriesCollection = value;
                this.OnPropertyChanged();
            }
        }

        public ObservableCollection<IExposableTeam> TeamsBySeriesCollection
        {
            get { return this.teamsBySeriesCollection; }
            set
            {
                this.TeamsCollection = value;
                this.OnPropertyChanged();
                
            }
        }

        public ObservableCollection<IExposableTeam> TeamsCollection
        {
            get { return this.teamsCollection; }
            set
            {
                this.teamsCollection = value;
                this.OnPropertyChanged();
            }
        }

        public ObservableCollection<IExposablePlayer> PlayersByTeamCollection
        {
            get { return this.playersByTeamCollection; }
            set
            {
                this.playersByTeamCollection = value;
                this.OnPropertyChanged();
            }
        }

        public ObservableCollection<IExposableTeam> AllTeamsCollection
        {
            get
            {
                return this.allTeamsCollection;
            }

            set
            {
                this.allTeamsCollection = value;
                this.OnPropertyChanged();
            }
        }

        public void LoadData()
        {
            this.SeriesCollection =
                this.seriesService.GetAll().ToObservableCollection();
            this.AllTeamsCollection =
                this.teamService.GetAllIExposableTeams().ToObservableCollection();
        }

        private void FilterTeamsBySeries()
        {
            var teams = new List<IExposableTeam>();

            if (this.SelectedSeries != null)
            {
                var teamsIds = this.SelectedSeries.TeamIds.ToList();
                foreach (var id in teamsIds)
                {
                    var team = this.teamService.FindIExposableTeamById(id);
                    teams.Add(team);
                }
            }
            this.TeamsBySeriesCollection = teams.ToObservableCollection();
        }

        private void FilterPlayersByTeam()
        {
            if (this.SelectedTeam != null)
            {
                this.PlayersByTeamCollection = this.playerService.GetAllExposablePlayersInTeam(
                    this.SelectedTeam.Id).ToObservableCollection();
                this.ArenaName = this.SelectedTeam.ArenaName.Value;
                this.Email = this.SelectedTeam.Email.Value;
            }
        }

        private void DismissPlayerFromTeam(object obj)
        {
            var player = (IExposablePlayer)obj;
            this.playerService.DismissPlayerFromTeam(player);
            this.FilterPlayersByTeam();
        }

        private void AssignPlayerToTeam(object obj)
        {
            var player = (IExposablePlayer)obj;
            this.playerService.AssignPlayerToTeam(player,
                this.SelectedTeam.Id);
            this.TeamlessPlayersSearchText = this.teamlessPlayersSearchText;
            this.FilterPlayersByTeam();
        }

        private void FilterTeamlessPlayersSearchData()
        {
            this.TeamlessPlayers = this.playerService.SearchForTeamlessPlayers(
                this.TeamlessPlayersSearchText).ToObservableCollection();
        }

        private void OpenTeamInfoEditPlayerView(object obj)
        {
            var teamInfoPlayerEditView = new TeamInfoEditPlayerView();
            if (obj != null)
            {
                Messenger.Default.Send<IExposablePlayer>((IExposablePlayer)obj);
            }

            teamInfoPlayerEditView.ShowDialog();
            this.FilterPlayersByTeam();
        }

        private void OnPlayerObjectRecieved(IExposablePlayer obj)
        {
            this.SelectedPlayer = obj;
        }

        private void ShallFilterBySeries(object obj)
        {
            this.shallFilterBySeriesCheckBox = !this.shallFilterBySeriesCheckBox;
            this.TeamsCollection = this.ShallFilterBySeriesCheckBox ?
                this.TeamsBySeriesCollection : this.AllTeamsCollection;

            if (this.ShallFilterBySeriesCheckBox)
            {
                this.FilterTeamsBySeries();
            }
            
        }
    }
}