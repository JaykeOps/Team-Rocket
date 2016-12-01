using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FootballManager.Admin.Utility;
using FootballManager.Admin.View;
using MaterialDesignThemes.Wpf;
using System.ComponentModel;
using Domain.Helper_Classes;

namespace FootballManager.Admin.ViewModel
{
    public class SeriesViewModel : ViewModelBase, IDataErrorInfo
    {
        private string seriesName;
        private string matchDuration;

        public SeriesViewModel()
        {
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
                    case "SeriesName":
                        if (string.IsNullOrEmpty(this.SeriesName))
                        {
                            return string.Empty;
                        }
                        if (!this.SeriesName.IsValidSeriesName(false)) // Parameter 'bool ignoreCase' set to false.
                        {
                            return "Must be 2-30 valid European characters long!";
                        }
                        break;
                    case "MatchDuration":
                        if (string.IsNullOrEmpty(this.MatchDuration))
                        {
                            return string.Empty;
                        }
                        int matchMinutes;
                        if (!int.TryParse(this.MatchDuration, out matchMinutes))
                        {
                            return "Must be an integer between 10 and 90!";
                        }
                        else
                        {
                            TimeSpan timeSpan = new TimeSpan(0, matchMinutes, 0);
                            if (!timeSpan.IsValidMatchDuration())
                            {
                                return "Must be an integer between 10 and 90!";
                            }
                        }
                        break;
                }
                return string.Empty;
            }
        }
        #endregion

        #region Properties
        public string SeriesName
        {
            get { return seriesName; }
            set
            {
                if (seriesName != value)
                {
                    seriesName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string MatchDuration
        {
            get { return matchDuration; }
            set
            {
                if (matchDuration != value)
                {
                    matchDuration = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Methods

        #endregion
    }
}
