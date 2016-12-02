using Domain.Entities;
using Domain.Value_Objects;
using FootballManager.Admin.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FootballManager.Admin.ViewModel
{
    public class SeriesScheduleEditViewModel : ViewModelBase
    {
        private Match matchToEdit;
        private string newLocation;

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

        public ICommand SaveEditMatchCommand { get; }



        private void SaveEditMatch(object obj)
        {
            this.matchToEdit.Location = new ArenaName(newLocation);
            Messenger.Default.Send<Match>(this.matchToEdit);
            
        }

        private void OnMatchObjReceived(Match obj)
        {
            this.matchToEdit = obj;
            
        }
    }
}
