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

        public ICommand AddPlayerToActivePlayersCommand
        {
            get
            {
                if (this.addPlayerToActivePlayersCommand == null)
                {
                    this.addPlayerToActivePlayersCommand = new RelayCommand(AddPlayerToActivePlayers);
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
                    this.removePlayerFromActivePlayersCommand = new RelayCommand(RemovePlayerFromActivePlayers);
                }
                return removePlayerFromActivePlayersCommand;

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
            get { return homeTeamActivePlayerCollection; }
            set
            {
                homeTeamActivePlayerCollection = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Player> AwayTeamActivePlayerCollection
        {
            get { return awayTeamActivePlayerCollection; }
            set
            {
                awayTeamActivePlayerCollection = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Methods 
        private void AddPlayerToActivePlayers(object playerObj)
        {
            if (playerObj == null) return;
            Player player = (Player)playerObj;
            if (player.TeamId == newGame.HomeTeamId)
            {
                HomeTeamPlayerCollection.Remove(player);
                HomeTeamActivePlayerCollection.Add(player);                                              
            }
            if (player.TeamId == newGame.AwayTeamId)
            {
                AwayTeamPlayerCollection.Remove(player);
                AwayTeamActivePlayerCollection.Add(player);
            }           
        }

        private void RemovePlayerFromActivePlayers(object playerObj)
        {
            if (playerObj == null) return;
            Player player = (Player)playerObj;
            if (player.TeamId == newGame.HomeTeamId)
            {                
                HomeTeamActivePlayerCollection.Remove(player);
                HomeTeamPlayerCollection.Add(player);
            }
            if (player.TeamId == newGame.AwayTeamId)
            {
                AwayTeamActivePlayerCollection.Remove(player);
                AwayTeamPlayerCollection.Add(player);                
            }
        }
                     
        private void OnMatchObjReceived(Match match)
        {            
            this.newGame = new Game(match);
            HomeTeamName = this.teamService.FindTeamById(newGame.HomeTeamId).Name.Value;
            AwayTeamName = this.teamService.FindTeamById(newGame.AwayTeamId).Name.Value;
            
            HomeTeamPlayerCollection = playerService.GetAllPlayersInTeam(newGame.HomeTeamId).ToObservableCollection();
            AwayTeamPlayerCollection = playerService.GetAllPlayersInTeam(newGame.AwayTeamId).ToObservableCollection();

            // Får flyttas till add goal kommandot för annars hämtar man null
            //HomeTeamResult = newGame.Protocol.GameResult.HomeTeamScore.ToString();
            //AwayTeamResult = newGame.Protocol.GameResult.AwayTeamScore.ToString();
        }
        #endregion
    }
}
