using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using Domain.Value_Objects;
using FootballManager.Admin.Extensions;
using FootballManager.Admin.Utility;

namespace FootballManager.Admin.ViewModel
{
    public class SeriesGameProtocolViewModel : ViewModelBase
    {
        private TeamService teamService;
        private GameService gameService;
        private PlayerService playerService;

        private ObservableCollection<Player> homeTeamPlayerCollection;
        private ObservableCollection<Player> awayTeamPlayerCollection;
        private ObservableCollection<Player> homeTeamActivePlayerCollection;
        private ObservableCollection<Player> awayTeamActivePlayerCollection;

        private ObservableCollection<string> eventsCollection;

        private Game newGame;

        private string homeTeamName;
        private string awayTeamName;
        private string homeTeamResult;
        private string awayTeamResult;

        private Player selectedHomeTeamActivePlayer;
        private Player selectedAwayTeamActivePlayer;

        private ICommand addPlayerToActivePlayersCommand;
        private ICommand removePlayerFromActivePlayersCommand;

        private string getGoalMatchMinute;
        private ICommand addGoalToGameCommand;


        public SeriesGameProtocolViewModel()
        {
            this.teamService = new TeamService();
            this.gameService = new GameService();
            this.playerService = new PlayerService();

            homeTeamPlayerCollection = new ObservableCollection<Player>();
            awayTeamPlayerCollection = new ObservableCollection<Player>();
            homeTeamActivePlayerCollection = new ObservableCollection<Player>();
            awayTeamActivePlayerCollection = new ObservableCollection<Player>();

            Messenger.Default.Register<Match>(this, this.OnMatchObjReceived);
        }

        public string GetGoalMatchMinute
        {
            get { return getGoalMatchMinute; }
            set
            {
                getGoalMatchMinute = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddGoalToGameCommand
        {
            get
            {
                if (this.addGoalToGameCommand == null)
                {
                    this.addGoalToGameCommand = new RelayCommand(this.AddGoalToGame);
                }
                return this.addGoalToGameCommand;
            }
        }

        private void AddGoalToGame(object obj)
        {
            throw new NotImplementedException();
        }


        #region Properties
        public string HomeTeamName
        {
            get { return this.homeTeamName; }
            set
            {
                this.homeTeamName = value;
                OnPropertyChanged();
            }
        }

        public string AwayTeamName
        {
            get { return this.awayTeamName; }
            set
            {
                this.awayTeamName = value;
                OnPropertyChanged();
            }
        }

        public string HomeTeamResult
        {
            get { return this.homeTeamResult; }
            set
            {
                this.homeTeamResult = value;
                OnPropertyChanged();
            }
        }

        public string AwayTeamResult
        {
            get { return this.awayTeamResult; }
            set
            {
                this.awayTeamResult = value;
                OnPropertyChanged();
            }
        }

        public Player SelectedHomeTeamActivePlayer
        {
            get { return this.selectedHomeTeamActivePlayer; }
            set
            {
                this.selectedHomeTeamActivePlayer = value;
                OnPropertyChanged();
            }
        }

        public Player SelectedAwayTeamActivePlayer
        {
            get { return this.selectedAwayTeamActivePlayer; }
            set
            {
                this.selectedAwayTeamActivePlayer = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddPlayerToActivePlayersCommand
        {
            get
            {
                if (this.addPlayerToActivePlayersCommand == null)
                {
                    this.addPlayerToActivePlayersCommand = new RelayCommand(this.AddPlayerToActivePlayers);
                }
                return this.addPlayerToActivePlayersCommand;
            }
        }

        public ICommand RemovePlayerFromActivePlayersCommand
        {
            get
            {
                if (this.removePlayerFromActivePlayersCommand == null)
                {
                    this.removePlayerFromActivePlayersCommand = new RelayCommand(this.RemovePlayerFromActivePlayers);
                }
                return this.removePlayerFromActivePlayersCommand;

            }
        }
        #endregion

        #region Collections        
        public ObservableCollection<Player> HomeTeamPlayerCollection
        {
            get { return this.homeTeamPlayerCollection; }
            set
            {
                this.homeTeamPlayerCollection = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Player> AwayTeamPlayerCollection
        {
            get { return this.awayTeamPlayerCollection; }
            set
            {
                this.awayTeamPlayerCollection = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Player> HomeTeamActivePlayerCollection
        {
            get { return this.homeTeamActivePlayerCollection; }
            set
            {
                this.homeTeamActivePlayerCollection = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Player> AwayTeamActivePlayerCollection
        {
            get { return this.awayTeamActivePlayerCollection; }
            set
            {
                this.awayTeamActivePlayerCollection = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Methods 
        private void AddPlayerToActivePlayers(object playerObj)
        {
            if (playerObj == null) return;
            Player player = (Player)playerObj;
            if (player.TeamId == this.newGame.HomeTeamId)
            {
                HomeTeamPlayerCollection.Remove(player);
                HomeTeamActivePlayerCollection.Add(player);                                              
            }
            if (player.TeamId == this.newGame.AwayTeamId)
            {
                AwayTeamPlayerCollection.Remove(player);
                AwayTeamActivePlayerCollection.Add(player);
            }           
        }

        private void RemovePlayerFromActivePlayers(object playerObj)
        {
            if (playerObj == null) return;
            Player player = (Player)playerObj;
            if (player.TeamId == this.newGame.HomeTeamId)
            {                
                HomeTeamActivePlayerCollection.Remove(player);
                HomeTeamPlayerCollection.Add(player);
            }
            if (player.TeamId == this.newGame.AwayTeamId)
            {
                AwayTeamActivePlayerCollection.Remove(player);
                AwayTeamPlayerCollection.Add(player);                
            }
        }
                     
        private void OnMatchObjReceived(Match match)
        {            
            this.newGame = new Game(match);
            HomeTeamName = this.teamService.FindTeamById(this.newGame.HomeTeamId).Name.Value;
            AwayTeamName = this.teamService.FindTeamById(this.newGame.AwayTeamId).Name.Value;
            
            HomeTeamPlayerCollection = this.playerService.GetAllPlayersInTeam(this.newGame.HomeTeamId).ToObservableCollection();
            AwayTeamPlayerCollection = this.playerService.GetAllPlayersInTeam(this.newGame.AwayTeamId).ToObservableCollection();

            // Får flyttas till add goal kommandot för annars hämtar man null
            //HomeTeamResult = newGame.Protocol.GameResult.HomeTeamScore.ToString();
            //AwayTeamResult = newGame.Protocol.GameResult.AwayTeamScore.ToString();
        }
        #endregion
    }
}
