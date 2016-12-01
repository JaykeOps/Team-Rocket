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
            this.seriesService = new SeriesService();
            this.Load();
        }

        public ObservableCollection<Series> AllSeries
        {
            get { return this.allSeries; }
            set
            {
                this.allSeries = this.seriesService.GetAll().ToObservableCollection();
                this.OnPropertyChanged();
            }
        }

        private ICommand openSeriesGameProtocolViewCommand;

        public ICommand OpenSeriesGameProtocolViewCommand
        {
            get
            {
                if (this.openSeriesGameProtocolViewCommand == null)
                {
                    this.openSeriesGameProtocolViewCommand = new RelayCommand(this.OpenSeriesGameProtocolView);
                }
                return this.openSeriesGameProtocolViewCommand;
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
                if (this.openSeriesScheduleEditViewCommand == null)
                {
                    this.openSeriesScheduleEditViewCommand = new RelayCommand(this.OpenSeriesScheduleEditView);
                }
                return this.openSeriesScheduleEditViewCommand;
            }
        }

        private void OpenSeriesScheduleEditView(object obj)
        {
            var view = new SeriesScheduleEditView();
            view.ShowDialog();
        }

        private void Load()
        {
            this.allSeries = this.seriesService.GetAll().ToObservableCollection();
        }
    }
}
