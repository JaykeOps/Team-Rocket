using Domain.Entities;
using Domain.Services;
using FootballManager.App.Utility;
using System.Collections.Generic;

namespace FootballManager.App.ViewModel
{
    public class MatchProtocolViewModel : ViewModelBase
    {
        private GameService gameService;
        private Game gameToDisplay;
        private IEnumerable<object> events;

        public MatchProtocolViewModel()
        {
            gameService = new GameService();
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

        public Game GameToDisplay
        {
            get
            { return gameToDisplay; }
            set
            {
                gameToDisplay = value;
                OnPropertyChanged();
            }
        }

        private void OnMatchObjReceived(Game obj)
        {
            if (obj != null)
            {
                this.Events = gameService.GetAllEventsFromGame(obj);
                this.gameToDisplay = obj;
            }
        }
    }
}