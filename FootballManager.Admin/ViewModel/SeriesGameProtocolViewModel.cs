using Domain.Entities;
using Domain.Helper_Classes;
using Domain.Services;
using FootballManager.Admin.Extensions;
using FootballManager.Admin.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Domain.Value_Objects;

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

        #region SelectedPlayer

        private Player selectedPlayer;

        public Player SelectedPlayer
        {
            get { return selectedPlayer; }
            set
            {
                selectedPlayer = value;
                OnPropertyChanged();
                OnPropertyChanged("GoalMatchMinute");
                OnPropertyChanged("AssistMatchMinute");
                OnPropertyChanged("PenaltyMatchMinute");
                OnPropertyChanged("YellowCardMatchMinute");
                OnPropertyChanged("RedCardMatchMinute");
            }
        }

        #endregion SelectedPlayer

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

        #endregion Teams and Results

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
                    this.addGoalToGameCommand = new RelayCommand(this.AddGoalToGame, this.CanAddEvent);
                }
                return this.addGoalToGameCommand;
            }
        }

        private void AddGoalToGame(object obj)
        {
            var p = selectedPlayer;
            if (p != null && p.TeamId == game.HomeTeamId)
            {
                this.gameService.AddGoalToGame(game.Id, p.Id, int.Parse(goalMatchMinute));
                HomeTeamResult = this.UpdateGameData(game.Id).Protocol.GameResult.HomeTeamScore.ToString();
            }
            else if (p != null && p.TeamId == game.AwayTeamId)
            {
                this.gameService.AddGoalToGame(game.Id, p.Id, int.Parse(goalMatchMinute));
                AwayTeamResult = this.UpdateGameData(game.Id).Protocol.GameResult.AwayTeamScore.ToString();
            }

            UpdateEventData();
            GoalMatchMinute = string.Empty;
            SelectedPlayer = null;
        }

        #endregion Goal

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
                    this.addAssistToGameCommand = new RelayCommand(this.AddAssistToGame, this.CanAddEvent);
                }
                return this.addAssistToGameCommand;
            }
        }

        private void AddAssistToGame(object obj)
        {
            var p = selectedPlayer;
            if (p != null && p.TeamId == game.HomeTeamId)
            {
                this.gameService.AddAssistToGame(game.Id, p.Id, int.Parse(AssistMatchMinute));
                HomeTeamResult = this.UpdateGameData(game.Id).Protocol.GameResult.HomeTeamScore.ToString();
            }
            else if (p != null && p.TeamId == game.AwayTeamId)
            {
                this.gameService.AddAssistToGame(game.Id, p.Id, int.Parse(AssistMatchMinute));
                AwayTeamResult = this.UpdateGameData(game.Id).Protocol.GameResult.AwayTeamScore.ToString();
            }

            UpdateEventData();
            AssistMatchMinute = string.Empty;
            SelectedPlayer = null;
        }

        #endregion Assist

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
                    this.addPentalyToGameCommand = new RelayCommand(this.AddPentalyToGame, this.CanAddEvent);
                }
                return this.addPentalyToGameCommand;
            }
        }

        private void AddPentalyToGame(object obj)
        {
            var p = selectedPlayer;
            if (p != null && p.TeamId == game.HomeTeamId)
            {
                this.gameService.AddPenaltyToGame(game.Id, p.Id, int.Parse(PenaltyMatchMinute), GetIsGoal);
                HomeTeamResult = this.UpdateGameData(game.Id).Protocol.GameResult.HomeTeamScore.ToString();
            }
            else if (p != null && p.TeamId == game.AwayTeamId)
            {
                this.gameService.AddPenaltyToGame(game.Id, p.Id, int.Parse(PenaltyMatchMinute), GetIsGoal);
                AwayTeamResult = this.UpdateGameData(game.Id).Protocol.GameResult.AwayTeamScore.ToString();
            }

            UpdateEventData();
            PenaltyMatchMinute = string.Empty;
            SelectedPlayer = null;
        }

        #endregion Penalty

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
                    this.addYellowCardToGameCommand = new RelayCommand(this.AddYellowCardToGame, this.CanAddEvent);
                }
                return this.addYellowCardToGameCommand;
            }
        }

        private void AddYellowCardToGame(object obj)
        {
            var p = selectedPlayer;
            if (p != null && p.TeamId == game.HomeTeamId)
            {
                this.gameService.AddYellowCardToGame(game.Id, p.Id, int.Parse(YellowCardMatchMinute));
                HomeTeamResult = this.UpdateGameData(game.Id).Protocol.GameResult.HomeTeamScore.ToString();
            }
            else if (p != null && p.TeamId == game.AwayTeamId)
            {
                this.gameService.AddYellowCardToGame(game.Id, p.Id, int.Parse(YellowCardMatchMinute));
                AwayTeamResult = this.UpdateGameData(game.Id).Protocol.GameResult.AwayTeamScore.ToString();
            }

            UpdateEventData();
            YellowCardMatchMinute = string.Empty;
            SelectedPlayer = null;
        }

        #endregion YellowCard

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
                    this.addRedCardToGameCommand = new RelayCommand(this.AddRedCardToGame, this.CanAddEvent);
                }
                return this.addRedCardToGameCommand;
            }
        }

        private void AddRedCardToGame(object obj)
        {
            var p = selectedPlayer;
            if (p != null && p.TeamId == game.HomeTeamId)
            {
                this.gameService.AddRedCardToGame(game.Id, p.Id, int.Parse(RedCardMatchMinute));
                HomeTeamResult = this.UpdateGameData(game.Id).Protocol.GameResult.HomeTeamScore.ToString();
            }
            else if (p != null && p.TeamId == game.AwayTeamId)
            {
                this.gameService.AddRedCardToGame(game.Id, p.Id, int.Parse(RedCardMatchMinute));
                AwayTeamResult = this.UpdateGameData(game.Id).Protocol.GameResult.AwayTeamScore.ToString();
            }

            UpdateEventData();
            RedCardMatchMinute = string.Empty;
            SelectedPlayer = null;
        }

        #endregion RedCard

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
                game.Protocol.OverTime = new OverTime(int.Parse(this.overtime));
            }
        }

        #endregion Overtime

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
            gameService.Add(game);
            SaveOvertime();
            CloseDialog();
        }
        #endregion Save Game Protocol

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

        #endregion Collections

        #region Home Lists and dependencies

        private ICommand homeFromActivePlayerCommand;
        private ICommand homeToActivePlayerCommand;

        public ICommand HomeFromActivePlayerCommand
        {
            get
            {
                if (this.homeFromActivePlayerCommand == null)
                {
                    this.homeFromActivePlayerCommand = new RelayCommand(this.MovePlayer);
                }
                return this.homeFromActivePlayerCommand;
            }
        }

        public ICommand HomeToActivePlayerCommand
        {
            get
            {
                if (this.homeToActivePlayerCommand == null)
                {
                    this.homeToActivePlayerCommand = new RelayCommand(this.MovePlayer);
                }
                return this.homeToActivePlayerCommand;
            }
        }

        #endregion Home Lists and dependencies

        #region Away Lists and dependencies

        private ICommand awayFromActivePlayerCommand;
        private ICommand awayToActivePlayerCommand;

        public ICommand AwayFromActivePlayersCommand
        {
            get
            {
                if (this.awayFromActivePlayerCommand == null)
                {
                    this.awayFromActivePlayerCommand = new RelayCommand(this.MovePlayer);
                }
                return this.awayFromActivePlayerCommand;
            }
        }

        public ICommand AwayToActivePlayerCommand
        {
            get
            {
                if (this.awayToActivePlayerCommand == null)
                {
                    this.awayToActivePlayerCommand = new RelayCommand(this.MovePlayer);
                }
                return this.awayToActivePlayerCommand;
            }
        }

        #endregion Away Lists and dependencies

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
        #endregion Events

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

                    if (game.Protocol.OverTime != null)
                    {
                        Overtime = game.Protocol.OverTime.Value.ToString();
                    }
                    
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

        private void MovePlayer(object obj)
        {
            if (obj != null)
            {
                var player = (Player)obj;
                var activePlayer = IsActivePlayer(player);

                if (player.TeamId == this.game.HomeTeamId)
                {
                    if (activePlayer != null)
                    {
                        HomeTeamActivePlayerCollection.Remove(player);
                        HomeTeamPlayerCollection.Add(player);

                        game.Protocol.HomeTeamActivePlayers.Remove(player.Id);
                    }
                    else
                    {
                        HomeTeamPlayerCollection.Remove(player);
                        HomeTeamActivePlayerCollection.Add(player);

                        game.Protocol.HomeTeamActivePlayers.Add(player.Id);
                    }
                }
                else if (player.TeamId == game.AwayTeamId)
                {
                    if (activePlayer != null)
                    {
                        AwayTeamActivePlayerCollection.Remove(player);
                        AwayTeamPlayerCollection.Add(player);

                        game.Protocol.AwayTeamActivePlayers.Remove(player.Id);
                    }
                    else
                    {
                        AwayTeamPlayerCollection.Remove(player);
                        AwayTeamActivePlayerCollection.Add(player);

                        game.Protocol.AwayTeamActivePlayers.Add(player.Id);
                    }
                }
            }
        }

        private bool CanAddEvent(object obj)
        {
            if (SelectedPlayer == null)
            {
                return false;
            }
            var p = IsActivePlayer(SelectedPlayer);
            if (p == null)
            {
                return false;
            }
            return true;
        }

        private Player IsActivePlayer(Player playerObj)
        {
            var homeActivePlayers = GetActivePlayers(game.Protocol.HomeTeamActivePlayers).ToList();
            var awayActivePlayers = GetActivePlayers(game.Protocol.AwayTeamActivePlayers).ToList();

            if (playerObj.TeamId == this.game.HomeTeamId)
            {
                foreach (var p in homeActivePlayers)
                {
                    if (p.Id == playerObj.Id)
                    {
                        return p;
                    }
                }
            }
            if (playerObj.TeamId == this.game.AwayTeamId)
            {
                foreach (var p in awayActivePlayers)
                {
                    if (p.Id == playerObj.Id)
                    {
                        return p;
                    }
                }
            }
            return null;
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

        private void CloseDialog()
        {
            var window = Application.Current.Windows.OfType<Window>().
                FirstOrDefault(x => x.IsActive);
            window?.Close();
        }
        #endregion Methods

        #region Validation Properties
        private bool goalMatchMinuteValid;
        private bool assistMatchMinuteValid;
        private bool penaltyMatchMinuteValid;
        private bool yellowCardMatchMinuteValid;
        private bool redCardMatchMinuteValid;
        private bool overtimeValid;

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

        public bool OvertimeValid
        {
            get { return overtimeValid; }
            set
            {
                if (overtimeValid != value)
                {
                    overtimeValid = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion Validation Properties

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
                            return "Only 1-120 are valid!"; // MatchMinute's max value is not yet limited by the value of MatchDuration.
                        }
                        if (!goalMatchMinute.IsMatchMinute())
                        {
                            this.GoalMatchMinuteValid = false;
                            return "Only 1-120 are valid!";
                        }                        
                        if (IsNotActivePlayer())
                        {
                            this.GoalMatchMinuteValid = false;
                            return "Select an active player!";
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
                        if (IsNotActivePlayer())
                        {
                            this.AssistMatchMinuteValid = false;
                            return "Select an active player!";
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
                        if (IsNotActivePlayer())
                        {
                            this.PenaltyMatchMinuteValid = false;
                            return "Select an active player!";
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
                        if (IsNotActivePlayer())
                        {
                            this.YellowCardMatchMinuteValid = false;
                            return "Select an active player!";
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
                        if (IsNotActivePlayer())
                        {
                            this.RedCardMatchMinuteValid = false;
                            return "Select an active player!";
                        }
                        break;

                    case "Overtime":
                        this.OvertimeValid = true;
                        if (string.IsNullOrEmpty(this.Overtime))
                        {
                            this.OvertimeValid = false;
                            return string.Empty;
                        }
                        int overtime;
                        if (!int.TryParse(this.Overtime, out overtime))
                        {
                            this.OvertimeValid = false;
                            return "Must be an integer between 0 and 30!";
                        }
                        if (!overtime.IsValidOverTime())
                        {
                            this.OvertimeValid = false;
                            return "Must be an integer between 0 and 30!";
                        }
                        break;
                }
                return string.Empty;
            }
        }

        private bool IsNotActivePlayer()
        {
            if (this.SelectedPlayer == null)
            {
                return true;
            }
            else
            {
                var nullIfNotActive = IsActivePlayer(this.SelectedPlayer);
                return (nullIfNotActive == null);
            }
        }
        #endregion
    }
}