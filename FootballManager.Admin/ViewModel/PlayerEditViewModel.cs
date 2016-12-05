using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FootballManager.Admin.Utility;

namespace FootballManager.Admin.ViewModel
{
    public class PlayerEditViewModel : ViewModelBase
    {
        private string firstName;
        private string lastName;
        private string dateOfBirth;

        public PlayerEditViewModel()
        {
            this.SaveEditPlayerCommand = new RelayCommand(SaveEditedPlayer);
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
            MessageBox.Show("Hrj");
        }
    }

}
