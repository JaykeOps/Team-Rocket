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

namespace FootballManager.Admin.ViewModel
{
    public class SeriesScheduleViewModel : ViewModelBase
    {
        private ObservableCollection<Series> allSeries;
        private SeriesService seriesService;
        private ICommand updateComboBoxSourceCommand;

        public SeriesScheduleViewModel()
        {
            seriesService = new SeriesService();
            //this.UpdateComboBoxSourceCommand = new RelayCommand(UpdateComboBoxSource);
            Load();
            
        }

        public ObservableCollection<Series> AllSeries
        {
            get { return allSeries; }
            set
            {
                allSeries = value;
                Messenger.Default.Register<Series>(this, OnSeriesObjReceived);
                OnPropertyChanged();
            }
        }

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

        public ICommand UpdateComboBoxSourceCommand { get; }

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
            Load();
        }
    }
}
