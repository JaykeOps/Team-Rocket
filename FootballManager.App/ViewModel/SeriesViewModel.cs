using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Domain.Entities;
using Domain.Services;
using FootballManager.App.Extensions;
using FootballManager.App.Utility;
using FootballManager.App.View;
using Dragablz;

namespace FootballManager.App.ViewModel
{
    public class SeriesViewModel
    {
        private SeriesService seriesService;

        public SeriesViewModel()
        {
            seriesService = new SeriesService();
        }

        #region Properties

        #endregion

        #region Methods

        #endregion

        #region Combobox population

        public IEnumerable<Series> AllSeries
        {
            get
            {
                return seriesService.GetAll(); // This will later be called as a WCF service.
            }
        }

        #endregion
    }
}
