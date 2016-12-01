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
                        if (this.SeriesName == null || this.SeriesName == "")
                        {
                            return string.Empty;
                        }
                        if (!this.SeriesName.IsValidSeriesName(false)) // Parameter is 'bool ignoreCase'.
                        {
                            return "Must be 2-30 valid European characters long!";
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
                seriesName = value;
                base.OnPropertyChanged("SeriesName");
            }
        }
        #endregion

        #region Methods

        #endregion
    }
}
