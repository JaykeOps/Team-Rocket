using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Domain.Helper_Classes;

namespace FootballManager.Admin.ViewModel
{
    public class SeriesScheduleEditViewModel : ViewModelBase, IDataErrorInfo
    {
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
        }

        #region IDataErrorInfo implementation
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
                switch(columnName)
                {
                    case "ArenaName":
                        if (string.IsNullOrEmpty(this.ArenaName))
                        {
                            validProperties[columnName] = false;
                            ValidateProperties();
                            return string.Empty;
                        }
                        if (!this.ArenaName.IsValidArenaName(false))
                        {
                            validProperties[columnName] = false;
                            ValidateProperties();
                            return "Must be 2-40 valid European characters long!";
                        }
                        break;
                    case "MatchDate":
                        if (string.IsNullOrEmpty(this.MatchDate))
                        {
                            validProperties[columnName] = false;
                            ValidateProperties();
                            return string.Empty;
                        }
                        DateTime matchDate;
                        if (!DateTime.TryParse(this.MatchDate, out matchDate))
                        {
                            validProperties[columnName] = false;
                            ValidateProperties();
                            return "Must be a valid date of the form \"yyyy-MM-dd\"!";
                        }
                        if (!matchDate.IsValidMatchDateAndTime())
                        {
                            validProperties[columnName] = false;
                            ValidateProperties();
                            return "Must be a future date set at most two years from now!";
                        }
                        break;
                    case "MatchTime":
                        if (string.IsNullOrEmpty(this.MatchTime))
                        {
                            validProperties[columnName] = false;
                            ValidateProperties();
                            return string.Empty;
                        }
                        DateTime matchTime;
                        if (!DateTime.TryParse(this.MatchTime, out matchTime))
                        {
                            validProperties[columnName] = false;
                            ValidateProperties();
                            return "Must be a valid time of the format \"HH:mm\"!";
                        }
                        break;
                }
                validProperties[columnName] = true;
                ValidateProperties();
                return string.Empty;
            }
        }
        #endregion

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
    }
}
