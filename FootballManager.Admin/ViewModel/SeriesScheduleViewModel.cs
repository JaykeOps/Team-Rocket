using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FootballManager.Admin.Utility;
using FootballManager.Admin.View;
using Domain.Entities;
using Domain.Services;
using FootballManager.Admin.Extensions;
using Domain.Helper_Classes;

namespace FootballManager.Admin.ViewModel
{
    public class SeriesScheduleViewModel : ViewModelBase
    {
        private ObservableCollection<Series> seriesCollection;
        private ObservableCollection<Match> matchesBySeriesCollection;
        private Match selectedMatch;

        private SeriesService seriesService;

        private Series selectedSeries;

        public SeriesScheduleViewModel()
        {
            matchesBySeriesCollection = new ObservableCollection<Match>();

            seriesService = new SeriesService();

            Messenger.Default.Register<Series>(this, OnSeriesObjReceived);
            Messenger.Default.Register<Match>(this, OnMatchObjReceived);
            LoadData();            
        }

        public Series SelectedSeries
        {
            get { return selectedSeries; }
            set
            {
                selectedSeries = value;
                OnPropertyChanged();
                FilterMatchesBySeries();
            }
        }

        public Match SelectedMatch
        {
            get { return selectedMatch; }
            set
            {
                selectedMatch = value;
                OnPropertyChanged();
            }
        }

        #region Collections               
        public ObservableCollection<Series> SeriesCollection
        {
            get { return seriesCollection; }
            set
            {
                seriesCollection = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Match> MatchesBySeriesCollection
        {
            get { return matchesBySeriesCollection; }
            set
            {
                matchesBySeriesCollection = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Protocol

        private ICommand openSeriesGameProtocolViewCommand;

        public ICommand OpenSeriesGameProtocolViewCommand
        {
            get
            {
                if (openSeriesGameProtocolViewCommand == null)
                {
                    openSeriesGameProtocolViewCommand = new RelayCommand(OpenSeriesGameProtocolView);
                }
                return openSeriesGameProtocolViewCommand;
            }
        }

        private void OpenSeriesGameProtocolView(object obj)
        {
            var view = new SeriesGameProtocolView();
            view.ShowDialog();
        }

        private ICommand openSeriesScheduleEditViewCommand;

        public ICommand OpenSeriesScheduleEditViewCommand
        {
            get
            {
                if (openSeriesScheduleEditViewCommand == null)
                {
                    openSeriesScheduleEditViewCommand = new RelayCommand(OpenSeriesScheduleEditView);
                }
                return openSeriesScheduleEditViewCommand;
            }
        }

        #endregion


        public void FilterMatchesBySeries()
        {
            var t = SelectedSeries.Schedule;

            MatchesBySeriesCollection = t.ToObservableCollection();
        }

        private void OpenSeriesScheduleEditView(object obj)
        {
            var view = new SeriesScheduleEditView();
            Messenger.Default.Send<Match>(selectedMatch);
            view.ShowDialog();
        }

        private void LoadData()
        {
            this.seriesCollection = seriesService.GetAll().ToObservableCollection();
        }

        private void OnSeriesObjReceived(Series serie)
        {
            seriesService.Add(serie);
            seriesService.ScheduleGenerator(serie.Id);
            SeriesCollection = seriesService.GetAll().ToObservableCollection();
        }

        private void OnMatchObjReceived(Match obj)
        {
            var t = SelectedSeries.Schedule;

            foreach (var match in t)
            {
                matchesBySeriesCollection.Remove(match);
                matchesBySeriesCollection.Add(match);
            }
            MatchesBySeriesCollection = t.ToObservableCollection();
        }
    }
}
