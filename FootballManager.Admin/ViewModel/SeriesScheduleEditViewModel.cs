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
    public class SeriesScheduleEditViewModel : ViewModelBase, IDataErrorInfo
    {
        private Match matchToEdit;
        private string arenaName;
        private string matchDate;
        private string matchTime;
        private bool allPropertiesValid;
        private Dictionary<string, bool> validProperties;

        public SeriesScheduleEditViewModel()
        {
            validProperties = new Dictionary<string, bool>();
            validProperties.Add("ArenaName", false);
            validProperties.Add("MatchDate", false);
            validProperties.Add("MatchTime", false);
            this.SaveEditMatchCommand = new RelayCommand(SaveEditMatch);
            Messenger.Default.Register<Match>(this, OnMatchObjReceived);
        }

        #region IDataErrorInfo implementation

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "ArenaName":
                        if (string.IsNullOrEmpty(this.ArenaName))
                        {
                            this.validProperties[columnName] = false;
                            ValidateProperties();
                            return string.Empty;
                        }
                        if (!this.ArenaName.IsValidArenaName(false))
                        {
                            this.validProperties[columnName] = false;
                            ValidateProperties();
                            return "Must be 2-40 valid European characters long!";
                        }
                        break;

                    case "MatchDate":
                        if (string.IsNullOrEmpty(this.MatchDate))
                        {
                            this.validProperties[columnName] = false;
                            ValidateProperties();
                            return string.Empty;
                        }
                        DateTime matchDate;
                        if (!DateTime.TryParseExact(this.MatchDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out matchDate))
                        {
                            this.validProperties[columnName] = false;
                            ValidateProperties();
                            return "Must be valid date in format \"yyyy-MM-dd\"!";
                        }
                        if (!matchDate.IsValidMatchDateAndTime())
                        {
                            this.validProperties[columnName] = false;
                            ValidateProperties();
                            return "Must be a future date set at most two years from now!";
                        }
                        break;

                    case "MatchTime":
                        if (string.IsNullOrEmpty(this.MatchTime))
                        {
                            this.validProperties[columnName] = false;
                            ValidateProperties();
                            return string.Empty;
                        }
                        DateTime matchTime;
                        if (!DateTime.TryParse(this.MatchTime, out matchTime))
                        {
                            this.validProperties[columnName] = false;
                            ValidateProperties();
                            return "Must be a valid time of the format \"HH:mm\"!";
                        }
                        break;
                }
                this.validProperties[columnName] = true;
                ValidateProperties();
                return string.Empty;
            }
        }

        #endregion IDataErrorInfo implementation

        #region Properties

        public ICommand SaveEditMatchCommand { get; }

        public string ArenaName
        {
            get { return arenaName; }
            set
            {
                if (arenaName != value)
                {
                    arenaName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string MatchDate
        {
            get { return matchDate; }
            set
            {
                if (matchDate != value)
                {
                    matchDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public string MatchTime
        {
            get { return matchTime; }
            set
            {
                if (matchTime != value)
                {
                    matchTime = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool AllPropertiesValid
        {
            get { return allPropertiesValid; }
            set
            {
                if (allPropertiesValid != value)
                {
                    allPropertiesValid = value;
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

        #endregion Properties

        private void SaveEditMatch(object obj)
        {
            InputCheck();

            Window window = Application.Current.Windows.OfType<Window>()
                .Where(w => w.Name == "EditMatchWindow").FirstOrDefault();
            if (window != null)
            {
                window.Close();
            }
        }

        private void OnMatchObjReceived(Match obj)
        {
            this.matchToEdit = obj;
            this.arenaName = this.matchToEdit.Location.Value;
            this.matchDate = this.matchToEdit.MatchDate.Value.ToString("yyyy-MM-dd");
            this.matchTime = this.matchToEdit.MatchDate.Value.ToString("HH:mm");
        }

        private void InputCheck()
        {
            if (ArenaName == null)
            {
                this.matchToEdit.Location = this.matchToEdit.Location;
            }
            else
            {
                this.matchToEdit.Location = new ArenaName(arenaName);
            }

            if (MatchDate == null && MatchTime == null)
            {
                this.matchToEdit.MatchDate = this.matchToEdit.MatchDate;
            }
            else if (MatchTime == null)
            {
                var matchDateAndTime = Convert.ToDateTime(matchToEdit.MatchDate.ToString());
                var matchOldTime = matchDateAndTime.ToString("HH:mm");
                var matchDateToSet = Convert.ToDateTime(this.matchDate + " " + matchOldTime);
                this.matchToEdit.MatchDate = new MatchDateAndTime(matchDateToSet);
            }
            else if (MatchDate == null)
            {
                var matchDateAndTime = Convert.ToDateTime(matchToEdit.MatchDate.ToString());
                var matchOldDate = matchDateAndTime.ToString("yyyy-MM-dd");
                var matchDateToSet = Convert.ToDateTime(matchOldDate + " " + this.matchTime);
                this.matchToEdit.MatchDate = new MatchDateAndTime(matchDateToSet);
            }
            else
            {
                var matchDateToSet = Convert.ToDateTime(this.matchDate + " " + this.matchTime);
                this.matchToEdit.MatchDate = new MatchDateAndTime(matchDateToSet);
            }
        }
    }
}