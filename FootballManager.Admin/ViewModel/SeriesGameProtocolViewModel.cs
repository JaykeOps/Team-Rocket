using System;
using System.ComponentModel;
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
using Domain.Helper_Classes;


namespace FootballManager.Admin.ViewModel
{
    public class SeriesGameProtocolViewModel : ViewModelBase, IDataErrorInfo
    {
        private TeamService teamService;
        private GameService gameService;
        private PlayerService playerService;

        private Game newGame;

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
        private string goalMatchMinute;
        private ICommand addGoalToGameCommand;
        public string GoalMatchMinute
        {
            get { return goalMatchMinute; }
            set
            {
                goalMatchMinute = value;
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
                    this.gameService.AddGoalToGame(newGame.Id, SelectedActivePlayer.Id, int.Parse(GoalMatchMinute));
                    HomeTeamResult = this.GetNewGameData(newGame.Id).Protocol.GameResult.HomeTeamScore.ToString();
                }
                else if (SelectedActivePlayer.TeamId == newGame.AwayTeamId)
                {
                    this.gameService.AddGoalToGame(newGame.Id, SelectedActivePlayer.Id, int.Parse(GoalMatchMinute));
                    AwayTeamResult = this.GetNewGameData(newGame.Id).Protocol.GameResult.AwayTeamScore.ToString();
                }
                GoalMatchMinute = DateTime.Now.Minute.ToString();
            }
        }
        #endregion

        #region Penalty
        private string penaltyMatchMinute;
        private ICommand addPentalyToGameCommand;
        private bool getIsGoal;
        public string PenaltyMatchMinute
        {
            get { return penaltyMatchMinute; }
            set
            {
                penaltyMatchMinute = value;
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
                    this.gameService.AddPenaltyToGame(newGame.Id, SelectedActivePlayer.Id, int.Parse(PenaltyMatchMinute), GetIsGoal);
                    HomeTeamResult = this.GetNewGameData(newGame.Id).Protocol.GameResult.HomeTeamScore.ToString();
                }
                else if (SelectedActivePlayer.TeamId == newGame.AwayTeamId)
                {
                    this.gameService.AddPenaltyToGame(newGame.Id, SelectedActivePlayer.Id, int.Parse(PenaltyMatchMinute), GetIsGoal);
                    AwayTeamResult = this.GetNewGameData(newGame.Id).Protocol.GameResult.AwayTeamScore.ToString();
                }
            }
        }
        #endregion

        #region Teams and Results
        private string homeTeamName;
        private string awayTeamName;
        private string homeTeamResult;
        private string awayTeamResult;
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
        #endregion

        #region Collections
        private ObservableCollection<Player> homeTeamPlayerCollection;
        private ObservableCollection<Player> awayTeamPlayerCollection;
        private ObservableCollection<Player> homeTeamActivePlayerCollection;
        private ObservableCollection<Player> awayTeamActivePlayerCollection;
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

        #region Lists and dependencies
        private Player selectedActivePlayer;
        private ICommand addPlayerToActivePlayersCommand;
        private ICommand removePlayerFromActivePlayersCommand;
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

        #region Button Properties
        private string assistMatchMinute;
        private string yellowCardMatchMinute;
        private string redCardMatchMinute;

        public string AssistMatchMinute
        {
            get { return assistMatchMinute; }
            set
            {
                if (assistMatchMinute != value)
                {
                    assistMatchMinute = value;
                    OnPropertyChanged();
                }
            }
        }

        public string YellowCardMatchMinute
        {
            get { return yellowCardMatchMinute; }
            set
            {
                if (yellowCardMatchMinute != value)
                {
                    yellowCardMatchMinute = value;
                    OnPropertyChanged();
                }
            }
        }

        public string RedCardMatchMinute
        {
            get { return redCardMatchMinute; }
            set
            {
                if (redCardMatchMinute != value)
                {
                    redCardMatchMinute = value;
                    OnPropertyChanged();
                }
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

        #region Validaiton Properties
        private bool goalMatchMinuteValid;
        private bool assistMatchMinuteValid;
        private bool penaltyMatchMinuteValid;
        private bool yellowCardMatchMinuteValid;
        private bool redCardMatchMinuteValid;
        public bool GoalMatchMinuteValid
        {
            get { return goalMatchMinuteValid; }
            set
            {
                if (goalMatchMinuteValid != value)
                {
                    goalMatchMinuteValid = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool AssistMatchMinuteValid
        {
            get { return assistMatchMinuteValid; }
            set
            {
                if (assistMatchMinuteValid != value)
                {
                    assistMatchMinuteValid = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool PenaltyMatchMinuteValid
        {
            get { return penaltyMatchMinuteValid; }
            set
            {
                if (penaltyMatchMinuteValid != value)
                {
                    penaltyMatchMinuteValid = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool YellowCardMatchMinuteValid
        {
            get { return yellowCardMatchMinuteValid; }
            set
            {
                if (yellowCardMatchMinuteValid != value)
                {
                    yellowCardMatchMinuteValid = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool RedCardMatchMinuteValid
        {
            get { return redCardMatchMinuteValid; }
            set
            {
                if (redCardMatchMinuteValid != value)
                {
                    redCardMatchMinuteValid = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region IDataErrorInfo implemetation
        public string Error
        {
            get
            {
                return null;
            }
        }
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "GoalMatchMinute":
                        this.GoalMatchMinuteValid = true;
                        if (string.IsNullOrEmpty(this.GoalMatchMinute))
                        {
                            this.GoalMatchMinuteValid = false;
                            return string.Empty;
                        }
                        int goalMatchMinute;
                        if (!int.TryParse(this.GoalMatchMinute, out goalMatchMinute))
                        {
                            this.GoalMatchMinuteValid = false;
                            return "Must be a number between 1 and 120!"; // MatchMinute's max value is not yet limited by the value of MatchDuration!
                        }
                        if (!goalMatchMinute.IsValidMatchMinute())
                        {
                            this.GoalMatchMinuteValid = false;
                            return "Must be a number between 1 and 120!";
                        }
                        break;
                    case "AssistMatchMinute":
                        this.AssistMatchMinuteValid = true;
                        if (string.IsNullOrEmpty(this.AssistMatchMinute))
                        {
                            this.AssistMatchMinuteValid = false;
                            return string.Empty;
                        }
                        int assistMatchMinute;
                        if (!int.TryParse(this.AssistMatchMinute, out assistMatchMinute))
                        {
                            this.AssistMatchMinuteValid = false;
                            return "Must be a number between 1 and 120!";
                        }
                        if (!assistMatchMinute.IsValidMatchMinute())
                        {
                            this.AssistMatchMinuteValid = false;
                            return "Must be a number between 1 and 120!";
                        }
                        break;
                    case "PenaltyMatchMinute":
                        this.PenaltyMatchMinuteValid = true;
                        if (string.IsNullOrEmpty(this.PenaltyMatchMinute))
                        {
                            this.PenaltyMatchMinuteValid = false;
                            return string.Empty;
                        }
                        int penaltyMatchMinute;
                        if (!int.TryParse(this.PenaltyMatchMinute, out penaltyMatchMinute))
                        {
                            this.PenaltyMatchMinuteValid = false;
                            return "Must be a number between 1 and 120!";
                        }
                        if (!penaltyMatchMinute.IsValidMatchMinute())
                        {
                            this.PenaltyMatchMinuteValid = false;
                            return "Must be a number between 1 and 120!";
                        }
                        break;
                    case "YellowCardMatchMinute":
                        this.YellowCardMatchMinuteValid = true;
                        if (string.IsNullOrEmpty(this.YellowCardMatchMinute))
                        {
                            this.YellowCardMatchMinuteValid = false;
                            return string.Empty;
                        }
                        int yellowCardMatchMinute;
                        if (!int.TryParse(this.YellowCardMatchMinute, out yellowCardMatchMinute))
                        {
                            this.YellowCardMatchMinuteValid = false;
                            return "Must be a number between 1 and 120!";
                        }
                        if (!yellowCardMatchMinute.IsValidMatchMinute())
                        {
                            this.YellowCardMatchMinuteValid = false;
                            return "Must be a number between 1 and 120!";
                        }
                        break;
                    case "RedCardMatchMinute":
                        this.RedCardMatchMinuteValid = true;
                        if (string.IsNullOrEmpty(this.RedCardMatchMinute))
                        {
                            this.RedCardMatchMinuteValid = false;
                            return string.Empty;
                        }
                        int redCardMatchMinute;
                        if (!int.TryParse(this.RedCardMatchMinute, out redCardMatchMinute))
                        {
                            this.RedCardMatchMinuteValid = false;
                            return "Must be a number between 1 and 120!";
                        }
                        if (!redCardMatchMinute.IsValidMatchMinute())
                        {
                            this.RedCardMatchMinuteValid = false;
                            return "Must be a number between 1 and 120!";
                        }
                        break;
                }
                return string.Empty;
            }
        }
        #endregion
    }
}
