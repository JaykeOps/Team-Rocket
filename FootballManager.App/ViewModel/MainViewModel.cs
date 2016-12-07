using FootballManager.App.Utility;
using FootballManager.App.View;
using System.Windows.Input;

namespace FootballManager.App.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand OpenPlayerCommand { get; set; }
        public ICommand OpenTeamCommand { get; set; }
        public ICommand OpenSeriesCommand { get; set; }
        public ICommand OpenMatchCommand { get; set; }

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
            this.OpenPlayerCommand = new RelayCommand(this.PlayerCommand);
            this.OpenTeamCommand = new RelayCommand(this.TeamCommand);
            this.OpenSeriesCommand = new RelayCommand(this.SeriesCommand);
            this.OpenMatchCommand = new RelayCommand(this.MatchCommand);
            this.selectedViewModel = new SeriesView();
        }

        private void PlayerCommand(object obj)
        {
            this.SelectedViewModel = new PlayerView();
        }

        private void TeamCommand(object obj)
        {
            this.SelectedViewModel = new TeamView();
        }

        private void SeriesCommand(object obj)
        {
            this.SelectedViewModel = new SeriesView();
        }

        private void MatchCommand(object obj)
        {
            this.SelectedViewModel = new MatchView();
        }
    }
}