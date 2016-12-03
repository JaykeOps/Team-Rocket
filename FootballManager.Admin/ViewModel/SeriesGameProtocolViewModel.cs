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

        private Game newGame;

        private string homeTeamName;
        private string awayTeamName;
        private string homeTeamResult;
        private string awayTeamResult;

        private Player selectedActivePlayer;

        private ICommand addPlayerToActivePlayersCommand;
        private ICommand removePlayerFromActivePlayersCommand;




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

        #region Goal
        private int? getGoalMatchMinute;
        private ICommand addGoalToGameCommand;
        public int? GetGoalMatchMinute
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
            if (SelectedActivePlayer != null)
            {
                if (SelectedActivePlayer.TeamId == newGame.HomeTeamId)
                {
                    this.gameService.AddGoalToGame(newGame.Id, SelectedActivePlayer.Id, GetGoalMatchMinute.GetValueOrDefault());
                    HomeTeamResult = this.GetNewGameData(newGame.Id).Protocol.GameResult.HomeTeamScore.ToString();
                }
                else if (SelectedActivePlayer.TeamId == newGame.AwayTeamId)
                {
                    this.gameService.AddGoalToGame(newGame.Id, SelectedActivePlayer.Id, GetGoalMatchMinute.GetValueOrDefault());
                    AwayTeamResult = this.GetNewGameData(newGame.Id).Protocol.GameResult.AwayTeamScore.ToString();
                }
                GetGoalMatchMinute = DateTime.Now.Minute;
            }
        }
        #endregion

        #region Penalty
        private int getPenaltyMatchMinute;
        private ICommand addPentalyToGameCommand;
        private bool getIsGoal;
        public int GetPenaltyMatchMinute
        {
            get { return getPenaltyMatchMinute; }
            set
            {
                getPenaltyMatchMinute = value;
                OnPropertyChanged();
            }
        }

        public bool GetIsGoal
        {
            get { return getIsGoal; }
            set
            {
                getIsGoal = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddPenaltyToGameCommand
        {
            get
            {
                if (this.addPentalyToGameCommand == null)
                {
                    this.addPentalyToGameCommand = new RelayCommand(this.AddPentalyToGame);
                }
                return this.addPentalyToGameCommand;
            }
        }

        private void AddPentalyToGame(object obj)
        {
            if (SelectedActivePlayer != null)
            {
                if (SelectedActivePlayer.TeamId == newGame.HomeTeamId)
                {
                    this.gameService.AddPenaltyToGame(newGame.Id, SelectedActivePlayer.Id, GetPenaltyMatchMinute, GetIsGoal);
                    HomeTeamResult = this.GetNewGameData(newGame.Id).Protocol.GameResult.HomeTeamScore.ToString();
                }
                else if (SelectedActivePlayer.TeamId == newGame.AwayTeamId)
                {
                    this.gameService.AddPenaltyToGame(newGame.Id, SelectedActivePlayer.Id, GetPenaltyMatchMinute, GetIsGoal);
                    AwayTeamResult = this.GetNewGameData(newGame.Id).Protocol.GameResult.AwayTeamScore.ToString();
                }
            }
        }
        #endregion

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

        public Player SelectedActivePlayer
        {
            get { return this.selectedActivePlayer; }
            set
            {
                this.selectedActivePlayer = value;                
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
            gameService.Add(newGame);
            HomeTeamName = this.teamService.FindTeamById(this.newGame.HomeTeamId).Name.Value;
            AwayTeamName = this.teamService.FindTeamById(this.newGame.AwayTeamId).Name.Value;
            
            HomeTeamPlayerCollection = this.playerService.GetAllPlayersInTeam(this.newGame.HomeTeamId).ToObservableCollection();
            AwayTeamPlayerCollection = this.playerService.GetAllPlayersInTeam(this.newGame.AwayTeamId).ToObservableCollection();
        }

        private Game GetNewGameData(Guid gameId)
        {
            return gameService.FindById(gameId);
        }
        #endregion
    }
}
