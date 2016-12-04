using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using FootballManager.Admin.Extensions;
using FootballManager.Admin.Utility;
using FootballManager.Admin.View;
using System;
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
        private ICommand removePlayerFromTeamCommand;
        private ICommand addPlayerToTeamCommand;
        private string playerSearchText;

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

        public ICommand RemovePlayerFromTeamCommand
        {
            get
            {
                return this.removePlayerFromTeamCommand ??
                       (this.removePlayerFromTeamCommand = new RelayCommand(this.RemovePlayerFromTeam));
            }
        }

        public ICommand AddPlayerToTeamCommand
        {
            get
            {
                return this.addPlayerToTeamCommand ??
                       (this.addPlayerToTeamCommand = new RelayCommand(this.AddPlayerToTeam));
            }
        }

        private void AddPlayerToTeam(object obj)
        {
            var player = (IExposablePlayer) obj;
            this.playerService.AssignPlayerToTeam(player,
                this.SelectedTeam.Id);
            this.PlayerSearchText = this.playerSearchText;
            this.FilterPlayersByTeam();

        }

        public string PlayerSearchText
        {
            get { return this.playerSearchText; }
            set
            {
                this.playerSearchText = value;
                this.OnPropertyChanged();
                this.FilterData();
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

        public void LoadData()
        {
            this.SeriesCollection = this.seriesService.GetAll().ToObservableCollection();
        }

        public void RemovePlayerFromTeam(object obj)
        {
            var player = (IExposablePlayer)obj;
            this.playerService.DismissPlayerFromTeam(player);
            this.FilterPlayersByTeam();
        }

        private void FilterData()
        {
            this.TeamlessPlayers = this.playerService.SearchForTeamlessPlayers(
                this.PlayerSearchText).ToObservableCollection();
        }

        private void OpenTeamInfoEditPlayerView(object obj)
        {
            var player = (IExposablePlayer)obj;
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