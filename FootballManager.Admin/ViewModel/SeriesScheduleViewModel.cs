using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public SeriesScheduleViewModel()
        {
            seriesService = new SeriesService();
            Load();
        }

        public ObservableCollection<Series> AllSeries
        {
            get { return allSeries; }
            set
            {
                allSeries = seriesService.GetAll().ToObservableCollection();
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

        private void OpenSeriesScheduleEditView(object obj)
        {
            var view = new SeriesScheduleEditView();
            view.ShowDialog();
        }

        private void Load()
        {
            this.allSeries = seriesService.GetAll().ToObservableCollection();
        }
    }
}
