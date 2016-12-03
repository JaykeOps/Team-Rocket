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
        private bool goalMatchMinuteValid;
        private bool assistMatchMinuteValid;
        private bool penaltyMatchMinuteValid;
        private bool yellowCardMatchMinuteValid;
        private bool redCardMatchMinuteValid;

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
                        this.GoalMatchMinuteValid = true;
                        if (string.IsNullOrEmpty(this.GoalMatchMinute))
                        {
                            this.GoalMatchMinuteValid = false;
                            return string.Empty;
                        }
                        int goalMatchMinute;
                        if (!int.TryParse(this.GoalMatchMinute, out goalMatchMinute))
                        {
                            this.GoalMatchMinuteValid = false;
                            return "Must be a number between 1 and 120!"; // MatchMinute's max value is not yet limited by the value of MatchDuration!
                        }
                        if (!goalMatchMinute.IsValidMatchMinute())
                        {
                            this.GoalMatchMinuteValid = false;
                            return "Must be a number between 1 and 120!";
                        }
                        break;
                    case "AssistMatchMinute":
                        this.AssistMatchMinuteValid = true;
                        if (string.IsNullOrEmpty(this.AssistMatchMinute))
                        {
                            this.AssistMatchMinuteValid = false;
                            return string.Empty;
                        }
                        int assistMatchMinute;
                        if (!int.TryParse(this.AssistMatchMinute, out assistMatchMinute))
                        {
                            this.AssistMatchMinuteValid = false;
                            return "Must be a number between 1 and 120!";
                        }
                        if (!assistMatchMinute.IsValidMatchMinute())
                        {
                            this.AssistMatchMinuteValid = false;
                            return "Must be a number between 1 and 120!";
                        }
                        break;
                    case "PenaltyMatchMinute":
                        this.PenaltyMatchMinuteValid = true;
                        if (string.IsNullOrEmpty(this.PenaltyMatchMinute))
                        {
                            this.PenaltyMatchMinuteValid = false;
                            return string.Empty;
                        }
                        int penaltyMatchMinute;
                        if (!int.TryParse(this.PenaltyMatchMinute, out penaltyMatchMinute))
                        {
                            this.PenaltyMatchMinuteValid = false;
                            return "Must be a number between 1 and 120!";
                        }
                        if (!penaltyMatchMinute.IsValidMatchMinute())
                        {
                            this.PenaltyMatchMinuteValid = false;
                            return "Must be a number between 1 and 120!";
                        }
                        break;
                    case "YellowCardMatchMinute":
                        this.YellowCardMatchMinuteValid = true;
                        if (string.IsNullOrEmpty(this.YellowCardMatchMinute))
                        {
                            this.YellowCardMatchMinuteValid = false;
                            return string.Empty;
                        }
                        int yellowCardMatchMinute;
                        if (!int.TryParse(this.YellowCardMatchMinute, out yellowCardMatchMinute))
                        {
                            this.YellowCardMatchMinuteValid = false;
                            return "Must be a number between 1 and 120!";
                        }
                        if (!yellowCardMatchMinute.IsValidMatchMinute())
                        {
                            this.YellowCardMatchMinuteValid = false;
                            return "Must be a number between 1 and 120!";
                        }
                        break;
                    case "RedCardMatchMinute":
                        this.RedCardMatchMinuteValid = true;
                        if (string.IsNullOrEmpty(this.RedCardMatchMinute))
                        {
                            this.RedCardMatchMinuteValid = false;
                            return string.Empty;
                        }
                        int redCardMatchMinute;
                        if (!int.TryParse(this.RedCardMatchMinute, out redCardMatchMinute))
                        {
                            this.RedCardMatchMinuteValid = false;
                            return "Must be a number between 1 and 120!";
                        }
                        if (!redCardMatchMinute.IsValidMatchMinute())
                        {
                            this.RedCardMatchMinuteValid = false;
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

        public bool GoalMatchMinuteValid
        {
            get { return goalMatchMinuteValid; }
            set
            {
                if (goalMatchMinuteValid != value)
                {
                    goalMatchMinuteValid = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool AssistMatchMinuteValid
        {
            get { return assistMatchMinuteValid; }
            set
            {
                if (assistMatchMinuteValid != value)
                {
                    assistMatchMinuteValid = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool PenaltyMatchMinuteValid
        {
            get { return penaltyMatchMinuteValid; }
            set
            {
                if (penaltyMatchMinuteValid != value)
                {
                    penaltyMatchMinuteValid = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool YellowCardMatchMinuteValid
        {
            get { return yellowCardMatchMinuteValid; }
            set
            {
                if (yellowCardMatchMinuteValid != value)
                {
                    yellowCardMatchMinuteValid = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool RedCardMatchMinuteValid
        {
            get { return redCardMatchMinuteValid; }
            set
            {
                if (redCardMatchMinuteValid != value)
                {
                    redCardMatchMinuteValid = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
