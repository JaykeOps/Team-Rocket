using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using Domain.Value_Objects;
using FootballManager.Admin.Extensions;
using FootballManager.Admin.Utility;
using Domain.Helper_Classes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;


namespace FootballManager.Admin.ViewModel
{
    public class SeriesGameProtocolViewModel : ViewModelBase, IDataErrorInfo
    {
        private TeamService teamService;
        private GameService gameService;
        private PlayerService playerService;

        private Game game;

        public SeriesGameProtocolViewModel()
        {
            this.teamService = new TeamService();
            this.gameService = new GameService();
            this.playerService = new PlayerService();

            homeTeamPlayerCollection = new ObservableCollection<Player>();
            awayTeamPlayerCollection = new ObservableCollection<Player>();
            homeTeamActivePlayerCollection = new ObservableCollection<Player>();
            awayTeamActivePlayerCollection = new ObservableCollection<Player>();
            eventsCollection = new ObservableCollection<object>();

            Messenger.Default.Register<Match>(this, this.OnMatchObjReceived);
        }

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
                    this.addGoalToGameCommand = new RelayCommand(this.AddGoalToGame, this.CanAddGoalToGame);
                }
                return this.addGoalToGameCommand;
            }
        }

        private void AddGoalToGame(object obj)
        {

            if (selectedHomeActivePlayer != null && selectedHomeActivePlayer.TeamId == game.HomeTeamId)
            {
                this.gameService.AddGoalToGame(game.Id, selectedHomeActivePlayer.Id, int.Parse(GoalMatchMinute));
                HomeTeamResult = this.UpdateGameData(game.Id).Protocol.GameResult.HomeTeamScore.ToString();
            }
            else if (selectedAwayActivePlayer != null && selectedAwayActivePlayer.TeamId == game.AwayTeamId)
            {
                this.gameService.AddGoalToGame(game.Id, selectedAwayActivePlayer.Id, int.Parse(GoalMatchMinute));
                AwayTeamResult = this.UpdateGameData(game.Id).Protocol.GameResult.AwayTeamScore.ToString();
            }                

            UpdateEventData();
            GoalMatchMinute = string.Empty;
        }

        private bool CanAddGoalToGame(object obj)
        {
            if ((selectedHomeActivePlayer == null && string.IsNullOrEmpty(GoalMatchMinute))
               || (selectedAwayActivePlayer == null && string.IsNullOrEmpty(GoalMatchMinute)))                        
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Assist
        private string assistMatchMinute;
        private ICommand addAssistToGameCommand;

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

        public ICommand AddAssistToGameCommand
        {
            get
            {
                if (this.addAssistToGameCommand == null)
                {
                    this.addAssistToGameCommand = new RelayCommand(this.AddAssistToGame);
                }
                return this.addAssistToGameCommand;
            }
        }

        private void AddAssistToGame(object obj)
        {
            if (selectedHomeActivePlayer != null && selectedHomeActivePlayer.TeamId == game.HomeTeamId)
            {
                this.gameService.AddAssistToGame(game.Id, selectedHomeActivePlayer.Id, int.Parse(GoalMatchMinute));
                HomeTeamResult = this.UpdateGameData(game.Id).Protocol.GameResult.HomeTeamScore.ToString();
            }
            else if (selectedAwayActivePlayer != null && selectedAwayActivePlayer.TeamId == game.AwayTeamId)
            {
                this.gameService.AddAssistToGame(game.Id, selectedAwayActivePlayer.Id, int.Parse(GoalMatchMinute));
                AwayTeamResult = this.UpdateGameData(game.Id).Protocol.GameResult.AwayTeamScore.ToString();
            }

            UpdateEventData();
            GoalMatchMinute = string.Empty;
            selectedHomeActivePlayer = null;
            selectedAwayActivePlayer = null;
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
            if (selectedHomeActivePlayer != null && selectedHomeActivePlayer.TeamId == game.HomeTeamId)
            {
                this.gameService.AddPenaltyToGame(game.Id, selectedHomeActivePlayer.Id, int.Parse(GoalMatchMinute), GetIsGoal);
                HomeTeamResult = this.UpdateGameData(game.Id).Protocol.GameResult.HomeTeamScore.ToString();
            }
            else if (selectedAwayActivePlayer != null && selectedAwayActivePlayer.TeamId == game.AwayTeamId)
            {
                this.gameService.AddPenaltyToGame(game.Id, selectedAwayActivePlayer.Id, int.Parse(GoalMatchMinute), GetIsGoal);
                AwayTeamResult = this.UpdateGameData(game.Id).Protocol.GameResult.AwayTeamScore.ToString();
            }

            UpdateEventData();
            GoalMatchMinute = string.Empty;
            selectedHomeActivePlayer = null;
            selectedAwayActivePlayer = null;
        }
        #endregion

        #region YellowCard
        private string yellowCardMatchMinute;
        private ICommand addYellowCardToGameCommand;

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

        public ICommand AddYellowCardToGameCommand
        {
            get
            {
                if (this.addYellowCardToGameCommand == null)
                {
                    this.addYellowCardToGameCommand = new RelayCommand(this.AddYellowCardToGame);
                }
                return this.addYellowCardToGameCommand;
            }
        }

        private void AddYellowCardToGame(object obj)
        {
            if (selectedHomeActivePlayer != null && selectedHomeActivePlayer.TeamId == game.HomeTeamId)
            {
                this.gameService.AddYellowCardToGame(game.Id, selectedHomeActivePlayer.Id, int.Parse(GoalMatchMinute));
                HomeTeamResult = this.UpdateGameData(game.Id).Protocol.GameResult.HomeTeamScore.ToString();
            }
            else if (selectedAwayActivePlayer != null && selectedAwayActivePlayer.TeamId == game.AwayTeamId)
            {
                this.gameService.AddYellowCardToGame(game.Id, selectedAwayActivePlayer.Id, int.Parse(GoalMatchMinute));
                AwayTeamResult = this.UpdateGameData(game.Id).Protocol.GameResult.AwayTeamScore.ToString();
            }

            UpdateEventData();
            GoalMatchMinute = string.Empty;
            selectedHomeActivePlayer = null;
            selectedAwayActivePlayer = null;
        }
        #endregion

        #region RedCard
        private string redCardMatchMinute;
        private ICommand addRedCardToGameCommand;

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

        public ICommand AddRedCardToGameCommand
        {
            get
            {
                if (this.addRedCardToGameCommand == null)
                {
                    this.addRedCardToGameCommand = new RelayCommand(this.AddRedCardToGame);
                }
                return this.addRedCardToGameCommand;
            }
        }

        private void AddRedCardToGame(object obj)
        {
            if (selectedHomeActivePlayer != null && selectedHomeActivePlayer.TeamId == game.HomeTeamId)
            {
                this.gameService.AddRedCardToGame(game.Id, selectedHomeActivePlayer.Id, int.Parse(GoalMatchMinute));
                HomeTeamResult = this.UpdateGameData(game.Id).Protocol.GameResult.HomeTeamScore.ToString();
            }
            else if (selectedAwayActivePlayer != null && selectedAwayActivePlayer.TeamId == game.AwayTeamId)
            {
                this.gameService.AddRedCardToGame(game.Id, selectedAwayActivePlayer.Id, int.Parse(GoalMatchMinute));
                AwayTeamResult = this.UpdateGameData(game.Id).Protocol.GameResult.AwayTeamScore.ToString();
            }

            UpdateEventData();
            GoalMatchMinute = string.Empty;
            selectedHomeActivePlayer = null;
            selectedAwayActivePlayer = null;
        }
        #endregion

        #region Overtime
        private string overtime;

        public string Overtime
        {
            get { return overtime; }
            set
            {
                if (overtime != value)
                {
                    overtime = value;
                    OnPropertyChanged();                     
                }
            }
        }

        private void SaveOvertime()
        {
            if (Overtime != null)
            {
                game.Protocol.OverTime = new OverTime(int.Parse(overtime));
            }
        }
        #endregion

        #region Save Game Protocol
        private ICommand saveGameProtocolCommand;

        public ICommand SaveGameProtocolCommand
        {
            get
            {
                if (this.saveGameProtocolCommand == null)
                {
                    this.saveGameProtocolCommand = new RelayCommand(this.SaveGameProtocol);
                }
                return this.saveGameProtocolCommand;
            }
        }

        private void SaveGameProtocol(object obj)
        {
            SaveOvertime();
            gameService.Add(game);            
        }
        #endregion

        #region Collections
        private ObservableCollection<Player> homeTeamPlayerCollection;
        private ObservableCollection<Player> awayTeamPlayerCollection;
        private ObservableCollection<Player> homeTeamActivePlayerCollection;
        private ObservableCollection<Player> awayTeamActivePlayerCollection;
        private ObservableCollection<object> eventsCollection;

        public ObservableCollection<Player> HomeTeamPlayerCollection
        {
            get { return this.homeTeamPlayerCollection; }
            set
            {
                if (this.homeTeamPlayerCollection != value)
                {
                    this.homeTeamPlayerCollection = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Player> AwayTeamPlayerCollection
        {
            get { return this.awayTeamPlayerCollection; }
            set
            {
                if (this.awayTeamPlayerCollection != value)
                {
                    this.awayTeamPlayerCollection = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Player> HomeTeamActivePlayerCollection
        {
            get { return this.homeTeamActivePlayerCollection; }
            set
            {
                if (this.homeTeamActivePlayerCollection != value)
                {
                    this.homeTeamActivePlayerCollection = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Player> AwayTeamActivePlayerCollection
        {
            get { return this.awayTeamActivePlayerCollection; }
            set
            {
                if (this.awayTeamActivePlayerCollection != value)
                {
                    this.awayTeamActivePlayerCollection = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<object> EventsCollection
        {
            get { return this.eventsCollection; }
            set
            {
                if (this.eventsCollection != value)
                {
                    this.eventsCollection = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Home Lists and dependencies
        private ICommand homeFromActivePlayerCommand;
        private ICommand homeToActivePlayerCommand;

        private Player selectedHomeActivePlayer;
        private Player selectedHomeNotActivePlayer;

        public Player SelectedHomeActivePlayer
        {
            get { return this.selectedHomeActivePlayer; }
            set
            {
                if (selectedHomeActivePlayer != value)
                {
                    this.selectedHomeActivePlayer = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand HomeFromActivePlayerCommand
        {
            get
            {
                if (this.homeFromActivePlayerCommand == null)
                {
                    this.homeFromActivePlayerCommand = new RelayCommand(this.HomeFromActivePlayer, this.CanHomeFromActivePlayer);
                }
                return this.homeFromActivePlayerCommand;
            }
        }

        private void HomeFromActivePlayer(object obj)
        {
            var player = selectedHomeActivePlayer;

            if (player.TeamId == this.game.HomeTeamId)
            {
                HomeTeamActivePlayerCollection.Remove(player);
                HomeTeamPlayerCollection.Add(player);

                game.Protocol.HomeTeamActivePlayers.Remove(player.Id);
            }
        }

        private bool CanHomeFromActivePlayer(object obj)
        {
            if (selectedHomeActivePlayer == null)
            {
                return false;
            }
            return true;
        }

        // Not active player

        public Player SelectedHomeNotActivePlayer
        {
            get { return selectedHomeNotActivePlayer; }
            set
            {
                if (selectedHomeNotActivePlayer != value)
                {
                    selectedHomeNotActivePlayer = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand HomeToActivePlayerCommand
        {
            get
            {
                if (this.homeToActivePlayerCommand == null)
                {
                    this.homeToActivePlayerCommand = new RelayCommand(this.HomeToActivePlayer, this.CanHomeToActivePlayer);
                }
                return this.homeToActivePlayerCommand;
            }
        }

        private void HomeToActivePlayer(object obj)
        {
            var player = selectedHomeNotActivePlayer;

            if (player.TeamId == this.game.HomeTeamId)
            {                
                HomeTeamPlayerCollection.Remove(player);
                HomeTeamActivePlayerCollection.Add(player);

                game.Protocol.HomeTeamActivePlayers.Add(player.Id);
            }
        }

        private bool CanHomeToActivePlayer(object obj)
        {
            if (selectedHomeNotActivePlayer == null)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Away Lists and dependencies
        private ICommand awayFromActivePlayerCommand;
        private ICommand awayToActivePlayerCommand;

        private Player selectedAwayActivePlayer;
        private Player selectedAwayNotActivePlayer;

        public Player SelectedAwayActivePlayer
        {
            get { return this.selectedAwayActivePlayer; }
            set
            {
                if (selectedAwayActivePlayer != value)
                {
                    this.selectedAwayActivePlayer = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand AwayFromActivePlayersCommand
        {
            get
            {
                if (this.awayFromActivePlayerCommand == null)
                {
                    this.awayFromActivePlayerCommand = new RelayCommand(this.AwayFromActivePlayers, this.CanAwayFromActivePlayers);
                }
                return this.awayFromActivePlayerCommand;
            }
        }

        private void AwayFromActivePlayers(object obj)
        {
            var player = selectedAwayActivePlayer;

            if (player.TeamId == this.game.AwayTeamId)
            {
                AwayTeamActivePlayerCollection.Remove(player);
                AwayTeamPlayerCollection.Add(player);
                
                game.Protocol.AwayTeamActivePlayers.Remove(player.Id);
            }
        }

        private bool CanAwayFromActivePlayers(object obj)
        {
            if (selectedAwayActivePlayer == null)
            {
                return false;
            }
            return true;
        }

        // Not active player

        public Player SelectedAwayNotActivePlayer
        {
            get { return selectedAwayNotActivePlayer; }
            set
            {
                if (selectedAwayNotActivePlayer != value)
                {
                    selectedAwayNotActivePlayer = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand AwayToActivePlayerCommand
        {
            get
            {
                if (this.awayToActivePlayerCommand == null)
                {
                    this.awayToActivePlayerCommand = new RelayCommand(this.AwayToActivePlayer, this.CanAwayToActivePlayer);
                }
                return this.awayToActivePlayerCommand;
            }
        }

        private void AwayToActivePlayer(object obj)
        {
            var player = selectedAwayNotActivePlayer;

            if (player.TeamId == this.game.AwayTeamId)
            {
                AwayTeamPlayerCollection.Remove(player);
                AwayTeamActivePlayerCollection.Add(player);
                
                game.Protocol.AwayTeamActivePlayers.Add(player.Id);
            }
        }

        private bool CanAwayToActivePlayer(object obj)
        {
            if (selectedAwayNotActivePlayer == null)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Events

        private ICommand removeEventCommand;
        private object selectedEvent;

        public object SelectedEvent
        {
            get { return this.selectedEvent; }
            set
            {
                this.selectedEvent = value;
                OnPropertyChanged();
            }
        }

        public ICommand RemoveEventCommand
        {
            get
            {
                if (this.removeEventCommand == null)
                {
                    this.removeEventCommand = new RelayCommand(this.RemoveEvent);
                }
                return this.removeEventCommand;
            }
        }

        private void RemoveEvent(object obj)
        {
            gameService.RemoveEvent(SelectedEvent, game.Id);
            UpdateEventData();
            UpdateMatchResultData();
            SelectedEvent = null;
        }
        #endregion

        #region Methods                      
        private void OnMatchObjReceived(Match match)
        {                    
            if (match != null)
            {
                game = gameService.GetGameFromMatch(match);
                if (game != null)
                {
                    HomeTeamName = this.teamService.FindTeamById(game.HomeTeamId).ToString();
                    AwayTeamName = this.teamService.FindTeamById(game.AwayTeamId).ToString();
                    HomeTeamResult = game.Protocol.GameResult.HomeTeamScore.ToString();
                    AwayTeamResult = game.Protocol.GameResult.AwayTeamScore.ToString();
                    HomeTeamPlayerCollection = this.playerService.GetAllPlayersInTeam(game.HomeTeamId).ToObservableCollection();
                    AwayTeamPlayerCollection = this.playerService.GetAllPlayersInTeam(game.AwayTeamId).ToObservableCollection();

                    HomeTeamActivePlayerCollection = this.GetActivePlayers(game.Protocol.HomeTeamActivePlayers);
                    AwayTeamActivePlayerCollection = this.GetActivePlayers(game.Protocol.AwayTeamActivePlayers);

                    EventsCollection = gameService.GetAllEventsFromGame(game).ToObservableCollection();
                }
                else if (game == null)
                {
                    this.game = new Game(match);
                    gameService.Add(game);
                    HomeTeamName = this.teamService.FindTeamById(this.game.HomeTeamId).Name.Value;
                    AwayTeamName = this.teamService.FindTeamById(this.game.AwayTeamId).Name.Value;

                    HomeTeamPlayerCollection = this.playerService.GetAllPlayersInTeam(this.game.HomeTeamId).ToObservableCollection();
                    AwayTeamPlayerCollection = this.playerService.GetAllPlayersInTeam(this.game.AwayTeamId).ToObservableCollection();
                }               
            }
        }

        private Game UpdateGameData(Guid gameId)
        {
            return this.gameService.FindById(gameId);
        }

        private void UpdateEventData()
        {
            EventsCollection = this.gameService.GetAllEventsFromGame(this.game).ToObservableCollection();          
        }

        private void UpdateMatchResultData()
        {
            HomeTeamResult = this.gameService.FindById(this.game.Id).Protocol.GameResult.HomeTeamScore.ToString();
            AwayTeamResult = this.gameService.FindById(this.game.Id).Protocol.GameResult.AwayTeamScore.ToString();
        }

        private ObservableCollection<Player> GetActivePlayers(IEnumerable<Guid> ids)
        {
            var activePlayers = new ObservableCollection<Player>();
            foreach (var id in ids)
            {                
                activePlayers.Add(playerService.FindById(id));
            }
            return activePlayers;
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
                            return "Only 1-120 are valid!"; // MatchMinute's max value is not yet limited by the value of MatchDuration!
                        }
                        if (!goalMatchMinute.IsMatchMinute())
                        {
                            this.GoalMatchMinuteValid = false;
                            return "Only 1-120 are valid!";
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
                            return "Only 1-120 are valid!";
                        }
                        if (!assistMatchMinute.IsMatchMinute())
                        {
                            this.AssistMatchMinuteValid = false;
                            return "Only 1-120 are valid!";
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
                            return "Only 1-120 are valid!";
                        }
                        if (!penaltyMatchMinute.IsMatchMinute())
                        {
                            this.PenaltyMatchMinuteValid = false;
                            return "Only 1-120 are valid!";
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
                            return "Only 1-120 are valid!";
                        }
                        if (!yellowCardMatchMinute.IsMatchMinute())
                        {
                            this.YellowCardMatchMinuteValid = false;
                            return "Only 1-120 are valid!";
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
                            return "Only 1-120 are valid!";
                        }
                        if (!redCardMatchMinute.IsMatchMinute())
                        {
                            this.RedCardMatchMinuteValid = false;
                            return "Only 1-120 are valid!";
                        }
                        break;
                }
                return string.Empty;
            }
        }
        #endregion
    }
}
