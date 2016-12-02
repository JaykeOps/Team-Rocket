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
using FootballManager.Admin.Utility;

namespace FootballManager.Admin.ViewModel
{
    public class SeriesGameProtocolViewModel : ViewModelBase
    {
        private TeamService teamService;
        private GameService gameService;
        private PlayerService playerService;

        private ObservableCollection<IExposablePlayer> homeTeamPlayerCollection;
        private ObservableCollection<IExposablePlayer> awayTeamPlayerCollection;
        private ObservableCollection<IExposablePlayer> homeTeamActivePlayerCollection;
        private ObservableCollection<IExposablePlayer> awayTeamActivePlayerCollection;

        private ObservableCollection<string> eventsCollection;

        private Game newGame;

        private string homeTeamName;
        private string awayTeamName;
        private string homeTeamResult;
        private string awayTeamResult;

        public SeriesGameProtocolViewModel()
        {
            this.teamService = new TeamService();
            this.gameService = new GameService();
            this.playerService = new PlayerService();
            Messenger.Default.Register<Match>(this, this.OnMatchObjReceived);
        }

        #region Protocol top section
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

        public ObservableCollection<IExposablePlayer> HomeTeamPlayerCollection
        {
            get { return homeTeamPlayerCollection; }
            set
            {
                homeTeamPlayerCollection = value;
                OnPropertyChanged();
            }
        }


        #region Methods              
        private void OnMatchObjReceived(Match match)
        {            
            this.newGame = new Game(match);
            HomeTeamName = this.teamService.FindTeamById(newGame.Protocol.HomeTeamId).Name.Value;
            AwayTeamName = this.teamService.FindTeamById(newGame.Protocol.AwayTeamId).Name.Value;
            
            //Fyll team listan med spelare - hemma/borta lagen
            

            // Får flyttas till add goal kommandot för annars hämtar man null
            //HomeTeamResult = newGame.Protocol.GameResult.HomeTeamScore.ToString();
            //AwayTeamResult = newGame.Protocol.GameResult.AwayTeamScore.ToString();
        }
        #endregion
    }
}
