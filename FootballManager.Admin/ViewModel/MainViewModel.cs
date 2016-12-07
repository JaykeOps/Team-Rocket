using FootballManager.Admin.Utility;
using FootballManager.Admin.View;
using System.Windows.Input;

namespace FootballManager.Admin.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand OpenSeriesCommand { get; }
        public ICommand OpenPlayerCommand { get; }
        public ICommand OpenTeamCommand { get; }

        private object selectedViewModel;

        public object SelectedViewModel
        {
            get { return this.selectedViewModel; }
            set
            {
                this.selectedViewModel = value;
                this.OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            this.OpenSeriesCommand = new RelayCommand(this.SeriesCommand);
            this.OpenPlayerCommand = new RelayCommand(this.PlayerCommand);
            this.OpenTeamCommand = new RelayCommand(this.TeamCommand);
            this.SelectedViewModel = new SeriesView();
        }

        private void SeriesCommand(object obj)
        {
            this.SelectedViewModel = new SeriesView();
        }

        private void PlayerCommand(object obj)
        {
            this.SelectedViewModel = new PlayerView();
        }

        private void TeamCommand(object obj)
        {
            this.SelectedViewModel = new TeamView();
        }
    }
}