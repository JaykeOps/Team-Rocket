using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Domain.Entities;
using Domain.Services;
using Domain.Value_Objects;
using FootballManager.App.Utility;

namespace FootballManager.App.ViewModel
{
    public class MatchProtocolViewModel : ViewModelBase
    {
        private GameService gameService;
        private Game gameToDisplay;
        private ObservableCollection<GameResult> gameResultToDisplay;
        private IEnumerable<object> events;

        public MatchProtocolViewModel()
        {
            gameService = new GameService();
            gameResultToDisplay = new ObservableCollection<GameResult>();
            Messenger.Default.Register<Game>(this, OnMatchObjReceived);
        }

        public IEnumerable<object> Events
        {
            get { return events; }
            set
            {
                events = value;
                OnPropertyChanged();
            }
        }

        public Game Game
        {
            get
            { return gameToDisplay; }
            set
            {
                gameToDisplay = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<GameResult> GameResultToDisplay
        {
            get
            { return gameResultToDisplay; }
            set
            {
                gameResultToDisplay = value;
                OnPropertyChanged();
            }
        }
        private void OnMatchObjReceived(Game obj)
        {
            if (obj != null)
            {
                this.Events = gameService.GetAllEventsFromGame(obj);
                this.gameToDisplay = obj;
                this.gameResultToDisplay.Add(obj.Protocol.GameResult);
            }
        }
    }
}
