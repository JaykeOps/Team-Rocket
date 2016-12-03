using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using FootballManager.Admin.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using FootballManager.Admin.Utility;
using FootballManager.Admin.View;

namespace FootballManager.Admin.ViewModel
{
    public class TeamInfoViewModel : ViewModelBase
    {
        private ObservableCollection<Series> seriesCollection;
        private ObservableCollection<IExposableTeam> teamsBySeriesCollection;
        private ObservableCollection<IExposablePlayer> playersByTeamCollection;
        private ICommand openTeamInfoEditPlayerViewCommand;

        private SeriesService seriesService;
        private TeamService teamService;
        private PlayerService playerService;

        private Series selectedSeries;
        private Team selectedTeam;

        private string arenaName;
        private string email;

        public TeamInfoViewModel()
        {
            this.seriesCollection = new ObservableCollection<Series>();
            this.teamsBySeriesCollection = new ObservableCollection<IExposableTeam>();
            this.playersByTeamCollection = new ObservableCollection<IExposablePlayer>();

            this.seriesService = new SeriesService();
            this.teamService = new TeamService();
            this.playerService = new PlayerService();

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

        private void OpenTeamInfoEditPlayerView(object obj)
        {
            var player = (IExposablePlayer) obj;
            var teamInfoPlayerEditView = new TeamInfoEditPlayerView();
            Messenger.Default.Send(player);
            teamInfoPlayerEditView.ShowDialog();
            this.FilterPlayersByTeam();
        }

        public string ArenaName
        {
            get { return this.arenaName; }
            set
            {
                if (arenaName != value)
                {
                    this.arenaName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Email
        {
            get { return this.email; }
            set
            {
                if (arenaName != value)
                {
                    this.email = value;
                    OnPropertyChanged();
                }
            }
        }

        public Series SelectedSeries
        {
            get { return selectedSeries; }
            set
            {
                selectedSeries = value;
                OnPropertyChanged();
                FilterTeamsBySeries();
            }
        }

        public Team SelectedTeam
        {
            get { return selectedTeam; }
            set
            {
                selectedTeam = value;
                OnPropertyChanged();
                FilterPlayersByTeam();
            }
        }

        #endregion Properties

        #region Collections

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

            if (SelectedSeries != null)
            {
                var teamsIds = SelectedSeries.TeamIds.ToList();
                foreach (var id in teamsIds)
                {
                    var team = teamService.FindIExposableTeamById(id);
                    teams.Add(team);
                }
            }
            TeamsBySeriesCollection = teams.ToObservableCollection();
        }

        private void FilterPlayersByTeam()
        {
            var players = new List<IExposablePlayer>();

            if (SelectedTeam != null)
            {
                var playerIds = SelectedTeam.PlayerIds.ToList();
                foreach (var id in playerIds)
                {
                    //TODO Behöver ha IExposablePlayerById
                    var player = (IExposablePlayer)playerService.FindById(id);
                    players.Add(player);
                }

                PlayersByTeamCollection = players.ToObservableCollection();
                ArenaName = SelectedTeam.ArenaName.Value;
                Email = SelectedTeam.Email.Value;
            }
        }

        public void LoadData()
        {
            this.SeriesCollection = this.seriesService.GetAll().ToObservableCollection();
        }

        #endregion Methods
    }
}