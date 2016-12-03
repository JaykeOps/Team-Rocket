using Domain.Entities;
using Domain.Value_Objects;
using FootballManager.Admin.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FootballManager.Admin.ViewModel
{
    public class SeriesScheduleEditViewModel : ViewModelBase
    {
        private Match matchToEdit;
        private string newLocation;
        private string newDate;
        private string newTime;

        public SeriesScheduleEditViewModel()
        {
            this.SaveEditMatchCommand = new RelayCommand(SaveEditMatch);
            Messenger.Default.Register<Match>(this, OnMatchObjReceived);
        }

        public string NewLocation
        {
            get { return this.newLocation; }
            set
            {
                newLocation = value;
                OnPropertyChanged();
            }
        }

        public string NewDate
        {
            get { return this.newDate; }
            set
            {
                newDate = value;
                OnPropertyChanged();
            }
        }

        public string NewTime
        {
            get { return this.newTime; }
            set
            {
                newTime = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveEditMatchCommand { get; }



        private void SaveEditMatch(object obj)
        {
            
            
            if (NewLocation == null)
            {
                this.matchToEdit.Location = this.matchToEdit.Location;
            }
            else
            {
                this.matchToEdit.Location = new ArenaName(newLocation);
            }

            if (NewTime == null)
            {
                var matchDateAndTime = Convert.ToDateTime(matchToEdit.MatchDate.ToString());
                var matchOldTime = matchDateAndTime.ToString("HH:mm");
                var matchDateToSet = Convert.ToDateTime(this.newDate + " " + matchOldTime);
                this.matchToEdit.MatchDate = new MatchDateAndTime(matchDateToSet);
            }
            else
            {
                this.matchToEdit.MatchDate = this.matchToEdit.MatchDate;
            }

            if (NewDate == null)
            {
                var matchDateAndTime = Convert.ToDateTime(matchToEdit.MatchDate.ToString());
                var matchOldDate = matchDateAndTime.ToString("yyyy-MM-dd");
                var matchDateToSet = Convert.ToDateTime(matchOldDate + " " + this.newTime);
                this.matchToEdit.MatchDate = new MatchDateAndTime(matchDateToSet);
            }
            else
            {
                this.matchToEdit.MatchDate = this.matchToEdit.MatchDate;
            }

            if (NewDate != null && NewTime != null)
            {
                DateTime matchDateToSet = Convert.ToDateTime(this.newDate + " " + this.newTime);
                this.matchToEdit.MatchDate = new MatchDateAndTime(matchDateToSet);
            }
            else
            {
                this.matchToEdit.MatchDate = this.matchToEdit.MatchDate;
            }

            Window window = Application.Current.Windows.OfType<Window>()
                .Where(w => w.Title == "Edit match").FirstOrDefault();
            if (window != null)
            {
                window.Close();
            }
        }

        private void OnMatchObjReceived(Match obj)
        {
            this.matchToEdit = obj;
        }
    }
}
