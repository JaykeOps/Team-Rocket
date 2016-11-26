using System.Windows.Input;
using FootballManager.App.Utility;
using FootballManager.App.View;

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
            get { return selectedViewModel; }
            set
            {
                selectedViewModel = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            OpenPlayerCommand = new RelayCommand(PlayerCommand);
            OpenTeamCommand = new RelayCommand(TeamCommand);
            OpenSeriesCommand = new RelayCommand(SeriesCommand);
            OpenMatchCommand = new RelayCommand(MatchCommand);
        }

        private void PlayerCommand(object obj)
        {
            SelectedViewModel = new PlayerView();
        }
        private void TeamCommand(object obj)
        {
            SelectedViewModel = new TeamView();
        }

        private void SeriesCommand(object obj)
        {
            SelectedViewModel = new SeriesView();
        }

        private void MatchCommand(object obj)
        {
            SelectedViewModel = new MatchView();
        }

    }
}
