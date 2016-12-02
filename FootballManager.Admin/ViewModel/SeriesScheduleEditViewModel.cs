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
        private DateTime today;

        public SeriesScheduleEditViewModel()
        {
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
                            return string.Empty;
                        }
                        if (!this.ArenaName.IsValidArenaName(false))
                        {
                            return "Must be 2-40 valid European characters long!";
                        }
                        break;
                    case "MatchDate":
                        if (string.IsNullOrEmpty(this.MatchDate))
                        {
                            return string.Empty;
                        }
                        DateTime matchDate;
                        if (!DateTime.TryParse(this.MatchDate, out matchDate))
                        {
                            return "Must be a valid date of the form \"yyyy-MM-dd\"!";
                        }
                        if (!matchDate.IsValidMatchDateAndTime())
                        {
                            return "Must be a future date set at most two years from now!";
                        }
                        break;
                    case "MatchTime":
                        if (string.IsNullOrEmpty(this.MatchTime))
                        {
                            return string.Empty;
                        }
                        DateTime matchTime;
                        if (!DateTime.TryParse(this.MatchTime, out matchTime))
                        {
                            return "Must be a valid time of the format \"HH:mm\"!";
                        }
                        break;
                }
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
    }
}
