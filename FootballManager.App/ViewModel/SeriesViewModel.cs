using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Domain.Entities;
using Domain.Services;
using Domain.Value_Objects;
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
        //public ObservableCollection<TeamStats> TeamStats
        //{
        //    get {
        //        return seriesService.GetLeagueTablePlacement();
        //        }
        //}
        #endregion

        #region Methods

        #endregion

        #region Combobox population
        public IEnumerable<SeriesName> AllSeries
        {
            get { return seriesService.GetAll().Select(s => s.SeriesName); }
        }
        #endregion
    }
}
