using Domain.Entities;
using Domain.Helper_Classes;
using Domain.Value_Objects;
using FootballManager.Admin.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Globalization;

namespace FootballManager.Admin.ViewModel
{
    public class PlayerEditViewModel : ViewModelBase, IDataErrorInfo
    {
        private string firstName;
        private string lastName;
        private string dateOfBirth;
        private Player playerToEdit;
        private bool allPropertiesValid;
        private Dictionary<string, bool> validProperties;

        public PlayerEditViewModel()
        {
            this.SaveEditPlayerCommand = new RelayCommand(SaveEditedPlayer);
            Messenger.Default.Register<Player>(this, this.OnPlayerObjReceived);
            validProperties = new Dictionary<string, bool>();
            validProperties.Add("FirstName", false);
            validProperties.Add("LastName", false);
            validProperties.Add("DateOfBirth", false);
        }

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
                    case "FirstName":
                        if (string.IsNullOrEmpty(this.FirstName))
                        {
                            validProperties[columnName] = false;
                            ValidateProperties();
                            return string.Empty;
                        }
                        if (!this.FirstName.IsValidName(false))
                        {
                            validProperties[columnName] = false;
                            ValidateProperties();
                            return "Must be 2-20 valid European characters long!";
                        }
                        break;

                    case "LastName":
                        if (string.IsNullOrEmpty(this.LastName))
                        {
                            validProperties[columnName] = false;
                            ValidateProperties();
                            return string.Empty;
                        }
                        if (!this.LastName.IsValidName(false))
                        {
                            validProperties[columnName] = false;
                            ValidateProperties();
                            return "Must be 2-20 valid European characters long!";
                        }
                        break;

                    case "DateOfBirth":
                        if (string.IsNullOrEmpty(this.DateOfBirth))
                        {
                            this.validProperties[columnName] = false;
                            ValidateProperties();
                            return string.Empty;
                        }
                        DateTime dateOfBirth;
                        if (!DateTime.TryParseExact(this.DateOfBirth, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfBirth))
                        {
                            this.validProperties[columnName] = false;
                            ValidateProperties();
                            return "Must be valid date in format \"yyyy-MM-dd\"!";
                        }
                        if (!this.DateOfBirth.IsValidBirthOfDate())
                        {
                            this.validProperties[columnName] = false;
                            ValidateProperties();
                            return "Earliest year = 1936, latest year = [current year - 4]!";
                        }
                        break;
                }
                validProperties[columnName] = true;
                ValidateProperties();
                return string.Empty;
            }
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
            this.playerToEdit.Name = new Name(this.firstName, this.playerToEdit.Name.LastName);
            this.playerToEdit.Name = new Name(this.playerToEdit.Name.FirstName, this.lastName);
            this.playerToEdit.DateOfBirth = new DateOfBirth(this.dateOfBirth);

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

        public bool AllPropertiesValid
        {
            get { return this.allPropertiesValid; }
            set
            {
                if (this.allPropertiesValid != value)
                {
                    this.allPropertiesValid = value;
                    OnPropertyChanged();
                }
            }
        }

        private void ValidateProperties()
        {
            foreach (var isValid in validProperties.Values)
            {
                if (isValid == false)
                {
                    this.AllPropertiesValid = false;
                    return;
                }
            }
            this.AllPropertiesValid = true;
        }
    }
}