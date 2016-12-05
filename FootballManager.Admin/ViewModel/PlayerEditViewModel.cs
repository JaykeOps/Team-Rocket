using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FootballManager.Admin.Utility;
using Domain.Entities;

namespace FootballManager.Admin.ViewModel
{
    public class PlayerEditViewModel : ViewModelBase
    {
        private string firstName;
        private string lastName;
        private string dateOfBirth;
        private Player playerToEdit;

        public PlayerEditViewModel()
        {
            this.SaveEditPlayerCommand = new RelayCommand(SaveEditedPlayer);

            Messenger.Default.Register<Player>(this, this.OnPlayerObjReceived);
        }

        public ICommand SaveEditPlayerCommand { get; }

        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                if (lastName != value)
                {
                    lastName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string DateOfBirth
        {
            get { return dateOfBirth; }
            set
            {
                if (dateOfBirth != value)
                {
                    dateOfBirth = value;
                    OnPropertyChanged();
                }
            }
        }

        private void SaveEditedPlayer(object obj)
        {
            this.playerToEdit.Name.FirstName = this.firstName;
            this.playerToEdit.Name.LastName = this.lastName;
            this.playerToEdit.DateOfBirth.Value = Convert.ToDateTime(this.dateOfBirth);

            Window window = Application.Current.Windows.OfType<Window>()
                    .Where(w => w.Name == "EditPlayerWindow").FirstOrDefault();
            if (window != null)
            {
                window.Close();
            }
        }

        private void OnPlayerObjReceived(Player obj)
        {
            this.playerToEdit = obj;
            this.FirstName = this.playerToEdit.Name.FirstName;
            this.LastName = this.playerToEdit.Name.LastName;
            this.DateOfBirth = this.playerToEdit.DateOfBirth.ToString();
        }
    }

}
