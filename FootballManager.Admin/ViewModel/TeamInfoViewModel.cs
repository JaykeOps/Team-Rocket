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
        private ObservableCollection<Series> seriesCollection;
        private ObservableCollection<IExposableTeam> teamsBySeriesCollection;
        private ObservableCollection<IExposablePlayer> playersByTeamCollection;
        private ObservableCollection<IExposablePlayer> teamlessPlayers;
        private ICommand openTeamInfoEditPlayerViewCommand;
        private ICommand dismissPlayerFromTeamCommand;
        private ICommand assignPlayerToTeamCommand;
        private string teamlessPlayersSearchText;

        private SeriesService seriesService;
        private TeamService teamService;
        private PlayerService playerService;

        private Series selectedSeries;
        private Team selectedTeam;
        private IExposablePlayer selectedPlayer;
        private IExposablePlayer selectedTeamlessPlayer;

        private string arenaName;
        private string email;

        public TeamInfoViewModel()
        {
            this.seriesCollection = new ObservableCollection<Series>();
            this.teamsBySeriesCollection = new ObservableCollection<IExposableTeam>();
            this.playersByTeamCollection = new ObservableCollection<IExposablePlayer>();
            this.teamlessPlayers = new ObservableCollection<IExposablePlayer>();

            this.seriesService = new SeriesService();
            this.teamService = new TeamService();
            this.playerService = new PlayerService();
            Messenger.Default.Register<IExposablePlayer>(this, this.OnPlayerObjectRecieved);

            this.LoadData();
        }

        #region Properties

        public ICommand OpenTeamInfoEditPlayerViewCommand
        {
            get
            {
                return this.openTeamInfoEditPlayerViewCommand ??
                  (this.openTeamInfoEditPlayerViewCommand = new RelayCommand(this.OpenTeamInfoEditPlayerView));
            }
        }

        public ICommand DismissPlayerFromTeamCommand
        {
            get
            {
                return this.dismissPlayerFromTeamCommand ??
                       (this.dismissPlayerFromTeamCommand = new RelayCommand(this.DismissPlayerFromTeam));
            }
        }

        public ICommand AssignPlayerToTeamCommand
        {
            get
            {
                return this.assignPlayerToTeamCommand ??
                       (this.assignPlayerToTeamCommand = new RelayCommand(this.AssignPlayerToTeam));
            }
        }

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

        #endregion Properties

        #region Collections

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
                this.teamsBySeriesCollection = value;
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

        #endregion Collections

        #region Methods

        public void LoadData()
        {
            this.SeriesCollection = this.seriesService.GetAll().ToObservableCollection();
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
            var players = new List<IExposablePlayer>();

            if (this.SelectedTeam != null)
            {
                var playerIds = this.SelectedTeam.PlayerIds.ToList();
                foreach (var id in playerIds)
                {
                    var player = (IExposablePlayer)this.playerService.FindById(id);
                    players.Add(player);
                }

                this.PlayersByTeamCollection = players.ToObservableCollection();
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
            var player = (IExposablePlayer) obj;
            var teamInfoPlayerEditView = new TeamInfoEditPlayerView();
            Messenger.Default.Send(player);
            teamInfoPlayerEditView.ShowDialog();
            this.FilterPlayersByTeam();
        }

        private void OnPlayerObjectRecieved(IExposablePlayer obj)
        {
            this.SelectedPlayer = obj;
        }

        #endregion Methods
    }
}