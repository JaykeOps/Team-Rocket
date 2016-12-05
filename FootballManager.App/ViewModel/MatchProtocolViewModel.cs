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
