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
        private ObservableCollection<Series> allSeries;
        private ObservableCollection<Match> selectedSeriesSchedule;
        private Series selectedSeries;
        private SeriesService seriesService;

        public SeriesScheduleViewModel()
        {
            selectedSeriesSchedule = new ObservableCollection<Match>();
            seriesService = new SeriesService();
            Messenger.Default.Register<Series>(this, OnSeriesObjReceived);
            Load();            
        }

        public ObservableCollection<Series> AllSeries
        {
            get { return allSeries; }
            set
            {
                allSeries = value;
                OnPropertyChanged();
            }
        }

        public Series SelectedSeries
        {
            get { return selectedSeries; }
            set
            {
                selectedSeries = value;
                OnPropertyChanged();
                SelectedSeriesSchedule = selectedSeries.Schedule.ToObservableCollection();
            }
        }

        public ObservableCollection<Match> SelectedSeriesSchedule
        {
            get { return selectedSeriesSchedule; }
            set
            {
                selectedSeriesSchedule = value;
                OnPropertyChanged();
            }
        }

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


        private void OpenSeriesScheduleEditView(object obj)
        {
            var view = new SeriesScheduleEditView();
            view.ShowDialog();
        }

        private void Load()
        {
            this.allSeries = seriesService.GetAll().ToObservableCollection();
        }

        private void OnSeriesObjReceived(Series serie)
        {
            seriesService.Add(serie);
            seriesService.ScheduleGenerator(serie.Id);
            AllSeries = seriesService.GetAll().ToObservableCollection();
        }
    }
}
