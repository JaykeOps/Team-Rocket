using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using FootballManager.Admin.Extensions;

namespace FootballManager.Admin.ViewModel
{
    public class TeamInfoViewModel : ViewModelBase
    {
        private ObservableCollection<Series> seriesCollection;
        private ObservableCollection<IExposableTeam> teamCollection;
        private SeriesService seriesService;
        private TeamService teamService;

        public TeamInfoViewModel()
        {
            this.seriesCollection = new ObservableCollection<Series>();
            this.teamCollection = new ObservableCollection<IExposableTeam>();
            this.seriesService = new SeriesService();
            this.teamService = new TeamService();
            this.LoadSeriesData();
        }

        public ObservableCollection<Series> SeriesCollection
        {
            get { return this.seriesCollection; }
            set
            {
                this.seriesCollection = value;
                this.OnPropertyChanged();
            }
        }

        public void LoadSeriesData()
        {
            this.SeriesCollection = this.seriesService.GetAll().ToObservableCollection();
        }

    }
}
