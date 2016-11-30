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
    public class SeriesViewModel : ViewModelBase
    {
        private SeriesService seriesService;
        private Series selectedSeries;

        public SeriesViewModel()
        {
            seriesService = new SeriesService();
        }

        #region Properties

        public ObservableCollection<int> Ranking
        {

            get
            {
                var numberOfTeams = selectedSeries.NumberOfTeams.Value;
                var rankingNumbers = new ObservableCollection<int>();
                for (int i = 1; i <= numberOfTeams; i++)
                {
                    rankingNumbers.Add(i);
                }
                return rankingNumbers;
            }
        }

        public ObservableCollection<TeamStats> TeamStats
        {
            get
            {
                if (selectedSeries == null)
                {
                    return new ObservableCollection<TeamStats>();
                }
                else
                {
                    return seriesService.GetLeagueTablePlacement(selectedSeries.Id).ToObservableCollection();
                }
            }
        }
        #endregion

        #region Combobox properties
        public Series SelectedSeries
        {
            get { return selectedSeries; }
            set
            {
                if (selectedSeries != value)
                {
                    selectedSeries = value;
                    OnPropertyChanged();
                }
            }
        }
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
