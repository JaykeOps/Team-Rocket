using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Helper_Classes;

namespace FootballManager.Admin.ViewModel
{
    public class SeriesGameProtocolViewModel : ViewModelBase, IDataErrorInfo
    {
        private string goalMatchMinute;
        private string assistMatchMinute;
        private string penaltyMatchMinute;
        private string yellowCardMatchMinute;
        private string redCardMatchMinute;

        public SeriesGameProtocolViewModel()
        {
        }

        #region IDataErrorInfo implemetation
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
                    case "GoalMatchMinute":
                        if (string.IsNullOrEmpty(this.GoalMatchMinute))
                        {
                            return string.Empty;
                        }
                        int goalMatchMinute;
                        if (!int.TryParse(this.GoalMatchMinute, out goalMatchMinute))
                        {
                            return "Must be a number between 1 and 120!"; // MatchMinute's max value is not yet limited by the value of MatchDuration!
                        }
                        if (!goalMatchMinute.IsValidMatchMinute())
                        {
                            return "Must be a number between 1 and 120!";
                        }
                        break;
                    case "AssistMatchMinute":
                        if (string.IsNullOrEmpty(this.AssistMatchMinute))
                        {
                            return string.Empty;
                        }
                        int assistMatchMinute;
                        if (!int.TryParse(this.AssistMatchMinute, out assistMatchMinute))
                        {
                            return "Must be a number between 1 and 120!";
                        }
                        if (!assistMatchMinute.IsValidMatchMinute())
                        {
                            return "Must be a number between 1 and 120!";
                        }
                        break;
                    case "PenaltyMatchMinute":
                        if (string.IsNullOrEmpty(this.PenaltyMatchMinute))
                        {
                            return string.Empty;
                        }
                        int penaltyMatchMinute;
                        if (!int.TryParse(this.PenaltyMatchMinute, out penaltyMatchMinute))
                        {
                            return "Must be a number between 1 and 120!";
                        }
                        if (!penaltyMatchMinute.IsValidMatchMinute())
                        {
                            return "Must be a number between 1 and 120!";
                        }
                        break;
                    case "YellowCardMatchMinute":
                        if (string.IsNullOrEmpty(this.YellowCardMatchMinute))
                        {
                            return string.Empty;
                        }
                        int yellowCardMatchMinute;
                        if (!int.TryParse(this.YellowCardMatchMinute, out yellowCardMatchMinute))
                        {
                            return "Must be a number between 1 and 120!";
                        }
                        if (!yellowCardMatchMinute.IsValidMatchMinute())
                        {
                            return "Must be a number between 1 and 120!";
                        }
                        break;
                    case "RedCardMatchMinute":
                        if (string.IsNullOrEmpty(this.RedCardMatchMinute))
                        {
                            return string.Empty;
                        }
                        int redCardMatchMinute;
                        if (!int.TryParse(this.RedCardMatchMinute, out redCardMatchMinute))
                        {
                            return "Must be a number between 1 and 120!";
                        }
                        if (!redCardMatchMinute.IsValidMatchMinute())
                        {
                            return "Must be a number between 1 and 120!";
                        }
                        break;
                }
                return string.Empty;
            }
        }
        #endregion

        #region Properties
        public string GoalMatchMinute
        {
            get { return goalMatchMinute; }
            set
            {
                if (goalMatchMinute != value)
                {
                    goalMatchMinute = value;
                    OnPropertyChanged();
                }
            }
        }

        public string AssistMatchMinute
        {
            get { return assistMatchMinute; }
            set
            {
                if (assistMatchMinute != value)
                {
                    assistMatchMinute = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PenaltyMatchMinute
        {
            get { return penaltyMatchMinute; }
            set
            {
                if (penaltyMatchMinute != value)
                {
                    penaltyMatchMinute = value;
                    OnPropertyChanged();
                }
            }
        }

        public string YellowCardMatchMinute
        {
            get { return yellowCardMatchMinute; }
            set
            {
                if (yellowCardMatchMinute != value)
                {
                    yellowCardMatchMinute = value;
                    OnPropertyChanged();
                }
            }
        }

        public string RedCardMatchMinute
        {
            get { return redCardMatchMinute; }
            set
            {
                if (redCardMatchMinute != value)
                {
                    redCardMatchMinute = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion
    }
}
